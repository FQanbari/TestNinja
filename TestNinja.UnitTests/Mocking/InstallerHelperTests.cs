using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private InstallerHelper _helpler;
        private Mock<IFileDownloader> _downloader;

        [SetUp] 
        public void SetUp() 
        {
            _downloader = new Mock<IFileDownloader>();
            _helpler = new InstallerHelper(_downloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadingFails_ReturnFale()
        {
            _downloader.Setup(x => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _helpler.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }
        [Test]
        public void DownloadInstaller_DownloadComplete_ReturnTrue()
        {

            var result = _helpler.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}
