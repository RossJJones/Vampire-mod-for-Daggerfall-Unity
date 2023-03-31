// Project:         No Vampire HP Drain
// Original Author: ConsoleZOMBIE


using UnityEngine;
using FullSerializer;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects;
using DaggerfallWorkshop.Game.MagicAndEffects;
using DaggerfallWorkshop.Game.UserInterfaceWindows;
using DaggerfallWorkshop.Game.Questing;
using DaggerfallWorkshop.Game.Items;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop.Utility;
using DaggerfallConnect;
using DaggerfallConnect.Utility;
using Wenzil.Console;

namespace NoVampireHPDrain
//namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{ 
    public class CompoundRaceOverride : VampirismEffect
    {
        RaceTemplate compoundRace;
        #region Constructors
        VampireClans vampireClan = VampireClans.Lyrezi;

        public CompoundRaceOverride()
        {
            VampireConsoleCommands.RegisterCommands();
        }
        #endregion

        public override RaceTemplate CustomRace
        {
            get { return GetCompoundRace(); }
        }

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);

            // Create compound vampire race from birth race
            CreateCompoundRace();

            // Get vampire clan from stage one disease
            // Otherwise start as Lyrezi by default if no infection found
            // Note: Classic save import will start this effect and set correct clan after load
            VampirismInfection infection = (VampirismInfection)GameManager.Instance.PlayerEffectManager.FindIncumbentEffect<VampirismInfection>();
            if (infection != null)
                vampireClan = infection.InfectionVampireClan;

            // Considered well fed on first start
            UpdateSatiation();

            // Our dark transformation is complete - cure everything on player (including stage one disease)
            GameManager.Instance.PlayerEffectManager.CureAll();

            // Refresh head texture after effect starts
            DaggerfallUI.RefreshLargeHUDHeadTexture();
        }

        public void CreateCompoundRace()
        {
            // Clone birth race and assign custom settings
            // New compound races will retain almost everything from birth race
            compoundRace = GameManager.Instance.PlayerEntity.BirthRaceTemplate.Clone();
            compoundRace.Name = TextManager.Instance.GetLocalizedText("vampire");
            
            // Set special vampire flags
            compoundRace.ImmunityFlags |= DFCareer.EffectFlags.Paralysis;
            compoundRace.ImmunityFlags |= DFCareer.EffectFlags.Disease;
        }

       public RaceTemplate GetCompoundRace()
        {
            // Create compound race if one doesn't already exist
            if (compoundRace == null)
                CreateCompoundRace();

            return compoundRace;
        }
    }
}