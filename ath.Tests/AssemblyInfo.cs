using Xunit;

// CliUtils.InstanceParser mutates shared static state; xUnit runs
// separate test classes in parallel by default, which races on it.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
