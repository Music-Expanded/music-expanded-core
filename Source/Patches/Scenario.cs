using HarmonyLib;
using Verse;
using System.Linq;
using System.Collections.Generic;
using System;

namespace MusicExpanded.Patches
{
    public class Scenario
    {
        [HarmonyPatch(typeof(RimWorld.Scenario), "PostGameStart")]
        class PostGameStart
        {
            static bool NameMatches(Pawn pawn, NamedPawn name)
            {
                return pawn.Name.ToStringFull.ToLower().Contains(name.ToString().ToLower());
            }
            static void Postfix(RimWorld.Scenario __instance)
            {
                Map map = Find.AnyPlayerHomeMap ?? Find.CurrentMap;
                List<Pawn> pawns = map.PlayerPawnsForStoryteller.ToList();
                if (pawns.Count() == 1)
                {
                    Utilities.PlayTrack(Cue.LoneColonist);
                }
                else
                {
                    foreach (NamedPawn name in Enum.GetValues(typeof(NamedPawn)))
                    {
                        if (pawns.Where(pawn => NameMatches(pawn, name)).Any())
                        {
                            Utilities.PlayTrack(Cue.StartWithNamedColonist, name);
                        }
                    }
                }
            }

        }
    }
}