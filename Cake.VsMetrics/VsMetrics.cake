#r "Cake.VsMetrics.dll"

try
{
    Information("Hello cake world!");
    EnsureDirectoryExists("BuildArtifacts");
    var projects = GetFiles("Cake.VsMetrics/bin/Debug/*.dll");
    VsMetrics(projects, "BuildArtifacts/metrics.xml");
}
catch(Exception ex)
{
    Error("{0}", ex);
}