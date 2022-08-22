// Decompiled with JetBrains decompiler
// Type: Discord.Lobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Discord
{
  public struct Lobby
  {
    public long Id;
    public LobbyType Type;
    public long OwnerId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string Secret;
    public uint Capacity;
    public bool Locked;
  }
}
