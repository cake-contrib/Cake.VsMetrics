#load pipeline.cake

var target = Argument("target", "Default");

PipelineSettings.Solution = "Cake.VsMetrics.sln";

Task("Default")
    .IsDependentOn("BuildPipeline")
    .Does(() =>
{
});

RunTarget(target);