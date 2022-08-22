// Decompiled with JetBrains decompiler
// Type: DiscordRichPresence
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using Discord;
using System;
using UnityEngine;

public class DiscordRichPresence : MonoBehaviour
{
  public static DiscordRichPresence instance;
  public long clientID = 964920505100554270;
  private Discord.Discord discord;
  private ActivityManager activityManager;
  private bool isDiscordRunning = true;

  private void Awake()
  {
    if (!Object.op_Equality((Object) DiscordRichPresence.instance, (Object) null))
      return;
    DiscordRichPresence.instance = this;
  }

  private void Start()
  {
    if (!this.isDiscordRunning)
      return;
    this.discord = new Discord.Discord(this.clientID, 0UL);
    this.activityManager = this.discord.GetActivityManager();
    this.activityManager.UpdateActivity(new Activity()
    {
      State = "In Lobby",
      Details = "Idling",
      Assets = {
        LargeImage = "gameicon-2",
        LargeText = "discord.gg/cubixworlds"
      },
      Timestamps = {
        Start = ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeMilliseconds()
      }
    }, (ActivityManager.UpdateActivityHandler) (res =>
    {
      switch (res)
      {
        case Result.Ok:
          Debug.Log((object) "Successfully set DRP!");
          break;
        case Result.InternalError:
          Debug.LogError((object) "An error occured while setting DRP.");
          break;
        case Result.OAuth2Error:
          Debug.LogError((object) "An error occured while setting DRP.");
          break;
        case Result.PurchaseError:
          Debug.LogError((object) "An error occured while setting DRP.");
          break;
      }
    }));
  }

  public void UpdateDiscordRichPresence()
  {
    if (!this.isDiscordRunning)
      return;
    if (WorldManager.instance.worldName == "" || WorldManager.instance.worldName == null)
      this.activityManager.UpdateActivity(new Activity()
      {
        State = "In Lobby",
        Details = "Idling",
        Assets = {
          LargeImage = "gameicon-2",
          LargeText = "discord.gg/cubixworlds"
        },
        Timestamps = {
          Start = ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeMilliseconds()
        }
      }, (ActivityManager.UpdateActivityHandler) (res =>
      {
        switch (res)
        {
          case Result.Ok:
            Debug.Log((object) "Successfully set DRP!");
            break;
          case Result.InternalError:
            Debug.LogError((object) "An error occured while setting DRP.");
            break;
          case Result.OAuth2Error:
            Debug.LogError((object) "An error occured while setting DRP.");
            break;
          case Result.PurchaseError:
            Debug.LogError((object) "An error occured while setting DRP.");
            break;
        }
      }));
    else
      this.activityManager.UpdateActivity(new Activity()
      {
        State = "In " + WorldManager.instance.worldName,
        Details = "Playing",
        Assets = {
          LargeImage = "gameicon-2",
          LargeText = "discord.gg/cubixworlds"
        },
        Timestamps = {
          Start = ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeMilliseconds()
        }
      }, (ActivityManager.UpdateActivityHandler) (res =>
      {
        switch (res)
        {
          case Result.Ok:
            Debug.Log((object) "Successfully set DRP!");
            break;
          case Result.InternalError:
            Debug.LogError((object) "An error occured while setting DRP.");
            break;
          case Result.OAuth2Error:
            Debug.LogError((object) "An error occured while setting DRP.");
            break;
          case Result.PurchaseError:
            Debug.LogError((object) "An error occured while setting DRP.");
            break;
        }
      }));
  }

  private void Update()
  {
    if (!this.isDiscordRunning)
      return;
    this.discord.RunCallbacks();
  }

  private void OnApplicationQuit()
  {
    if (!this.isDiscordRunning)
      return;
    this.discord.Dispose();
  }
}
