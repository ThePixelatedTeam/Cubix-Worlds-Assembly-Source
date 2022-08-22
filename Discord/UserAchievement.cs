// Decompiled with JetBrains decompiler
// Type: Discord.UserAchievement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Discord
{
  public struct UserAchievement
  {
    public long UserId;
    public long AchievementId;
    public byte PercentComplete;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
    public string UnlockedAt;
  }
}
