using HugsLib;
using HugsLib.Settings;
using Verse;

namespace MusicExpanded
{
    public class Core : ModBase
    {
        public static SettingHandle<string> selectedTheme;
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
        }
    }
}
