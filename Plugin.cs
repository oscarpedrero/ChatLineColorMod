using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using Wetstone.API;

namespace ChatLineColorMod
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("xyz.molenzwiebel.wetstone")]
    [Reloadable]
    public class Plugin : BasePlugin
    {
        public static ManualLogSource Logger;

        private Harmony _harmony;

        public static ConfigEntry<bool> EmojiEnabled;

        public override void Load()
        {
            Logger = Log;
            _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            InitConfig();
            // Plugin startup logic
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void InitConfig()
        {

            EmojiEnabled = Config.Bind("Emojis", "enabled", true, "Enable Emojis replace");

        }

        public override bool Unload()
        {
            _harmony.UnpatchSelf();
            return true;
        }
    }
}
