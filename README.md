A client mod that adds channel name and change the color of the chat input text as the color of the cannel where you are typing. It also replace text string with emojis.

## Configuration

Once the mod installed, a configuration file will be created in the \BepInEx\config client folder where you can activate or desactivate any of the mod functions.

**ChatLineColorMod.cfg**

```
## Settings file was created by plugin ChatLineColorMod v1.3.0
## Plugin GUID: ChatLineColorMod

[AutoCleanChat]

## Enable AutoCleanChat replace
# Setting type: Boolean
# Default value: true
enabled = true

## Time interval in seconds in which the chat is cleared
# Setting type: Int32
# Default value: 3600
interval = 60

[ChatColor]

## Enable adds channel name and change the color of the chat input text as the color of the cannel where you are typing
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
|AutoCleanChat|`enabled `            | Enable AutoCleanChat replace              | true
|AutoCleanChat        |`interval`            | Time interval in seconds in which the chat is cleared | 3600
|ChatColor|`enabled `| Enable adds channel name and change the color of the chat input text as the color of the channel where you are typing.                  |true
|Emojis|`enabled `| Enable Emojis replace                               |true

## Channel Name

 Adds channel name in the chat input where you are typing.

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