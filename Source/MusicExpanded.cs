using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using HugsLib;
using HugsLib.Settings;
using UnityEngine;
using Verse;

namespace MusicExpanded
{
    public class Core : ModBase
    {
        public static SettingHandle<string> selectedTheme;
        public static SettingHandle<bool> showNowPlaying;
        private static Vector2 scrollPosition = Vector2.zero;
        private static float viewHeight;

        public override void DefsLoaded()
        {

            showNowPlaying = Settings.GetHandle<bool>(
                "showNowPlaying",
                "ME_ShowNowPlaying".Translate(),
                "ME_ShowNowPlayingDescription".Translate(),
                true
            );

            selectedTheme.CustomDrawerHeight = 480f;
            selectedTheme.CustomDrawerFullWidth = rect => { return ThemeSelectScrollWindow(rect); };

            //selectedTheme = Settings.GetHandle<string>(
            //    "selectedTheme",
            //    "ME_Theme".Translate(),
            //    "ME_ThemeDescription".Translate(),
            //    "ME_Glitterworld",
            //    (string value) => DefDatabase<ThemeDef>.GetNamedSilentFail(value) != null
            //);

            selectedTheme.ValueChanged += (handle) => ThemeDef.ResolveSounds();
            ThemeDef.ResolveSounds();
        }

        private bool ThemeSelectScrollWindow(Rect rect)
        {
            List<ThemeDef> themes = DefDatabase<ThemeDef>.AllDefs.ToList();
            float entryHeight = 100f;
            viewHeight = entryHeight * themes.Count();
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(rect);
            foreach (ThemeDef theme in themes)
            {
                if (ThemeWidget(theme, listing.GetRect(entryHeight))) ;
                {
                    return true;
                }
            }
            listing.End();
            return false;
        }
        private bool ThemeWidget(ThemeDef themeDef, Rect mainRect)
        {
            float height = mainRect.height;
            if (themeDef.defName == selectedTheme.StringValue)
            {
                Widgets.DrawHighlight(mainRect);
            }
            Rect iconRect = mainRect;
            iconRect.xMax -= mainRect.width - height;
            iconRect.xMin = iconRect.xMax - height;

            Rect textRect = mainRect;
            textRect.xMin += height + 5f;
            textRect.xMax -= 5f;
            textRect.yMin += 5f;

            if (!themeDef.iconPath.NullOrEmpty())
            {
                Texture2D icon = ContentFinder<Texture2D>.Get(themeDef.iconPath, true);
                Widgets.DrawTextureFitted(iconRect, icon, height / icon.height);
            }

            Listing_Standard textListing = new Listing_Standard();
            textListing.Begin(textRect);

            Text.Font = GameFont.Medium;
            textListing.Label(themeDef.label);
            Text.Font = GameFont.Small;
            textListing.Label(themeDef.description);
            Rect selectButtonRect = textListing.GetRect(30f).LeftHalf();
            Listing_Standard selectButtonListing = new Listing_Standard();
            selectButtonListing.Begin(selectButtonRect);
            if (selectButtonListing.ButtonText("Select")) ;
            {
                string defName = themeDef.defName;
                selectedTheme.Value = themeDef.defName;
                return true;
            }
            selectButtonListing.End();
            textListing.End();
            return false;
        }



    }
}