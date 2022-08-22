// Decompiled with JetBrains decompiler
// Type: Discord.OAuth2Token
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Discord
{
  public struct OAuth2Token
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string AccessToken;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string Scopes;
    public long Expires;
  }
}
