using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MusicExpanded
{
    public class TrackDef : SongDef
    {
        public List<BiomeDef> allowedBiomes;
        public bool playDuringBattles = false;
        public bool playOnMainMenu = false;
        public bool playOnCredits = false;
        public bool vanillaLogic = false;
        public Cue cue = Cue.None;
        public string namedPawn;
        public bool AppropriateNow(MusicManagerPlay manager, SongDef lastPlayed)
        {
            if (
                lastPlayed == this
                || cue != Cue.None
            )
                return false;

            if (vanillaLogic)
                return (bool)Patches.MusicManagerPlay.appropriateNow.Invoke(manager, new SongDef[] { this as SongDef });

            return true;
        }
    }
}
