// Decompiled with JetBrains decompiler
// Type: ClientSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientSend : MonoBehaviour
{
  private static void SendTCPData(Packet _packet)
  {
    _packet.WriteLength();
    Client.instance.tcp.SendData(_packet);
  }

  private static void SendUDPData(Packet _packet)
  {
    _packet.WriteLength();
    Client.instance.udp.SendData(_packet);
  }

  public static void SendIntoGame()
  {
    using (Packet _packet = new Packet(1))
    {
      _packet.Write(Client.instance.myId);
      _packet.Write(GameManager.instance.username);
      _packet.Write(WorldManager.instance.worldName);
      _packet.Write(CultureInfo.CurrentCulture.Name);
      UIController.instance.SetActive(UIController.instance.loadingScreen, true);
      ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = UIController.instance.tips[Random.Range(0, UIController.instance.tips.Length)];
      ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
      ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Waiting For Response 0/6)";
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void AuthUser()
  {
    using (Packet _packet = new Packet(6))
    {
      if (UIController.instance.isLogin)
      {
        _packet.Write(UIController.instance.loginUserNameInputField.text);
        _packet.Write(UIController.instance.loginPasswordInputField.text);
        _packet.Write("null");
      }
      else
      {
        _packet.Write(UIController.instance.registerUserNameInputField.text);
        _packet.Write(UIController.instance.registerPasswordInputField.text);
        _packet.Write(UIController.instance.registerEmailInputField.text);
      }
      _packet.Write(UIController.instance.isLogin);
      _packet.Write(Client.instance.version);
      _packet.Write(SystemInfo.deviceUniqueIdentifier);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void PlayerMovement()
  {
    using (Packet _packet = new Packet(2))
    {
      _packet.Write(((Component) GameManager.players[Client.instance.myId]).transform.position);
      ClientSend.SendUDPData(_packet);
    }
  }

  public static void PlayerSize()
  {
    using (Packet _packet = new Packet(13))
    {
      _packet.Write(((Component) GameManager.players[Client.instance.myId]).transform.localScale);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void EditWorldData(int _newID, Vector2 _mouseWorldPosition)
  {
    using (Packet _packet = new Packet(3))
    {
      _packet.Write(_newID);
      _packet.Write(_mouseWorldPosition);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void TakeDroppedItem(int _id, DroppedItem _droppedItem)
  {
    using (Packet _packet = new Packet(4))
    {
      _packet.Write(_id);
      _packet.Write(_droppedItem.item.id);
      _packet.Write(_droppedItem.item.quantity);
      _packet.Write(_droppedItem.position);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void SendChatMessage(string _text)
  {
    using (Packet _packet = new Packet(5))
    {
      _packet.Write(_text);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void ItemAction(int _actionID, int _itemID, int _quantity)
  {
    using (Packet _packet = new Packet(7))
    {
      _packet.Write(_actionID);
      _packet.Write(_itemID);
      _packet.Write(_quantity);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void ItemAction(int _actionID, int _itemID, int _quantity, Vector2 _blockPosition)
  {
    using (Packet _packet = new Packet(7))
    {
      _packet.Write(_actionID);
      _packet.Write(_itemID);
      _packet.Write(_quantity);
      _packet.Write(_blockPosition);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void LeaveFromWorld()
  {
    using (Packet _packet = new Packet(8))
    {
      _packet.Write(1);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void BuyPack(int _packID)
  {
    using (Packet _packet = new Packet(9))
    {
      _packet.Write(_packID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void ButtonClick(int _buttonID)
  {
    using (Packet _packet = new Packet(10))
    {
      _packet.Write(_buttonID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void TradeAction(int _actionID)
  {
    using (Packet _packet = new Packet(12))
    {
      _packet.Write(_actionID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void AddItemToTrade(int _itemID, int _quantity)
  {
    using (Packet _packet = new Packet(11))
    {
      _packet.Write(_itemID);
      _packet.Write(_quantity);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void EditSignText(Vector2 _position, string _text)
  {
    using (Packet _packet = new Packet(14))
    {
      _packet.Write(_position);
      _packet.Write(_text);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void WearCostume(int _costumeID)
  {
    using (Packet _packet = new Packet(15))
    {
      _packet.Write(_costumeID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void ChangeAnimation(int _animationID)
  {
    GameManager.players[Client.instance.myId].ChangeAnimation(_animationID);
    using (Packet _packet = new Packet(16))
    {
      _packet.Write(_animationID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void ChangeBio(string _bio)
  {
    using (Packet _packet = new Packet(18))
    {
      _packet.Write(_bio);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void ChangeAuthData(string _password, string _dataToChangeData, int _dataToChange)
  {
    using (Packet _packet = new Packet(19))
    {
      _packet.Write(_password);
      _packet.Write(_dataToChangeData);
      _packet.Write(_dataToChange);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void RequestSignData(Vector2 _sign)
  {
    using (Packet _packet = new Packet(20))
    {
      _packet.Write(_sign);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void EditEntranceData(Vector2 _door, bool _value)
  {
    using (Packet _packet = new Packet(21))
    {
      _packet.Write(_door);
      _packet.Write(_value);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void RequestDoorData(Vector2 _door)
  {
    using (Packet _packet = new Packet(22))
    {
      _packet.Write(_door);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void EditDoorData(Vector2 _door, string _name, string _text)
  {
    using (Packet _packet = new Packet(23))
    {
      _packet.Write(_door);
      _packet.Write(_name);
      _packet.Write(_text);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void RequestData(int _id)
  {
    using (Packet _packet = new Packet(24))
    {
      _packet.Write(_id);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void LookForCraft(
    int _item1ID,
    int _item2ID,
    int _item1Quantity,
    int _item2Quantity)
  {
    using (Packet _packet = new Packet(25))
    {
      _packet.Write(_item1ID);
      _packet.Write(_item1Quantity);
      _packet.Write(_item2ID);
      _packet.Write(_item2Quantity);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void Craft(int _craftID, int _count)
  {
    using (Packet _packet = new Packet(26))
    {
      _packet.Write(_craftID);
      _packet.Write(_count);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void StartQuest(int _questID)
  {
    using (Packet _packet = new Packet(27))
    {
      _packet.Write(_questID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void SellItem(int _itemID, int _itemQuantity, int _price)
  {
    using (Packet _packet = new Packet(28))
    {
      _packet.Write(_itemID);
      _packet.Write(_itemQuantity);
      _packet.Write(_price);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void SaleItemAction(int _saleItemID, int _actionID)
  {
    using (Packet _packet = new Packet(29))
    {
      _packet.Write(_saleItemID);
      _packet.Write(_actionID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void UseCapsule(int _capsuleID, string _worldName)
  {
    using (Packet _packet = new Packet(30))
    {
      _packet.Write(_capsuleID);
      _packet.Write(_worldName);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void FishingAction(
    int _actionID,
    int _baitID,
    Vector2 _blockPosition,
    float _containerWidth)
  {
    using (Packet _packet = new Packet(31))
    {
      _packet.Write(_actionID);
      _packet.Write(_baitID);
      _packet.Write(_blockPosition);
      _packet.Write(_containerWidth);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void FishingAction(int _actionID)
  {
    using (Packet _packet = new Packet(31))
    {
      _packet.Write(_actionID);
      ClientSend.SendTCPData(_packet);
    }
  }

  public static void SelectSkinTone(int _skinToneID)
  {
    using (Packet _packet = new Packet(32))
    {
      _packet.Write(_skinToneID);
      ClientSend.SendTCPData(_packet);
    }
  }
}
