// Decompiled with JetBrains decompiler
// Type: Discord.ResultException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;

namespace Discord
{
  public class ResultException : Exception
  {
    public readonly Result Result;

    public ResultException(Result result)
      : base(result.ToString())
    {
    }
  }
}
