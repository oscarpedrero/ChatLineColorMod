using HarmonyLib;
using ProjectM.Network;
using ProjectM.UI;
using Unity.Entities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChatLineColorMod.Hooks;

[HarmonyPatch]
internal class HUDChatWindow_Path
{

    private static TMP_InputField chatLine = null;

    private static Color colorLocal = new(218 / 255f, 201 / 255f, 192 / 255f);
    private static Color colorGLobal = new(153 / 255f, 204 / 255f, 255 / 255f);
    private static Color colorTeam = new(121 / 255f, 218 / 255f, 166 / 255f);
    private static Color colorWhisper = new(255 / 255f, 159 / 255f, 255 / 255f);
    private static Color colorSystem = new(228 / 255f, 141 / 255f, 40 / 255f);
    private static string lastChannel = "Local";

    [HarmonyPatch(typeof(HUDChatWindow), nameof(HUDChatWindow.FocusInputField))]
    [HarmonyPrefix]
    public static void FocusInputField_Prefix(HUDChatWindow __instance)
    {
            chatLine = __instance.ChatInputField;
    }

    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem.OnUpdate))]
    [HarmonyPrefix]
    public static void CheckedState_Prefix(ClientChatSystem __instance)
    {

        var channel = __instance._DefaultMode.ToString();

        lastChannel = channel;

        if (chatLine != null)
        {
            if (channel == "Local")
            {
               
                chatLine.textComponent.color = colorLocal;
            }
            else if (channel == "Global")
            {
                
                chatLine.textComponent.color = colorGLobal;
            }
            else if (channel == "Team")
            {
                
                chatLine.textComponent.color = colorTeam;
            }
            else if (channel == "Whisper")
            {
                
                chatLine.textComponent.color = colorWhisper;
            }
            else if (channel == "System")
            {
                chatLine.textComponent.color = colorSystem;
            }
        }

        

    }
}
