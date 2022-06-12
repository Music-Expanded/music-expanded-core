using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MusicExpanded
{
    public class Patches
    {
        [HarmonyPatch(typeof(MusicManagerPlay), "ChooseNextSong")]
        class ChooseNextSong
        {
            static bool Prefix(MusicManagerPlay __instance, ref SongDef __result)
            {
                // TODO: Get current theme.
                ThemeDef theme = DefDatabase<ThemeDef>.GetNamed("ME_Glitterworld");
                IEnumerable<TrackDef> tracks = theme.tracks.Where(track => Utilities.AppropriateNow(track));
                __result = tracks.RandomElementByWeight((TrackDef s) => s.commonality) as SongDef;
                Log.Warning("Playing " + __result);
                return false;
            }
        }
    }
}