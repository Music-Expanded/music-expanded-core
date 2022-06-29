using Verse;
using System.Linq;
using System.Collections.Generic;
using RimWorld;

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
        public static void PlayTrack(Cue cue)
        {
            TrackDef track = GetTrack(cue);
            if (track == null)
            {
                Log.Warning("Tried to play cue'd track " + cue + " but none was found");
                return;
            }
            Find.MusicManagerPlay.ForceStartSong(track as SongDef, false);
        }
        public static void ShowNowPlaying(SongDef song)
        {
            if (Core.showNowPlaying)
                Messages.Message("ME_NowPlaying".Translate(song.label).ToString(), null, MessageTypeDefOf.NeutralEvent, null, false);
        }
        public static Cue BattleCue(float points)
        {
            // through debug options, I got around 5000 for a raid, so I'll arbitrarily say that's a legendary threat.
            return Cue.BattleLegendary;
        }
    }
}
