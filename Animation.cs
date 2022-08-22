// Decompiled with JetBrains decompiler
// Type: Animation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[Serializable]
public class Animation
{
  public string name;
  public int updateFrameMS;
  public Thread animationThread;
  public List<Sprite> sprites;
  public bool isLoop = true;
}
