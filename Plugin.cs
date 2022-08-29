using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using ChatLineColorMod.Hooks;
using ChatLineColorMod.Timers;
using ChatLineColorMod.UI;
using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using Unity.Entities;
using VRising.GameData;
using Wetstone.API;

namespace ChatLineColorMod
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("xyz.molenzwiebel.wetstone")]
    [Reloadable]
    public class Plugin : BasePlugin , IRunOnInitialized
    {
        public static ManualLogSource Logger;

        private Harmony _harmony;

        public static ConfigEntry<bool> ChatColorEnabled;
        public static ConfigEntry<bool> ChatChannelEnabled;
        public static ConfigEntry<bool> EmojiEnabled;
        public static ConfigEntry<bool> AutoCleanEnabled;
        public static ConfigEntry<int> AutoCleanInterval;

        private static AutoCleanTimer _autoCleanTimer;

        public static int _autocleanInterval { get; set; } = 0;
        
        public static bool UIInit { get; set; } = false;

        internal static Plugin Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
            Logger = Log;
            _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            InitConfig();
            // Plugin startup logic
            GameData.OnInitialize += GameDataOnInitialize;
            GameData.OnDestroy += GameDataOnDestroy;
            GameFrame.Initialize();
            UIManager.Initialize();
            UIInit = true;
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void InitConfig()
        {

            ChatColorEnabled = Config.Bind("ChatColor", "enabled", true, "Enable change the color of the chat input text as the color of the channel where you are typing");
            ChatChannelEnabled = Config.Bind("ChatChannel", "enabled", true, "Enable adds channel name in input where you are typing. This option will disable the ability to select a text or move within it.");
            EmojiEnabled = Config.Bind("Emojis", "enabled", true, "Enable Emojis replace");
            AutoCleanEnabled = Config.Bind("AutoCleanChat", "enabled", true, "Enable AutoCleanChat replace");
            AutoCleanInterval = Config.Bind("AutoCleanChat", "interval", 3600, "Time interval in seconds in which the chat is cleared");
            
            if(AutoCleanEnabled.Value) _autocleanInterval = AutoCleanInterval.Value;

        }

        public override bool Unload()
        {
            GameData.OnInitialize -= GameDataOnInitialize;
            GameData.OnDestroy -= GameDataOnDestroy;
            Config.Clear();
            GameFrame.Uninitialize();
            _harmony.UnpatchSelf();
            return true;
        }

        public void OnGameInitialized()
        {
            Log.LogInfo("Game has initialized!");
        }

        public static void StartAutoAnnouncer()
        {
            _autoCleanTimer.Start(
                world =>
                {
                    Logger.LogInfo("Starting AutoAnnouncer");
                    HUDChatWindow_Path.clearChat = true;
                },
                input =>
                {
                    if (input is not int secondAutoAnnouncer)
                    {
                        Logger.LogError("AutoCleanChat timer delay function parameter is not a valid integer");
                        return TimeSpan.MaxValue;
                    }

                    var seconds = _autocleanInterval;
                    Logger.LogInfo($"Next AutoCleanChat will start in {seconds} seconds.");
                    return TimeSpan.FromSeconds(seconds);
                });
        }

        public static void StopAutoAnnouncer()
        {
            _autoCleanTimer.Stop();
        }


        private static void GameDataOnInitialize(World world)
        {
            Logger.LogInfo("GameData Init");
            _autoCleanTimer = new AutoCleanTimer();
            StartAutoAnnouncer();
            UIManager.CreateAllPanels();
            if (AutoCleanEnabled.Value) StartAutoAnnouncer();
        }


        private static void GameDataOnDestroy()
        {
            Logger.LogInfo("GameData Destroy");
            StopAutoAnnouncer();
            GameFrame.Uninitialize();
            UIManager.DestroyAllPanels();
            if (AutoCleanEnabled.Value) StartAutoAnnouncer();
        }
    }
}
