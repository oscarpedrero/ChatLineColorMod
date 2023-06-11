using System;
using System.Diagnostics;
using Bloodstone.API;
using HarmonyLib;
using ProjectM;
using ProjectM.Auth;
using static ProjectM.Metrics;

namespace ChatLineColorMod.Hooks;

[HarmonyPatch]
internal class ClientEvents
{
 //   internal static event OnGamePla OnGameDataInitializedEventHandler OnGameDataInitialized;
 //   internal static event OnGameDataDestroyedEventHandler OnGameDataDestroyed;

    private static bool _onGameDataInitializedTriggered;
    [HarmonyPatch(typeof(GameDataManager), "OnUpdate")]
    [HarmonyPostfix]
    private static void GameDataManagerOnUpdatePostfix(GameDataManager __instance)
    {
        if (_onGameDataInitializedTriggered)
        {
            return;
        }

        try
        {
            if (!__instance.GameDataInitialized)
            {
                return;
            }

            _onGameDataInitializedTriggered = true;
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Debug.WriteLine("GameDataManagerOnUpdatePostfix Trigger");
            Plugin.GameDataOnInitialize(VWorld.Client);
            //OnGameDataInitialized?.Invoke(__instance.World);
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError(ex);
        }
    }

    [HarmonyPatch(typeof(ClientBootstrapSystem), "OnDestroy")]
    [HarmonyPostfix]
    private static void ClientBootstrapSystemOnDestroyPostfix(ClientBootstrapSystem __instance)
    {
        _onGameDataInitializedTriggered = false;
        try
        {
            //OnGameDataDestroyed?.Invoke();
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError(ex);
        }
    }

    [HarmonyPatch(typeof(SteamPlatformSystem), "TryInitClient")]
    [HarmonyPostfix]
    private static void SteamPlatformSystemOnTryInitClientPostfix(SteamPlatformSystem __instance)
    {
        try
        {
            //Users.CurrentUserSteamId = __instance.UserID;
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError(ex);
        }
    }
}
