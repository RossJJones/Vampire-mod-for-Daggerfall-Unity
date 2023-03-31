using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using DaggerfallWorkshop.Game.MagicAndEffects;
using DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects;
using UnityEngine;

namespace NoVampireHPDrain
//namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    public class NoVampireHPDrain : MonoBehaviour
    {
        static Mod mod;

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;
            var go = new GameObject("NoVampireHPDrain");
            go.AddComponent<NoVampireHPDrain>();
        }

        void Awake()
        {
            InitMod();
            mod.IsReady = true;
        }

        private static void InitMod()
        {
           Debug.Log("Begin mod init: NoVampireHPDrain");

            //GameManager.Instance.PlayerEffectManager.FindIncumbentEffect<VampirismEffect>() = new CompoundRaceOverride();

            GameManager.Instance.EntityEffectBroker.RegisterEffectTemplate(new CompoundRaceOverride(), true);


            Debug.Log("Finished mod init: NoVampireHPDrain");
        }

    }
}
