using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace MusicExpanded.Patches
{
    public class BattleCue
    {
        [HarmonyPatch(typeof(RimWorld.IncidentWorker_RaidEnemy), "TryExecuteWorker")]
        class RaidEnemy
        {
            static void Postfix(IncidentParms parms, ref bool __result)
            {
                Log.Message("Found incident with points " + parms.points);
                Utilities.PlayTrack(Utilities.BattleCue(parms.points));
            }

        }
    }
}