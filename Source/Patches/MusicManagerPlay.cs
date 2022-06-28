using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MusicExpanded.Patches
{
    public class MusicManagerPlay
    {
        public static FieldInfo forcedSong = AccessTools.Field(typeof(RimWorld.MusicManagerPlay), "forcedNextSong");
        [HarmonyPatch(typeof(RimWorld.MusicManagerPlay), "ChooseNextSong")]
        class ChooseNextSong
        {
            static bool Prefix(RimWorld.MusicManagerPlay __instance, ref SongDef __result)
            {
                Object forcedSong = MusicManagerPlay.forcedSong.GetValue(__instance);
                if (forcedSong != null)
                    return true;
                ThemeDef theme = Utilities.GetTheme();
                IEnumerable<TrackDef> tracks = theme.tracks.Where(track => Utilities.AppropriateNow(track));
                __result = tracks.RandomElementByWeight((TrackDef s) => s.commonality) as SongDef;
                return false;
            }
        }
        [HarmonyPatch(typeof(Screen_Credits), "EndCreditsSong", MethodType.Getter)]
        class EndCreditsSong
        {
            static bool Prefix(ref SongDef __result)
            {
                // This is way too much on one line, maybe this should be cleaned up a bit?
                IEnumerable<TrackDef> creditTracks = Utilities.GetTheme().tracks.Where(track => track.playOnCredits);
                if (creditTracks.Any())
                {
                    __result = creditTracks.RandomElementByWeight((TrackDef s) => s.commonality) as SongDef;
                    return false;
                }
                return true;
            }
        }
    }
}