using HarmonyLib;
using ProjectM.UI;
using UnityEngine;
using TMPro;
using System;
using ChatLineColorMod.Utils;
using System.Text.RegularExpressions;
using System.Linq;

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
    private static string pattern = @"(\:(\w|\+|\-)+\:)(?=|[\!\.\?]|$)";
    private static string textEmoji = "";

    [HarmonyPatch(typeof(HUDChatWindow), nameof(HUDChatWindow.FocusInputField))]
    [HarmonyPrefix]
    public static void FocusInputField_Prefix(HUDChatWindow __instance)
    {
            chatLine = __instance.ChatInputField;
    }

    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem._OnInputChanged))]
    [HarmonyPrefix]
    public static void OnInputChanged_Prefix(ClientChatSystem __instance, string text)
    {
        if(text != textEmoji)
        {
            textEmoji = convertEmoji(text);
        }
        
    }

    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem._OnInputSubmit))]
    [HarmonyPrefix]
    public static void OnInputSubmit_Prefix(ClientChatSystem __instance, string text)
    {
        textEmoji ="";
    }


    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem.OnUpdate))]
    [HarmonyPrefix]
    public static void CheckedState_Prefix(ClientChatSystem __instance)
    {

        var channel = __instance._DefaultMode.ToString();

        lastChannel = channel;

        

        if (chatLine != null)
        {

            chatLine.textComponent.text = textEmoji;
            chatLine.text = textEmoji;

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

    public static string convertEmoji(string text)
    {
        RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;

        Emoji oEmoji = new();

        text = Emoji.convertCharactersToEoji(text);

        foreach (Match m in Regex.Matches(text, pattern, options))
        {
            string property = stringUpper(m.Value.Replace(":", ""));
            try
            {
                string emojiVal = Convert.ToString(typeof(Emoji).GetField(property).GetValue(oEmoji));
                text = text.Replace(m.Value, emojiVal);
            }
            catch
            {
                //Plugin.Logger.LogInfo($"Not found '{property}'.");
            }
            
        }

        return text;
    }

    public static string stringUpper(string str)
    {
        return string.Join("_", str.Split('_').Select(str => char.ToUpper(str[0]) + str.Substring(1)));
    }
}
