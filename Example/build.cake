#addin Cake.VsMetrics

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
{
    Information("Generate VsMetrics XML report:");
    var projects = GetFiles("../Source/Cake.VsMetrics/bin/Debug/*.dll");
    VsMetrics(projects, "metrics_result.xml");
});

RunTarget(target);
