using BepInEx;
using BepInEx.Configuration;
using ContentSettings.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zorro.Settings;

namespace UITweaks
{
    [BepInPlugin("commander__cat.contentwarning.uitweaks", "UITweaks", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static Main Instance { get; private set; }

        GameObject helmUI;
        GameObject vignetteUI;

        bool UIEnabled = true;
        public bool VignetteEnabled = true;

        public KeyCode HideHelmKey = KeyCode.P;
        void Awake()
        {
            Instance = this;

            SettingsLoader.RegisterSetting(new VignetteSetting());
            SettingsLoader.RegisterSetting(new UIToggleSetting());

            SceneManager.activeSceneChanged += OnSceneChanged;
        }
        void Update()
        {
            if (helmUI != null)
            {
                if (Input.GetKeyUp(HideHelmKey))
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
                vignetteUI.SetActive(VignetteEnabled);
            }
            else
                helmUI = null;
        }
    }

    public class VignetteSetting : EnumSetting, IExposedSetting
    {
        public override void ApplyValue()
        {
            Main.Instance.VignetteEnabled = Value == 0 ? false : true;
        }

        public override List<string> GetChoices()
        {
            return new List<string>()
            {
                "Off",
                "On"
            };
        }

        public string GetDisplayName()
        {
            return "Vignette Enabled?";
        }

        public SettingCategory GetSettingCategory()
        {
            return SettingCategory.Graphics;
        }

        protected override int GetDefaultValue()
        {
            return 1;
        }
    }
    public class UIToggleSetting : KeyCodeSetting, IExposedSetting
    {
        public override void ApplyValue()
        {
            Main.Instance.HideHelmKey = (KeyCode)Value;
        }

        public string GetDisplayName()
        {
            return "Hide UI Key";
        }

        public SettingCategory GetSettingCategory()
        {
            return SettingCategory.Controls;
        }

        protected override KeyCode GetDefaultKey()
        {
            return KeyCode.P;
        }
    }
}
