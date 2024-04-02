using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UITweaks
{
    [BepInPlugin("commander__cat.contentwarning.uitweaks", "UITweaks", "0.0.1")]
    public class Main : BaseUnityPlugin
    {
        private ConfigEntry<bool> VignetteEnabled;

        GameObject helmUI;
        GameObject vignetteUI;

        bool UIEnabled = true;
        void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
            GenConfig();
        }
        void GenConfig()
        {
            VignetteEnabled = Config.Bind("General", "Vignette Enabled?", true, "Whether Vignette is enabled or not.");
        }
        void Update()
        {
            if (helmUI != null)
            {
                if (Input.GetKeyUp(KeyCode.P))
                {
                    UIEnabled = !UIEnabled;
                    helmUI.SetActive(UIEnabled);
                }
            }
        }
        public void OnSceneChanged(Scene scene, Scene next)
        {
            if (next.name != "NewMainMenu")
            {
                helmUI = GameObject.Find("HelmetUI");
                vignetteUI = helmUI.transform.Find("Pivot").Find("Edge").gameObject;

                helmUI.SetActive(UIEnabled);
                vignetteUI.SetActive(VignetteEnabled.Value);
            }
            else
                helmUI = null;
        }
    }
}
