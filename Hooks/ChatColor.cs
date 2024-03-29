﻿using HarmonyLib;
using ProjectM.UI;
using UnityEngine;
using TMPro;
using System;
using ChatLineColorMod.Utils;
using System.Text.RegularExpressions;
using System.Linq;
using ChatLineColorMod.UI;

namespace ChatLineColorMod.Hooks;

[HarmonyPatch]
public class HUDChatWindow_Path
{

    private static TMP_InputField chatLine = null;

    public static bool clearChat { get; set; } = false;
    public static bool testLoadChat { get; set; } = false;

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
        if (Plugin.ChatChannelEnabled.Value)
        {
            if (text != textEmoji)
            {
                if (lastChannel == "Whisper")
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
                }
                else
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
            }
        }

        if (Plugin.EmojiEnabled.Value)
        {
            if (text != textEmoji)
            {
                textEmoji = convertEmoji(text);
            }
        }
        else
        {
            textEmoji = text;
        }

    }

    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem._OnInputSubmit))]
    [HarmonyPrefix]
    public static void OnInputSubmit_Prefix(ClientChatSystem __instance, ref string text)
    {
        textEmoji = $"";
        if (Plugin.ChatChannelEnabled.Value)
        {
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
    }


    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem.OnUpdate))]
    [HarmonyPrefix]
    public static void CheckedState_PrefixAsync(ClientChatSystem __instance)
    {

        var channel = __instance._DefaultMode.ToString();
        if (Plugin.UIInit && Plugin.ButtonCleanChat.Value)
        {
            if (__instance._Hovered)
            {
                UIManager.ShowClearChatPanel();
            }
            else
            {
                UIManager.HideClearChatPanel();
            }
        }
        if (Plugin.AutoCleanEnabled.Value || Plugin.ButtonCleanChat.Value)
        {
            if (clearChat)
            {
                var chatLines = __instance._ChatMessages.Count;
                __instance._ChatMessages.RemoveRange(0, chatLines);
                clearChat = false;
            }
        }
        

        if(channel == "Team")
        {
            channel = "Clan";
        }

        

        if (channel != lastChannel)
        {
            textEmoji = textEmoji.Replace(lastChannel, channel);
            lastChannel = channel;
        }


        if (chatLine != null)
        {

            chatLine.textComponent.text = textEmoji;
            chatLine.text = textEmoji;
            if (Plugin.ChatColorEnabled.Value)
            {
                if (channel == "Local")
                {
                    chatLine.textComponent.color = colorLocal;
                }
                else if (channel == "Global")
                {
                    chatLine.textComponent.color = colorGLobal;
                }
                else if (channel == "Clan")
                {
                    chatLine.textComponent.color = colorTeam;
                }
                else if (channel == "Whisper")
                {
                    try
                    {
                        whispTo = __instance._WhisperUserName;
                    }
                    catch
                    {
                        whispTo = "Whisper";
                    }

                    chatLine.textComponent.color = colorWhisper;
                }
                else if (channel == "System")
                {
                    chatLine.textComponent.color = colorSystem;
                }
            }

            if (Plugin.ChatChannelEnabled.Value) chatLine.MoveToEndOfLine(shift: false, ctrl: false);
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
