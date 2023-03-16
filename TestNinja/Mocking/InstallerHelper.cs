using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private IFileDownloader _fileDownloader;
        public InstallerHelper(IFileDownloader fileDownloader)
        {
            _fileDownloader = fileDownloader;
        }
        public bool DownloadInstaller(string customerName, string installerName)
        {            
            try
            {
                _fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }
    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            var client = new WebClient();

            client.DownloadFile(url, path);
        }
    }
}