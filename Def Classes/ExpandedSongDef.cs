using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MusicExpanded.Def_Classes
{
    public class ExpandedSongDef : SongDef
    {
        public List<BiomeDef> allowedBiomes;
        public bool playDuringBattles = false;
        public bool playOnMainMenu = false;
        public bool playOnCredits = false;
    }
}
