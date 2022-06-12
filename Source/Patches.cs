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
            static void Postfix(MusicManagerPlay __instance, ref SongDef __result)
            {
                Log.Message("Patched! Saw " + __result);
                // It's here, that we can return a different Song, and the MusicManagerPlay should just play it natively, I figure
                // Alternatively, we could patch `StartNewSong()` with a prefix and re-write it to suit our needs
                // Oooor crazier, `MusicUpdate()` and change everything.
                __result = DefDatabase<TrackDef>.GetNamed("MusicExpanded_Glitterworld_ScenarioBegin") as SongDef;
            }
        }
    }
}