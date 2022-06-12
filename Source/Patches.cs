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
                IEnumerable<TrackDef> tracks = DefDatabase<TrackDef>.AllDefs.Where((TrackDef song) => Utilities.AppropriateNow(__instance, song));
                __result = tracks.First();
                return true;
            }
        }
    }
}