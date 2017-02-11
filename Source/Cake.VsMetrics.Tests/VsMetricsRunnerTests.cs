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
    }
}
