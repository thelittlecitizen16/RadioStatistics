using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RadioStatistics.Abstract;

namespace RadioStatistics
{
    public class ArtistStatistics : IArtistStatistics
    {
        IEnumerable<Artist> _artists;

        public ArtistStatistics(IEnumerable<Artist> artists)
        {
            _artists = artists;
        }
        public IEnumerable<ArtistMetadata> GetArtistsAndAlbumCount()
        {

            return _artists.ToList().Select(a =>
            new ArtistMetadata() { AlbumCount = a.Albums.Count(), Name = a.Name }).ToList();
        }

        public IEnumerable<Artist> GetArtistsOrderdByName()
        {
            return _artists.OrderBy(a => a.Name).ToList();
        }

        public Artist GetArtistWithMostAlbums()
        {
            return _artists.OrderByDescending(a => a.Albums.Count()).ToList().First();
        }

        public IEnumerable<Artist> GetCatchyNamedArtists()
        {
            return _artists.Where(a => a.Name.Length < 7).ToList();
        }

        public IEnumerable<Artist> GetDiggingArtists() 
        {
            return _artists.Where(a => a.Albums
            .Where(a => a.Tracks
            .Where(t => t.Duration > TimeSpan.FromHours(1))
            .Any())
            .Any()).ToList();
        }

        public IEnumerable<Artist> GetEligibleForIsraelArtists()
        {
            return _artists.Where(a => a.Albums
            .Sum(a => a.Tracks
            .Sum(t => t.Duration.TotalSeconds)) > 7200).ToList();
        }

        public Artist GetFirstArtistWithTwoAlbums()
        {
            return _artists.FirstOrDefault(a => a.Albums.Count() == 2);
        }

        public IEnumerable<Artist> GetSlackerArtists()
        {
            return _artists.Where(a => a.Albums
            .Where(a => a.Tracks
            .Where(t => t.Duration.TotalSeconds < 60).Count() >= 2)
            .Any()).ToList();
        }

        public IEnumerable<Artist> GetYoungArtists()
        {
            return _artists.Where(a => a.Albums.Count() <= 2).ToList();
        }

        public IEnumerable<Artist> GetWritersBlockArtists()
        {
              return _artists.Where(a => a.Albums
              .GroupBy(a=>a.Tracks
              .Select(t=>t.Name))
              .Where(g=>g.Count() >= 3)
              .Any()).ToList();

        }
    }
}
