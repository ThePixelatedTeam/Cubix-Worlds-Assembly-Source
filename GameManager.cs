// Decompiled with JetBrains decompiler
// Type: GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public string username;
  public static GameManager instance;
  public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
  [SerializeField]
  public List<Item> items = new List<Item>();
  public PhysicsMaterial2D physicsMaterial2D;
  public string textureURL;
  public Texture2D itemsTexture;
  public int bTPixelSize = 32;
  public List<Sprite> blockSprites = new List<Sprite>();
  public List<Sprite> breakingAnimationSprites = new List<Sprite>();
  public List<DropShadow> dropShadows = new List<DropShadow>();
  public GameObject playerPrefab;
  public TextMeshProUGUI nicknamePrefab;
  public float r;
  public float g;
  public float b;
  public float rainbowSpeed = 0.5f;
  public long cubix;
  private Vector2 lastClickedBlock;
  public float delay = 0.3f;
  private float nextTime;
  private float playerClickDelay = 0.2f;
  private float playerClickNextTime;
  private string lastUsername;
  public GameObject effect;
  public GameObject effects;
  public GameObject grids;
  public bool isMovementFreeze;
  public Dictionary<string, GameObject> friends = new Dictionary<string, GameObject>();
  public List<Quest> quests = new List<Quest>();
  public Sprite[] achievementSprites;
  public List<Achievement> achievements = new List<Achievement>();
  public float powerMultiplier = 1f;
  public int[] currentRequirements;
  public Dictionary<PlayerManager, GameObject> pets = new Dictionary<PlayerManager, GameObject>();
  public GameObject pet;
  public GameObject petsContainer;
  public Dictionary<int, Category> categories = new Dictionary<int, Category>();
  public Dictionary<int, SaleItem> saleItems = new Dictionary<int, SaleItem>();
  public Dictionary<int, SaleItem> mySaleItems = new Dictionary<int, SaleItem>();
  public string[] types = new string[11]
  {
    "Block",
    "Prop",
    "Hair",
    "Shirt",
    "Pant",
    "Shoes",
    "Wings",
    "Mask",
    "Hand Item",
    "Face",
    "Pet"
  };
  public string[] accountTypes = new string[14]
  {
    "Beginner",
    "Player",
    "Verified",
    "Content Creator",
    "Moderator",
    "Admin",
    "Supporter",
    "Producer",
    "Marked",
    "Experienced",
    "Way More Experienced",
    "Professional",
    "God Level",
    "Beyond Universe"
  };
  public Dictionary<int, Effect> gameEffects = new Dictionary<int, Effect>();
  public Dictionary<int, Craft> recipes = new Dictionary<int, Craft>();
  public Dictionary<int, EffectType> effectTypes = new Dictionary<int, EffectType>();
  public GameObject wrenchIconContainer;
  public GameObject wrenchIcon;
  public List<GameObject> wrenchIcons = new List<GameObject>();
  public AdHandler adHandler;
  private bool isPunchButtonDown;

  private IEnumerator SetGameObjectActive(
    GameObject _gameObject,
    bool _activity,
    float _waitMS)
  {
    _gameObject.SetActive(!_activity);
    yield return (object) new WaitForSeconds(_waitMS);
    _gameObject.SetActive(_activity);
    if (Object.op_Inequality((Object) _gameObject.GetComponent<PlayerManager>(), (Object) null))
      _gameObject.GetComponent<PlayerManager>().ChangeSize(new Vector2(1f, 1f), true);
  }

  private IEnumerator PlaceSpriteFromWeb(Sprite _sprite, string _mediaUrl)
  {
    UnityWebRequest request = UnityWebRequestTexture.GetTexture(_mediaUrl);
    yield return (object) request.SendWebRequest();
    if (request.isNetworkError || request.isHttpError)
    {
      Debug.Log((object) request.error);
    }
    else
    {
      Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
      _sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) ((Texture) texture).width, (float) ((Texture) texture).height), new Vector2((float) (((Texture) texture).width / 2), (float) (((Texture) texture).height / 2)));
    }
  }

  private IEnumerator PlaceItemsFromWeb(string _mediaUrl)
  {
    UnityWebRequest request = UnityWebRequestTexture.GetTexture(_mediaUrl);
    yield return (object) request.SendWebRequest();
    if (request.isNetworkError || request.isHttpError)
    {
      Debug.Log((object) request.error);
    }
    else
    {
      this.itemsTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;
      ((Texture) this.itemsTexture).wrapMode = (TextureWrapMode) 0;
      ((Texture) this.itemsTexture).filterMode = (FilterMode) 0;
      this.InitializeTextures(this.itemsTexture);
    }
  }

  private void Awake()
  {
    if (Object.op_Equality((Object) GameManager.instance, (Object) null))
    {
      GameManager.instance = this;
    }
    else
    {
      if (!Object.op_Inequality((Object) GameManager.instance, (Object) this))
        return;
      Debug.Log((object) "Instance already exists, destroying object!");
      Object.Destroy((Object) this);
    }
  }

  public void InitializeTexturesFromWeb() => this.StartCoroutine(this.PlaceItemsFromWeb(this.textureURL));

  private void Start()
  {
    this.nextTime = Time.timeSinceLevelLoad + this.delay;
    this.playerClickNextTime = Time.timeSinceLevelLoad + this.playerClickDelay;
    // ISSUE: method pointer
    ((UnityEvent) ((Component) UIController.instance.questMenu.transform.GetChild(1).GetChild(3).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Eb__55_0)));
  }

  private void Update()
  {
    if (Input.GetKeyDown((KeyCode) 27) && !UIController.instance.MenuScreen.activeSelf)
      UIController.instance.SetActive(UIController.instance.inGameMenu);
    bool flag1 = false;
    if (Input.GetKeyDown((KeyCode) 325) && !this.IsPointerOverUIElement())
    {
      Vector3 mousePosition = Input.mousePosition;
      mousePosition.z = Camera.main.nearClipPlane;
      Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
      if ((double) worldPoint.x >= 0.0 && (double) worldPoint.x < (double) WorldManager.instance.worldWidth && (double) worldPoint.y >= 0.0)
      {
        if ((double) worldPoint.y < (double) WorldManager.instance.worldHeight)
        {
          try
          {
            int index = WorldManager.instance.worldLayers[0][(int) MathF.Round(worldPoint.x), (int) MathF.Round(worldPoint.y)].id == 0 ? WorldManager.instance.worldLayers[1][(int) MathF.Round(worldPoint.x), (int) MathF.Round(worldPoint.y)].id : WorldManager.instance.worldLayers[0][(int) MathF.Round(worldPoint.x), (int) MathF.Round(worldPoint.y)].id;
            bool flag2 = false;
            foreach (Slot slot in InventoryManager.instance.slots)
            {
              if (slot.item.id == index)
              {
                WorldManager.instance.currentBlockToPlaceID = index;
                InventoryManager.instance.indicator.SetActive(true);
                InventoryManager.instance.slotToFollow = slot.gameObject;
                if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].typeID != 1)
                {
                  if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isHarvestable)
                    PutGridManager.instance.SetGridOn();
                  else
                    PutGridManager.instance.SetGridOff();
                }
                else
                  PutGridManager.instance.SetGridOn();
                UIController.instance.CreateTextEffect(string.Format("Name : {0}\nID : {1}", (object) GameManager.instance.items[index].name, (object) index), new Vector2((float) (int) MathF.Round(worldPoint.x), (float) (int) MathF.Round(worldPoint.y)));
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              UIController.instance.CreateTextEffect("You do not have any <color=red>" + GameManager.instance.items[index].name + "!", new Vector2((float) (int) MathF.Round(worldPoint.x), (float) (int) MathF.Round(worldPoint.y)));
          }
          catch
          {
          }
        }
      }
    }
    if (Input.GetKeyDown((KeyCode) 102) && !this.IsPointerOverUIElement())
    {
      Vector3 mousePosition = Input.mousePosition;
      mousePosition.z = Camera.main.nearClipPlane;
      Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
      if ((double) worldPoint.x >= 0.0 && (double) worldPoint.x < (double) WorldManager.instance.worldWidth && (double) worldPoint.y >= 0.0)
      {
        if ((double) worldPoint.y < (double) WorldManager.instance.worldHeight)
        {
          try
          {
            int index = WorldManager.instance.worldLayers[0][(int) MathF.Round(worldPoint.x), (int) MathF.Round(worldPoint.y)].id == 0 ? WorldManager.instance.worldLayers[1][(int) MathF.Round(worldPoint.x), (int) MathF.Round(worldPoint.y)].id : WorldManager.instance.worldLayers[0][(int) MathF.Round(worldPoint.x), (int) MathF.Round(worldPoint.y)].id;
            UIController.instance.CreateTextEffect(string.Format("Name : {0}\nID : {1}", (object) GameManager.instance.items[index].name, (object) index), new Vector2((float) (int) MathF.Round(worldPoint.x), (float) (int) MathF.Round(worldPoint.y)));
          }
          catch
          {
          }
        }
      }
    }
    if (Input.GetMouseButtonDown(0) && !this.IsPointerOverUIElement())
    {
      if (WorldManager.instance.currentBlockToPlaceID == 8)
      {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        foreach (PlayerManager playerManager in GameManager.players.Values)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GameManager.\u003C\u003Ec__DisplayClass56_0 cDisplayClass560 = new GameManager.\u003C\u003Ec__DisplayClass56_0();
          // ISSUE: reference to a compiler-generated field
          cDisplayClass560._player = playerManager;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if ((double) worldPoint.x < (double) ((Component) cDisplayClass560._player).gameObject.transform.position.x + (double) Mathf.Abs(((Component) cDisplayClass560._player).gameObject.transform.localScale.x) / 2.0 && (double) worldPoint.x > (double) ((Component) cDisplayClass560._player).gameObject.transform.position.x - (double) Mathf.Abs(((Component) cDisplayClass560._player).gameObject.transform.localScale.x) / 2.0 && (double) worldPoint.y > (double) ((Component) cDisplayClass560._player).gameObject.transform.position.y - (double) Mathf.Abs(((Component) cDisplayClass560._player).gameObject.transform.localScale.y) / 2.0 && (double) worldPoint.y < (double) ((Component) cDisplayClass560._player).gameObject.transform.position.y + (double) Mathf.Abs(((Component) cDisplayClass560._player).gameObject.transform.localScale.y) / 2.0)
          {
            UIController.instance.SetActive(UIController.instance.darkScreen);
            UIController.instance.SetActive(UIController.instance.playerMenu);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = this.accountTypes[cDisplayClass560._player.accountLevel] + " <sprite=" + cDisplayClass560._player.accountLevel.ToString() + ">";
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass560._player.username + " <sprite=" + cDisplayClass560._player.accountLevel.ToString() + ">";
            // ISSUE: reference to a compiler-generated field
            ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass560._player.bio;
            // ISSUE: reference to a compiler-generated field
            ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(3).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "Level " + cDisplayClass560._player.level.ToString() + " <sprite=0>";
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(4).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass560._player.currentXP.ToString() + "xp/" + cDisplayClass560._player.maxXP.ToString() + "xp";
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(4).GetChild(0)).GetComponent<Slider>().value = (float) ((double) Math.Clamp((float) cDisplayClass560._player.currentXP, 0.01f, (float) int.MaxValue) * 100.0 / (double) cDisplayClass560._player.maxXP / 100.0);
            ((UnityEventBase) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(0)).GetComponent<Button>().onClick).RemoveAllListeners();
            ((UnityEventBase) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
            ((UnityEventBase) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(2)).GetComponent<Button>().onClick).RemoveAllListeners();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            ((UnityEvent) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(2)).GetComponent<Button>().onClick).AddListener(GameManager.\u003C\u003Ec.\u003C\u003E9__56_0 ?? (GameManager.\u003C\u003Ec.\u003C\u003E9__56_0 = new UnityAction((object) GameManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdate\u003Eb__56_0))));
            // ISSUE: method pointer
            ((UnityEvent) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass560, __methodptr(\u003CUpdate\u003Eb__1)));
            // ISSUE: method pointer
            ((UnityEvent) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass560, __methodptr(\u003CUpdate\u003Eb__2)));
            break;
          }
        }
      }
    }
    else if (Input.GetKeyDown((KeyCode) 32) && !UIController.instance.isChatOn && !GameManager.instance.isMovementFreeze && WorldManager.instance.currentBlockToPlaceID == 8)
    {
      Vector3 vector3 = Vector2.op_Implicit(new Vector2(((Component) GameManager.players[Client.instance.myId]).transform.position.x + ((Component) GameManager.players[Client.instance.myId]).transform.localScale.x, ((Component) GameManager.players[Client.instance.myId]).transform.position.y));
      foreach (PlayerManager playerManager in GameManager.players.Values)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GameManager.\u003C\u003Ec__DisplayClass56_1 cDisplayClass561 = new GameManager.\u003C\u003Ec__DisplayClass56_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass561._player = playerManager;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if ((double) vector3.x < (double) ((Component) cDisplayClass561._player).gameObject.transform.position.x + (double) Mathf.Abs(((Component) cDisplayClass561._player).gameObject.transform.localScale.x) / 2.0 && (double) vector3.x > (double) ((Component) cDisplayClass561._player).gameObject.transform.position.x - (double) Mathf.Abs(((Component) cDisplayClass561._player).gameObject.transform.localScale.x) / 2.0 && (double) vector3.y > (double) ((Component) cDisplayClass561._player).gameObject.transform.position.y - (double) Mathf.Abs(((Component) cDisplayClass561._player).gameObject.transform.localScale.y) / 2.0 && (double) vector3.y < (double) ((Component) cDisplayClass561._player).gameObject.transform.position.y + (double) Mathf.Abs(((Component) cDisplayClass561._player).gameObject.transform.localScale.y) / 2.0)
        {
          UIController.instance.SetActive(UIController.instance.darkScreen);
          UIController.instance.SetActive(UIController.instance.playerMenu);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = this.accountTypes[cDisplayClass561._player.accountLevel] + " <sprite=" + cDisplayClass561._player.accountLevel.ToString() + ">";
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass561._player.username + " <sprite=" + cDisplayClass561._player.accountLevel.ToString() + ">";
          // ISSUE: reference to a compiler-generated field
          ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass561._player.bio;
          // ISSUE: reference to a compiler-generated field
          ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(3).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "Level " + cDisplayClass561._player.level.ToString() + " <sprite=0>";
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ((TMP_Text) ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(4).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass561._player.currentXP.ToString() + "xp/" + cDisplayClass561._player.maxXP.ToString() + "xp";
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ((Component) UIController.instance.playerMenu.transform.GetChild(0).GetChild(4).GetChild(0)).GetComponent<Slider>().value = (float) ((double) Math.Clamp((float) cDisplayClass561._player.currentXP, 0.01f, (float) int.MaxValue) * 100.0 / (double) cDisplayClass561._player.maxXP / 100.0);
          ((UnityEventBase) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(0)).GetComponent<Button>().onClick).RemoveAllListeners();
          ((UnityEventBase) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
          ((UnityEventBase) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(2)).GetComponent<Button>().onClick).RemoveAllListeners();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(2)).GetComponent<Button>().onClick).AddListener(GameManager.\u003C\u003Ec.\u003C\u003E9__56_3 ?? (GameManager.\u003C\u003Ec.\u003C\u003E9__56_3 = new UnityAction((object) GameManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdate\u003Eb__56_3))));
          // ISSUE: method pointer
          ((UnityEvent) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass561, __methodptr(\u003CUpdate\u003Eb__4)));
          // ISSUE: method pointer
          ((UnityEvent) ((Component) UIController.instance.playerMenu.transform.GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass561, __methodptr(\u003CUpdate\u003Eb__5)));
          break;
        }
      }
    }
    if (flag1)
      return;
    if (Input.GetMouseButton(0) && !this.IsPointerOverUIElement() && WorldManager.instance.worldName != "")
    {
      Vector3 mousePosition = Input.mousePosition;
      mousePosition.z = Camera.main.nearClipPlane;
      this.HandleClick(Camera.main.ScreenToWorldPoint(mousePosition));
    }
    else
    {
      if (!Input.GetKey((KeyCode) 32) && !this.isPunchButtonDown || UIController.instance.isChatOn || GameManager.instance.isMovementFreeze || !(WorldManager.instance.worldName != ""))
        return;
      this.HandleClick(Vector2.op_Implicit(new Vector2(((Component) GameManager.players[Client.instance.myId]).transform.position.x + ((Component) GameManager.players[Client.instance.myId]).transform.localScale.x, ((Component) GameManager.players[Client.instance.myId]).transform.position.y)));
    }
  }

  public void HandleClick(Vector3 _mouseWorldPosition)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GameManager.\u003C\u003Ec__DisplayClass57_0 cDisplayClass570 = new GameManager.\u003C\u003Ec__DisplayClass57_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass570._mouseWorldPosition = _mouseWorldPosition;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass570.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (WorldManager.instance.currentBlockToPlaceID != 7 && !Vector2.op_Inequality(new Vector2(Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)), new Vector2(Mathf.Round(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x), Mathf.Round(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.y))))
      return;
    if (WorldManager.instance.currentBlockToPlaceID != 8 && !GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isBait && (double) Time.timeSinceLevelLoad > (double) this.nextTime)
    {
      if (!UIController.instance.isHoldingLeftButton && !UIController.instance.isHoldingRightButton && !UIController.instance.isHoldingJumpButton)
      {
        AudioManager.instance.PlayAudio("Punch");
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), (int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)].id].isDoor && Vector2.op_Equality(new Vector2(Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)), new Vector2(Mathf.Round(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x), Mathf.Round(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.y))))
        {
          // ISSUE: reference to a compiler-generated method
          Action action = new Action(cDisplayClass570.\u003CHandleClick\u003Eb__0);
          UIController.instance.CreateDialog("Are you sure?", "You will teleport, are you sure?", new string[2]
          {
            "Accept",
            "Cancel"
          }, new int[2]{ 0, 1 }, new Action[2]
          {
            action,
            null
          });
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.lastClickedBlock = new Vector2(cDisplayClass570._mouseWorldPosition.x, cDisplayClass570._mouseWorldPosition.y);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (WorldManager.instance.worldLayers[0][(int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), (int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)].id != 0 || WorldManager.instance.worldLayers[1][(int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), (int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)].id != 0)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            GameManager.instance.CreateEffect(5, new Vector2(Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)));
          }
          if (GameManager.players[Client.instance.myId].animationSystem.currentID != 4)
          {
            GameManager.players[Client.instance.myId].animationSystem.currentID = 4;
            ClientSend.ChangeAnimation(4);
          }
          // ISSUE: reference to a compiler-generated field
          if ((double) cDisplayClass570._mouseWorldPosition.x > (double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x)
            ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.x), ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.y));
          else
            ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(-Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.x), ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.y));
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ClientSend.EditWorldData(WorldManager.instance.currentBlockToPlaceID, new Vector2(cDisplayClass570._mouseWorldPosition.x + 0.5f, cDisplayClass570._mouseWorldPosition.y + 0.5f));
        }
      }
      this.nextTime = Time.timeSinceLevelLoad + this.delay;
    }
    else if (WorldManager.instance.currentBlockToPlaceID == 8 && (double) Time.timeSinceLevelLoad > (double) this.nextTime)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if ((double) cDisplayClass570._mouseWorldPosition.x > Math.Round((double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x) + 4.0 || (double) cDisplayClass570._mouseWorldPosition.x < Math.Round((double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x) - 3.0 || (double) cDisplayClass570._mouseWorldPosition.y > (double) MathF.Round(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.y) + 4.0 || (double) cDisplayClass570._mouseWorldPosition.y < Math.Round((double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.y) - 3.0)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      Vector2 vector2 = new Vector2(cDisplayClass570._mouseWorldPosition.x + 0.5f, cDisplayClass570._mouseWorldPosition.y + 0.5f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isSign && GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isWrenchable && WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id != 93 && !GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isDoor && !GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isDealer && WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id != 148)
      {
        this.isMovementFreeze = true;
        UIController.instance.SetActive(UIController.instance.signDialogBar, true);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (WorldManager.instance.signs.ContainsKey(new Vector2((float) (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (float) (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y))))
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ((Component) UIController.instance.signDialogBar.transform.GetChild(1).GetChild(2)).GetComponent<TMP_InputField>().text = WorldManager.instance.signs[new Vector2((float) (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (float) (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y))];
        }
        ((UnityEventBase) ((Component) UIController.instance.signDialogBar.transform.GetChild(1).GetChild(3).GetChild(0)).GetComponent<Button>().onClick).RemoveAllListeners();
        // ISSUE: method pointer
        ((UnityEvent) ((Component) UIController.instance.signDialogBar.transform.GetChild(1).GetChild(3).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass570, __methodptr(\u003CHandleClick\u003Eb__1)));
        ((UnityEventBase) ((Component) UIController.instance.signDialogBar.transform.GetChild(1).GetChild(3).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
        // ISSUE: method pointer
        ((UnityEvent) ((Component) UIController.instance.signDialogBar.transform.GetChild(1).GetChild(3).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass570, __methodptr(\u003CHandleClick\u003Eb__2)));
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isEntrance)
        {
          this.isMovementFreeze = true;
          // ISSUE: reference to a compiler-generated method
          Action action1 = new Action(cDisplayClass570.\u003CHandleClick\u003Eb__7);
          // ISSUE: reference to a compiler-generated method
          Action action2 = new Action(cDisplayClass570.\u003CHandleClick\u003Eb__8);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (WorldManager.instance.entrances[new Vector2((float) (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (float) (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y))])
            UIController.instance.CreateDialog("Entrance Settings", "Door is currently <color=red>private</color>.", new string[2]
            {
              "Public",
              "Cancel"
            }, new int[2]{ 0, 1 }, new Action[2]
            {
              action2,
              null
            });
          else
            UIController.instance.CreateDialog("Entrance Settings", "Door is currently <color=green>public</color>.", new string[2]
            {
              "Private",
              "Cancel"
            }, new int[2]{ 0, 1 }, new Action[2]
            {
              action1,
              null
            });
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id == 93)
          {
            // ISSUE: reference to a compiler-generated method
            Action action = new Action(cDisplayClass570.\u003CHandleClick\u003Eb__9);
            UIController.instance.CreateDialog("Win Cubix", "Do you want to watch a video to win 500 Cubix?", new string[2]
            {
              "Accept",
              "Cancel"
            }, new int[2]{ 0, 1 }, new Action[2]
            {
              action,
              null
            });
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isDoor)
            {
              this.isMovementFreeze = true;
              UIController.instance.SetActive(UIController.instance.DoorDialogBar, true);
              ((UnityEventBase) ((Component) UIController.instance.DoorDialogBar.transform.GetChild(1).GetChild(5).GetChild(0)).GetComponent<Button>().onClick).RemoveAllListeners();
              // ISSUE: method pointer
              ((UnityEvent) ((Component) UIController.instance.DoorDialogBar.transform.GetChild(1).GetChild(5).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass570, __methodptr(\u003CHandleClick\u003Eb__3)));
              ((UnityEventBase) ((Component) UIController.instance.DoorDialogBar.transform.GetChild(1).GetChild(5).GetChild(1)).GetComponent<Button>().onClick).RemoveAllListeners();
              // ISSUE: method pointer
              ((UnityEvent) ((Component) UIController.instance.DoorDialogBar.transform.GetChild(1).GetChild(5).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass570, __methodptr(\u003CHandleClick\u003Eb__4)));
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id == 148)
              {
                try
                {
                  this.isMovementFreeze = true;
                  UIController.instance.SetActive(UIController.instance.craftingMenu, true);
                  ((UnityEventBase) ((Component) UIController.instance.craftingMenu.transform.GetChild(5).GetChild(0)).GetComponent<Button>().onClick).RemoveAllListeners();
                  // ISSUE: method pointer
                  ((UnityEvent) ((Component) UIController.instance.craftingMenu.transform.GetChild(5).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass570, __methodptr(\u003CHandleClick\u003Eb__5)));
                  ((UnityEventBase) ((Component) UIController.instance.craftingMenu.transform.GetChild(5).GetChild(2)).GetComponent<Button>().onClick).RemoveAllListeners();
                  // ISSUE: method pointer
                  ((UnityEvent) ((Component) UIController.instance.craftingMenu.transform.GetChild(5).GetChild(2)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass570, __methodptr(\u003CHandleClick\u003Eb__6)));
                  for (int index = 0; index < ((Component) UIController.instance.craftingMenu.transform.GetChild(3).GetChild(2)).transform.childCount; ++index)
                  {
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: variable of a compiler-generated type
                    GameManager.\u003C\u003Ec__DisplayClass57_1 cDisplayClass571 = new GameManager.\u003C\u003Ec__DisplayClass57_1();
                    // ISSUE: reference to a compiler-generated field
                    cDisplayClass571.CS\u0024\u003C\u003E8__locals1 = cDisplayClass570;
                    // ISSUE: reference to a compiler-generated field
                    cDisplayClass571._x = index;
                    // ISSUE: reference to a compiler-generated field
                    cDisplayClass571._slot = UIController.instance.craftingMenu.transform.GetChild(3).GetChild(2).GetChild(index);
                    // ISSUE: reference to a compiler-generated field
                    ((UnityEventBase) ((Component) cDisplayClass571._slot).gameObject.GetComponent<Button>().onClick).RemoveAllListeners();
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: method pointer
                    ((UnityEvent) ((Component) cDisplayClass571._slot).gameObject.GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass571, __methodptr(\u003CHandleClick\u003Eb__12)));
                  }
                }
                catch (Exception ex)
                {
                  Debug.Log((object) ex.ToString());
                }
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                if (WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id == 149)
                {
                  try
                  {
                    this.isMovementFreeze = true;
                    UIController.instance.SetActive(UIController.instance.questMenu, true);
                  }
                  catch (Exception ex)
                  {
                    Debug.Log((object) ex.ToString());
                  }
                }
                else
                {
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  if (GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].isDealer)
                  {
                    UIController.instance.SetActive(UIController.instance.dealerMenu, true);
                    foreach (Component component in UIController.instance.dealContainer.transform)
                      Object.Destroy((Object) component.gameObject);
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    ((TMP_Text) ((Component) UIController.instance.dealerMenu.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].deals.Length.ToString() + " Item(s) Available <sprite=8>";
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    ((TMP_Text) ((Component) UIController.instance.dealerMenu.transform.GetChild(1).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].name;
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    foreach (Deal deal in GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].deals)
                    {
                      // ISSUE: object of a compiler-generated type is created
                      // ISSUE: variable of a compiler-generated type
                      GameManager.\u003C\u003Ec__DisplayClass57_3 cDisplayClass573 = new GameManager.\u003C\u003Ec__DisplayClass57_3();
                      // ISSUE: reference to a compiler-generated field
                      cDisplayClass573.CS\u0024\u003C\u003E8__locals3 = cDisplayClass570;
                      // ISSUE: reference to a compiler-generated field
                      cDisplayClass573._deal = deal;
                      GameObject gameObject = Object.Instantiate<GameObject>(UIController.instance.deal, UIController.instance.dealContainer.transform);
                      // ISSUE: reference to a compiler-generated field
                      ((Component) gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[cDisplayClass573._deal.dealItemID].texture;
                      // ISSUE: reference to a compiler-generated field
                      ((TMP_Text) ((Component) gameObject.transform.GetChild(1).GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = GameManager.instance.items[cDisplayClass573._deal.dealItemID].name;
                      // ISSUE: reference to a compiler-generated field
                      ((TMP_Text) ((Component) gameObject.transform.GetChild(1).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass573._deal.dealItemQuantity.ToString();
                      // ISSUE: reference to a compiler-generated field
                      ((TMP_Text) ((Component) gameObject.transform.GetChild(2).GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass573._deal.dealReward.ToString();
                      ((UnityEventBase) ((Component) gameObject.transform.GetChild(0)).GetComponent<Button>().onClick).RemoveAllListeners();
                      // ISSUE: method pointer
                      ((UnityEvent) ((Component) gameObject.transform.GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass573, __methodptr(\u003CHandleClick\u003Eb__16)));
                    }
                  }
                  else
                  {
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    this.lastClickedBlock = new Vector2(cDisplayClass570._mouseWorldPosition.x, cDisplayClass570._mouseWorldPosition.y);
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    if (WorldManager.instance.worldLayers[0][(int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), (int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)].id != 0 || WorldManager.instance.worldLayers[1][(int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), (int) Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)].id != 0)
                    {
                      // ISSUE: reference to a compiler-generated field
                      // ISSUE: reference to a compiler-generated field
                      GameManager.instance.CreateEffect(5, new Vector2(Mathf.Floor(cDisplayClass570._mouseWorldPosition.x + 0.5f), Mathf.Floor(cDisplayClass570._mouseWorldPosition.y + 0.5f)));
                    }
                    // ISSUE: reference to a compiler-generated field
                    if ((double) cDisplayClass570._mouseWorldPosition.x > (double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x)
                      ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.x), ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.y));
                    else
                      ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(-Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.x), ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.localScale.y));
                    if (GameManager.players[Client.instance.myId].animationSystem.currentID != 4)
                    {
                      GameManager.players[Client.instance.myId].animationSystem.currentID = 4;
                      ClientSend.ChangeAnimation(4);
                    }
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    ClientSend.EditWorldData(WorldManager.instance.currentBlockToPlaceID, new Vector2(cDisplayClass570._mouseWorldPosition.x + 0.5f, cDisplayClass570._mouseWorldPosition.y + 0.5f));
                  }
                }
              }
            }
          }
        }
      }
      this.nextTime = Time.timeSinceLevelLoad + this.delay;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isBait || (double) Time.timeSinceLevelLoad <= (double) this.nextTime || (double) cDisplayClass570._mouseWorldPosition.x > Math.Round((double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x) + 4.0 || (double) cDisplayClass570._mouseWorldPosition.x < Math.Round((double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.x) - 3.0 || (double) cDisplayClass570._mouseWorldPosition.y > (double) MathF.Round(((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.y) + 4.0 || (double) cDisplayClass570._mouseWorldPosition.y < Math.Round((double) ((Component) GameManager.players[Client.instance.myId]).gameObject.transform.position.y) - 3.0)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (GameManager.instance.items[GameManager.players[Client.instance.myId].clothes[6]].isRod && GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isBait && GameManager.instance.items[WorldManager.instance.worldLayers[0][(int) Mathf.Round(cDisplayClass570._mouseWorldPosition.x), (int) Mathf.Round(cDisplayClass570._mouseWorldPosition.y)].id].waterPhysics)
      {
        if (GameManager.players[Client.instance.myId].animationSystem.currentID != 4)
        {
          GameManager.players[Client.instance.myId].animationSystem.currentID = 4;
          ClientSend.ChangeAnimation(4);
        }
        int currentBlockToPlaceId = WorldManager.instance.currentBlockToPlaceID;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        Vector2 _blockPosition = new Vector2(MathF.Round(cDisplayClass570._mouseWorldPosition.x), MathF.Round(cDisplayClass570._mouseWorldPosition.y));
        Rect rect = ((RectTransform) UIController.instance.fishingContainer.transform).rect;
        double width = (double) ((Rect) ref rect).width;
        ClientSend.FishingAction(0, currentBlockToPlaceId, _blockPosition, (float) width);
      }
      this.nextTime = Time.timeSinceLevelLoad + this.delay;
    }
  }

  public void SetActive(GameObject _gameObject, bool _activity, float _secondsLater) => this.StartCoroutine(this.SetGameObjectActive(_gameObject, _activity, _secondsLater));

  public void InitializeTextures(Texture2D bTexture)
  {
    for (int index1 = 0; index1 < ((Texture) bTexture).height / 32; ++index1)
    {
      for (int index2 = 0; index2 < ((Texture) bTexture).width / 32; ++index2)
      {
        Rect rect;
        // ISSUE: explicit constructor call
        ((Rect) ref rect).\u002Ector((float) (index2 * 32), (float) (((Texture) bTexture).height - (index1 + 1) * 32), 32f, 32f);
        this.blockSprites.Add(Sprite.Create(bTexture, rect, new Vector2(0.5f, 0.5f), 32f));
      }
    }
  }

  public void SpawnPlayer(
    int _id,
    string _username,
    int _accountLevel,
    int _skinID,
    Vector2 _position,
    Vector2 _size,
    Vector2 _collisionSize,
    int[] _costumeDataArray,
    string _bio,
    int _level,
    int _currentXP,
    int _maxXP)
  {
    if (GameManager.players.ContainsKey(_id))
      return;
    GameObject gameObject1;
    if (_id == Client.instance.myId)
    {
      gameObject1 = Object.Instantiate<GameObject>(this.playerPrefab);
      gameObject1.transform.position = Vector2.op_Implicit(_position);
      gameObject1.transform.localScale = Vector2.op_Implicit(_size);
      gameObject1.GetComponent<PlayerManager>().accountLevel = _accountLevel;
      gameObject1.GetComponent<PlayerManager>().ChangeAnimation(0);
      gameObject1.AddComponent<MovementManager>().checkIsGroundedLength = 0.1f;
      Rigidbody2D rigidbody2D = gameObject1.AddComponent<Rigidbody2D>();
      gameObject1.GetComponent<Rigidbody2D>().angularDrag = 0.0f;
      rigidbody2D.drag = 0.0f;
      rigidbody2D.sharedMaterial = this.physicsMaterial2D;
      rigidbody2D.constraints = (RigidbodyConstraints2D) 4;
      gameObject1.AddComponent<BoxCollider2D>().size = _collisionSize;
      ((Collider2D) gameObject1.GetComponent<BoxCollider2D>()).offset = new Vector2(0.0f, -0.04f);
      GameObject gameObject2 = Object.Instantiate<GameObject>(((Component) this.nicknamePrefab).gameObject);
      gameObject2.transform.SetParent(UIController.instance.nicknameCanvas.transform);
      gameObject1.GetComponent<PlayerManager>().nicknameObject = gameObject2.GetComponent<TextMeshProUGUI>();
      gameObject1.GetComponent<PlayerManager>().SetSkin(_skinID);
      ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = 1000;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(0)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(1)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(2)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(3)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(4)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder - 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(5)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder - 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(6)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 2;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(7)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 3;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(8)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 2;
      GameObject gameObject3 = Object.Instantiate<GameObject>(this.wrenchIcon, this.wrenchIconContainer.transform);
      gameObject3.SetActive(false);
      gameObject1.GetComponent<PlayerManager>().wrenchIcon = gameObject3;
      this.wrenchIcons.Add(gameObject3);
      CameraManager.instance.SetFollowObject(gameObject1);
    }
    else
    {
      gameObject1 = Object.Instantiate<GameObject>(this.playerPrefab);
      gameObject1.transform.position = Vector2.op_Implicit(_position);
      gameObject1.transform.localScale = Vector2.op_Implicit(_size);
      gameObject1.GetComponent<PlayerManager>().SetSkin(_skinID);
      gameObject1.GetComponent<PlayerManager>().ChangeAnimation(0);
      gameObject1.GetComponent<PlayerManager>().accountLevel = _accountLevel;
      GameObject gameObject4 = Object.Instantiate<GameObject>(((Component) this.nicknamePrefab).gameObject);
      gameObject4.transform.SetParent(UIController.instance.worldCanvas.transform);
      ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = (GameManager.players.Count + 1) * 20;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(0)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(1)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(2)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(3)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(4)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder - 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(5)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder - 1;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(6)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 2;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(7)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 3;
      ((Renderer) ((Component) gameObject1.gameObject.transform.GetChild(2).GetChild(8)).GetComponent<SpriteRenderer>()).sortingOrder = ((Renderer) gameObject1.gameObject.GetComponent<SpriteRenderer>()).sortingOrder + 2;
      GameObject gameObject5 = Object.Instantiate<GameObject>(this.wrenchIcon, this.wrenchIconContainer.transform);
      gameObject5.SetActive(false);
      gameObject1.GetComponent<PlayerManager>().wrenchIcon = gameObject5;
      this.wrenchIcons.Add(gameObject5);
      gameObject1.GetComponent<PlayerManager>().nicknameObject = gameObject4.GetComponent<TextMeshProUGUI>();
    }
    gameObject1.GetComponent<PlayerManager>().id = _id;
    gameObject1.GetComponent<PlayerManager>().bio = _bio;
    gameObject1.GetComponent<PlayerManager>().level = _level;
    gameObject1.GetComponent<PlayerManager>().currentXP = _currentXP;
    gameObject1.GetComponent<PlayerManager>().maxXP = _maxXP;
    gameObject1.GetComponent<PlayerManager>().username = _username;
    GameManager.players.Add(_id, gameObject1.GetComponent<PlayerManager>());
    int index1;
    for (int index2 = 0; index2 < _costumeDataArray.Length; ++index2)
    {
      if (this.items[_costumeDataArray[index2]].costumesToHide != null)
      {
        int[] costumesToHide = this.items[_costumeDataArray[index2]].costumesToHide;
        for (index1 = 0; index1 < costumesToHide.Length; ++index1)
        {
          int num = costumesToHide[index1];
          ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(num)).gameObject.SetActive(false);
        }
      }
      if (this.items[_costumeDataArray[index2]].typeID > 2)
      {
        if (_id == Client.instance.myId)
        {
          if (_costumeDataArray[index2] != 0)
          {
            if (InventoryManager.instance.FindSlotFromItemID(_costumeDataArray[index2]) != null)
              InventoryManager.instance.FindSlotFromItemID(_costumeDataArray[index2]).isUsed = true;
          }
          else if (InventoryManager.instance.FindSlotFromItemID(GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3]) != null)
            InventoryManager.instance.FindSlotFromItemID(_costumeDataArray[index2]).isUsed = false;
        }
        if (GameManager.instance.items[_costumeDataArray[index2]].typeID == 7)
        {
          ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[_costumeDataArray[index2]].texture;
          for (int index3 = 0; index3 < 5; ++index3)
          {
            for (int index4 = 0; index4 < ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index3].sprites.Count; ++index4)
            {
              string[] strArray = new string[6]
              {
                "Images/Textures/Clothe Animations/",
                _costumeDataArray[index2].ToString(),
                " ",
                index3.ToString(),
                " ",
                null
              };
              index1 = index4 + 1;
              strArray[5] = index1.ToString();
              Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
              if (Object.op_Inequality((Object) sprite, (Object) null))
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index3].sprites[index4] = sprite;
              else
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index3].sprites[index4] = GameManager.instance.blockSprites[this.items[_costumeDataArray[index2]].textureID];
            }
          }
          GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3] = _costumeDataArray[index2];
        }
        else if (GameManager.instance.items[GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3]].typeID == 7 && _costumeDataArray[index2] == 0)
        {
          ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[_costumeDataArray[index2]].texture;
          for (int index5 = 0; index5 < 5; ++index5)
          {
            for (int index6 = 0; index6 < ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index5].sprites.Count; ++index6)
            {
              string[] strArray = new string[6]
              {
                "Images/Textures/Clothe Animations/",
                _costumeDataArray[index2].ToString(),
                " ",
                index5.ToString(),
                " ",
                null
              };
              index1 = index6 + 1;
              strArray[5] = index1.ToString();
              Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
              if (Object.op_Inequality((Object) sprite, (Object) null))
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index5].sprites[index6] = sprite;
              else
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index5].sprites[index6] = GameManager.instance.blockSprites[this.items[_costumeDataArray[index2]].textureID];
            }
          }
          GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3] = _costumeDataArray[index2];
        }
        else if (GameManager.instance.items[_costumeDataArray[index2]].typeID == 11)
        {
          GameObject gameObject6 = Object.Instantiate<GameObject>(this.pet, this.petsContainer.transform);
          TextMeshProUGUI textMeshProUgui = Object.Instantiate<TextMeshProUGUI>(this.nicknamePrefab, UIController.instance.nicknameCanvas.transform);
          ((TMP_Text) textMeshProUgui).text = "<size=75%>" + GameManager.instance.items[_costumeDataArray[index2]].name.Split(' ', StringSplitOptions.None)[1];
          gameObject6.GetComponent<PetBehaivour>().title = textMeshProUgui;
          gameObject6.transform.position = gameObject1.transform.position;
          gameObject6.GetComponent<PetBehaivour>().player = gameObject1;
          gameObject6.GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[_costumeDataArray[index2]].texture;
          this.pets.Add(gameObject1.GetComponent<PlayerManager>(), gameObject6);
          for (int index7 = 0; index7 < gameObject6.GetComponent<AnimationSystem>().animations[0].sprites.Count; ++index7)
          {
            string[] strArray = new string[6]
            {
              "Images/Textures/Clothe Animations/",
              _costumeDataArray[index2].ToString(),
              " ",
              null,
              null,
              null
            };
            index1 = 0;
            strArray[3] = index1.ToString();
            strArray[4] = " ";
            index1 = index7 + 1;
            strArray[5] = index1.ToString();
            Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
            if (Object.op_Inequality((Object) sprite, (Object) null))
              gameObject6.GetComponent<AnimationSystem>().animations[0].sprites[index7] = sprite;
            else
              gameObject6.GetComponent<AnimationSystem>().animations[0].sprites[index7] = GameManager.instance.blockSprites[this.items[_costumeDataArray[index2]].textureID];
          }
          GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3] = _costumeDataArray[index2];
        }
        else if (GameManager.instance.items[_costumeDataArray[index2]].typeID - 3 >= 5)
        {
          ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3 + 1)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[_costumeDataArray[index2]].texture;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            for (int index9 = 0; index9 < ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3 + 1)).GetComponent<AnimationSystem>().animations[index8].sprites.Count; ++index9)
            {
              string[] strArray = new string[6]
              {
                "Images/Textures/Clothe Animations/",
                _costumeDataArray[index2].ToString(),
                " ",
                index8.ToString(),
                " ",
                null
              };
              index1 = index9 + 1;
              strArray[5] = index1.ToString();
              Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
              if (Object.op_Inequality((Object) sprite, (Object) null))
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3 + 1)).GetComponent<AnimationSystem>().animations[index8].sprites[index9] = sprite;
              else
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3 + 1)).GetComponent<AnimationSystem>().animations[index8].sprites[index9] = GameManager.instance.blockSprites[this.items[_costumeDataArray[index2]].textureID];
            }
          }
          GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3] = _costumeDataArray[index2];
        }
        else
        {
          ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<SpriteRenderer>().sprite = GameManager.instance.items[_costumeDataArray[index2]].texture;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            for (int index11 = 0; index11 < ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index10].sprites.Count; ++index11)
            {
              string[] strArray = new string[6]
              {
                "Images/Textures/Clothe Animations/",
                _costumeDataArray[index2].ToString(),
                " ",
                index10.ToString(),
                " ",
                null
              };
              index1 = index11 + 1;
              strArray[5] = index1.ToString();
              Sprite sprite = Resources.Load(string.Concat(strArray), typeof (Sprite)) as Sprite;
              if (Object.op_Inequality((Object) sprite, (Object) null))
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index10].sprites[index11] = sprite;
              else
                ((Component) ((Component) GameManager.players[_id]).gameObject.transform.GetChild(2).GetChild(GameManager.instance.items[_costumeDataArray[index2]].typeID - 3)).GetComponent<AnimationSystem>().animations[index10].sprites[index11] = GameManager.instance.blockSprites[this.items[_costumeDataArray[index2]].textureID];
            }
          }
          GameManager.players[_id].clothes[GameManager.instance.items[_costumeDataArray[index2]].typeID - 3] = _costumeDataArray[index2];
        }
      }
    }
    TextMeshProUGUI component = ((Component) UIController.instance.claimButton.transform.parent.GetChild(1)).GetComponent<TextMeshProUGUI>();
    index1 = GameManager.players.Count;
    string str = index1.ToString() + " <sprite=0>";
    ((TMP_Text) component).text = str;
  }

  public void CreateEffect(int _effectID, Vector2 _position)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.effect, this.effects.transform);
    gameObject.transform.position = Vector2.op_Implicit(_position);
    gameObject.GetComponent<EffectSystem>().PlayEffect(_effectID);
  }

  public void PunchButtonDown() => this.isPunchButtonDown = true;

  public void PunchButtonUp() => this.isPunchButtonDown = false;

  public void CreateEffect(int _effectID, Vector2 _position, int _timeToDestroy)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.effect, this.effects.transform);
    gameObject.transform.position = Vector2.op_Implicit(_position);
    gameObject.GetComponent<EffectSystem>().PlayEffect(_effectID, _timeToDestroy, false);
  }

  public void CreateEffect(int _effectID, Vector2 _position, Vector2 _size)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.effect, this.effects.transform);
    gameObject.transform.position = Vector2.op_Implicit(_position);
    gameObject.transform.localScale = Vector2.op_Implicit(_size);
    gameObject.GetComponent<EffectSystem>().PlayEffect(_effectID);
  }

  public void CreateEffect(int _effectID, Vector2 _position, Vector2 _size, int _timeToDestroy)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.effect, this.effects.transform);
    gameObject.transform.localScale = Vector2.op_Implicit(_size);
    gameObject.transform.position = Vector2.op_Implicit(_position);
    gameObject.GetComponent<EffectSystem>().PlayEffect(_effectID, _timeToDestroy, true);
  }

  public bool IsPointerOverUIElement() => this.IsPointerOverUIElement(GameManager.GetEventSystemRaycastResults());

  private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
  {
    for (int index = 0; index < eventSystemRaysastResults.Count; ++index)
    {
      RaycastResult systemRaysastResult = eventSystemRaysastResults[index];
      if (((RaycastResult) ref systemRaysastResult).gameObject.layer == 5)
        return true;
    }
    return false;
  }

  private static List<RaycastResult> GetEventSystemRaycastResults()
  {
    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
    pointerEventData.position = Vector2.op_Implicit(Input.mousePosition);
    List<RaycastResult> systemRaycastResults = new List<RaycastResult>();
    EventSystem.current.RaycastAll(pointerEventData, systemRaycastResults);
    return systemRaycastResults;
  }

  public Color RainbowColor()
  {
    if ((double) this.r > 0.0 && (double) this.g < (double) byte.MaxValue && (double) this.b == 0.0)
    {
      this.g += this.rainbowSpeed;
      if ((double) this.r > (double) byte.MaxValue)
        this.r = (float) byte.MaxValue;
      if ((double) this.g > (double) byte.MaxValue)
        this.g = (float) byte.MaxValue;
      if ((double) this.b > (double) byte.MaxValue)
        this.b = (float) byte.MaxValue;
      if ((double) this.r < 0.0)
        this.r = 0.0f;
      if ((double) this.g < 0.0)
        this.g = 0.0f;
      if ((double) this.b < 0.0)
        this.b = 0.0f;
    }
    else if ((double) this.r > 0.0 && (double) this.g == (double) byte.MaxValue && (double) this.b == 0.0)
    {
      this.r -= this.rainbowSpeed;
      if ((double) this.r > (double) byte.MaxValue)
        this.r = (float) byte.MaxValue;
      if ((double) this.g > (double) byte.MaxValue)
        this.g = (float) byte.MaxValue;
      if ((double) this.b > (double) byte.MaxValue)
        this.b = (float) byte.MaxValue;
      if ((double) this.r < 0.0)
        this.r = 0.0f;
      if ((double) this.g < 0.0)
        this.g = 0.0f;
      if ((double) this.b < 0.0)
        this.b = 0.0f;
    }
    else if ((double) this.r == 0.0 && (double) this.g == (double) byte.MaxValue && (double) this.b < (double) byte.MaxValue)
    {
      this.b += this.rainbowSpeed;
      if ((double) this.r > (double) byte.MaxValue)
        this.r = (float) byte.MaxValue;
      if ((double) this.g > (double) byte.MaxValue)
        this.g = (float) byte.MaxValue;
      if ((double) this.b > (double) byte.MaxValue)
        this.b = (float) byte.MaxValue;
      if ((double) this.r < 0.0)
        this.r = 0.0f;
      if ((double) this.g < 0.0)
        this.g = 0.0f;
      if ((double) this.b < 0.0)
        this.b = 0.0f;
    }
    else if ((double) this.r == 0.0 && (double) this.g > 0.0 && (double) this.b == (double) byte.MaxValue)
    {
      this.g -= this.rainbowSpeed;
      if ((double) this.r > (double) byte.MaxValue)
        this.r = (float) byte.MaxValue;
      if ((double) this.g > (double) byte.MaxValue)
        this.g = (float) byte.MaxValue;
      if ((double) this.b > (double) byte.MaxValue)
        this.b = (float) byte.MaxValue;
      if ((double) this.r < 0.0)
        this.r = 0.0f;
      if ((double) this.g < 0.0)
        this.g = 0.0f;
      if ((double) this.b < 0.0)
        this.b = 0.0f;
    }
    else if ((double) this.r < (double) byte.MaxValue && (double) this.g == 0.0 && (double) this.b == (double) byte.MaxValue)
    {
      this.r += this.rainbowSpeed;
      if ((double) this.r > (double) byte.MaxValue)
        this.r = (float) byte.MaxValue;
      if ((double) this.g > (double) byte.MaxValue)
        this.g = (float) byte.MaxValue;
      if ((double) this.b > (double) byte.MaxValue)
        this.b = (float) byte.MaxValue;
      if ((double) this.r < 0.0)
        this.r = 0.0f;
      if ((double) this.g < 0.0)
        this.g = 0.0f;
      if ((double) this.b < 0.0)
        this.b = 0.0f;
    }
    else if ((double) this.r == (double) byte.MaxValue && (double) this.g == 0.0 && (double) this.b > 0.0)
    {
      this.b -= this.rainbowSpeed;
      if ((double) this.r > (double) byte.MaxValue)
        this.r = (float) byte.MaxValue;
      if ((double) this.g > (double) byte.MaxValue)
        this.g = (float) byte.MaxValue;
      if ((double) this.b > (double) byte.MaxValue)
        this.b = (float) byte.MaxValue;
      if ((double) this.r < 0.0)
        this.r = 0.0f;
      if ((double) this.g < 0.0)
        this.g = 0.0f;
      if ((double) this.b < 0.0)
        this.b = 0.0f;
    }
    return new Color(this.r / (float) byte.MaxValue, this.g / (float) byte.MaxValue, this.b / (float) byte.MaxValue);
  }
}
