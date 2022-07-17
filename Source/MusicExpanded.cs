using System.Linq;
using HarmonyLib;
using HugsLib;
using HugsLib.Settings;
using Verse;

namespace MusicExpanded
{
    public class Core : ModBase
    {
        public static SettingHandle<string> selectedTheme;
        public static SettingHandle<bool> showNowPlaying;
        public override void DefsLoaded()
        {
            selectedTheme = Settings.GetHandle<string>(
                "selectedTheme",
                "ME_Theme".Translate(),
                "ME_ThemeDescription".Translate(),
                "ME_Glitterworld",
                (string value) => DefDatabase<ThemeDef>.GetNamedSilentFail(value) != null
            );

            showNowPlaying = Settings.GetHandle<bool>(
                "showNowPlaying",
                "ME_ShowNowPlaying".Translate(),
                "ME_ShowNowPlayingDescription".Translate(),
                true
            );

            selectedTheme.ValueChanged += (handle) => ThemeDef.ResolveSounds();
            ThemeDef.ResolveSounds();
        }
    }
}
