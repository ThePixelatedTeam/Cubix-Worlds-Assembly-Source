// Decompiled with JetBrains decompiler
// Type: Quest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class Quest
{
  public int id;
  public string title;
  public string description;
  public Step[] steps;
  public TemporaryItem prize;
  public GameObject gameObject;
}
