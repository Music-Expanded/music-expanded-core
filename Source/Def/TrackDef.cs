using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MusicExpanded
{
    public class TrackDef : SongDef
    {
        public List<BiomeDef> allowedBiomes;
        public bool playDuringBattles = false;
        public bool playOnMainMenu = false;
        public bool playOnCredits = false;
    }
}
