using Verse;
using System.Linq;
using System.Collections.Generic;
using RimWorld;

namespace MusicExpanded
{
    public static class Utilities
    {
        public static ThemeDef GetTheme()
        {
            return DefDatabase<ThemeDef>.GetNamed(Core.selectedTheme);
        }
        public static TrackDef GetTrack(Cue cue, NamedPawn name = NamedPawn.None)
        {
            ThemeDef theme = GetTheme();
            IEnumerable<TrackDef> tracks = theme.tracks.Where(track =>
            {
                return track.cue == cue && (name != NamedPawn.None || name == track.namedPawn);
            });
            return tracks.RandomElementByWeight((TrackDef s) => s.commonality);
        }
        public static void PlayTrack(Cue cue, NamedPawn name = NamedPawn.None)
        {
            TrackDef track = GetTrack(cue, name);
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
            if (points > 5000)
                return Cue.BattleLegendary;
            if (points > 2500)
                return Cue.BattleLarge;
            if (points > 500)
                return Cue.BattleMedium;
            return Cue.BattleSmall;
        }
    }
}
