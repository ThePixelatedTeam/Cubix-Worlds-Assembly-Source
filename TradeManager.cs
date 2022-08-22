// Decompiled with JetBrains decompiler
// Type: TradeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
  public static TradeManager instance;
  public TradeSlot[] peerTradeSlots = new TradeSlot[4];
  public TradeSlot[] selfTradeSlots = new TradeSlot[4];
  public GameObject[] peertradeSlotGameObjects;
  public GameObject[] selftradeSlotGameObjects;
  public bool isTrading;

  private void Awake()
  {
    if (!Object.op_Equality((Object) TradeManager.instance, (Object) null))
      return;
    TradeManager.instance = this;
  }

  private void Start()
  {
    for (int index = 0; index < this.peertradeSlotGameObjects.Length; ++index)
      this.peerTradeSlots[index] = new TradeSlot()
      {
        gameObject = this.peertradeSlotGameObjects[index]
      };
    for (int index = 0; index < this.selftradeSlotGameObjects.Length; ++index)
      this.selfTradeSlots[index] = new TradeSlot()
      {
        gameObject = this.selftradeSlotGameObjects[index]
      };
  }

  private void Update()
  {
  }

  public void AddItem(int _itemID, int _quantity, int _row, int _clientID)
  {
    if (_quantity != 0)
    {
      if (_clientID == Client.instance.myId)
      {
        this.selfTradeSlots[_row].item = new GameItem()
        {
          id = _itemID,
          quantity = _quantity
        };
        ((Component) this.selfTradeSlots[_row].gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[_itemID].texture;
        ((TMP_Text) ((Component) this.selfTradeSlots[_row].gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = _quantity.ToString();
      }
      else
      {
        this.peerTradeSlots[_row].item = new GameItem()
        {
          id = _itemID,
          quantity = _quantity
        };
        ((Component) this.peerTradeSlots[_row].gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[_itemID].texture;
        ((TMP_Text) ((Component) this.peerTradeSlots[_row].gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = _quantity.ToString();
      }
    }
    else if (_clientID == Client.instance.myId)
    {
      this.selfTradeSlots[_row].item = (GameItem) null;
      ((Component) this.selfTradeSlots[_row].gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = UIController.instance.UIMask;
      ((TMP_Text) ((Component) this.selfTradeSlots[_row].gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "";
    }
    else
    {
      this.peerTradeSlots[_row].item = (GameItem) null;
      ((Component) this.peerTradeSlots[_row].gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = UIController.instance.UIMask;
      ((TMP_Text) ((Component) this.peerTradeSlots[_row].gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "";
    }
  }
}
