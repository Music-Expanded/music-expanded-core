using HugsLib;
using RimWorld;
using Verse;

namespace MusicExpanded
{
    public static class Utilities
    {
        private static TrackDef lastPlayed;
        public static bool AppropriateNow(TrackDef track)
        {
            // Figure out if a track is appropriate right now.
            // Map map = Find.AnyPlayerHomeMap ?? Find.CurrentMap;
            if (
                lastPlayed == track
                || track.playOnCredits
            ) return false;
            lastPlayed = track;
            return true;
        }
        public static ThemeDef GetTheme()
        {
            return DefDatabase<ThemeDef>.GetNamed("ME_Glitterworld");
        }
    }
}
