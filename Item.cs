// Decompiled with JetBrains decompiler
// Type: Item
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class Item
{
  public string name;
  public string description;
  public int layer;
  public int textureID;
  public int rarity;
  public int collisionType;
  public int typeID;
  public bool isRod;
  public bool isDropable;
  public bool isTrashable;
  public bool isConvertable;
  public bool isSolid;
  public bool isSign;
  public bool isEntrance;
  public bool isDoor;
  public bool isWrenchable;
  public bool isCapsule;
  public bool waterPhysics;
  public int skin;
  public int[] costumesToHide;
  public int health;
  public Sprite texture;
  public int[] textureIDs;
  public int[] animationTextureIDs;
  public bool isHarvestable;
  public bool isDealer;
  public Deal[] deals;
  public bool isBait;

  public int id { get; set; }
}
