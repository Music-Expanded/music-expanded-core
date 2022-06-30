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
                return AccessTools.AllTypes()
                    .Where(type => type.IsSubclassOf(typeof(RimWorld.IncidentWorker)))
                    .SelectMany(type => type.GetMethods())
                    .Where(method => method.Name == "TryExecute");
            }
            static void Postfix(RimWorld.IncidentWorker __instance, IncidentParms parms, ref bool __result)
            {
                if (__result != true) return;
                ModExtension.PlayCue playCue = __instance.def.GetModExtension<ModExtension.PlayCue>();
                if (playCue == null) return;
                if (playCue.playBattleTrack)
                    Utilities.PlayTrack(Utilities.BattleCue(parms.points));
                else
                    Utilities.PlayTrack(playCue.cue);
            }

        }
    }
}