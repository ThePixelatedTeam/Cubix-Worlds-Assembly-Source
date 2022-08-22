// Decompiled with JetBrains decompiler
// Type: AdHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdHandler : MonoBehaviour
{
  private RewardedAd rewardedAd;
  private string rewardedAdID;

  private void Start()
  {
    this.rewardedAdID = "ca-app-pub-7859129870154714/2205559924";
    MobileAds.Initialize((Action<InitializationStatus>) (initStatus => { }));
    this.RequestRewardedVideo();
  }

  private void RequestRewardedVideo()
  {
    this.rewardedAd = new RewardedAd(this.rewardedAdID);
    this.rewardedAd.OnUserEarnedReward += new EventHandler<Reward>(this.HandleOnEarned);
    this.rewardedAd.OnAdClosed += new EventHandler<EventArgs>(this.HandleOnClosed);
    this.rewardedAd.OnAdFailedToShow += new EventHandler<AdErrorEventArgs>(this.HandleOnFailedToShow);
    this.rewardedAd.LoadAd(new AdRequest.Builder().Build());
  }

  public void ShowRewardedVideo()
  {
    if (!this.rewardedAd.IsLoaded())
      return;
    this.rewardedAd.Show();
  }

  public void HandleOnEarned(object _sender, EventArgs _args)
  {
    this.RequestRewardedVideo();
    ClientSend.RequestData(5);
  }

  public void HandleOnClosed(object _sender, EventArgs _args) => this.RequestRewardedVideo();

  public void HandleOnFailedToShow(object _sender, EventArgs _args)
  {
    this.RequestRewardedVideo();
    UIController.instance.CreateCubotMessageFunction("Ad failed to load!", "<color=red>Error", 3f);
  }
}
