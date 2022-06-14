using k2_s22825.Models;
using k2_s22825.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k2_s22825.Services
{
    public class MusicService : IMusicService
    {
        private readonly MusicDbContext _context;
        public MusicService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMusician(int id)
        {
            var tracks = await _context.MusicianTracks
                .Where(e => IsTrackInAlbum(e.IdTrack) == false && e.IdMusician == id)
                .FirstOrDefaultAsync();

            if (tracks == null)
                return false;

            await DeleteMusicianTrack(id);
            await DeleteArtist(id);

            return true;
        }

        private bool IsTrackInAlbum(int id)
        {
            return _context.Tracks
                .Where(e => e.IdTrack == id && e.IdMusicAlbum != null).FirstOrDefault() != null;
        }

        private async Task DeleteArtist(int id)
        {
            var musician = new Musician
            {
                IdMusician = id
            };

            _context.Attach(musician);
            var entry = _context.Entry(musician);
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        private async Task DeleteMusicianTrack(int id)
        {
            var musicianTrack = new MusicianTrack
            {
                IdMusician = id
            };
            _context.Attach(musicianTrack);
            var entry = _context.Entry(musicianTrack);
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MusicianDTO>> GetMusician(int id)
        {
            var mus = _context.Musicians
                .Where(e => e.IdMusician == id)
                .Include(e => e.MusicianTracks)
                .ThenInclude(e => e.Track);
            if ( await mus.FirstOrDefaultAsync() == null)
                return null;
            return await mus
                .Select(e => new MusicianDTO
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Nickname = e.Nickname,
                    Tracks = e.MusicianTracks.Select(e => new TrackDTO
                    {
                        TrackName = e.Track.TrackName,
                        Duration = e.Track.Duration
                    }).OrderBy(e => e.Duration)
                    .ToList()
                }).ToListAsync();
        }
    }
}
