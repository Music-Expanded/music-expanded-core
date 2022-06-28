using HugsLib;
using RimWorld;
using Verse;

namespace MusicExpanded
{
    public static class Utilities
    {
        public static bool AppropriateNow(TrackDef track, SongDef lastPlayed)
        {
            // Figure out if a track is appropriate right now.
            // Map map = Find.AnyPlayerHomeMap ?? Find.CurrentMap;
            if (
                lastPlayed == track
                || track.playOnCredits
            ) return false;
            return true;
        }
        public static ThemeDef GetTheme()
        {
            return DefDatabase<ThemeDef>.GetNamed(Core.selectedTheme);
        }
    }
}
