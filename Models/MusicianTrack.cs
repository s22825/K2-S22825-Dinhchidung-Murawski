using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k2_s22825.Models
{
    public class MusicianTrack
    {
        public virtual int IdTrack { get; set; }
        public virtual int IdMusician { get; set; }
        public virtual Musician Musician { get; set; }
        public virtual Track Track { get; set; }
    }
}
