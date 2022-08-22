// Decompiled with JetBrains decompiler
// Type: Inventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;

[Serializable]
public class Inventory
{
  public int maxSlots;
  public GameItem[] items;

  public void Initialize()
  {
    this.items = new GameItem[GameManager.instance.items.Count];
    for (int index = 0; index < GameManager.instance.items.Count; ++index)
      this.items[index] = new GameItem()
      {
        id = GameManager.instance.items[index].id,
        quantity = 0
      };
  }

  public void AddItem(int _itemID, int _quantity)
  {
    foreach (GameItem gameItem in this.items)
    {
      if (gameItem.id == _itemID)
        gameItem.quantity += _quantity;
    }
  }

  public void SetItem(int _itemID, int _quantity)
  {
    foreach (GameItem gameItem in this.items)
    {
      if (gameItem.id == _itemID)
        gameItem.quantity = _quantity;
    }
  }
}
