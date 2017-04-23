# Cake.VsMetrics

Cake.VsMetrics is an Addin for [Cake](http://cakebuild.net/) which can calculate code metrics using Visual Studio's `metrics.exe` tool.

## Usage

Download and install the [Metrics Powertool](https://www.microsoft.com/en-us/download/details.aspx?id=48213) (this link is for Visual Studio 2015).

Include the Addin in your cake build script:

```csharp
#addin "Cake.VsMetrics"
```

Or NuGet reference:

```csharp
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.VsMetrics"
```

Afterwards you can start to use the Addin like this:

```csharp
var projects = GetFiles("bin/Debug/*.dll");
VsMetrics(projects, "metrics_result.xml");
```

Or like this using VsMetricsSettings:

```csharp
var projects = GetFiles("bin/Debug/*.dll");
var settings = new VsMetricsSettings()
{
    SuccessFile = true,
    IgnoreGeneratedCode = true
};

VsMetrics(projects, "metrics_result.xml", settings);
```

The settings object can also be used to specify the `metrics.exe` version:

```csharp
var settings = new VsMetricsSettings()
{
    ToolVersion = VsMetricsToolVersion.VS2015
};
```

## License

[MIT](http://opensource.org/licenses/MIT)
