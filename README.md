# Streamlabs OBS Plugin

![GitHub](https://img.shields.io/github/license/RecklessBoon/Macro-Deck-Streamlabs-OBS-Plugin)

A plugin for Macro Deck 2 to interact with Streamlabs OBS Desktop application.

If you like my work and want to support/encourage me in making more plugins, you certainly can do so on Ko-Fi.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Z8Z37FRBM)

## Features

### Variables
This plugin updates the following variables for use anywhere variables are
allowed in Macro Deck 2

| Variable Name                      | Description                                                                                                       |
| ---------------------------------- | :---------------------------------------------------------------------------------------------------------------- |
| slobs_connected                    | Connection status (as true/false)<br>~<i>True if you are connected to the Streamlabs OBS Desktop application</i>~ |
| slobs_replay_buffer_state          | Replay Buffer status (as text)                                                                                    |
| slobs_replay_buffer                | Replay Buffer status (as true/false)<br>~<i>True if Replay Buffer status is <b>NOT</b> </i>`OFFLINE`~             |
| slobs_recording_status             | Recording status (as text)                                                                                        |
| slobs_recording                    | Recording status (as true/false)<br>~<i>True if Recording status is <b>NOT</b> </i>`OFFLINE`~                     |
| slobs_streaming_status             | Streaming status (as text)                                                                                        |
| slobs_streaming                    | Streaming status (as true/false)<br>~<i>True if Streaming status is <b>NOT</b> </i>`OFFLINE`~                     |
| slobs_active_scene_name            | Name of the active scene                                                                                          |
| slobs_active_scene_id              | ID of the active scene                                                                                            |
| slobs_active_scene_colleciton_name | Name of the active scene collection                                                                               |
| slobs_active_scene_collection_id   | ID of the active scene collection                                                                                 |

### Actions
This plugin adds the following actions

| Action                             | Description                                                                                                      |
| ---------------------------------- | :--------------------------------------------------------------------------------------------------------------- |
| Toggle Connection                  | Connect/Disconnect with the Streamlabs OBS Desktop client                                                        |
| Switch Scene Collection            | Switch to a specific Scene Collection                                                                            |
| Switch Scene                       | Switch scene within the current site collection<br>~<i>Only works if scene is in the active site colleciton</i>~ |
| Set Streaming State                | Start/Stop Streaming                                                                                             |
| Set Recording State                | Start/Stop Recording                                                                                             |
| Set Replay Buffer State            | Start/Stop Replay Buffer                                                                                         |
| Save Replay                        | Save the replay currently in the buffer                                                                          |
| Mute/Unmute Audio Source           | Set audio source mute/unmute state                                                                               |
| Set Audio Source Volume/Deflection | Set audio source volume deflection                                                                               |
| Update Scene Item Settings         | Set scene item settings (Visibility)                                                                             |
| Update Source Properties           | Set source properties                                                                                            |

## Installation
Download/Install it directly in Macro Deck from the package manager.

## Configuration

There is no configuration needed! 

If you have Streamlabs OBS Desktop open, then this plugin should connect automatically. 
If the connection has dropped, simply click the icon on the left side of your server application to initiate a new connection.

> **NOTE:** Currently only local connections are supported. This means that the `Settings` -> `Remote Control` 
option with the QR code won't work and SLOBS must be on the same computer as Macro Deck. ***Submit a Feature 
Request in Issues if you'd like this feature added***.

## Addendum

### Not a Standalone App
<img align="right" height="96px" src="https://macrodeck.org/images/macro_deck_2_community_plugin.png" />

This is a plugin for [Macro Deck 2](https://github.com/SuchByte/Macro-Deck), it does **NOT** function as a standalone app

### Third Party Licenses / Thank you

This plugin is built upon the shoulders of giants. Here are their licenses:

- [Newtonsoft.Json](https://www.newtonsoft.com/json)