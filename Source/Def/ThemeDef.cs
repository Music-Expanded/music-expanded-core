using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MusicExpanded
{
    public class ThemeDef : Def
    {
        public static ThemeDef ActiveTheme => DefDatabase<ThemeDef>.GetNamed(Core.selectedTheme);
        public List<TrackDef> tracks;
        public IEnumerable<TrackDef> TracksWithNamedColonist => tracks.Where(track => track.cue == Cue.StartWithNamedColonist);
        public IEnumerable<TrackDef> TracksByCue(Cue cue, string name = null)
        {
            return tracks.Where(track =>
            {
                return track.cue == cue && (!name.NullOrEmpty() || name == track.namedPawn);
            });
        }
    }
}
