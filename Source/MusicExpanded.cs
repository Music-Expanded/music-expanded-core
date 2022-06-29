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
                (string value) =>
                {
                    // Validates that there is a ThemeDef that exists with this defName
                    return DefDatabase<ThemeDef>.GetNamedSilentFail(value) != null;
                }
            );
            showNowPlaying = Settings.GetHandle<bool>(
                "showNowPlaying",
                "ME_ShowNowPlaying".Translate(),
                "ME_ShowNowPlayingDescription".Translate(),
                true
            );
        }
    }
}
