using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
using BepInEx.Logging;
using ChatLineColorMod.Hooks;
using ChatLineColorMod.Timers;
using ChatLineColorMod.UI;
using HarmonyLib;
using System;
using System.Reflection;
using Unity.Entities;
using Bloodstone.API;

namespace ChatLineColorMod
{
    [BepInProcess("VRising.exe")]
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("gg.deca.Bloodstone")]
    [Reloadable]
    public class Plugin : BasePlugin , IRunOnInitialized
    {
        public static ManualLogSource Logger;

        private Harmony _harmony;

        public static ConfigEntry<bool> ChatColorEnabled;
        public static ConfigEntry<bool> ChatChannelEnabled;
        public static ConfigEntry<bool> ButtonCleanChat;
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
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            InitConfig();
            // Plugin startup logic
            //GameData.OnInitialize += GameDataOnInitialize;
            //GameData.OnDestroy += GameDataOnDestroy;
            GameFrame.Initialize();
            UIManager.Initialize();
            
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void InitConfig()
        {

            ButtonCleanChat = Config.Bind("ButtonCleanChat", "enabled", true, "Enabled button when you hover in the chat window");
            ChatColorEnabled = Config.Bind("ChatColor", "enabled", true, "Enable change the color of the chat input text as the color of the channel where you are typing");
            ChatChannelEnabled = Config.Bind("ChatChannel", "enabled", true, "Enable adds channel name in input where you are typing. This option will disable the ability to select a text or move within it.");
            EmojiEnabled = Config.Bind("Emojis", "enabled", true, "Enable Emojis replace");
            AutoCleanEnabled = Config.Bind("AutoCleanChat", "enabled", false, "Enable AutoCleanChat");
            AutoCleanInterval = Config.Bind("AutoCleanChat", "interval", 3600, "Time interval in seconds in which the chat is cleared");
            
            if(AutoCleanEnabled.Value) _autocleanInterval = AutoCleanInterval.Value;

        }

        public override bool Unload()
        {
            //GameData.OnInitialize -= GameDataOnInitialize;
            //GameData.OnDestroy -= GameDataOnDestroy;
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


        public static void GameDataOnInitialize(World world)
        {
            UIInit = true;
            Logger.LogInfo("GameData Init");
            _autoCleanTimer = new AutoCleanTimer();
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
