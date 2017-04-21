using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cake.VsMetrics.Tests
{
    [TestClass]
    public sealed class VsMetricsRunnerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RunFixtureWithNoInputFilePaths()
        {
            var fixture = new VsMetricsRunnerFixture() { InputFilePaths = null };

            fixture.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RunFixtureWithNoOutputFilePath()
        {
            var fixture = new VsMetricsRunnerFixture() { OutputFilePath = null };

            fixture.Run();
        }

        [TestMethod]
        public void RunFixtureWithNoSettings()
        {
            var fixture = new VsMetricsRunnerFixture() { Settings = null };

            fixture.Run();

            // We expect this to not throw any exception
        }

        [TestMethod]
        [ExpectedException(typeof(CakeException))]
        public void RunFixtureWithProcessReturningError()
        {
            var fixture = new VsMetricsRunnerFixture();
            fixture.GivenProcessExitsWithCode(1);

            fixture.Run();
        }

        [TestMethod]
        public void RunFixtureWithSettings()
        {
            var fixture = new VsMetricsRunnerFixture()
            {
                Settings = { SearchGac = true, AssemblyDirectories = new DirectoryPath[] { "." } }
            };

            var result = fixture.Run();

            Assert.AreEqual("/f:\"c:/tool.exe\" /o:\"/Working/metrics_result.xml\" /d:\"/Working\" /gac", result.Args);
        }

        [TestMethod]
        public void RunFixtureWithAlternativePathWindows()
        {
            AssertAlternativePath(VsMetricsToolVersion.Default, "/ProgramFilesX86/Microsoft Visual Studio 14.0/Team Tools/Static Analysis Tools/FxCop/metrics.exe");
            AssertAlternativePath(VsMetricsToolVersion.VS2015, "/ProgramFilesX86/Microsoft Visual Studio 14.0/Team Tools/Static Analysis Tools/FxCop/metrics.exe");
            AssertAlternativePath(VsMetricsToolVersion.VS2013, "/ProgramFilesX86/Microsoft Visual Studio 12.0/Team Tools/Static Analysis Tools/FxCop/metrics.exe");
        }

        [TestMethod]
        [ExpectedException(typeof(CakeException))]
        public void RunFixtureWithAlternativePathWindowsNotSupported()
        {
            var fixture = new VsMetricsRunnerFixture(false)
            {
                Settings = { ToolVersion = (VsMetricsToolVersion)1000 }
            };

            fixture.Environment.SetSpecialPath(SpecialPath.ProgramFilesX86, "/ProgramFilesX86");

            fixture.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(CakeException))]
        public void RunFixtureWithAlternativePathLinuxNotSupported()
        {
            var fixture = new VsMetricsRunnerFixture(false, PlatformFamily.Linux);

            fixture.Run();
        }

        private void AssertAlternativePath(VsMetricsToolVersion version, string expectedPath)
        {
            var fixture = new VsMetricsRunnerFixture(false)
            {
                Settings = { ToolVersion = version }
            };

            fixture.FileSystem.CreateFile(expectedPath);
            fixture.Environment.SetSpecialPath(SpecialPath.ProgramFilesX86, "/ProgramFilesX86");

            var result = fixture.Run();

            Assert.AreEqual(expectedPath, result.Path.FullPath);
        }
    }
}
