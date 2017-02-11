Setup(context => {
    context.Tools.RegisterFile("C:/Program Files (x86)/Microsoft Visual Studio 14.0/Team Tools/Static Analysis Tools/FxCop/metrics.exe");
});

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
{
    MSBuild("./Source/Cake.VsMetrics.sln");
});

RunTarget(target);

#load "./Source/Cake.VsMetrics/bin/Debug/VsMetrics.cake"
