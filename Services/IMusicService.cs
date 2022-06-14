using k2_s22825.Models;
using k2_s22825.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k2_s22825.Services
{
    public interface IMusicService
    {
        public Task<IEnumerable<MusicianDTO>> GetMusician(int id);
        public Task<bool> DeleteMusician(int id);
    }
}
