using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;
using System;

namespace MusicExpanded.Patches
{
    public class IncidentWorker
    {
        [HarmonyPatch]
        class BattleCue
        {
            static IEnumerable<MethodBase> TargetMethods()
            {
                foreach (Type type in new List<Type> {
                    typeof(RimWorld.IncidentWorker_RaidEnemy),
                    typeof(RimWorld.IncidentWorker_Infestation),
                    typeof(RimWorld.IncidentWorker_Ambush),
                    typeof(RimWorld.IncidentWorker_AnimalInsanityMass),
                    typeof(RimWorld.IncidentWorker_AnimalInsanitySingle),
                    typeof(RimWorld.IncidentWorker_MechCluster)
                })
                {
                    yield return AccessTools.Method(type, "TryExecuteWorker");
                }
            }
            static void Postfix(IncidentParms parms, ref bool __result) => Utilities.PlayTrack(Utilities.BattleCue(parms.points));

        }
    }
}