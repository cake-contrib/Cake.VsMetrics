#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2016.3.20161223.160402
#tool nuget:?package=OpenCover&version=4.6.519
#tool nuget:?package=ReportGenerator&version=2.4.5
#tool nuget:?package=ReportUnit&version=1.2.1
#tool nuget:?package=ReSharperReports&version=0.4.0

#addin nuget:?package=Cake.ReSharperReports&version=0.6.0

var testResultsDir = new DirectoryPath("TestResults");
var artifactsDir = new DirectoryPath("BuildArtifacts");
var openCoverDir = new DirectoryPath(artifactsDir + "/OpenCover");
var openCoverXml = new FilePath(openCoverDir + "/openCover.xml");
var vsTestDir = new DirectoryPath(artifactsDir + "/VSTest");
var dupFinderDir = new DirectoryPath(artifactsDir + "/DupFinder");
var dupFinderXml = new FilePath(dupFinderDir + "/dupFinder.xml");
var dupFinderHtml = new FilePath(dupFinderDir + "/dupFinder.html");
var inspectCodeDir = new DirectoryPath(artifactsDir + "/InspectCode");
var inspectCodeXml = new FilePath(inspectCodeDir + "/inspectCode.xml");
var inspectCodeHtml = new FilePath(inspectCodeDir + "/inspectCode.html");

public static class PipelineSettings
{
    static PipelineSettings()
    {
        DoAnalyze = true;
        DoTest = true;
        DoTreatWarningsAsErrors = true;
        Solution = "";
        Configuration = "Release";
        ToolVersion = MSBuildToolVersion.Default;
        Platform = MSBuildPlatform.Automatic;
        Properties = new Dictionary<string, string[]>();
        TestDllWhitelist = "*Tests.dll";
        OpenCoverFilter = "+[*]* -[*Tests]*";
        OpenCoverExcludeByFile = "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs";
        DupFinderExcludePattern = new string[] {};
    }

    public static bool DoAnalyze { get; set; }
    public static bool DoTest { get; set; }
    public static bool DoTreatWarningsAsErrors { get; set; }
    public static string Solution { get; set; }
    public static string Configuration { get; set; }
    public static MSBuildToolVersion ToolVersion { get; set; }
    public static MSBuildPlatform Platform { get; set; }
    public static Dictionary<string, string[]> Properties { get; private set; }
    public static string TestDllWhitelist { get; set; }
    public static string OpenCoverFilter { get; set; }
    public static string OpenCoverExcludeByFile { get; set; }
    public static string[] DupFinderExcludePattern { get; set; }
}

Task("Clean")
    .Does(() =>
{
    MSBuildSettings settings = new MSBuildSettings()
        .SetConfiguration(PipelineSettings.Configuration)
        .SetMSBuildPlatform(PipelineSettings.Platform)
        .UseToolVersion(PipelineSettings.ToolVersion)
        .WithTarget("Clean");

    if (PipelineSettings.DoTreatWarningsAsErrors)
    {
        settings.WithProperty("TreatWarningsAsErrors", new string[] { "true" });
    }

    CleanDirectories(new string[] { testResultsDir.FullPath, artifactsDir.FullPath });
    MSBuild(PipelineSettings.Solution, settings);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(PipelineSettings.Solution);
});

Task("Build")
    .IsDependentOn("Restore") 
    .Does(() =>
{
    MSBuildSettings settings = new MSBuildSettings()
        .SetConfiguration(PipelineSettings.Configuration)
        .SetMSBuildPlatform(PipelineSettings.Platform)
        .UseToolVersion(PipelineSettings.ToolVersion);

    if (PipelineSettings.DoTreatWarningsAsErrors)
    {
        settings.WithProperty("TreatWarningsAsErrors", new string[] { "true" });
    }

    MSBuild(PipelineSettings.Solution, settings);
});

Task("Test")
    .IsDependentOn("Build")
    .WithCriteria(PipelineSettings.DoTest)
    .Does(() =>
{
    EnsureDirectoryExists(openCoverDir);
    EnsureDirectoryExists(vsTestDir);

    OpenCover(
        tool => { tool.VSTest("**/bin/" + PipelineSettings.Configuration + "/" + PipelineSettings.TestDllWhitelist, new VSTestSettings().WithVisualStudioLogger()); },
        openCoverXml,
        new OpenCoverSettings() { ReturnTargetCodeOffset = 0 }
            .WithFilter(PipelineSettings.OpenCoverFilter)
            .ExcludeByFile(PipelineSettings.OpenCoverExcludeByFile));
})
.Finally(() =>
{
    CopyFiles(testResultsDir + "/*", vsTestDir);
    ReportGenerator(openCoverXml, openCoverDir);
    ReportUnit(testResultsDir, vsTestDir, new ReportUnitSettings());
});

Task("DupFinder")
    .IsDependentOn("Test")
    .WithCriteria(PipelineSettings.DoAnalyze)
    .Does(() =>
{
    EnsureDirectoryExists(dupFinderDir);
    DupFinder(PipelineSettings.Solution, new DupFinderSettings {
        ShowStats = true,
        ShowText = true,
        OutputFile = dupFinderXml,
        ThrowExceptionOnFindingDuplicates = true,
        ExcludePattern = PipelineSettings.DupFinderExcludePattern });
})
.Finally(() =>
{
    ReSharperReports(dupFinderXml, dupFinderHtml);
});

Task("InspectCode")
    .IsDependentOn("DupFinder")
    .WithCriteria(PipelineSettings.DoAnalyze)
    .Does(() =>
{
    EnsureDirectoryExists(inspectCodeDir);
    InspectCode(PipelineSettings.Solution, new InspectCodeSettings {
        SolutionWideAnalysis = true,
        OutputFile = inspectCodeXml,
        ThrowExceptionOnFindingViolations = true });
})
.Finally(() =>
{
    ReSharperReports(inspectCodeXml, inspectCodeHtml);
});

Task("BuildPipeline")
    .IsDependentOn("InspectCode")
    .Does(() =>
{
});