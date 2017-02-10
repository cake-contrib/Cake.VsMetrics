#r "Cake.VsMetrics.dll"

try
{
    Information("Hello cake world!");
    EnsureDirectoryExists("BuildArtifacts");
    VsMetrics("Cake.VsMetrics/bin/Debug/Cake.VsMetrics.dll", "BuildArtifacts/metrics.xml");
}
catch(Exception ex)
{
    Error("{0}", ex);
}