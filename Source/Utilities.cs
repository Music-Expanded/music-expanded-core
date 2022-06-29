using Verse;
using System.Linq;
using System.Collections.Generic;

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
                || track.playOnMainMenu
                || track.cue != Cue.None
            ) return false;
            return true;
        }
        public static ThemeDef GetTheme()
        {
            return DefDatabase<ThemeDef>.GetNamed(Core.selectedTheme);
        }
        public static TrackDef GetTrack(Cue cue)
        {
            ThemeDef theme = GetTheme();
            IEnumerable<TrackDef> tracks = theme.tracks.Where(track =>
            {
                return track.cue == cue;
            });
            return tracks.RandomElementByWeight((TrackDef s) => s.commonality);
        }
    }
}
