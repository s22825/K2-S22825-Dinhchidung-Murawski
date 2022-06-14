using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k2_s22825.Models.DTOs
{
    public class MusicianDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public virtual IEnumerable<TrackDTO> Tracks { get; set; }
    }
}
