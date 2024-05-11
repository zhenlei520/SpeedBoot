// See https://aka.ms/new-console-template for more information

var config = DefaultConfig.Instance
    .AddValidator(ExecutionValidator.FailOnError)
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);
BenchmarkRunner.Run<Benchmarks>(config);
