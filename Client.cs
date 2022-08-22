// Decompiled with JetBrains decompiler
// Type: Client
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
  public static Client instance;
  public static int dataBufferSize = 65536;
  public string ip = "127.0.0.1";
  public int port = 26950;
  public string version = "B2";
  public int myId;
  public Client.TCP tcp;
  public Client.UDP udp;
  private bool isConnected;
  private static Dictionary<int, Client.PacketHandler> packetHandlers;

  private IEnumerator checkIfConnected()
  {
    Client client = this;
    yield return (object) new WaitForSeconds(10f);
    if (client.myId == 0)
    {
      UIController.instance.CreateNotificationFunction("<color=red>Error", "Cannot connect to the server,\ntrying again!", 5f);
      client.ConnectToServer();
      client.StartCoroutine(client.checkIfConnected());
    }
  }

  private void Awake()
  {
    if (Object.op_Equality((Object) Client.instance, (Object) null))
      Client.instance = this;
    else if (Object.op_Inequality((Object) Client.instance, (Object) this))
    {
      Debug.Log((object) "Instance already exists, destroying object!");
      Object.Destroy((Object) this);
    }
    this.ip = "161.97.102.32";
    this.port = 26950;
  }

  private void Start()
  {
    this.tcp = new Client.TCP();
    this.udp = new Client.UDP();
    this.ConnectToServer();
    this.StartCoroutine(this.checkIfConnected());
  }

  private void Update()
  {
  }

  private void OnApplicationQuit() => this.Disconnect();

  public void ConnectToServer()
  {
    this.InitializeClientData();
    this.isConnected = true;
    this.tcp.Connect();
  }

  private void InitializeClientData()
  {
    Client.packetHandlers = new Dictionary<int, Client.PacketHandler>()
    {
      {
        1,
        new Client.PacketHandler(ClientHandle.Welcome)
      },
      {
        2,
        new Client.PacketHandler(ClientHandle.SpawnPlayer)
      },
      {
        3,
        new Client.PacketHandler(ClientHandle.PlayerPosition)
      },
      {
        4,
        new Client.PacketHandler(ClientHandle.PlayerSize)
      },
      {
        5,
        new Client.PacketHandler(ClientHandle.DisconnectUser)
      },
      {
        6,
        new Client.PacketHandler(ClientHandle.GetItemData)
      },
      {
        30,
        new Client.PacketHandler(ClientHandle.GetWorldData)
      },
      {
        8,
        new Client.PacketHandler(ClientHandle.EditWorldData)
      },
      {
        9,
        new Client.PacketHandler(ClientHandle.GetInventoryData)
      },
      {
        10,
        new Client.PacketHandler(ClientHandle.EditInventoryData)
      },
      {
        11,
        new Client.PacketHandler(ClientHandle.CreateDroppedItem)
      },
      {
        12,
        new Client.PacketHandler(ClientHandle.RemoveDroppedItem)
      },
      {
        13,
        new Client.PacketHandler(ClientHandle.GetChatMessage)
      },
      {
        14,
        new Client.PacketHandler(ClientHandle.GetChatBubble)
      },
      {
        15,
        new Client.PacketHandler(ClientHandle.AuthUser)
      },
      {
        16,
        new Client.PacketHandler(ClientHandle.Reply)
      },
      {
        17,
        new Client.PacketHandler(ClientHandle.GetPackData)
      },
      {
        18,
        new Client.PacketHandler(ClientHandle.SetPosition)
      },
      {
        19,
        new Client.PacketHandler(ClientHandle.CreateTrade)
      },
      {
        22,
        new Client.PacketHandler(ClientHandle.RemoveTrade)
      },
      {
        20,
        new Client.PacketHandler(ClientHandle.AddItemToTrade)
      },
      {
        23,
        new Client.PacketHandler(ClientHandle.GetNotification)
      },
      {
        24,
        new Client.PacketHandler(ClientHandle.JoinWorld)
      },
      {
        25,
        new Client.PacketHandler(ClientHandle.SignData)
      },
      {
        26,
        new Client.PacketHandler(ClientHandle.EditCostumeData)
      },
      {
        27,
        new Client.PacketHandler(ClientHandle.ChangeAnimation)
      },
      {
        28,
        new Client.PacketHandler(ClientHandle.SetHealth)
      },
      {
        29,
        new Client.PacketHandler(ClientHandle.SetStatus)
      },
      {
        31,
        new Client.PacketHandler(ClientHandle.GetActiveWorlds)
      },
      {
        33,
        new Client.PacketHandler(ClientHandle.GetNews)
      },
      {
        34,
        new Client.PacketHandler(ClientHandle.GetCubixCount)
      },
      {
        35,
        new Client.PacketHandler(ClientHandle.GetAccountData)
      },
      {
        36,
        new Client.PacketHandler(ClientHandle.UpdateClientData)
      },
      {
        37,
        new Client.PacketHandler(ClientHandle.EntranceData)
      },
      {
        38,
        new Client.PacketHandler(ClientHandle.DoorData)
      },
      {
        32,
        new Client.PacketHandler(ClientHandle.CreateDialog)
      },
      {
        39,
        new Client.PacketHandler(ClientHandle.GetFriendsData)
      },
      {
        40,
        new Client.PacketHandler(ClientHandle.GetCraftData)
      },
      {
        41,
        new Client.PacketHandler(ClientHandle.GetQuestsData)
      },
      {
        42,
        new Client.PacketHandler(ClientHandle.UpdateQuestData)
      },
      {
        43,
        new Client.PacketHandler(ClientHandle.UpdateBlockTexture)
      },
      {
        44,
        new Client.PacketHandler(ClientHandle.GetAchievementsData)
      },
      {
        45,
        new Client.PacketHandler(ClientHandle.CompleteAchievement)
      },
      {
        46,
        new Client.PacketHandler(ClientHandle.AddSaleItem)
      },
      {
        47,
        new Client.PacketHandler(ClientHandle.SaleItemAction)
      },
      {
        48,
        new Client.PacketHandler(ClientHandle.AddEffect)
      },
      {
        49,
        new Client.PacketHandler(ClientHandle.RemoveEffect)
      },
      {
        50,
        new Client.PacketHandler(ClientHandle.ClientEvent)
      },
      {
        51,
        new Client.PacketHandler(ClientHandle.CreateItemAlert)
      },
      {
        52,
        new Client.PacketHandler(ClientHandle.RemoveItemAlert)
      },
      {
        53,
        new Client.PacketHandler(ClientHandle.EditItemAlert)
      },
      {
        54,
        new Client.PacketHandler(ClientHandle.GetRecipesData)
      },
      {
        55,
        new Client.PacketHandler(ClientHandle.FishingAction)
      },
      {
        56,
        new Client.PacketHandler(ClientHandle.GetCubotMessage)
      }
    };
    Debug.Log((object) "Initialized packets.");
  }

  private void Disconnect()
  {
    if (!this.isConnected)
      return;
    WorldManager.instance.worldDataFilled = false;
    this.isConnected = false;
    this.tcp.socket.Close();
    this.udp.socket.Close();
    this.myId = 0;
    ThreadManager.ExecuteOnMainThread((Action) (() =>
    {
      WorldManager.instance.visibleBlocks.Clear();
      foreach (Slot slot in InventoryManager.instance.slots)
        Object.Destroy((Object) slot.gameObject);
      InventoryManager.instance.slots.Clear();
      if (GameManager.players.ContainsKey(Client.instance.myId))
      {
        ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().isNoclip = false;
        foreach (Object signBar in MovementManager.instance.signBars)
          Object.Destroy(signBar);
        MovementManager.instance.signBars.Clear();
        if (Object.op_Inequality((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar, (Object) null))
        {
          Object.Destroy((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar);
          ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().currentSignBar = (GameObject) null;
        }
      }
      foreach (Object @object in UIController.instance.itemAlerts.Values)
        Object.Destroy(@object);
      UIController.instance.itemAlerts.Clear();
      UIController.instance.itemAlerts.Clear();
      WorldManager.instance.animatedBlocks.Clear();
      GameManager.instance.effectTypes.Clear();
      WorldManager.instance.rainbowBlocks.Clear();
      foreach (GameObject gameObject in GameManager.instance.pets.Values)
      {
        Object.Destroy((Object) ((Component) gameObject.GetComponent<PetBehaivour>().title).gameObject);
        Object.Destroy((Object) gameObject.GetComponent<DropShadow>().shadowGameobject);
        Object.Destroy((Object) gameObject);
      }
      GameManager.instance.pets.Clear();
      UIController.instance.CreateNotificationFunction("<color=red>Warning", "You have been disconnected from\nserver!", 3f);
      WorldManager.instance.entrances.Clear();
      WorldManager.instance.doors.Clear();
      UIController.instance.SetActive(UIController.instance.inGameMenu, false);
      foreach (PlayerManager playerManager in GameManager.players.Values)
      {
        GameObject wrenchIcon = playerManager.wrenchIcon;
        GameManager.instance.wrenchIcons.Remove(wrenchIcon);
        Object.Destroy((Object) wrenchIcon);
        if (Object.op_Inequality((Object) ((Component) playerManager).gameObject, (Object) null))
        {
          Object.Destroy((Object) ((Component) playerManager).gameObject);
          Object.Destroy((Object) ((Component) playerManager.nicknameObject).gameObject);
        }
      }
      GameManager.players.Clear();
      if (WorldManager.instance.worldLayers != null)
      {
        foreach (WorldManager.Block[,] worldLayer in WorldManager.instance.worldLayers)
        {
          if (worldLayer != null)
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
      }
      foreach (DropShadow dropShadow in GameManager.instance.dropShadows)
        Object.Destroy((Object) dropShadow.shadowGameobject);
      GameManager.instance.dropShadows.Clear();
      foreach (DroppedItem droppedItem in WorldManager.instance.droppedItems.Values)
        Object.Destroy((Object) droppedItem.gameObject);
      WorldManager.instance.droppedItems.Clear();
      foreach (WorldManager.BreakingAnimationFrame breakingAnimation in WorldManager.instance.breakingAnimations)
        Object.Destroy((Object) breakingAnimation.gameObject);
      foreach (ChatBubble chatBubble in UIController.instance.chatBubbles)
        Object.Destroy((Object) chatBubble.gameObject);
      foreach (Slot slot in InventoryManager.instance.slots)
        Object.Destroy((Object) slot.gameObject);
      if (Object.op_Inequality((Object) MovementManager.instance, (Object) null) && Object.op_Inequality((Object) MovementManager.instance.currentSignBar, (Object) null))
      {
        MovementManager.instance.isReadingSign = false;
        Object.Destroy((Object) MovementManager.instance.currentSignBar);
        MovementManager.instance.currentSignBar = (GameObject) null;
      }
      InventoryManager.instance.slots.Clear();
      UIController.instance.chatBubbles.Clear();
      UIController.instance.SetActive(UIController.instance.select);
      WorldManager.instance.breakingAnimations.Clear();
      WorldManager.instance.signs.Clear();
      UIController.instance.SetActive(UIController.instance.MenuScreen, true);
      ((Selectable) UIController.instance.enterWorldButton).interactable = true;
      UIController.instance.worldMenu.SetActive(false);
      UIController.instance.chat.text = "";
      this.tcp = new Client.TCP();
      this.udp = new Client.UDP();
      UIController.instance.CreateNotificationFunction("<color=red>Error", "Cannot connect to the server,\ntrying again!", 5f);
      this.ConnectToServer();
      this.StartCoroutine(this.checkIfConnected());
    }));
  }

  public void Log(string text) => Debug.Log((object) text);

  private delegate void PacketHandler(Packet _packet);

  public class TCP
  {
    public TcpClient socket;
    private NetworkStream stream;
    private Packet receivedData;
    private byte[] receiveBuffer;

    public void Connect()
    {
      this.socket = new TcpClient()
      {
        ReceiveBufferSize = Client.dataBufferSize,
        SendBufferSize = Client.dataBufferSize
      };
      this.receiveBuffer = new byte[Client.dataBufferSize];
      this.socket.BeginConnect(Client.instance.ip, Client.instance.port, new AsyncCallback(this.ConnectCallback), (object) this.socket);
    }

    private void ConnectCallback(IAsyncResult _result)
    {
      this.socket.EndConnect(_result);
      if (!this.socket.Connected)
        return;
      this.stream = this.socket.GetStream();
      this.receivedData = new Packet();
      this.stream.BeginRead(this.receiveBuffer, 0, Client.dataBufferSize, new AsyncCallback(this.ReceiveCallback), (object) null);
    }

    public void SendData(Packet _packet)
    {
      try
      {
        if (this.socket == null)
          return;
        this.stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), (AsyncCallback) null, (object) null);
      }
      catch (Exception ex)
      {
        Debug.Log((object) string.Format("Error sending data to server via TCP: {0}", (object) ex));
      }
    }

    private void ReceiveCallback(IAsyncResult _result)
    {
      try
      {
        int length = this.stream.EndRead(_result);
        if (length <= 0)
        {
          Client.instance.Disconnect();
        }
        else
        {
          byte[] numArray = new byte[length];
          Array.Copy((Array) this.receiveBuffer, (Array) numArray, length);
          this.receivedData.Reset(this.HandleData(numArray));
          this.stream.BeginRead(this.receiveBuffer, 0, Client.dataBufferSize, new AsyncCallback(this.ReceiveCallback), (object) null);
        }
      }
      catch
      {
        this.Disconnect();
      }
    }

    private bool HandleData(byte[] _data)
    {
      int _length = 0;
      this.receivedData.SetBytes(_data);
      if (this.receivedData.UnreadLength() >= 4)
      {
        _length = this.receivedData.ReadInt();
        if (_length <= 0)
          return true;
      }
      while (_length > 0 && _length <= this.receivedData.UnreadLength())
      {
        byte[] _packetBytes = this.receivedData.ReadBytes(_length);
        ThreadManager.ExecuteOnMainThread((Action) (() =>
        {
          using (Packet _packet = new Packet(_packetBytes))
          {
            int key = _packet.ReadInt();
            try
            {
              Client.packetHandlers[key](_packet);
            }
            catch (Exception ex)
            {
              Debug.Log((object) ex.ToString());
            }
          }
        }));
        _length = 0;
        if (this.receivedData.UnreadLength() >= 4)
        {
          _length = this.receivedData.ReadInt();
          if (_length <= 0)
            return true;
        }
      }
      return _length <= 1;
    }

    private void Disconnect()
    {
      Client.instance.Disconnect();
      this.stream = (NetworkStream) null;
      this.receivedData = (Packet) null;
      this.receiveBuffer = (byte[]) null;
      this.socket = (TcpClient) null;
    }
  }

  public class UDP
  {
    public UdpClient socket;
    public IPEndPoint endPoint;

    public UDP() => this.endPoint = new IPEndPoint(IPAddress.Parse(Client.instance.ip), Client.instance.port);

    public void Connect(int _localPort)
    {
      this.socket = new UdpClient(_localPort);
      this.socket.Connect(this.endPoint);
      this.socket.BeginReceive(new AsyncCallback(this.ReceiveCallback), (object) null);
      using (Packet _packet = new Packet())
        this.SendData(_packet);
    }

    public void SendData(Packet _packet)
    {
      try
      {
        _packet.InsertInt(Client.instance.myId);
        if (this.socket == null)
          return;
        this.socket.BeginSend(_packet.ToArray(), _packet.Length(), (AsyncCallback) null, (object) null);
      }
      catch (Exception ex)
      {
        Debug.Log((object) string.Format("Error sending data to server via UDP: {0}", (object) ex));
      }
    }

    private void ReceiveCallback(IAsyncResult _result)
    {
      try
      {
        byte[] _data = this.socket.EndReceive(_result, ref this.endPoint);
        this.socket.BeginReceive(new AsyncCallback(this.ReceiveCallback), (object) null);
        if (_data.Length < 4)
          Client.instance.Disconnect();
        else
          this.HandleData(_data);
      }
      catch
      {
        this.Disconnect();
      }
    }

    private void HandleData(byte[] _data)
    {
      using (Packet packet = new Packet(_data))
      {
        int _length = packet.ReadInt();
        _data = packet.ReadBytes(_length);
      }
      ThreadManager.ExecuteOnMainThread((Action) (() =>
      {
        using (Packet _packet = new Packet(_data))
        {
          int key = _packet.ReadInt();
          Client.packetHandlers[key](_packet);
        }
      }));
    }

    private void Disconnect()
    {
      Client.instance.Disconnect();
      this.endPoint = (IPEndPoint) null;
      this.socket = (UdpClient) null;
    }
  }
}
