#r "Cake.VsMetrics.dll"

try
{
    Information("Hello cake world!");
    VsMetrics();
}
catch(Exception ex)
{
    Error("{0}", ex);
}