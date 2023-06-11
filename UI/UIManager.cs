using UniverseLib.UI;
using System.IO;
using UniverseLib;
using UnityEngine;
using System;
using ChatLineColorMod.UI.Panels;

namespace ChatLineColorMod.UI;

internal class UIManager
{

    public static UIBase UiBase { get; private set; }
    public static ClearChatPanel ClearChat { get; private set; }
    public static int SizeOfProdutcs { get; set; }

    internal static void Initialize()
    {
        const float startupDelay = 3f;
        UniverseLib.Config.UniverseLibConfig config = new()
        {
            Disable_EventSystem_Override = false, // or null
            Force_Unlock_Mouse = false, // or null
            Allow_UI_Selection_Outside_UIBase = true,
            Unhollowed_Modules_Folder = Path.Combine(BepInEx.Paths.BepInExRootPath, "interop") // or null
        };

        Universe.Init(startupDelay, OnInitialized, LogHandler, config);

    }

    static void OnInitialized()
    {
        Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        UiBase = UniversalUI.RegisterUI(unixTimestamp.ToString(), UiUpdate);
        //CreateAllPanels();
    }

    public static void CreateAllPanels()
    {
        CreateClearChatPanel();
    }

    public static void DestroyAllPanels()
    {
        DestroyClearChatPanel();
    }

    public static void CreateClearChatPanel()
    {
        ClearChat = new ClearChatPanel(UiBase);
        ClearChat.SetActive(false);
    }

    public static void ShowClearChatPanel()
    {
        ClearChat.SetActive(true);
    }

    public static void HideClearChatPanel()
    {
        ClearChat.SetActive(false);
    }

    public static void DestroyClearChatPanel()
    {
        ClearChat.Destroy();
    }

    static void UiUpdate()
    {
        // Called once per frame when your UI is being displayed.
    }

    static void LogHandler(string message, LogType type)
    {
        // ...
    }

}