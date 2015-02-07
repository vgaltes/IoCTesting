namespace IoCTesting.Tests.UnitTests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public class IoCTestingTests
    {
        const string InvalidRegistrationAssemblyFullPath = "Invalid path";
        const string AssemblyToScanFullPath = "IoCTesting.Tests.TestingLibrary.dll";
        private const string ValidAssemblyName = "IoCTesting.Tests.StructureMap.dll";
        const string InvalidAssemblyPath = "InvalidAssemblyToScan.dll";
        private const string InvalidRegistrationClass = "InvalidRegistrationClassQualifiedName";
        private const string ValidRegistrationClass = "IoCTesting.Tests.StructureMap.StructureMapUserMissingConfiguration";


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRegistrationAssemblyPath()
        {
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(InvalidRegistrationAssemblyFullPath, 
                ValidRegistrationClass, AssemblyToScanFullPath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRegistrationAssemblyPath()
        {
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(null, ValidRegistrationClass, AssemblyToScanFullPath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRegistrationClassQualifiedName()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();
            
            iocTesting.CheckDependencies(assemblyToScanFullPath,
                InvalidRegistrationClass, assemblyToScanFullPath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRegistrationClassQualifiedName()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(assemblyToScanFullPath,
                null, assemblyToScanFullPath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidAssemblyToScanFullPath()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(assemblyToScanFullPath,
                ValidRegistrationClass, InvalidAssemblyPath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullAssemblyToScanFullPath()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(assemblyToScanFullPath,
                ValidRegistrationClass, null);
        }

        private static string GetValidAssemblyToScanFullPath()
        {
            var assemblyLocation = Directory.GetCurrentDirectory() + "\\";
            var assemblyToScanFullPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), ValidAssemblyName);
            return assemblyToScanFullPath;
        }
    }
}