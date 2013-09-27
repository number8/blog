/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.prosoftnearshore.blog.y2013.m09.hadoop.mapreduce;

import java.io.IOException;
import java.util.StringTokenizer;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.conf.Configured;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.input.TextInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;
import org.apache.hadoop.mapreduce.lib.output.TextOutputFormat;
import org.apache.hadoop.util.Tool;
import org.apache.hadoop.util.ToolRunner;

/**
 *
 * @author mmorales
 */
public class HtmlTagCount extends Configured implements Tool {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws Exception {
        HtmlTagCount tool = new HtmlTagCount();
        Configuration config = new Configuration();
        ToolRunner.run(config, tool, args);
    }

    public int run(String[] args) throws Exception {
        Job job = new Job(getConf(), "HTML Tag Count");
        job.setJarByClass(HtmlTagCount.class);
        job.setMapOutputKeyClass(Text.class);
        job.setMapOutputValueClass(IntWritable.class);
        job.setOutputKeyClass(Text.class);
        job.setOutputValueClass(IntWritable.class);

        job.setMapperClass(Map.class);
        job.setCombinerClass(Reduce.class);
        job.setReducerClass(Reduce.class);
        job.setInputFormatClass(TextInputFormat.class);
        job.setOutputFormatClass(TextOutputFormat.class);

        FileInputFormat.setInputPaths(job, new Path(args[0]));
        FileOutputFormat.setOutputPath(job, new Path(args[1]));

        boolean b = job.waitForCompletion(true);
        if (!b) {
            throw new IOException("Job failed!");
        }

        return 0;
    }

    public static class Map extends Mapper<LongWritable, Text, Text, IntWritable> {

        private static Pattern HTML_TAG_PATTERN = Pattern.compile("<([a-z0-9]+).*>", Pattern.CASE_INSENSITIVE);

        @Override
        protected void map(
                LongWritable key,
                Text value,
                Mapper<LongWritable, Text, Text, IntWritable>.Context context)
                throws IOException, InterruptedException {
            String line = value.toString();
            Matcher m = HTML_TAG_PATTERN.matcher(line);
            while (m.find()) {
                context.write(new Text(m.group(1)), new IntWritable(1));
            }
        }
    }

    public static class Reduce extends Reducer<Text, IntWritable, Text, IntWritable> {

        @Override
        protected void reduce(
                Text key,
                Iterable<IntWritable> values,
                Reducer<Text, IntWritable, Text, IntWritable>.Context context)
                throws IOException, InterruptedException {
            int sum = 0;
            for (IntWritable i : values) {
                sum += i.get();
            }
            if (sum > 10) {
                context.write(key, new IntWritable(sum));
            }
        }
    }
}