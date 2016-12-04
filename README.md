# AWSLambdaCSharpIntroduction

This is Sample project for AWS Lambad with .NET Core C#. May this repository help you understand AWS Lambda better.

# Recommend Lambda Functions settings for stability

- May be complex execution requires more than 128MB. Be careful with Max memory used.

![](images/MemorySettings.png)

# What you can know

This sample include following.

FunctionName | Language | Description
---- | ---- | ----
SimpleClassFunction | C# | Simple minumum set of JSON Input sample.
SimpleAsyncFunction | C# | Simple minumum async/await with TEST for string Input sample. (SimpleAsyncFunction.Tests)

# Reference

http://tech.guitarrapc.com/archive/category/AWSLambda

# License

[MIT](https://github.com/guitarrapc/AzureFunctionsIntroduction/blob/master/LICENSE)
