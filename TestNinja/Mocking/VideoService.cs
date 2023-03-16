using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader;
        private IVideoRepository _repository;
        public VideoService(IFileReader fileReader, IVideoRepository repository)
        {
            _fileReader = fileReader;
            _repository = repository;
        }
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _repository.GetUnProssedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);
          
            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
    public interface IFileReader
    {
        string Read(string path);
    }
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnProssedVideos();
    }
    public class VideoRepository:IVideoRepository
    {
        private VideoContext _context;

        public IEnumerable<Video> GetUnProssedVideos()
        {
            using (_context = new VideoContext())
            {
                var videos =
                    (from video in _context.Videos
                     where !video.IsProcessed
                     select video).ToList();

                return videos;
            }
        }
    }
}