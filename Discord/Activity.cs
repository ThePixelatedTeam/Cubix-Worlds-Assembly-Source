// Decompiled with JetBrains decompiler
// Type: Discord.Activity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Discord
{
  public struct Activity
  {
    public ActivityType Type;
    public long ApplicationId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string Name;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string State;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string Details;
    public ActivityTimestamps Timestamps;
    public ActivityAssets Assets;
    public ActivityParty Party;
    public ActivitySecrets Secrets;
    public bool Instance;
  }
}
