# Cake.VsMetrics

Cake.VsMetrics is an Addin for [Cake](http://cakebuild.net/) which can calculate code metrics using Visual Studio's `metrics.exe` tool.

## Usage

Download and install the [Metrics Powertool](https://www.microsoft.com/en-us/download/details.aspx?id=48213) (this link is for Visual Studio 2015).

Include the Addin in your cake build script and register `metrics.exe` depending on which version you have installed:

```csharp
#addin "Cake.VsMetrics"

Setup(context => {
    context.Tools.RegisterFile("C:/Program Files (x86)/Microsoft Visual Studio 14.0/Team Tools/Static Analysis Tools/FxCop/metrics.exe");
});
```

Afterwards you can start to use the Addin:

```csharp
var projects = GetFiles("bin/Debug/*.dll");
var settings = new VsMetricsSettings()
{
    SuccessFile = true,
    IgnoreGeneratedCode = true
};

VsMetrics(projects, "metrics_result.xml", settings);
```

## License

[MIT](http://opensource.org/licenses/MIT)
