// Decompiled with JetBrains decompiler
// Type: ClientHandle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientHandle : MonoBehaviour
{
  public static void Welcome(Packet _packet)
  {
    string _text = _packet.ReadString();
    int num = _packet.ReadInt();
    string str = _packet.ReadString();
    UIController.instance.CreateNotificationFunction("<color=green>Successful", _text, 3f);
    Client.instance.myId = num;
    GameManager.instance.textureURL = str;
    GameManager.instance.InitializeTexturesFromWeb();
    Client.instance.udp.Connect(((IPEndPoint) Client.instance.tcp.socket.Client.LocalEndPoint).Port);
  }

  public static void AuthUser(Packet _packet)
  {
    int num1 = _packet.ReadInt();
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ClientHandle.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new ClientHandle.\u003C\u003Ec__DisplayClass1_0();
    switch (num1)
    {
      case 0:
        string str = _packet.ReadString();
        int num2 = _packet.ReadInt();
        int index = _packet.ReadInt();
        long num3 = _packet.ReadLong();
        _packet.ReadString();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass10._ownedWorldsData = _packet.ReadString();
        ((TMP_Text) ((Component) UIController.instance.shop.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "<sprite index=0> " + num3.ToString();
        ((TMP_Text) UIController.instance.cubixsCount.GetComponent<TextMeshProUGUI>()).text = "<sprite index=0> " + num3.ToString();
        UIController.instance.CreateNotificationFunction("<color=green>Successful", "Successfully logged in", 3f);
        GameManager.instance.username = UIController.instance.loginUserNameInputField.text;
        PlayerPrefs.SetString("username", UIController.instance.loginUserNameInputField.text);
        PlayerPrefs.SetString("password", UIController.instance.loginPasswordInputField.text);
        ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = UIController.instance.loginUserNameInputField.text;
        ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TMP_InputField>().text = str;
        string[] accountTypes = GameManager.instance.accountTypes;
        ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = accountTypes[index] + " <sprite=" + index.ToString() + ">";
        ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = num3.ToString() + " <sprite=0>";
        ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "Level " + num2.ToString() + " <sprite=1>";
        ((UnityEventBase) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(9)).GetComponent<Button>().onClick).RemoveAllListeners();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((UnityEvent) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(9)).GetComponent<Button>().onClick).AddListener(ClientHandle.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (ClientHandle.\u003C\u003Ec.\u003C\u003E9__1_0 = new UnityAction((object) ClientHandle.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAuthUser\u003Eb__1_0))));
        ((UnityEventBase) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(5)).GetComponent<Button>().onClick).RemoveAllListeners();
        // ISSUE: method pointer
        ((UnityEvent) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(5)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass10, __methodptr(\u003CAuthUser\u003Eb__1)));
        UIController.instance.OpenMenuScreen();
        break;
      case 1:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "Account already exists", 3f);
        break;
      case 2:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "Account doesn't exist", 3f);
        break;
      case 3:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "Password doesn't match", 3f);
        break;
      case 4:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "An error occured", 3f);
        break;
      case 5:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "Inputs must be filled", 3f);
        break;
      case 6:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "This account is already in-game", 3f);
        break;
      default:
        UIController.instance.CreateNotificationFunction("<color=red>Warning", "An error occured", 3f);
        break;
    }
  }

  public static void SpawnPlayer(Packet _packet)
  {
    int _id = _packet.ReadInt();
    string _username = _packet.ReadString();
    int _accountLevel = _packet.ReadInt();
    int _skinID = _packet.ReadInt();
    Vector2 _position = _packet.ReadVector2();
    Vector2 _size = _packet.ReadVector2();
    Vector2 _collisionSize = _packet.ReadVector2();
    string str = _packet.ReadString();
    int[] _costumeDataArray = new int[str.Split(':', StringSplitOptions.None).Length - 1];
    for (int index = 1; index < str.Split(':', StringSplitOptions.None).Length; ++index)
      _costumeDataArray[index - 1] = int.Parse(str.Split(':', StringSplitOptions.None)[index]);
    string _bio = _packet.ReadString();
    int _level = _packet.ReadInt();
    int _currentXP = _packet.ReadInt();
    int _maxXP = _packet.ReadInt();
    GameManager.instance.SpawnPlayer(_id, _username, _accountLevel, _skinID, _position, _size, _collisionSize, _costumeDataArray, _bio, _level, _currentXP, _maxXP);
  }

  public static void PlayerPosition(Packet _packet)
  {
    int key = _packet.ReadInt();
    Vector2 vector2 = _packet.ReadVector2();
    if (!GameManager.players.ContainsKey(key))
      return;
    GameManager.players[key].isArrived = false;
    GameManager.players[key].destination = vector2;
  }

  public static void PlayerSize(Packet _packet)
  {
    int key = _packet.ReadInt();
    Vector2 vector2 = _packet.ReadVector2();
    if (!GameManager.players.ContainsKey(key))
      return;
    ((Component) GameManager.players[key]).transform.localScale = Vector2.op_Implicit(vector2);
  }

  public static void DisconnectUser(Packet _packet)
  {
    int key = _packet.ReadInt();
    GameObject wrenchIcon = ((Component) GameManager.players[key]).GetComponent<PlayerManager>().wrenchIcon;
    GameManager.instance.wrenchIcons.Remove(wrenchIcon);
    Object.Destroy((Object) wrenchIcon);
    Object.Destroy((Object) ((Component) GameManager.players[key]).GetComponent<DropShadow>().shadowGameobject);
    GameManager.instance.dropShadows.Remove(((Component) GameManager.players[key]).GetComponent<DropShadow>());
    Object.Destroy((Object) ((Component) GameManager.players[key]).GetComponent<DropShadow>());
    if (GameManager.instance.pets.ContainsKey(GameManager.players[key]))
    {
      Object.Destroy((Object) ((Component) GameManager.instance.pets[GameManager.players[key]].GetComponent<PetBehaivour>().title).gameObject);
      Object.Destroy((Object) GameManager.instance.pets[GameManager.players[key]].GetComponent<DropShadow>().shadowGameobject);
      Object.Destroy((Object) GameManager.instance.pets[GameManager.players[key]]);
      GameManager.instance.pets.Remove(GameManager.players[key]);
    }
    foreach (Component component in ((Component) ((Component) GameManager.players[key]).transform.GetChild(2)).transform)
    {
      GameObject gameObject = component.gameObject;
      Object.Destroy((Object) gameObject.GetComponent<DropShadow>().shadowGameobject);
      GameManager.instance.dropShadows.Remove(gameObject.GetComponent<DropShadow>());
      Object.Destroy((Object) gameObject.GetComponent<DropShadow>());
    }
    foreach (ChatBubble chatBubble in UIController.instance.chatBubbles)
    {
      if (chatBubble.playerID == key)
      {
        Object.Destroy((Object) chatBubble.gameObject);
        UIController.instance.chatBubbles.Remove(chatBubble);
        break;
      }
    }
    PlayerManager player = GameManager.players[key];
    player.currentThread.Abort();
    Object.Destroy((Object) ((Component) player.nicknameObject).gameObject);
    GameManager.players.Remove(key);
    Object.Destroy((Object) ((Component) player).gameObject);
    ((TMP_Text) ((Component) UIController.instance.claimButton.transform.parent.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.players.Count.ToString() + " <sprite=0>";
  }

  public static void GetItemData(Packet _packet)
  {
    int num = _packet.ReadInt();
    for (int index = 0; index < num; ++index)
    {
      int _length = _packet.ReadInt();
      Item obj = JsonUtility.FromJson<Item>(Encoding.UTF8.GetString(_packet.ReadBytes(_length)));
      obj.texture = GameManager.instance.blockSprites[obj.textureID];
      GameManager.instance.items.Add(obj);
    }
  }

  public static void GetWorldData(Packet _packet)
  {
    UIController.instance.SetActive(UIController.instance.worldMenu, false);
    UIController.instance.SetActive(UIController.instance.loadingScreen, true);
    ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
    ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Got World Data 1/6)";
    WorldManager.instance.worldDataFilled = true;
    string str1 = _packet.ReadString();
    WorldManager.instance.worldName = str1;
    DiscordRichPresence.instance.UpdateDiscordRichPresence();
    ((TMP_Text) ((Component) UIController.instance.claimButton.transform.parent.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = str1;
    int _worldWidth = _packet.ReadInt();
    int _worldHeight = _packet.ReadInt();
    UIController.instance.worldBorders.SetPath(0, new Vector2[4]
    {
      new Vector2(-0.5f, -0.5f),
      new Vector2((float) _worldWidth - 0.5f, -0.5f),
      new Vector2((float) _worldWidth - 0.5f, (float) _worldHeight - 0.5f),
      new Vector2(-0.5f, (float) _worldHeight - 0.5f)
    });
    EdgeCollider2D worldColliders = UIController.instance.worldColliders;
    List<Vector2> vector2List = new List<Vector2>();
    vector2List.Add(new Vector2(-0.5f, -0.5f));
    vector2List.Add(new Vector2((float) _worldWidth - 0.5f, -0.5f));
    vector2List.Add(new Vector2((float) _worldWidth - 0.5f, (float) _worldHeight - 0.5f));
    vector2List.Add(new Vector2(-0.5f, (float) _worldHeight - 0.5f));
    vector2List.Add(new Vector2(-0.5f, -0.5f));
    worldColliders.SetPoints(vector2List);
    UIController.instance.confiner.InvalidateCache();
    int _length1 = _packet.ReadInt();
    int _length2 = _packet.ReadInt();
    string _foregroundWorldData = Compress.DecompressString(Encoding.UTF8.GetString(_packet.ReadBytes(_length1)));
    string _backgroundWorldData = Compress.DecompressString(Encoding.UTF8.GetString(_packet.ReadBytes(_length2)));
    string str2 = _packet.ReadString();
    ((Selectable) UIController.instance.enterWorldButton).interactable = false;
    UIController.instance.SetActive(UIController.instance.MenuScreen, false);
    Color color = new Color();
    ref Color local = ref color;
    ColorUtility.TryParseHtmlString(str2, ref local);
    Camera.main.backgroundColor = color;
    ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
    ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Loaded Background & UI OBjects' Data 2/6)";
    int num = _packet.ReadInt();
    WorldManager.instance.worldValue = num;
    if (num == 0)
    {
      UIController.instance.claimButton.SetActive(false);
    }
    else
    {
      ((TMP_Text) ((Component) UIController.instance.claimButton.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "Claim for " + num.ToString() + " <sprite=0>";
      UIController.instance.claimButton.SetActive(true);
    }
    ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
    ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Loaded Signs Data 3/6)";
    WorldManager.instance.SetWorldData(_foregroundWorldData, _backgroundWorldData, _worldWidth, _worldHeight);
    ((Component) GameManager.instance).GetComponent<PutGridManager>().CreateGrid(7);
  }

  public static void EditWorldData(Packet _packet)
  {
    int _newID = _packet.ReadInt();
    int _blockHealth = _packet.ReadInt();
    Vector2 _blockPosition = _packet.ReadVector2();
    int _layer = _packet.ReadInt();
    int _clientID = _packet.ReadInt();
    bool _animation = _packet.ReadBool();
    WorldManager.instance.EditBlockData(_newID, (float) _blockHealth, _layer, _blockPosition, false, _clientID, _animation);
  }

  public static void GetInventoryData(Packet _packet)
  {
    string str = Compress.DecompressString(_packet.ReadString());
    Compress.DecompressString(_packet.ReadString());
    Inventory _uiInventory = JsonUtility.FromJson<Inventory>(str);
    Inventory _inventory = JsonUtility.FromJson<Inventory>(str);
    InventoryManager.instance.SetMainInventory(_inventory, _uiInventory);
    InventoryManager.instance.SetInventory();
  }

  public static void EditInventoryData(Packet _packet)
  {
    int _itemID = _packet.ReadInt();
    int _quantity = _packet.ReadInt();
    if (_packet.ReadBool())
    {
      int _itemQuantity = _packet.ReadInt();
      UIController.instance.CreateNewItemDialog(_itemID, _itemQuantity);
    }
    InventoryManager.instance.uiInventory.SetItem(_itemID, _quantity);
    InventoryManager.instance.UpdateInventorySlot(_itemID);
  }

  public static void CreateDroppedItem(Packet _packet)
  {
    int _id = _packet.ReadInt();
    int num1 = _packet.ReadInt();
    int num2 = _packet.ReadInt();
    Vector2 _position = _packet.ReadVector2();
    WorldManager.instance.CreateDroppedItem(_id, new GameItem()
    {
      id = num1,
      quantity = num2
    }, _position);
  }

  public static void RemoveDroppedItem(Packet _packet)
  {
    int _id = _packet.ReadInt();
    WorldManager.instance.RemoveDroppedItem(_id);
  }

  public static void GetChatMessage(Packet _packet)
  {
    string str = _packet.ReadString();
    TMP_Text chat = UIController.instance.chat;
    chat.text = chat.text + "\n" + str;
  }

  public static void GetChatBubble(Packet _packet)
  {
    int _clientID = _packet.ReadInt();
    string _text = _packet.ReadString();
    float _seconds = _packet.ReadFloat();
    UIController.instance.CreateBubbleVoid(_clientID, _text, _seconds);
  }

  public static void Reply(Packet _packet)
  {
    switch (_packet.ReadInt())
    {
      case 1:
        WorldManager.instance.visibleBlocks.Clear();
        UIController.instance.SetActive(UIController.instance.inGameMenu, false);
        WorldManager.instance.rainbowBlocks.Clear();
        foreach (Object @object in UIController.instance.itemAlerts.Values)
          Object.Destroy(@object);
        UIController.instance.itemAlerts.Clear();
        WorldManager.instance.entrances.Clear();
        WorldManager.instance.doors.Clear();
        GameManager.instance.effectTypes.Clear();
        WorldManager.instance.animatedBlocks.Clear();
        if (GameManager.players.ContainsKey(Client.instance.myId))
        {
          ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().isNoclip = false;
          if (Object.op_Inequality((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar, (Object) null))
          {
            Object.Destroy((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar);
            ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar = (GameObject) null;
          }
        }
        foreach (GameObject gameObject in GameManager.instance.pets.Values)
        {
          Object.Destroy((Object) ((Component) gameObject.GetComponent<PetBehaivour>().title).gameObject);
          Object.Destroy((Object) gameObject.GetComponent<DropShadow>().shadowGameobject);
          Object.Destroy((Object) gameObject);
        }
        GameManager.instance.pets.Clear();
        foreach (PlayerManager playerManager in GameManager.players.Values)
        {
          GameObject wrenchIcon = playerManager.wrenchIcon;
          GameManager.instance.wrenchIcons.Remove(wrenchIcon);
          Object.Destroy((Object) wrenchIcon);
          if (Object.op_Inequality((Object) ((Component) playerManager).gameObject, (Object) null))
          {
            playerManager.currentThread.Abort();
            Object.Destroy((Object) ((Component) playerManager).gameObject);
            Object.Destroy((Object) ((Component) playerManager.nicknameObject).gameObject);
          }
        }
        foreach (DropShadow dropShadow in GameManager.instance.dropShadows)
          Object.Destroy((Object) dropShadow.shadowGameobject);
        GameManager.instance.dropShadows.Clear();
        GameManager.players.Clear();
        if (WorldManager.instance.worldLayers[0] != null)
        {
          foreach (WorldManager.Block[,] worldLayer in WorldManager.instance.worldLayers)
          {
            for (int index1 = 0; index1 < worldLayer.Length; ++index1)
            {
              int index2 = index1 % worldLayer.GetLength(0);
              int index3 = Mathf.FloorToInt((float) (index1 / worldLayer.GetLength(0)));
              Object.Destroy((Object) worldLayer[index2, index3].gameObject);
              worldLayer[index2, index3] = (WorldManager.Block) null;
            }
          }
        }
        foreach (DroppedItem droppedItem in WorldManager.instance.droppedItems.Values)
          Object.Destroy((Object) droppedItem.gameObject);
        WorldManager.instance.droppedItems.Clear();
        foreach (WorldManager.BreakingAnimationFrame breakingAnimation in WorldManager.instance.breakingAnimations)
          Object.Destroy((Object) breakingAnimation.gameObject);
        foreach (ChatBubble chatBubble in UIController.instance.chatBubbles)
          Object.Destroy((Object) chatBubble.gameObject);
        foreach (PutGridManager.Grid grid in ((Component) GameManager.instance).GetComponent<PutGridManager>().grids)
          Object.Destroy((Object) grid.grid);
        ((Component) GameManager.instance).GetComponent<PutGridManager>().grids.Clear();
        if (GameManager.players.ContainsKey(Client.instance.myId) && Object.op_Inequality((Object) MovementManager.instance.currentSignBar, (Object) null))
        {
          MovementManager.instance.isReadingSign = false;
          Object.Destroy((Object) MovementManager.instance.currentSignBar);
          MovementManager.instance.currentSignBar = (GameObject) null;
        }
        UIController.instance.chatBubbles.Clear();
        WorldManager.instance.worldName = (string) null;
        DiscordRichPresence.instance.UpdateDiscordRichPresence();
        WorldManager.instance.worldLayers = new WorldManager.Block[2][,];
        WorldManager.instance.worldDataFilled = false;
        WorldManager.instance.breakingAnimations.Clear();
        WorldManager.instance.signs.Clear();
        ((Selectable) UIController.instance.enterButton).interactable = true;
        UIController.instance.SetActive(UIController.instance.MenuScreen, true);
        UIController.instance.SetActive(UIController.instance.worldMenu, true);
        UIController.instance.chat.text = "";
        break;
      case 2:
        UIController.instance.claimButton.SetActive(false);
        break;
      case 3:
        UIController.instance.SetActive(UIController.instance.loadingScreen, false);
        break;
      case 4:
        UIController.instance.SetActive(UIController.instance.inventory, false);
        UIController.instance.SetActive(UIController.instance.fishingModeDialog, true);
        GameManager.instance.isMovementFreeze = true;
        break;
      case 5:
        UIController.instance.SetActive(UIController.instance.inventory, true);
        UIController.instance.SetActive(UIController.instance.fishingModeDialog, false);
        GameManager.instance.isMovementFreeze = false;
        break;
      case 6:
        UIController.instance.SetActive(UIController.instance.inventory, true);
        UIController.instance.SetActive(UIController.instance.fishingModeDialog, false);
        UIController.instance.SetActive(UIController.instance.fishingMenu, true);
        if (GameManager.players[Client.instance.myId].animationSystem.currentID != 4)
        {
          GameManager.players[Client.instance.myId].animationSystem.currentID = 4;
          ClientSend.ChangeAnimation(4);
        }
        FishingManager.instance.slider.value = 0.5f;
        FishingManager.instance.sliderValue = 0.5f;
        FishingManager instance = FishingManager.instance;
        Rect rect1 = ((RectTransform) FishingManager.instance.container.transform).rect;
        double num1 = -((double) ((Rect) ref rect1).width / 2.0);
        Rect rect2 = ((RectTransform) FishingManager.instance.fish.transform).rect;
        double num2 = (double) ((Rect) ref rect2).width / 2.0;
        Vector2 vector2 = new Vector2((float) (num1 + num2), 0.0f);
        instance.fishPosition = vector2;
        Transform transform = FishingManager.instance.fish.transform;
        rect2 = ((RectTransform) FishingManager.instance.container.transform).rect;
        double num3 = -((double) ((Rect) ref rect2).width / 2.0);
        rect2 = ((RectTransform) FishingManager.instance.fish.transform).rect;
        double num4 = (double) ((Rect) ref rect2).width / 2.0;
        Vector3 vector3 = Vector2.op_Implicit(new Vector2((float) (num3 + num4), 0.0f));
        transform.localPosition = vector3;
        FishingManager.instance.isCatching = true;
        break;
      case 7:
        UIController.instance.SetActive(UIController.instance.fishingMenu, false);
        GameManager.instance.isMovementFreeze = false;
        break;
      case 8:
        TutorialHandler.StartTutorial();
        break;
      case 9:
        UIController.instance.SetActive(UIController.instance.skinToneMenu, true);
        break;
    }
  }

  public static void GetPackData(Packet _packet)
  {
    foreach (string str in _packet.ReadString().Split('|', StringSplitOptions.None))
    {
      Pack _pack = JsonUtility.FromJson<Pack>(str);
      UIController.instance.CreatePack(_pack);
    }
  }

  public static void SetPosition(Packet _packet)
  {
    Vector2 vector2 = _packet.ReadVector2();
    ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position = Vector2.op_Implicit(vector2);
  }

  public static void CreateTrade(Packet _packet)
  {
    _packet.ReadInt();
    TradeManager.instance.isTrading = true;
    UIController.instance.SetActive(UIController.instance.tradeScreen);
  }

  public static void RemoveTrade(Packet _packet)
  {
    _packet.ReadInt();
    TradeManager.instance.isTrading = false;
    UIController.instance.SetActive(UIController.instance.tradeScreen, false);
    for (int index = 0; index < TradeManager.instance.selfTradeSlots.Length; ++index)
    {
      TradeManager.instance.selfTradeSlots[index].item = (GameItem) null;
      ((Component) TradeManager.instance.selfTradeSlots[index].gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = UIController.instance.UIMask;
      ((TMP_Text) ((Component) TradeManager.instance.selfTradeSlots[index].gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "";
      TradeManager.instance.peerTradeSlots[index].item = (GameItem) null;
      ((Component) TradeManager.instance.peerTradeSlots[index].gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = UIController.instance.UIMask;
      ((TMP_Text) ((Component) TradeManager.instance.peerTradeSlots[index].gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "";
    }
  }

  public static void AddItemToTrade(Packet _packet)
  {
    int _itemID = _packet.ReadInt();
    int _quantity = _packet.ReadInt();
    int _row = _packet.ReadInt();
    int _clientID = _packet.ReadInt();
    TradeManager.instance.AddItem(_itemID, _quantity, _row, _clientID);
  }

  public static void GetNotification(Packet _packet)
  {
    string _title = _packet.ReadString();
    string _text = _packet.ReadString();
    float _time = _packet.ReadFloat();
    UIController.instance.CreateNotificationFunction(_title, _text, _time);
  }

  public static void JoinWorld(Packet _packet)
  {
    string str = _packet.ReadString();
    WorldManager.instance.worldName = str;
    DiscordRichPresence.instance.UpdateDiscordRichPresence();
    ClientSend.SendIntoGame();
  }

  public static void SignData(Packet _packet)
  {
    Vector2 key = _packet.ReadVector2();
    string str = _packet.ReadString();
    switch (_packet.ReadInt())
    {
      case 0:
        WorldManager.instance.signs.Add(key, str);
        break;
      case 1:
        WorldManager.instance.signs.Remove(key);
        break;
      case 2:
        if (Object.op_Inequality((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar, (Object) null))
        {
          if (((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().signBars.Contains(((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar))
            ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().signBars.Remove(((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar);
          Object.Destroy((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar);
          ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar = (GameObject) null;
        }
        WorldManager.instance.signs[key] = str;
        ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar = Object.Instantiate<GameObject>(UIController.instance.signBar, UIController.instance.signBubbleCanvas.transform);
        ((TMP_Text) ((Component) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = WorldManager.instance.signs[key];
        ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar.transform.position = Vector2.op_Implicit(new Vector2((float) Mathf.FloorToInt(key.x + Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.x) / 2f), (float) Mathf.FloorToInt(key.y + ((Component) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform).transform.localScale.y / 2f) + 0.75f));
        ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().signBars.Add(((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar);
        break;
      case 3:
        WorldManager.instance.signs.Add(key, str);
        break;
    }
  }

  public static void EditCostumeData(Packet _packet)
  {
    int key = _packet.ReadInt();
    int num1 = _packet.ReadInt();
    int index1 = _packet.ReadInt();
    GameManager.instance.CreateEffect(1, Vector2.op_Implicit(((Component) GameManager.players[key]).gameObject.transform.position));
    if (GameManager.instance.items[GameManager.players[key].clothes[index1]].costumesToHide != null)
    {
      foreach (int num2 in GameManager.instance.items[GameManager.players[key].clothes[index1]].costumesToHide)
        ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(num2)).gameObject.SetActive(true);
    }
    if (num1 != 0 && key == Client.instance.myId)
    {
      if (InventoryManager.instance.FindSlotFromItemID(num1) != null)
        InventoryManager.instance.FindSlotFromItemID(num1).isUsed = true;
      if (InventoryManager.instance.FindSlotFromItemID(GameManager.players[key].clothes[index1]) != null && InventoryManager.instance.FindSlotFromItemID(GameManager.players[key].clothes[index1]).item.id != 0)
        InventoryManager.instance.FindSlotFromItemID(GameManager.players[key].clothes[index1]).isUsed = false;
    }
    else if (InventoryManager.instance.FindSlotFromItemID(GameManager.players[key].clothes[index1]) != null)
      InventoryManager.instance.FindSlotFromItemID(GameManager.players[key].clothes[index1]).isUsed = false;
    if (GameManager.instance.items[GameManager.players[key].clothes[index1]].typeID == 11 && num1 == 0 && GameManager.instance.pets.ContainsKey(GameManager.players[key]))
    {
      GameManager.instance.CreateEffect(1, Vector2.op_Implicit(GameManager.instance.pets[GameManager.players[key]].transform.position));
      Object.Destroy((Object) ((Component) GameManager.instance.pets[GameManager.players[key]].GetComponent<PetBehaivour>().title).gameObject);
      Object.Destroy((Object) GameManager.instance.pets[GameManager.players[key]].GetComponent<DropShadow>().shadowGameobject);
      Object.Destroy((Object) GameManager.instance.pets[GameManager.players[key]]);
      GameManager.instance.pets.Remove(GameManager.players[key]);
    }
    if (key == Client.instance.myId)
      GameManager.players[key].clothes[index1] = num1;
    if (GameManager.instance.items[num1].costumesToHide != null)
    {
      foreach (int num3 in GameManager.instance.items[num1].costumesToHide)
        ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(num3)).gameObject.SetActive(false);
    }
    if (GameManager.instance.items[num1].skin != 0)
      return;
    if (GameManager.instance.items[num1].typeID == 7)
    {
      ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[num1].texture;
      for (int index2 = 0; index2 < 5; ++index2)
      {
        for (int index3 = 0; index3 < ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index2].sprites.Count; ++index3)
        {
          Sprite sprite = Resources.Load("Images/Textures/Clothe Animations/" + num1.ToString() + " " + index2.ToString() + " " + (index3 + 1).ToString(), typeof (Sprite)) as Sprite;
          if (Object.op_Inequality((Object) sprite, (Object) null))
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index2].sprites[index3] = sprite;
          else
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index2].sprites[index3] = GameManager.instance.items[num1].texture;
        }
      }
    }
    else if (GameManager.instance.items[num1].typeID == 11)
    {
      if (!GameManager.instance.pets.ContainsKey(GameManager.players[key]))
      {
        GameObject gameObject = Object.Instantiate<GameObject>(GameManager.instance.pet, GameManager.instance.petsContainer.transform);
        gameObject.transform.position = ((Component) GameManager.players[key]).transform.position;
        TextMeshProUGUI textMeshProUgui = Object.Instantiate<TextMeshProUGUI>(GameManager.instance.nicknamePrefab, UIController.instance.nicknameCanvas.transform);
        ((TMP_Text) textMeshProUgui).text = "<size=75%>" + GameManager.instance.items[num1].name.Split(' ', StringSplitOptions.None)[1];
        gameObject.GetComponent<PetBehaivour>().title = textMeshProUgui;
        gameObject.GetComponent<PetBehaivour>().player = ((Component) GameManager.players[key]).gameObject;
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[num1].texture;
        GameManager.instance.pets.Add(GameManager.players[key], gameObject);
        for (int index4 = 0; index4 < gameObject.GetComponent<AnimationSystem>().animations[0].sprites.Count; ++index4)
        {
          string[] strArray = new string[6]
          {
            "Images/Textures/Clothe Animations/",
            num1.ToString(),
            " ",
            null,
            null,
            null
          };
          int num4 = 0;
          strArray[3] = num4.ToString();
          strArray[4] = " ";
          num4 = index4 + 1;
          strArray[5] = num4.ToString();
          Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
          if (Object.op_Inequality((Object) sprite, (Object) null))
            gameObject.GetComponent<AnimationSystem>().animations[0].sprites[index4] = sprite;
          else
            gameObject.GetComponent<AnimationSystem>().animations[0].sprites[index4] = GameManager.instance.items[num1].texture;
        }
      }
      else
      {
        GameManager.instance.CreateEffect(1, Vector2.op_Implicit(GameManager.instance.pets[GameManager.players[key]].transform.position));
        Object.Destroy((Object) ((Component) GameManager.instance.pets[GameManager.players[key]].GetComponent<PetBehaivour>().title).gameObject);
        Object.Destroy((Object) GameManager.instance.pets[GameManager.players[key]].GetComponent<DropShadow>().shadowGameobject);
        Object.Destroy((Object) GameManager.instance.pets[GameManager.players[key]]);
        GameManager.instance.pets.Remove(GameManager.players[key]);
        GameObject gameObject = Object.Instantiate<GameObject>(GameManager.instance.pet, GameManager.instance.petsContainer.transform);
        gameObject.transform.position = ((Component) GameManager.players[key]).transform.position;
        TextMeshProUGUI textMeshProUgui = Object.Instantiate<TextMeshProUGUI>(GameManager.instance.nicknamePrefab, UIController.instance.nicknameCanvas.transform);
        ((TMP_Text) textMeshProUgui).text = "<size=75%>" + GameManager.instance.items[num1].name.Split(' ', StringSplitOptions.None)[1];
        gameObject.GetComponent<PetBehaivour>().title = textMeshProUgui;
        gameObject.GetComponent<PetBehaivour>().player = ((Component) GameManager.players[key]).gameObject;
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[num1].texture;
        GameManager.instance.pets.Add(GameManager.players[key], gameObject);
        for (int index5 = 0; index5 < gameObject.GetComponent<AnimationSystem>().animations[0].sprites.Count; ++index5)
        {
          string[] strArray = new string[6]
          {
            "Images/Textures/Clothe Animations/",
            num1.ToString(),
            " ",
            null,
            null,
            null
          };
          int num5 = 0;
          strArray[3] = num5.ToString();
          strArray[4] = " ";
          num5 = index5 + 1;
          strArray[5] = num5.ToString();
          Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
          if (Object.op_Inequality((Object) sprite, (Object) null))
            gameObject.GetComponent<AnimationSystem>().animations[0].sprites[index5] = sprite;
          else
            gameObject.GetComponent<AnimationSystem>().animations[0].sprites[index5] = GameManager.instance.items[num1].texture;
        }
      }
    }
    else if (GameManager.instance.items[GameManager.players[key].clothes[index1]].typeID == 7 && num1 == 0)
    {
      ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[num1].texture;
      for (int index6 = 0; index6 < 5; ++index6)
      {
        for (int index7 = 0; index7 < ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index6].sprites.Count; ++index7)
        {
          Sprite sprite = Resources.Load("Images/Textures/Clothe Animations/" + num1.ToString() + " " + index6.ToString() + " " + (index7 + 1).ToString(), typeof (Sprite)) as Sprite;
          if (Object.op_Inequality((Object) sprite, (Object) null))
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index6].sprites[index7] = sprite;
          else
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index6].sprites[index7] = GameManager.instance.items[num1].texture;
        }
      }
    }
    else if (index1 >= 5)
    {
      ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1 + 1)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[num1].texture;
      for (int index8 = 0; index8 < 5; ++index8)
      {
        for (int index9 = 0; index9 < ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1 + 1)).GetComponent<AnimationSystem>().animations[index8].sprites.Count; ++index9)
        {
          Sprite sprite = Resources.Load("Images/Textures/Clothe Animations/" + num1.ToString() + " " + index8.ToString() + " " + (index9 + 1).ToString(), typeof (Sprite)) as Sprite;
          if (Object.op_Inequality((Object) sprite, (Object) null))
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1 + 1)).GetComponent<AnimationSystem>().animations[index8].sprites[index9] = sprite;
          else
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1 + 1)).GetComponent<AnimationSystem>().animations[index8].sprites[index9] = GameManager.instance.items[num1].texture;
        }
      }
    }
    else
    {
      ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[num1].texture;
      for (int index10 = 0; index10 < 5; ++index10)
      {
        for (int index11 = 0; index11 < ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index10].sprites.Count; ++index11)
        {
          Sprite sprite = Resources.Load("Images/Textures/Clothe Animations/" + num1.ToString() + " " + index10.ToString() + " " + (index11 + 1).ToString(), typeof (Sprite)) as Sprite;
          if (Object.op_Inequality((Object) sprite, (Object) null))
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index10].sprites[index11] = sprite;
          else
            ((Component) ((Component) GameManager.players[key]).gameObject.transform.GetChild(2).GetChild(index1)).GetComponent<AnimationSystem>().animations[index10].sprites[index11] = GameManager.instance.items[num1].texture;
        }
      }
    }
  }

  public static void ChangeAnimation(Packet _packet)
  {
    int key = _packet.ReadInt();
    int _animationID = _packet.ReadInt();
    if (!GameManager.players.ContainsKey(key))
      return;
    if (_animationID == 1 && (double) ((Component) GameManager.players[key]).gameObject.transform.localScale.x == 1.0)
      GameManager.instance.CreateEffect(2, new Vector2(((Component) GameManager.players[key]).gameObject.transform.position.x - ((Component) GameManager.players[key]).gameObject.transform.localScale.x / 3f, ((Component) GameManager.players[key]).gameObject.transform.position.y));
    else if (_animationID == 1)
      GameManager.instance.CreateEffect(3, new Vector2(((Component) GameManager.players[key]).gameObject.transform.position.x - ((Component) GameManager.players[key]).gameObject.transform.localScale.x / 3f, ((Component) GameManager.players[key]).gameObject.transform.position.y));
    GameManager.players[key].ChangeAnimation(_animationID);
  }

  public static void SetHealth(Packet _packet)
  {
    int key = _packet.ReadInt();
    int num = _packet.ReadInt();
    if (!GameManager.players.ContainsKey(key))
      return;
    if (num < GameManager.players[key].health)
      GameManager.instance.CreateEffect(6, new Vector2(((Component) GameManager.players[key]).gameObject.transform.position.x, ((Component) GameManager.players[key]).gameObject.transform.position.y));
    GameManager.players[key].health = num;
  }

  public static void SetStatus(Packet _packet)
  {
    int key = _packet.ReadInt();
    int num = _packet.ReadInt();
    bool flag = _packet.ReadBool();
    if (num != 0 || key != Client.instance.myId)
      return;
    ((Component) GameManager.players[key]).GetComponent<MovementManager>().isNoclip = flag;
    if (flag)
      ((Component) GameManager.players[key]).GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D) 2;
    else
      ((Component) GameManager.players[key]).GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D) 0;
  }

  public static void GetActiveWorlds(Packet _packet)
  {
    string _popularWorld = _packet.ReadString();
    string _activeWorlds = _packet.ReadString();
    ThreadManager.ExecuteOnMainThread((Action) (() =>
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ClientHandle.\u003C\u003Ec__DisplayClass28_1 cDisplayClass281 = new ClientHandle.\u003C\u003Ec__DisplayClass28_1();
      ((TMP_Text) ((Component) UIController.instance.popularWorld.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _popularWorld;
      string[] strArray = _activeWorlds.Split('|', StringSplitOptions.None);
      if (strArray.Length == 1 && strArray[0].Split(":", StringSplitOptions.None)[0] == "" || strArray[0].Split(":", StringSplitOptions.None)[0] == null)
        strArray = new string[0];
      for (int index = 0; index < strArray.Length; ++index)
        ((TMP_Text) ((Component) UIController.instance.activeWorlds[index].transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = strArray[index].Split(':', StringSplitOptions.None)[0] + " (" + strArray[index].Split(':', StringSplitOptions.None)[1] + " <sprite=0>)";
      // ISSUE: reference to a compiler-generated field
      cDisplayClass281._popularWorldButton = UIController.instance.popularWorld;
      // ISSUE: reference to a compiler-generated field
      ((UnityEventBase) cDisplayClass281._popularWorldButton.GetComponent<Button>().onClick).RemoveAllListeners();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) cDisplayClass281._popularWorldButton.GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass281, __methodptr(\u003CGetActiveWorlds\u003Eb__1)));
      foreach (GameObject activeWorld in UIController.instance.activeWorlds)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ClientHandle.\u003C\u003Ec__DisplayClass28_2 cDisplayClass282 = new ClientHandle.\u003C\u003Ec__DisplayClass28_2();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass282._worldButton = activeWorld;
        // ISSUE: reference to a compiler-generated field
        if (((TMP_Text) ((Component) cDisplayClass282._worldButton.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text == "null")
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass282._worldButton.SetActive(false);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          ((UnityEventBase) cDisplayClass282._worldButton.GetComponent<Button>().onClick).RemoveAllListeners();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent) cDisplayClass282._worldButton.GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass282, __methodptr(\u003CGetActiveWorlds\u003Eb__2)));
        }
      }
    }));
  }

  public static void GetNews(Packet _packet)
  {
    foreach (string str in _packet.ReadString().Split('|', StringSplitOptions.None))
      UIController.instance.CreateNews(JsonUtility.FromJson<News>(str).title, JsonUtility.FromJson<News>(str).imageURL, JsonUtility.FromJson<News>(str).details);
  }

  public static void GetCubixCount(Packet _packet)
  {
    long num = _packet.ReadLong();
    if (num != GameManager.instance.cubix && GameManager.instance.cubix != 0L)
      UIController.instance.CreateCubixEffect(num - GameManager.instance.cubix);
    GameManager.instance.cubix = num;
    ((TMP_Text) ((Component) UIController.instance.shop.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "<sprite index=0> " + num.ToString();
    ((TMP_Text) UIController.instance.cubixsCount.GetComponent<TextMeshProUGUI>()).text = "<sprite index=0> " + num.ToString();
    ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = num.ToString() + "Cubix <sprite=0>";
  }

  public static void GetAccountData(Packet _packet)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ClientHandle.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new ClientHandle.\u003C\u003Ec__DisplayClass31_0();
    long num1 = _packet.ReadLong();
    int num2 = _packet.ReadInt();
    int index = _packet.ReadInt();
    _packet.ReadString();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310._ownedWorldsData = _packet.ReadString();
    string[] accountTypes = GameManager.instance.accountTypes;
    ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = accountTypes[index] + " <sprite=" + index.ToString() + ">";
    ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = num1.ToString() + " <sprite=0>";
    ((TMP_Text) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "Level " + num2.ToString() + " <sprite=1>";
    ((UnityEventBase) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(5)).GetComponent<Button>().onClick).RemoveAllListeners();
    // ISSUE: method pointer
    ((UnityEvent) ((Component) UIController.instance.scenes[0].transform.GetChild(0).GetChild(0).GetChild(5)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass310, __methodptr(\u003CGetAccountData\u003Eb__0)));
  }

  public static void UpdateClientData(Packet _packet)
  {
    int key = _packet.ReadInt();
    string str = _packet.ReadString();
    int num1 = _packet.ReadInt();
    int num2 = _packet.ReadInt();
    int num3 = _packet.ReadInt();
    int num4 = _packet.ReadInt();
    int _skinID = _packet.ReadInt();
    GameManager.players[key].SetSkin(_skinID);
    GameManager.players[key].bio = str;
    GameManager.players[key].accountLevel = num1;
    GameManager.players[key].level = num2;
    GameManager.players[key].currentXP = num3;
    GameManager.players[key].maxXP = num4;
    GameManager.players[key].skinID = _skinID;
  }

  public static void EntranceData(Packet _packet)
  {
    Vector2 key = _packet.ReadVector2();
    bool flag1 = _packet.ReadBool();
    bool flag2 = _packet.ReadBool();
    switch (_packet.ReadInt())
    {
      case 0:
        WorldManager.instance.entrances.Add(key, flag1);
        ((Behaviour) WorldManager.instance.worldLayers[0][(int) key.x, (int) key.y].gameObject.GetComponent<EdgeCollider2D>()).enabled = flag2;
        break;
      case 1:
        WorldManager.instance.entrances.Remove(key);
        break;
      case 2:
        WorldManager.instance.entrances[key] = flag1;
        ((Behaviour) WorldManager.instance.worldLayers[0][(int) key.x, (int) key.y].gameObject.GetComponent<EdgeCollider2D>()).enabled = flag2;
        break;
    }
  }

  public static void DoorData(Packet _packet)
  {
    Vector2 key = _packet.ReadVector2();
    string str = _packet.ReadString();
    switch (_packet.ReadInt())
    {
      case 0:
        WorldManager.instance.doors.Add(key, str);
        break;
      case 1:
        WorldManager.instance.doors.Remove(key);
        break;
      case 2:
        WorldManager.instance.doors[key] = str;
        break;
      case 3:
        WorldManager.instance.doors.Add(key, str);
        break;
    }
  }

  public static void CreateDialog(Packet _packet)
  {
    string _title = _packet.ReadString();
    string _text = _packet.ReadString();
    string str = _packet.ReadString();
    int num = _packet.ReadInt();
    List<int> intList = new List<int>();
    foreach (char ch in num.ToString())
      intList.Add(Convert.ToInt32(ch));
    UIController.instance.CreateDialog(_title, _text, str.Split(':', StringSplitOptions.None), intList.ToArray(), new Action[intList.Count]);
  }

  public static void GetFriendsData(Packet _packet)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ClientHandle.\u003C\u003Ec__DisplayClass36_0 cDisplayClass360 = new ClientHandle.\u003C\u003Ec__DisplayClass36_0();
    string str1 = _packet.ReadString();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass360._onlineCount = 0;
    foreach (Object @object in GameManager.instance.friends.Values)
      Object.Destroy(@object);
    GameManager.instance.friends.Clear();
    if (!string.IsNullOrWhiteSpace(str1))
    {
      foreach (string str2 in str1.Split('|', StringSplitOptions.None))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ClientHandle.\u003C\u003Ec__DisplayClass36_1 cDisplayClass361 = new ClientHandle.\u003C\u003Ec__DisplayClass36_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass361.CS\u0024\u003C\u003E8__locals1 = cDisplayClass360;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass361._friend = str2;
        GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.friendSlot, UIController.instance.friendsContainer.transform);
        // ISSUE: reference to a compiler-generated field
        ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass361._friend.Split(':', StringSplitOptions.None)[0];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass361._name = cDisplayClass361._friend.Split(':', StringSplitOptions.None)[0];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass361._bio = cDisplayClass361._friend.Split(':', StringSplitOptions.None)[1];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass361._level = Convert.ToInt32(cDisplayClass361._friend.Split(':', StringSplitOptions.None)[2]);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass361._badge = Convert.ToInt32(cDisplayClass361._friend.Split(':', StringSplitOptions.None)[3]);
        // ISSUE: method pointer
        ((UnityEvent) ((Component) gameObject.transform.GetChild(2).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass361, __methodptr(\u003CGetFriendsData\u003Eb__0)));
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass361._friend.Split(':', StringSplitOptions.None).Length == 5)
        {
          ((Component) gameObject.transform.GetChild(0)).GetComponent<Image>().sprite = UIController.instance.activeStatus;
          ((Component) gameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(true);
          ((UnityEventBase) ((Component) gameObject.transform.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
          // ISSUE: method pointer
          ((UnityEvent) ((Component) gameObject.transform.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass361, __methodptr(\u003CGetFriendsData\u003Eb__1)));
        }
        else
        {
          ((Component) gameObject.transform.GetChild(0)).GetComponent<Image>().sprite = UIController.instance.offlineStatus;
          ((Component) gameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(false);
        }
        // ISSUE: reference to a compiler-generated field
        GameManager.instance.friends.Add(cDisplayClass361._friend, gameObject);
      }
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) UIController.instance.friendCountText).text = cDisplayClass360._onlineCount.ToString() + "/" + GameManager.instance.friends.Count.ToString() + " Online Players";
    }
    UIController.instance.friendsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(UIController.instance.friendsContainer.GetComponent<RectTransform>().sizeDelta.x, Math.Clamp(89.5942f * (float) GameManager.instance.friends.Count, 460.7078f, float.MaxValue));
  }

  public static void GetCraftData(Packet _packet)
  {
    int index = _packet.ReadInt();
    int num1 = _packet.ReadInt();
    int num2 = _packet.ReadInt();
    UIController.instance.craftItemID = index;
    UIController.instance.craftID = num2;
    GameObject gameObject = ((Component) UIController.instance.craftingMenu.transform.GetChild(4).GetChild(2).GetChild(0)).gameObject;
    if (GameManager.instance.items[index].textureID != 0)
    {
      UIController.instance.isCraftable = true;
      ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[index].texture;
      ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = num1.ToString();
    }
    else
    {
      ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.blockSprites[0];
      ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "";
    }
  }

  public static void GetQuestsData(Packet _packet)
  {
    string str1 = _packet.ReadString();
    int num = _packet.ReadInt();
    int index1 = _packet.ReadInt();
    string str2 = _packet.ReadString();
    foreach (string str3 in str1.Split('|', StringSplitOptions.None))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ClientHandle.\u003C\u003Ec__DisplayClass38_0 cDisplayClass380 = new ClientHandle.\u003C\u003Ec__DisplayClass38_0();
      int int32 = Convert.ToInt32(str3.Split('~', StringSplitOptions.None)[1]);
      string str4 = str3.Split('~', StringSplitOptions.None)[0];
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380._quest = JsonUtility.FromJson<Quest>(str4);
      // ISSUE: reference to a compiler-generated field
      GameManager.instance.quests.Add(cDisplayClass380._quest);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380._questGameObject = Object.Instantiate<GameObject>(UIController.instance.quest, UIController.instance.questsContainer.transform);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) cDisplayClass380._questGameObject.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass380._quest.title;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) cDisplayClass380._questGameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass380._quest.description;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380._quest.gameObject = cDisplayClass380._questGameObject;
      switch (int32)
      {
        case 0:
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(3)).gameObject.SetActive(false);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(2)).gameObject.SetActive(false);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(0)).gameObject.SetActive(false);
          break;
        case 1:
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(3)).gameObject.SetActive(false);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(2)).gameObject.SetActive(false);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(false);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(0)).gameObject.SetActive(true);
          break;
        case 2:
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(3)).gameObject.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(2)).gameObject.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(false);
          // ISSUE: reference to a compiler-generated field
          ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(0)).gameObject.SetActive(false);
          break;
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass380, __methodptr(\u003CGetQuestsData\u003Eb__0)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(2)).GetComponent<Button>().onClick).AddListener(ClientHandle.\u003C\u003Ec.\u003C\u003E9__38_1 ?? (ClientHandle.\u003C\u003Ec.\u003C\u003E9__38_1 = new UnityAction((object) ClientHandle.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetQuestsData\u003Eb__38_1))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) ((Component) cDisplayClass380._questGameObject.transform.GetChild(2).GetChild(3)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass380, __methodptr(\u003CGetQuestsData\u003Eb__2)));
    }
    if (str2 != "")
    {
      string[] strArray = str2.Split(',', StringSplitOptions.None);
      GameManager.instance.currentRequirements = new int[strArray.Length];
      ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.quests[num - 1].title;
      ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = string.Format("You are currently in <color=#62ff42>Step {0}</color> out of <color=#62ff42>{1} Steps</color>.", (object) (index1 + 1), (object) GameManager.instance.quests[num - 1].steps.Length);
      ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(0)).GetComponent<Slider>().value = (float) index1 * 1f / (float) GameManager.instance.quests[num - 1].steps.Length;
      ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = string.Format("{0}/{1} Steps Done", (object) index1, (object) GameManager.instance.quests[num - 1].steps.Length);
      ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.quests[num - 1].steps[index1].description;
      ((UnityEventBase) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(ClientHandle.\u003C\u003Ec.\u003C\u003E9__38_5 ?? (ClientHandle.\u003C\u003Ec.\u003C\u003E9__38_5 = new UnityAction((object) ClientHandle.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetQuestsData\u003Eb__38_5))));
      for (int index2 = 0; index2 < strArray.Length; ++index2)
      {
        for (int index3 = 0; index3 < GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems.Length; ++index3)
        {
          GameManager.instance.currentRequirements[index2] = Convert.ToInt32(strArray[index2]);
          GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.questDetailsBar, UIController.instance.questDetailsContainer.transform);
          try
          {
            ((Component) gameObject.transform.GetChild(0)).GetComponent<Slider>().value = (float) GameManager.instance.currentRequirements[index2] * 1f / (float) GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity;
          }
          catch
          {
            ((Component) gameObject.transform.GetChild(0)).GetComponent<Slider>().value = 0.0f;
          }
          switch (GameManager.instance.quests[num - 1].steps[index1].requirements[index2].eventID)
          {
            case 0:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Put";
              break;
            case 1:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Broken";
              break;
            case 2:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Brought";
              break;
            case 3:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Dropped";
              break;
            case 4:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Traded";
              break;
            case 5:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Trashed";
              break;
            case 6:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Converted";
              break;
            case 7:
              ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Crafted";
              break;
          }
        }
      }
    }
    ((TMP_Text) UIController.instance.questsText).text = GameManager.instance.quests.Count.ToString() + " Quests Available <sprite=8> ";
  }

  public static void UpdateQuestData(Packet _packet)
  {
    int num1 = _packet.ReadInt();
    int num2 = _packet.ReadInt();
    int index1 = _packet.ReadInt();
    string str = _packet.ReadString();
    switch (num1)
    {
      case 0:
        foreach (Component component in UIController.instance.questDetailsContainer.transform)
          Object.Destroy((Object) component.gameObject);
        if (!(str != ""))
          break;
        string[] strArray1 = str.Split(',', StringSplitOptions.None);
        GameManager.instance.currentRequirements = new int[strArray1.Length];
        ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.quests[num2 - 1].title;
        ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = string.Format("You are currently in <color=#62ff42>Step {0}</color> out of <color=#62ff42>{1} Step(s)</color>.", (object) (index1 + 1), (object) GameManager.instance.quests[num2 - 1].steps.Length);
        ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(0)).GetComponent<Slider>().value = (float) index1 * 1f / (float) GameManager.instance.quests[num2 - 1].steps.Length;
        ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = string.Format("{0}/{1} Steps Done", (object) index1, (object) GameManager.instance.quests[num2 - 1].steps.Length);
        ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.quests[num2 - 1].steps[index1].description;
        ((UnityEventBase) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((UnityEvent) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(ClientHandle.\u003C\u003Ec.\u003C\u003E9__39_0 ?? (ClientHandle.\u003C\u003Ec.\u003C\u003E9__39_0 = new UnityAction((object) ClientHandle.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateQuestData\u003Eb__39_0))));
        for (int index2 = 0; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = 0; index3 < GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems.Length; ++index3)
          {
            GameManager.instance.currentRequirements[index2] = Convert.ToInt32(strArray1[index2]);
            GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.questDetailsBar, UIController.instance.questDetailsContainer.transform);
            try
            {
              ((Component) gameObject.transform.GetChild(0)).GetComponent<Slider>().value = (float) GameManager.instance.currentRequirements[index2] * 1f / (float) GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity;
            }
            catch
            {
              ((Component) gameObject.transform.GetChild(0)).GetComponent<Slider>().value = 0.0f;
            }
            switch (GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].eventID)
            {
              case 0:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Put";
                break;
              case 1:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Broken";
                break;
              case 2:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Brought";
                break;
              case 3:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Dropped";
                break;
              case 4:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Traded";
                break;
              case 5:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Trashed";
                break;
              case 6:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Converted";
                break;
              case 7:
                ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index2].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index2].requiredItems[index3].id].name + "(s) Crafted";
                break;
            }
          }
        }
        break;
      case 1:
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(0)).gameObject.SetActive(false);
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(true);
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(2)).gameObject.SetActive(false);
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(3)).gameObject.SetActive(false);
        break;
      case 2:
        foreach (Component component in UIController.instance.questDetailsContainer.transform)
          Object.Destroy((Object) component.gameObject);
        if (str != "")
        {
          string[] strArray2 = str.Split(',', StringSplitOptions.None);
          GameManager.instance.currentRequirements = new int[strArray2.Length];
          ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.quests[num2 - 1].title;
          ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = string.Format("You are currently in <color=#62ff42>Step {0}</color> out of <color=#62ff42>{1} Steps</color>.", (object) (index1 + 1), (object) GameManager.instance.quests[num2 - 1].steps.Length);
          ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(0)).GetComponent<Slider>().value = (float) index1 * 1f / (float) GameManager.instance.quests[num2 - 1].steps.Length;
          ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.parent.GetChild(0).GetChild(2).GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = string.Format("{0}/{1} Steps Done", (object) index1, (object) GameManager.instance.quests[num2 - 1].steps.Length);
          ((TMP_Text) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.quests[num2 - 1].steps[index1].description;
          ((UnityEventBase) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent) ((Component) UIController.instance.questDetailsContainer.transform.parent.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(ClientHandle.\u003C\u003Ec.\u003C\u003E9__39_2 ?? (ClientHandle.\u003C\u003Ec.\u003C\u003E9__39_2 = new UnityAction((object) ClientHandle.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateQuestData\u003Eb__39_2))));
          for (int index4 = 0; index4 < strArray2.Length; ++index4)
          {
            for (int index5 = 0; index5 < GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems.Length; ++index5)
            {
              GameManager.instance.currentRequirements[index4] = Convert.ToInt32(strArray2[index4]);
              GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.questDetailsBar, UIController.instance.questDetailsContainer.transform);
              try
              {
                ((Component) gameObject.transform.GetChild(0)).GetComponent<Slider>().value = (float) GameManager.instance.currentRequirements[index4] * 1f / (float) GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity;
              }
              catch
              {
                ((Component) gameObject.transform.GetChild(0)).GetComponent<Slider>().value = 0.0f;
              }
              switch (GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].eventID)
              {
                case 0:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Put";
                  break;
                case 1:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Broken";
                  break;
                case 2:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Brought";
                  break;
                case 3:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Dropped";
                  break;
                case 4:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Traded";
                  break;
                case 5:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Trashed";
                  break;
                case 6:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Converted";
                  break;
                case 7:
                  ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.currentRequirements[index4].ToString() + " of " + GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].quantity.ToString() + " " + GameManager.instance.items[GameManager.instance.quests[num2 - 1].steps[index1].requirements[index4].requiredItems[index5].id].name + "(s) Crafted";
                  break;
              }
            }
          }
        }
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(0)).gameObject.SetActive(false);
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(1)).gameObject.SetActive(false);
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(2)).gameObject.SetActive(true);
        ((Component) GameManager.instance.quests[num2 - 1].gameObject.transform.GetChild(2).GetChild(3)).gameObject.SetActive(true);
        break;
    }
  }

  public static void UpdateBlockTexture(Packet _packet)
  {
    Vector2 vector2 = _packet.ReadVector2();
    int index1 = _packet.ReadInt();
    int index2 = _packet.ReadInt();
    if (!WorldManager.instance.DoesExist((int) vector2.x, (int) vector2.y))
      return;
    WorldManager.instance.worldLayers[index1][(int) vector2.x, (int) vector2.y].gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.blockSprites[index2];
  }

  public static void GetAchievementsData(Packet _packet)
  {
    foreach (string str in _packet.ReadString().Split('|', StringSplitOptions.None))
    {
      int int32 = Convert.ToInt32(str.Split('~', StringSplitOptions.None)[1]);
      Achievement achievement = JsonUtility.FromJson<Achievement>(str.Split('~', StringSplitOptions.None)[0]);
      if (!GameManager.instance.achievements.Contains(achievement))
      {
        GameManager.instance.achievements.Add(achievement);
        GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.achievement, UIController.instance.achievementsContainer.transform);
        ((TMP_Text) ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = achievement.title;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(0).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = achievement.description;
        ((Component) gameObject.transform.GetChild(2)).GetComponent<Image>().sprite = GameManager.instance.achievementSprites[achievement.logoID * 2 + (1 - int32)];
        ((Component) gameObject.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = (float) int32;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(1).GetChild(0).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = int32.ToString() + "/1";
        achievement.gameObject = gameObject;
      }
    }
    UIController.instance.achievementsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(UIController.instance.achievementsContainer.GetComponent<RectTransform>().sizeDelta.x, 10f + Math.Clamp(141.1563f * (float) GameManager.instance.achievements.Count, 862.7781f, float.MaxValue));
  }

  public static void CompleteAchievement(Packet _packet)
  {
    int index = _packet.ReadInt();
    UIController.instance.CreateAchievementNotificationFunction(GameManager.instance.achievements[index].title, GameManager.instance.achievementSprites[GameManager.instance.achievements[index].logoID * 2], 3f);
    ((Component) GameManager.instance.achievements[index].gameObject.transform.GetChild(2)).GetComponent<Image>().sprite = GameManager.instance.achievementSprites[GameManager.instance.achievements[index].logoID * 2];
    ((Component) GameManager.instance.achievements[index].gameObject.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 1f;
  }

  public static void AddSaleItem(Packet _packet)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ClientHandle.\u003C\u003Ec__DisplayClass43_0 cDisplayClass430 = new ClientHandle.\u003C\u003Ec__DisplayClass43_0();
    string str1 = _packet.ReadString();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass430._saleItem = JsonUtility.FromJson<SaleItem>(str1);
    // ISSUE: reference to a compiler-generated field
    if (GameManager.instance.saleItems.ContainsKey(cDisplayClass430._saleItem.id))
      return;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass430._saleItem.status == 0)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      GameManager.instance.saleItems.Add(cDisplayClass430._saleItem.id, cDisplayClass430._saleItem);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass430._saleItem.clientUsername == GameManager.instance.username)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.mySaleItem, UIController.instance.mySaleItemContainer.transform);
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass430._saleItem.price.ToString() + "<sprite=0>";
      // ISSUE: reference to a compiler-generated field
      ((Component) gameObject.transform.GetChild(2).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[cDisplayClass430._saleItem.itemID].texture;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) gameObject.transform.GetChild(3)).GetComponent<TextMeshProUGUI>()).text = "<color=#ffef61>" + cDisplayClass430._saleItem.itemQuantity.ToString() + "x</color> " + GameManager.instance.items[cDisplayClass430._saleItem.itemID].name;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass430._saleItem.gameObject = gameObject;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      GameManager.instance.mySaleItems.Add(cDisplayClass430._saleItem.id, cDisplayClass430._saleItem);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass430._saleItem.status == 0)
      {
        // ISSUE: method pointer
        ((UnityEvent) ((Component) gameObject.transform.GetChild(4).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass430, __methodptr(\u003CAddSaleItem\u003Eb__0)));
      }
      else
      {
        ((Component) gameObject.transform.GetChild(4).GetChild(1)).gameObject.SetActive(false);
        ((Component) gameObject.transform.GetChild(4).GetChild(2)).gameObject.SetActive(true);
      }
      UIController.instance.mySaleItemContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(UIController.instance.mySaleItemContainer.GetComponent<RectTransform>().sizeDelta.x, Math.Clamp(147.7696f * (float) GameManager.instance.mySaleItems.Count, 460.9418f, float.MaxValue));
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass430._saleItem.status != 0)
      return;
    // ISSUE: reference to a compiler-generated field
    if (GameManager.instance.categories.ContainsKey(cDisplayClass430._saleItem.itemID))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      GameManager.instance.categories[cDisplayClass430._saleItem.itemID].saleItems.Add(cDisplayClass430._saleItem.id, cDisplayClass430._saleItem);
      // ISSUE: reference to a compiler-generated field
      Category category = GameManager.instance.categories[cDisplayClass430._saleItem.itemID];
      GameObject gameObject = category.gameObject;
      // ISSUE: reference to a compiler-generated field
      if (category.floorPrice > cDisplayClass430._saleItem.price)
      {
        // ISSUE: reference to a compiler-generated field
        category.floorPrice = cDisplayClass430._saleItem.price;
      }
      ((TMP_Text) ((Component) gameObject.transform.GetChild(2)).GetComponent<TextMeshProUGUI>()).text = category.saleItems.Count.ToString() + " Items on Sale";
      ((TMP_Text) ((Component) gameObject.transform.GetChild(4)).GetComponent<TextMeshProUGUI>()).text = "Minimum Price: " + category.floorPrice.ToString() + "<sprite=0>";
    }
    else
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ClientHandle.\u003C\u003Ec__DisplayClass43_1 cDisplayClass431 = new ClientHandle.\u003C\u003Ec__DisplayClass43_1()
      {
        _category = new Category()
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass431._category.itemID = cDisplayClass430._saleItem.itemID;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass431._category.saleItems.Add(cDisplayClass430._saleItem.id, cDisplayClass430._saleItem);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass431._category.floorPrice = cDisplayClass430._saleItem.price;
      GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.category, UIController.instance.categoryContainer.transform);
      // ISSUE: reference to a compiler-generated field
      ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[cDisplayClass431._category.itemID].texture;
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[cDisplayClass431._category.itemID].name;
      TextMeshProUGUI component = ((Component) gameObject.transform.GetChild(2)).GetComponent<TextMeshProUGUI>();
      // ISSUE: reference to a compiler-generated field
      int index = cDisplayClass431._category.saleItems.Count;
      string str2 = index.ToString() + " Items on Sale";
      ((TMP_Text) component).text = str2;
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) gameObject.transform.GetChild(3)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.types[GameManager.instance.items[cDisplayClass431._category.itemID].typeID - 1];
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) gameObject.transform.GetChild(4)).GetComponent<TextMeshProUGUI>()).text = "Minimum Price: " + cDisplayClass431._category.floorPrice.ToString() + "<sprite=0>";
      // ISSUE: reference to a compiler-generated field
      cDisplayClass431._category.gameObject = gameObject;
      // ISSUE: reference to a compiler-generated field
      ((UnityEventBase) cDisplayClass431._category.gameObject.GetComponent<Button>().onClick).RemoveAllListeners();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) cDisplayClass431._category.gameObject.GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass431, __methodptr(\u003CAddSaleItem\u003Eb__2)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      GameManager.instance.categories.Add(cDisplayClass430._saleItem.itemID, cDisplayClass431._category);
      TextMeshProUGUI[] saleItemCountTexts = UIController.instance.saleItemCountTexts;
      for (index = 0; index < saleItemCountTexts.Length; ++index)
        ((TMP_Text) saleItemCountTexts[index]).text = GameManager.instance.saleItems.Count.ToString() + " Items on Sale";
      UIController.instance.categoryContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(UIController.instance.categoryContainer.GetComponent<RectTransform>().sizeDelta.x, Math.Clamp(131.5168f * (float) GameManager.instance.categories.Count, 460.9418f, float.MaxValue));
    }
  }

  public static void SaleItemAction(Packet _packet)
  {
    int key = _packet.ReadInt();
    int num = _packet.ReadInt();
    SaleItem saleItem = GameManager.instance.saleItems[key];
    switch (num)
    {
      case 0:
        if (GameManager.instance.categories.ContainsKey(GameManager.instance.saleItems[key].itemID))
        {
          Category category = GameManager.instance.categories[GameManager.instance.saleItems[key].itemID];
          GameManager.instance.categories[GameManager.instance.saleItems[key].itemID].saleItems.Remove(key);
          if (GameManager.instance.categories[GameManager.instance.saleItems[key].itemID].saleItems.Count > 0)
          {
            category.itemID = saleItem.itemID;
            category.saleItems.Add(saleItem.id, saleItem);
            category.floorPrice = saleItem.price;
            GameObject gameObject = category.gameObject;
            ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[category.itemID].texture;
            ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[category.itemID].name;
            ((TMP_Text) ((Component) gameObject.transform.GetChild(2)).GetComponent<TextMeshProUGUI>()).text = category.saleItems.Count.ToString() + " Items on Sale";
            ((TMP_Text) ((Component) gameObject.transform.GetChild(3)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.types[GameManager.instance.items[category.itemID].typeID - 1];
            ((TMP_Text) ((Component) gameObject.transform.GetChild(4)).GetComponent<TextMeshProUGUI>()).text = "Minimum Price: " + category.floorPrice.ToString() + "<sprite=0>";
          }
          else
          {
            GameManager.instance.categories.Remove(GameManager.instance.saleItems[key].itemID);
            Object.Destroy((Object) category.gameObject);
          }
        }
        if (GameManager.instance.mySaleItems.ContainsKey(key) && Object.op_Inequality((Object) GameManager.instance.mySaleItems[key].gameObject, (Object) null))
        {
          ((Component) GameManager.instance.mySaleItems[key].gameObject.transform.GetChild(4).GetChild(1)).gameObject.SetActive(false);
          ((Component) GameManager.instance.mySaleItems[key].gameObject.transform.GetChild(4).GetChild(2)).gameObject.SetActive(true);
        }
        GameManager.instance.saleItems.Remove(key);
        foreach (TMP_Text saleItemCountText in UIController.instance.saleItemCountTexts)
          saleItemCountText.text = GameManager.instance.saleItems.Count.ToString() + " Items on Sale";
        break;
      case 1:
        if (GameManager.instance.mySaleItems.ContainsKey(key))
        {
          if (Object.op_Inequality((Object) GameManager.instance.mySaleItems[key].gameObject, (Object) null))
            Object.Destroy((Object) GameManager.instance.mySaleItems[key].gameObject);
          GameManager.instance.mySaleItems.Remove(key);
        }
        if (GameManager.instance.categories.ContainsKey(GameManager.instance.saleItems[key].itemID))
        {
          Category category = GameManager.instance.categories[GameManager.instance.saleItems[key].itemID];
          GameManager.instance.categories[GameManager.instance.saleItems[key].itemID].saleItems.Remove(key);
          if (GameManager.instance.categories[GameManager.instance.saleItems[key].itemID].saleItems.Count > 0)
          {
            category.itemID = saleItem.itemID;
            category.saleItems.Add(saleItem.id, saleItem);
            category.floorPrice = saleItem.price;
            GameObject gameObject = category.gameObject;
            ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[category.itemID].texture;
            ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[category.itemID].name;
            ((TMP_Text) ((Component) gameObject.transform.GetChild(2)).GetComponent<TextMeshProUGUI>()).text = category.saleItems.Count.ToString() + " Items on Sale";
            ((TMP_Text) ((Component) gameObject.transform.GetChild(3)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.types[GameManager.instance.items[category.itemID].typeID - 1];
            ((TMP_Text) ((Component) gameObject.transform.GetChild(4)).GetComponent<TextMeshProUGUI>()).text = "Minimum Price: " + category.floorPrice.ToString() + "<sprite=0>";
          }
          else
          {
            GameManager.instance.categories.Remove(GameManager.instance.saleItems[key].itemID);
            Object.Destroy((Object) category.gameObject);
          }
        }
        GameManager.instance.saleItems.Remove(key);
        foreach (TMP_Text saleItemCountText in UIController.instance.saleItemCountTexts)
          saleItemCountText.text = GameManager.instance.saleItems.Count.ToString() + " Items on Sale";
        break;
    }
  }

  public static void AddEffect(Packet _packet)
  {
    EffectType effectType = JsonUtility.FromJson<EffectType>(_packet.ReadString());
    MovementManager component = ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>();
    AnimationSystem animationSystem = GameManager.players[Client.instance.myId].animationSystem;
    foreach (TemporaryEffect effect in effectType.effects)
    {
      switch (effect.typeID)
      {
        case 0:
          effect.previousEffectValue = component.gravity;
          break;
        case 1:
          effect.previousEffectValue = component.gravityInWater;
          break;
        case 2:
          effect.previousEffectValue = component.movementSpeed;
          break;
        case 3:
          effect.previousEffectValue = (float) animationSystem.animations[1].updateFrameMS;
          break;
        case 4:
          effect.previousEffectValue = (float) component.actualJumpCount;
          break;
        case 5:
          effect.previousEffectValue = component.jumpPower;
          break;
        case 6:
          effect.previousEffectValue = component.airJumpMaxAmount;
          break;
        case 7:
          effect.previousEffectValue = (float) Convert.ToInt32(component.isContinuous);
          break;
        case 8:
          effect.previousEffectValue = component.airJumpPower;
          break;
        case 9:
          effect.previousEffectValue = GameManager.instance.powerMultiplier;
          break;
      }
    }
    GameManager.instance.effectTypes.Add(effectType.id, effectType);
    foreach (TemporaryEffect effect in effectType.effects)
    {
      switch (effect.typeID)
      {
        case 0:
          component.gravity = effect.effectValue;
          break;
        case 1:
          component.gravityInWater = effect.effectValue;
          break;
        case 2:
          component.movementSpeed = effect.effectValue;
          break;
        case 3:
          animationSystem.animations[1].updateFrameMS = (int) effect.effectValue;
          break;
        case 4:
          component.actualJumpCount = (int) effect.effectValue;
          break;
        case 5:
          component.jumpPower = (float) (int) effect.effectValue;
          break;
        case 6:
          component.airJumpMaxAmount = (float) (int) effect.effectValue;
          break;
        case 7:
          component.isContinuous = Convert.ToBoolean((int) effect.effectValue);
          break;
        case 8:
          component.airJumpPower = effect.effectValue;
          break;
        case 9:
          GameManager.instance.powerMultiplier = effect.effectValue;
          ((Component) component).GetComponent<AnimationSystem>().animations[4].updateFrameMS = (int) (100.0 / (double) GameManager.instance.powerMultiplier);
          GameManager.instance.delay = 0.3f / GameManager.instance.powerMultiplier;
          break;
      }
    }
  }

  public static void RemoveEffect(Packet _packet)
  {
    int key = _packet.ReadInt();
    MovementManager component = ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>();
    AnimationSystem animationSystem = GameManager.players[Client.instance.myId].animationSystem;
    EffectType effectType = GameManager.instance.effectTypes[key];
    foreach (TemporaryEffect effect in effectType.effects)
    {
      switch (effect.typeID)
      {
        case 0:
          component.gravity = effect.previousEffectValue;
          break;
        case 1:
          component.gravityInWater = effect.previousEffectValue;
          break;
        case 2:
          component.movementSpeed = effect.previousEffectValue;
          break;
        case 3:
          animationSystem.animations[1].updateFrameMS = (int) effect.previousEffectValue;
          break;
        case 4:
          component.actualJumpCount = (int) effect.previousEffectValue;
          break;
        case 5:
          component.jumpPower = (float) (int) effect.previousEffectValue;
          break;
        case 6:
          component.airJumpMaxAmount = (float) (int) effect.previousEffectValue;
          break;
        case 7:
          component.isContinuous = Convert.ToBoolean((int) effect.previousEffectValue);
          break;
        case 8:
          component.airJumpPower = effect.previousEffectValue;
          break;
        case 9:
          GameManager.instance.powerMultiplier = effect.previousEffectValue;
          ((Component) component).GetComponent<AnimationSystem>().animations[4].updateFrameMS = (int) (100.0 / (double) GameManager.instance.powerMultiplier);
          GameManager.instance.delay = 0.3f / GameManager.instance.powerMultiplier;
          break;
      }
    }
    GameManager.instance.effectTypes.Remove(effectType.id);
  }

  public static void ClientEvent(Packet _packet)
  {
    int key = _packet.ReadInt();
    int num = _packet.ReadInt();
    if (!GameManager.players.ContainsKey(key))
      return;
    switch (num)
    {
      case 0:
        GameManager.instance.CreateEffect(1, Vector2.op_Implicit(((Component) GameManager.players[key]).transform.position), new Vector2(1.5f, 1.5f));
        if (key == Client.instance.myId)
        {
          GameManager.players[Client.instance.myId].isDying = true;
          ((Component) GameManager.players[key]).GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D) 2;
        }
        GameManager.players[key].ChangeSize(new Vector2(0.1f, 0.1f), true);
        GameManager.instance.CreateEffect(8, Vector2.op_Implicit(((Component) GameManager.players[key]).transform.position), Vector2.op_Implicit(((Component) GameManager.players[key]).transform.localScale), 2);
        if (key != Client.instance.myId)
          break;
        UIController.instance.ActivateDeathScreenFunction(2f);
        break;
      case 1:
        GameManager.instance.CreateEffect(9, Vector2.op_Implicit(((Component) GameManager.players[key]).transform.position), 1);
        if (key == Client.instance.myId)
        {
          GameManager.players[Client.instance.myId].isDying = false;
          ((Component) GameManager.players[key]).GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D) 2;
        }
        ((Component) GameManager.players[key]).transform.localScale = Vector2.op_Implicit(new Vector2(0.0f, 0.0f));
        GameManager.instance.SetActive(((Component) GameManager.players[key]).gameObject, true, 1f);
        break;
    }
  }

  public static void CreateItemAlert(Packet _packet)
  {
    Vector2 _position = _packet.ReadVector2();
    int _itemID = _packet.ReadInt();
    int _status = _packet.ReadInt();
    UIController.instance.CreateItemAlert(_position, _itemID, _status);
  }

  public static void RemoveItemAlert(Packet _packet)
  {
    Vector2 _position = _packet.ReadVector2();
    UIController.instance.RemoveItemAlert(_position);
  }

  public static void EditItemAlert(Packet _packet)
  {
    Vector2 _position = _packet.ReadVector2();
    int _status = _packet.ReadInt();
    if (_status == 2)
      UIController.instance.CreateTextEffect("Ready to Collect!", new Vector2(_position.x, _position.y - 0.7f));
    UIController.instance.EditItemAlert(_position, _status);
  }

  public static void GetRecipesData(Packet _packet)
  {
    string str1 = _packet.ReadString();
    List<Craft> craftList = new List<Craft>();
    foreach (string str2 in str1.Split('|', StringSplitOptions.None))
      craftList.Add(JsonUtility.FromJson<Craft>(str2));
    foreach (Craft craft in craftList)
    {
      if (!GameManager.instance.recipes.ContainsKey(craft.id))
      {
        GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.recipe, UIController.instance.recipesContainer.transform);
        ((Component) gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[craft.item1.id].texture;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(0).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[craft.item1.id].name;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(0).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = craft.item1.quantity.ToString();
        ((Component) gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[craft.item2.id].texture;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(1).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[craft.item2.id].name;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(1).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = craft.item2.quantity.ToString();
        ((Component) gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[craft.item.id].texture;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(2).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[craft.item.id].name;
        ((TMP_Text) ((Component) gameObject.transform.GetChild(2).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = craft.item.quantity.ToString();
        GameManager.instance.recipes.Add(craft.id, craft);
      }
    }
    UIController.instance.dealerMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(UIController.instance.dealerMenu.GetComponent<RectTransform>().sizeDelta.x, Math.Clamp((float) (233.38180541992188 * (double) GameManager.instance.recipes.Count + 160.0), 163f, float.MaxValue));
  }

  public static void FishingAction(Packet _packet)
  {
    if (_packet.ReadInt() != 0)
      return;
    float num1 = _packet.ReadFloat();
    float num2 = _packet.ReadFloat();
    FishingManager.instance.movementSpeed = num2;
    FishingManager.instance.fishPosition = new Vector2(FishingManager.instance.fishPosition.x + num1, FishingManager.instance.fishPosition.y);
  }

  public static void GetCubotMessage(Packet _packet)
  {
    string _title = _packet.ReadString();
    string _text = _packet.ReadString();
    float _time = _packet.ReadFloat();
    UIController.instance.CreateCubotMessageFunction(_title, _text, _time);
  }
}
