// Decompiled with JetBrains decompiler
// Type: Discord.ActivityParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Discord
{
  public struct ActivityParty
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string Id;
    public PartySize Size;
  }
}
