
A client mod that adds extra functionality to the chat window:

- Channel name in front of the text in the input text.
- Color of the letters of the input text of the same color as the channel where you are writing.
- Button to clear the chat when you hover over the chat window.
- AutoClean functionality of the chat with a configurable interval.
- Functionality to replace text with emojis.

## Configuration

Once the mod installed, a configuration file will be created in the \BepInEx\config client folder where you can activate or desactivate any of the mod functions.

**ChatLineColorMod.cfg**

```
## Settings file was created by plugin ChatLineColorMod v1.3.5
## Plugin GUID: ChatLineColorMod

[AutoCleanChat]

## Enable AutoCleanChat
# Setting type: Boolean
# Default value: false
enabled = false

## Time interval in seconds in which the chat is cleared
# Setting type: Int32
# Default value: 3600
interval = 3600

[ButtonCleanChat]

## Enabled button when you hover in the chat window
# Setting type: Boolean
# Default value: true
enabled = true

[ChatChannel]

## Enable adds channel name in input where you are typing. This option will disable the ability to select a text or move within it.
# Setting type: Boolean
# Default value: true
enabled = true

[ChatColor]

## Enable change the color of the chat input text as the color of the channel where you are typing
# Setting type: Boolean
# Default value: true
enabled = true

[Emojis]

## Enable Emojis replace
# Setting type: Boolean
# Default value: true
enabled = true


```


|SECTION|PARAM| DESCRIPTION                                                     | DEFAULT
|----------------|-------------------------------|-----------------------------------------------------------------|-----------------------------|
|AutoCleanChat|`enabled `            | Enable AutoCleanChat              | false
|AutoCleanChat        |`interval`            | Time interval in seconds in which the chat is cleared | 3600
|ButtonCleanChat|`enabled `            | Activate button when you hover in the chat window              | true
|ChatChannel|`enabled `| Enable adds channel name in input where you are typing. This option will disable the ability to select a text or move within it.                  |true
|ChatColor|`enabled `| Enable change the color of the chat input text as the color of the channel where you are typing                  |true
|Emojis|`enabled `| Enable Emojis replace                               |true

## Button Clear Chat

 Added a button to delete the chat every time you hover in the chat window.

![alt text](https://github.com/oscarpedrero/ChatLineColorMod/blob/master/imgs/clearbutton.jpg?raw=true)

## Channel Name

 Adds channel name in the chat input where you are typing.

 > This option will disable the ability to select a text or move within it.

![alt text](https://github.com/oscarpedrero/ChatLineColorMod/blob/master/imgs/local.png?raw=true)

![alt text](https://github.com/oscarpedrero/ChatLineColorMod/blob/master/imgs/global.PNG?raw=true)

![alt text](https://github.com/oscarpedrero/ChatLineColorMod/blob/master/imgs/team.PNG?raw=true)

![alt text](https://github.com/oscarpedrero/ChatLineColorMod/blob/master/imgs/whisp.PNG?raw=true)

![alt text](https://github.com/oscarpedrero/ChatLineColorMod/blob/master/imgs/system.PNG?raw=true)

## Emojis

Now replace string to emoticons:

|STRING|STRING|EMOJI
|----------------|-------------------------------|-----------------------------|
|xE|`:grin:`|😁
|xD|`:joy:`|😂
|:)|`:smiley:`|😃
|:D|`:smile:`|😄
|;D|`:sweat_smile:`|😅
|lol|`:laughing:`|😆
|;)|`:wink:`|😉
|x)|`:blush:`|😊
|:P|`:yum:`|😋
|<3)|`:heart_eyes:`|😍
|:*|`:kissing_heart:`|😘