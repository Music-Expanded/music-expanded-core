﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Verse;
using Verse.Sound;

namespace MusicExpanded
{
    public class ThemeDef : Def
    {
        public static ThemeDef ActiveTheme => DefDatabase<ThemeDef>.GetNamed(Core.selectedTheme);
        public List<TrackDef> tracks;
        public List<SoundDef> sounds = new List<SoundDef>();
        public string iconPath;
        public static IEnumerable<TrackDef> TracksWithNamedColonist => ActiveTheme.tracks.Where(track => track.cue == Cue.StartWithNamedColonist);
        public static TrackDef TrackByDefName(string defName) => ActiveTheme.tracks.Find(track => track.defName == defName);
        public static IEnumerable<TrackDef> TracksByCue(Cue cue, string name = null)
        {
            return ActiveTheme.tracks.Where(track =>
            {
                return track.cue == cue && (!name.NullOrEmpty() || name == track.namedPawn);
            });
        }
        private static Dictionary<string, List<SubSoundDef>> vanillaSubSounds = new Dictionary<string, List<SubSoundDef>>();
        public static void ResolveSounds(ThemeDef theme = null)
        {
            if (theme == null) theme = ActiveTheme;
            List<Verse.SoundDef> vanillaSoundDefs = DefDatabase<Verse.SoundDef>.AllDefsListForReading;
            ResetSounds(vanillaSoundDefs);
            foreach (Verse.SoundDef vanillaSound in vanillaSoundDefs)
            {
                SoundDef expandedSound = theme.sounds.Find(sound => sound.replaces.Contains(vanillaSound));
                if (expandedSound == null) continue;
                vanillaSubSounds.SetOrAdd(vanillaSound.defName, vanillaSound.subSounds);
                vanillaSound.subSounds = expandedSound.subSounds;
                vanillaSound.ResolveReferences();
            }
        }
        private static void ResetSounds(List<Verse.SoundDef> vanillaSoundDefs)
        {
            foreach (Verse.SoundDef vanillaSound in vanillaSoundDefs)
            {
                List<SubSoundDef> subSounds = vanillaSubSounds.GetValueSafe(vanillaSound.defName);
                if (subSounds == null) continue;
                vanillaSound.subSounds = subSounds;
                vanillaSound.ResolveReferences();
                vanillaSubSounds.Remove(vanillaSound.defName);
            }
        }
    }
}
