using HarmonyLib;
using ProjectM.UI;
using UnityEngine;
using TMPro;
using System;
using ChatLineColorMod.Utils;
using System.Text.RegularExpressions;
using System.Linq;
using Unity.Entities;
using ProjectM;
using ProjectM.Network;

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
    private static string textEmoji = $"";
    private static bool insertChannel = false;
    private static string whispTo = "";

    [HarmonyPatch(typeof(HUDChatWindow), nameof(HUDChatWindow.FocusInputField))]
    [HarmonyPrefix]
    public static void FocusInputField_Postfix(HUDChatWindow __instance)
    {
        chatLine = __instance.ChatInputField;
    }

    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem._OnInputChanged))]
    [HarmonyPrefix]
    public static void OnInputChanged_Prefix(ClientChatSystem __instance, ref string text)
    {
        if(text != textEmoji)
        {
                if(lastChannel == "Whisper")
                {
                    if (!text.Contains($"[{whispTo}]: "))
                    {
                        if (insertChannel)
                        {
                            text = $"[{whispTo}]: ";
                        }
                        else
                        {
                            text = text.Insert(0, $"[{whispTo}]: ");
                            insertChannel = true;

                        }
                    }
                } else
                {
                    if (!text.Contains($"[{lastChannel}]: "))
                    {
                        if (insertChannel)
                        {
                            text = $"[{lastChannel}]: ";
                        }
                        else
                        {
                            text = text.Insert(0, $"[{lastChannel}]: ");
                            insertChannel = true;

                        }
                    }
                }
            textEmoji = convertEmoji(text);

        }
 
    }

    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem._OnInputSubmit))]
    [HarmonyPrefix]
    public static void OnInputSubmit_Prefix(ClientChatSystem __instance, ref string text)
    {
        textEmoji = $"";
        if (lastChannel == "Whisper")
        {
            text = text.Replace($"[{whispTo}]:", "");
        }
        else
        {
            text = text.Replace($"[{lastChannel}]:", "");
        }
            
        insertChannel = false;
    }


    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem.OnUpdate))]
    [HarmonyPrefix]
    public static void CheckedState_PrefixAsync(ClientChatSystem __instance)
    {

        var channel = __instance._DefaultMode.ToString();

        

        if(channel != lastChannel)
        {
            textEmoji = textEmoji.Replace(lastChannel, channel);
            lastChannel = channel;
        }
            

        if (chatLine != null)
        {

            chatLine.textComponent.text = textEmoji;
            chatLine.text = textEmoji;

            if (channel == "Local")
            {
                chatLine.textComponent.color = colorLocal;
                chatLine.MoveToEndOfLine(shift: false, ctrl: false);
            }
            else if (channel == "Global")
            {
                chatLine.textComponent.color = colorGLobal;
                chatLine.MoveToEndOfLine(shift: false, ctrl: false);
            }
            else if (channel == "Team")
            {
                chatLine.textComponent.color = colorTeam;
                chatLine.MoveToEndOfLine(shift: false, ctrl: false);
            }
            else if (channel == "Whisper")
            {
                try
                {
                    var netWorkId = __instance._WhisperUser;
                    __instance.TryGetUserCharacterName(netWorkId, out string userName);
                    whispTo = userName;
                } catch
                {
                    whispTo = "Whisper";
                }
                
                chatLine.textComponent.color = colorWhisper;
                chatLine.MoveToEndOfLine(shift: false, ctrl: false);
            }
            else if (channel == "System")
            {
                chatLine.textComponent.color = colorSystem;
                chatLine.MoveToEndOfLine(shift: false, ctrl: false);
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
