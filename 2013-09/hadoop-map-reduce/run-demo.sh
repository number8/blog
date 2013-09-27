#!/bin/bash

HADOOP=~/hadoop-1.2.1/bin/hadoop

# Copy the data into HDFS
$HADOOP fs -mkdir /user/hadoop-test/input
$HADOOP fs -put data/* /user/hadoop-test/input

# Run Word Count
$HADOOP fs -rmr /user/hadoop-test/wordcount-output
$HADOOP jar mapreduce-1.0.jar com.prosoftnearshore.blog.y2013.m09.hadoop.mapreduce.WordCount /user/hadoop-test/input /user/hadoop-test/wordcount-output

# Run HTML Tag Count
$HADOOP fs -rmr /user/hadoop-test/htmltagcount-output
$HADOOP jar mapreduce-1.0.jar com.prosoftnearshore.blog.y2013.m09.hadoop.mapreduce.HtmlTagCount /user/hadoop-test/input /user/hadoop-test/htmltagcount-output

