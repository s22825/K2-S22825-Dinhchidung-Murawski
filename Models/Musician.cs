using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k2_s22825.Models
{
    public class Musician
    {
        public int IdMusician { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public virtual ICollection<MusicianTrack> MusicianTracks { get; set; }
    }
}
