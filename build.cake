#load pipeline.cake

var target = Argument("target", "Default");

PipelineSettings.Solution = "Source/Cake.VsMetrics.sln";

Task("Default")
    .IsDependentOn("BuildPipeline")
    .Does(() =>
{
});

Task("CreateNuGet").Does(() =>
{
	EnsureDirectoryExists("BuildArtifacts/NuGet");
	NuGetPack("Source/Cake.VsMetrics/Cake.VsMetrics.nuspec", new NuGetPackSettings() { BasePath = "Source/Cake.VsMetrics/bin/" + PipelineSettings.Configuration, OutputDirectory = "BuildArtifacts/NuGet" });
});

RunTarget(target);