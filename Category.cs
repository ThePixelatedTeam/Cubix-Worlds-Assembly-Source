// Decompiled with JetBrains decompiler
// Type: Category
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class Category
{
  public int itemID;
  public Dictionary<int, SaleItem> saleItems = new Dictionary<int, SaleItem>();
  public int floorPrice;
  public GameObject gameObject;
}
