using HugsLib;
using RimWorld;
using Verse;

namespace MusicExpanded
{
    public static class Utilities
    {
        public static bool AppropriateNow(TrackDef track)
        {
            // Figure out if a track is appropriate right now.
            // Map map = Find.AnyPlayerHomeMap ?? Find.CurrentMap;
            return true;
        }
    }
}
