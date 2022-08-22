// Decompiled with JetBrains decompiler
// Type: UIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  public static UIController instance;
  public GameObject inGameMenu;
  public TMP_InputField loginUserNameInputField;
  public TMP_InputField loginPasswordInputField;
  public TMP_InputField registerUserNameInputField;
  public TMP_InputField registerPasswordInputField;
  public TMP_InputField registerEmailInputField;
  public TMP_InputField worldNameInputField;
  public Button connectButton;
  public Button enterWorldButton;
  public Button enterButton;
  public GameObject MenuScreen;
  public TMP_Text chat;
  public GameObject scrollerImage;
  public GameObject ifOnPosition;
  public GameObject input;
  public bool isChatOn;
  public bool isHoldingLeftButton;
  public bool isHoldingRightButton;
  public GameObject worldCanvas;
  public GameObject bubblePrefab;
  public List<ChatBubble> chatBubbles = new List<ChatBubble>();
  public GameObject rlButton;
  public GameObject darkScreen;
  public GameObject itemInfoMenu;
  public GameObject itemActionMenu;
  public GameObject pack;
  public GameObject packsContainer;
  public GameObject shopCategoriesContainer;
  public GameObject verification;
  public GameObject shop;
  public GameObject tradeScreen;
  public Sprite UIMask;
  public bool isLogin = true;
  public GameObject notification;
  public Transform NotificationDestination;
  public Transform NotificationStatic;
  public GameObject playerMenu;
  public GameObject signBar;
  private int lineCount;
  public bool isJumpButtonDown;
  public bool isHoldingJumpButton;
  public GameObject[] logos;
  private bool logoSizeRound;
  private bool logoRotationRound;
  public GameObject[] movementButtons;
  public GameObject loadingScreen;
  public bool logoSetActiveAnimation;
  public GameObject worldMenu;
  public GameObject select;
  public GameObject cubixsCount;
  public GameObject dialogs;
  public GameObject dialog;
  public GameObject button;
  public GameObject dialogDarkscreen;
  public GameObject popularWorld;
  public GameObject[] activeWorlds;
  public GameObject backButton;
  public GameObject[] menuButtons;
  public GameObject[] scenes;
  public GameObject newsContainer;
  public GameObject news;
  public PolygonCollider2D worldBorders;
  public EdgeCollider2D worldColliders;
  public CinemachineConfiner2D confiner;
  public GameObject changePasswordPassword;
  public GameObject changePasswordNewPassword;
  public GameObject changeEmailPassword;
  public GameObject changeEmailNewEmail;
  public Sprite[] buttonTypes;
  public GameObject claimButton;
  public GameObject chatBubbleCanvas;
  public GameObject nicknameCanvas;
  public GameObject signBubbleCanvas;
  public GameObject signDialogBar;
  public GameObject DoorDialogBar;
  public GameObject friendSlot;
  public GameObject friendsContainer;
  public Sprite activeStatus;
  public Sprite offlineStatus;
  public GameObject yourFriendsMenu;
  public GameObject friendMenu;
  public TextMeshProUGUI friendCountText;
  public GameObject yourWorldsMenu;
  public GameObject yourWorldsContainer;
  public GameObject worldSlot;
  public GameObject craftingMenu;
  public GameObject selectAnItemMenu;
  public GameObject craftingSlotToFollow;
  public GameObject craftingSelectedIndicator;
  public int craftingSelectedItemID;
  public int craftingSelectedItemQuantity;
  public int craftingItem1ID;
  public int craftingItem1Quantity;
  public int craftingItem2ID;
  public int craftingItem2Quantity;
  public int craftID;
  public int craftItemID;
  public bool isCraftable;
  public Sprite craftingPlus;
  public GameObject craftingDialogBar;
  public GameObject questMenu;
  public TextMeshProUGUI questsText;
  public GameObject questsContainer;
  public GameObject quest;
  public GameObject questDetailsMenu;
  public GameObject questDetailsContainer;
  public GameObject questDetailsBar;
  public GameObject achievementMenu;
  public GameObject achievementsContainer;
  public GameObject achievement;
  public GameObject achievementNotification;
  public Transform achievementNotificationAimD;
  public Transform achievementNotificationAimS;
  public GameObject selectItemMarketplaceSlot;
  public GameObject marketplaceDialogBar;
  public int marketplaceSellItemID;
  public int marketplaceSellItemQuantity;
  public GameObject categoryContainer;
  public GameObject category;
  public GameObject mySaleItemContainer;
  public GameObject mySaleItem;
  public GameObject categoryContainerMenu;
  public GameObject buyItemMenu;
  public GameObject saleItemsContainer;
  public GameObject saleItem;
  public TextMeshProUGUI[] saleItemCountTexts;
  public TextMeshProUGUI buySaleItemCountText;
  public List<GameObject> newItemDialogs = new List<GameObject>();
  public GameObject newItemDialog;
  public GameObject newItemDialogContainer;
  public GameObject deathScreen;
  public Dictionary<int, TextEffect> textEffects = new Dictionary<int, TextEffect>();
  public Dictionary<Vector2, GameObject> itemAlerts = new Dictionary<Vector2, GameObject>();
  public GameObject itemAlertContainer;
  public Sprite[] itemAlertStatusSprites;
  public GameObject itemAlert;
  public GameObject dealerMenu;
  public GameObject dealContainer;
  public GameObject deal;
  public GameObject recipesContainer;
  public GameObject recipe;
  public GameObject iconsContainer;
  public GameObject cubixIcon;
  public Dictionary<GameObject, IconEffect> iconEffects = new Dictionary<GameObject, IconEffect>();
  public float iconAnimationSpeed = 2f;
  public GameObject cubixEffectStartPoint;
  public GameObject cubixEffectEndPoint;
  public GameObject capsuleDialog;
  public GameObject inventory;
  public GameObject fishingMenu;
  public GameObject fishingModeDialog;
  public GameObject fishingContainer;
  public GameObject tutorialManager;
  public GameObject cubotMessage;
  public GameObject skinToneMenu;
  public string[] tips = new string[3]
  {
    "TEST TIP STRING TEXT 1",
    "TEST TIP STRING TEXT 2",
    "TEST TIP STRING TEXT 3"
  };

  private IEnumerator PlaceImageFromWeb(Image _image, string _mediaUrl)
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
      _image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) ((Texture) texture).width, (float) ((Texture) texture).height), new Vector2((float) (((Texture) texture).width / 2), (float) (((Texture) texture).height / 2)));
    }
  }

  private IEnumerator StartCubixEffectAnimation(
    GameObject _cubixIcon,
    IconEffect _iconEffect,
    float _secondsLater)
  {
    yield return (object) new WaitForSeconds(_secondsLater);
    this.iconEffects.Add(_cubixIcon, _iconEffect);
  }

  private IEnumerator SetActiveAnimation(
    GameObject _gameObject,
    int _waitMS,
    bool _status)
  {
    UIController uiController = this;
    yield return (object) new WaitForSeconds((float) (_waitMS / 1000));
    if (Object.op_Inequality((Object) _gameObject, (Object) null))
    {
      if (!_status)
      {
        if ((double) _gameObject.transform.localScale.x > 0.10000000149011612)
        {
          _gameObject.transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(_gameObject.transform.localScale), Vector2.zero, 10f * Time.deltaTime));
          uiController.StartCoroutine(uiController.SetActiveAnimation(_gameObject, _waitMS, _status));
        }
        else
        {
          _gameObject.SetActive(_status);
          if (((Object) _gameObject).name == "Logo")
            uiController.logoSetActiveAnimation = false;
        }
      }
      else if ((double) _gameObject.transform.localScale.x < 1.0)
      {
        _gameObject.transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(_gameObject.transform.localScale), new Vector2(1.01f, 1.01f), 10f * Time.deltaTime));
        uiController.StartCoroutine(uiController.SetActiveAnimation(_gameObject, _waitMS, _status));
      }
      else if (((Object) _gameObject).name == "Logo")
        uiController.logoSetActiveAnimation = false;
    }
  }

  private IEnumerator SetActiveAnimation(
    GameObject _gameObject,
    int _waitMS,
    bool _status,
    Vector2 _size)
  {
    UIController uiController = this;
    if (Object.op_Inequality((Object) _gameObject, (Object) null))
    {
      yield return (object) new WaitForSeconds((float) (_waitMS / 1000));
      if (Object.op_Inequality((Object) _gameObject, (Object) null))
      {
        if (!_status)
        {
          if ((double) _gameObject.transform.localScale.x > (double) _size.x / 10.0)
          {
            _gameObject.transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(_gameObject.transform.localScale), Vector2.zero, 20f * Time.deltaTime));
            uiController.StartCoroutine(uiController.SetActiveAnimation(_gameObject, _waitMS, _status, _size));
          }
          else
          {
            _gameObject.SetActive(_status);
            if (((Object) _gameObject).name == "Logo")
              uiController.logoSetActiveAnimation = false;
          }
        }
        else if ((double) _gameObject.transform.localScale.x < (double) _size.x)
        {
          _gameObject.transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(_gameObject.transform.localScale), new Vector2(_size.x + _size.x / 100f, _size.y + _size.y / 100f), 20f * Time.deltaTime));
          uiController.StartCoroutine(uiController.SetActiveAnimation(_gameObject, _waitMS, _status, _size));
        }
        else if (((Object) _gameObject).name == "Logo")
          uiController.logoSetActiveAnimation = false;
      }
    }
  }

  public IEnumerator CreateBubble(int _clientID, string _text, float _seconds)
  {
    GameObject _bubble = Object.Instantiate<GameObject>(this.bubblePrefab);
    _bubble.SetActive(false);
    this.SetActive(_bubble, Vector2.op_Implicit(_bubble.transform.localScale));
    _bubble.transform.SetParent(this.chatBubbleCanvas.transform);
    ((TMP_Text) ((Component) _bubble.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _text;
    _bubble.transform.position = ((Component) ((Component) GameManager.players[_clientID]).gameObject.transform.GetChild(0)).transform.position;
    foreach (ChatBubble chatBubble in this.chatBubbles)
    {
      if (chatBubble.playerID == _clientID)
      {
        Object.Destroy((Object) chatBubble.gameObject);
        this.chatBubbles.Remove(chatBubble);
        break;
      }
    }
    ChatBubble _chatBubble = new ChatBubble();
    _chatBubble.gameObject = _bubble;
    _chatBubble.playerID = _clientID;
    _chatBubble.text = _text;
    _chatBubble.playerToFollow = ((Component) ((Component) GameManager.players[_clientID]).gameObject.transform.GetChild(0)).gameObject;
    this.chatBubbles.Add(_chatBubble);
    yield return (object) new WaitForSeconds(_seconds);
    if (Object.op_Inequality((Object) _bubble, (Object) null))
    {
      this.SetActive(_bubble, Vector2.op_Implicit(_bubble.transform.localScale));
      yield return (object) new WaitForSeconds(0.1f);
      this.chatBubbles.Remove(_chatBubble);
      Object.Destroy((Object) _bubble);
    }
  }

  public IEnumerator ActivateDeathScreen(float _time)
  {
    this.SetActive(this.deathScreen, true);
    yield return (object) new WaitForSeconds(_time);
    this.SetActive(this.deathScreen, false);
    ClientSend.RequestData(3);
  }

  public IEnumerator CreateNotification(string _title, string _text, float _time)
  {
    UIController uiController = this;
    GameObject _notification = Object.Instantiate<GameObject>(uiController.notification, ((Component) uiController).gameObject.transform);
    _notification.transform.position = uiController.NotificationStatic.position;
    _notification.GetComponent<NotificationMovement>().destinationPosition = uiController.NotificationDestination;
    _notification.GetComponent<NotificationMovement>().staticPosition = uiController.NotificationStatic;
    ((TMP_Text) ((Component) _notification.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _title;
    ((TMP_Text) ((Component) _notification.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _text;
    _notification.GetComponent<NotificationMovement>().TrueState();
    yield return (object) new WaitForSeconds(_time);
    _notification.GetComponent<NotificationMovement>().FalseState();
    yield return (object) new WaitForSeconds(0.5f);
    Object.Destroy((Object) _notification);
  }

  public IEnumerator CreateCubotMessage(string _title, string _text, float _time)
  {
    UIController uiController = this;
    GameObject _notification = Object.Instantiate<GameObject>(uiController.cubotMessage, ((Component) uiController).gameObject.transform);
    _notification.GetComponent<DialogueHandler>().dialogues[0].expressionID = 2;
    _notification.GetComponent<DialogueHandler>().dialogues[0].dialogue = _text;
    _notification.GetComponent<DialogueHandler>().ShowTextFunction(0, _title);
    yield return (object) new WaitForSeconds(_time);
    uiController.SetActive(_notification, false);
    yield return (object) new WaitForSeconds(0.5f);
    Object.Destroy((Object) _notification);
  }

  public IEnumerator CreateAchievementNotification(
    string _name,
    Sprite _sprite,
    float _time)
  {
    UIController uiController = this;
    GameObject _notification = Object.Instantiate<GameObject>(uiController.achievementNotification, ((Component) uiController).gameObject.transform);
    _notification.transform.position = uiController.achievementNotificationAimS.position;
    _notification.GetComponent<NotificationMovement>().destinationPosition = uiController.achievementNotificationAimD;
    _notification.GetComponent<NotificationMovement>().staticPosition = uiController.achievementNotificationAimS;
    ((TMP_Text) ((Component) _notification.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = _name;
    ((Component) _notification.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = _sprite;
    _notification.GetComponent<NotificationMovement>().TrueState();
    yield return (object) new WaitForSeconds(_time);
    _notification.GetComponent<NotificationMovement>().FalseState();
    yield return (object) new WaitForSeconds(0.5f);
    Object.Destroy((Object) _notification);
  }

  public void CreateNotificationFunction(string _title, string _text, float _time) => this.StartCoroutine(this.CreateNotification(_title, _text, _time));

  public void CreateCubotMessageFunction(string _title, string _text, float _time) => this.StartCoroutine(this.CreateCubotMessage(_title, _text, _time));

  public void CreateAchievementNotificationFunction(string _name, Sprite _sprite, float _time) => this.StartCoroutine(this.CreateAchievementNotification(_name, _sprite, _time));

  public void CreateBubbleVoid(int _clientID, string _text, float _seconds) => this.StartCoroutine(this.CreateBubble(_clientID, _text, _seconds));

  public void ActivateDeathScreenFunction(float _time) => this.StartCoroutine(this.ActivateDeathScreen(_time));

  public void CreateItemAlert(Vector2 _position, int _itemID, int _status)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.itemAlert, this.itemAlertContainer.transform);
    ((Component) gameObject.transform.GetChild(0).GetChild(1)).GetComponent<Image>().sprite = GameManager.instance.items[_itemID].texture;
    ((Component) gameObject.transform.GetChild(0)).GetComponent<Image>().sprite = this.itemAlertStatusSprites[_status];
    gameObject.transform.position = Vector2.op_Implicit(_position);
    this.itemAlerts.Add(_position, gameObject);
  }

  public void EditItemAlert(Vector2 _position, int _status)
  {
    if (!this.itemAlerts.ContainsKey(_position))
      return;
    ((Component) this.itemAlerts[_position].transform.GetChild(0)).GetComponent<Image>().sprite = this.itemAlertStatusSprites[_status];
  }

  public void RemoveItemAlert(Vector2 _position)
  {
    if (!this.itemAlerts.ContainsKey(_position))
      return;
    Object.Destroy((Object) this.itemAlerts[_position]);
    this.itemAlerts.Remove(_position);
  }

  public void CreateCubixEffect(long _cubixCount)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.cubixIcon, this.iconsContainer.transform);
    ((TMP_Text) ((Component) gameObject.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = (_cubixCount > 0L ? "<color=#62ff42>+" : "<color=#FF3614>") + _cubixCount.ToString() + "x";
    ((Transform) gameObject.GetComponent<RectTransform>()).position = ((Transform) this.cubixEffectStartPoint.GetComponent<RectTransform>()).position;
    this.SetActive(gameObject, true);
    this.StartCoroutine(this.StartCubixEffectAnimation(gameObject, new IconEffect()
    {
      destination = Vector2.op_Implicit(((Transform) this.cubixEffectEndPoint.GetComponent<RectTransform>()).position)
    }, 0.75f));
  }

  public void CreateNewItemDialog(int _itemID, int _itemQuantity)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIController.\u003C\u003Ec__DisplayClass175_0 displayClass1750 = new UIController.\u003C\u003Ec__DisplayClass175_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1750.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1750._newItemDialog = Object.Instantiate<GameObject>(this.newItemDialog, this.newItemDialogContainer.transform);
    // ISSUE: reference to a compiler-generated field
    displayClass1750._newItemDialog.SetActive(false);
    // ISSUE: reference to a compiler-generated field
    this.SetActive(displayClass1750._newItemDialog, true);
    // ISSUE: reference to a compiler-generated field
    ((Component) displayClass1750._newItemDialog.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1)).GetComponent<Image>().sprite = GameManager.instance.items[_itemID].texture;
    // ISSUE: reference to a compiler-generated field
    ((TMP_Text) ((Component) displayClass1750._newItemDialog.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(3)).GetComponent<TextMeshProUGUI>()).text = _itemQuantity.ToString() + "x " + GameManager.instance.items[_itemID].name;
    foreach (GameObject newItemDialog in this.newItemDialogs)
      newItemDialog.SetActive(false);
    // ISSUE: reference to a compiler-generated field
    this.newItemDialogs.Add(displayClass1750._newItemDialog);
    if (this.newItemDialogs.Count == 0)
      ((Component) this.newItemDialogContainer.transform.GetChild(0)).gameObject.SetActive(false);
    else
      ((Component) this.newItemDialogContainer.transform.GetChild(0)).gameObject.SetActive(true);
    if (this.newItemDialogs.Count > 1)
    {
      // ISSUE: reference to a compiler-generated field
      ((Component) displayClass1750._newItemDialog.transform.GetChild(0).GetChild(2).GetChild(1)).gameObject.SetActive(true);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) ((Component) displayClass1750._newItemDialog.transform.GetChild(0).GetChild(2).GetChild(1)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) displayClass1750, __methodptr(\u003CCreateNewItemDialog\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((UnityEvent) ((Component) displayClass1750._newItemDialog.transform.GetChild(0).GetChild(2).GetChild(0)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) displayClass1750, __methodptr(\u003CCreateNewItemDialog\u003Eb__1)));
  }

  public void CreateNews(string _title, string _url, string _details)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIController.\u003C\u003Ec__DisplayClass176_0 displayClass1760 = new UIController.\u003C\u003Ec__DisplayClass176_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1760.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1760._details = _details;
    this.newsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2((float) (898.1102294921875 * (double) this.newsContainer.transform.childCount + 150.0), this.newsContainer.GetComponent<RectTransform>().sizeDelta.y);
    GameObject gameObject = Object.Instantiate<GameObject>(this.news, this.newsContainer.transform);
    this.StartCoroutine(this.PlaceImageFromWeb(((Component) gameObject.transform.GetChild(0).GetChild(0).GetChild(0)).GetComponent<Image>(), _url));
    ((TMP_Text) ((Component) gameObject.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _title;
    // ISSUE: reference to a compiler-generated field
    if (!(displayClass1760._details != ""))
      return;
    ((Component) gameObject.transform.GetChild(2)).gameObject.SetActive(true);
    // ISSUE: method pointer
    ((UnityEvent) ((Component) gameObject.transform.GetChild(2)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) displayClass1760, __methodptr(\u003CCreateNews\u003Eb__0)));
  }

  public void CreateDialog(
    string _title,
    string _text,
    string[] _buttons,
    int[] _buttonType,
    Action[] _actions)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIController.\u003C\u003Ec__DisplayClass177_0 displayClass1770 = new UIController.\u003C\u003Ec__DisplayClass177_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1770._actions = _actions;
    // ISSUE: reference to a compiler-generated field
    displayClass1770.\u003C\u003E4__this = this;
    foreach (Transform transform in this.dialogs.transform)
    {
      if (((Object) ((Component) transform).gameObject).name != "Darkscreen")
      {
        this.SetActive(((Component) transform).gameObject, false);
        Object.Destroy((Object) ((Component) transform).gameObject, 1f);
      }
    }
    // ISSUE: reference to a compiler-generated field
    displayClass1770._dialog = Object.Instantiate<GameObject>(this.dialog, this.dialogs.transform);
    // ISSUE: reference to a compiler-generated field
    this.SetActive(displayClass1770._dialog, true);
    this.SetActive(this.dialogDarkscreen, true);
    // ISSUE: reference to a compiler-generated field
    ((TMP_Text) ((Component) displayClass1770._dialog.transform.GetChild(0).GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _title;
    // ISSUE: reference to a compiler-generated field
    ((TMP_Text) ((Component) displayClass1770._dialog.transform.GetChild(0).GetChild(1)).GetComponent<TextMeshProUGUI>()).text = _text;
    for (int index = 0; index < _buttons.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UIController.\u003C\u003Ec__DisplayClass177_1 displayClass1771 = new UIController.\u003C\u003Ec__DisplayClass177_1();
      // ISSUE: reference to a compiler-generated field
      displayClass1771.CS\u0024\u003C\u003E8__locals1 = displayClass1770;
      // ISSUE: reference to a compiler-generated field
      displayClass1771.x = index;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      GameObject gameObject = Object.Instantiate<GameObject>(this.button, ((Component) displayClass1771.CS\u0024\u003C\u003E8__locals1._dialog.transform.GetChild(0).GetChild(2)).transform);
      gameObject.GetComponent<Image>().sprite = this.buttonTypes[_buttonType[index]];
      ((TMP_Text) ((Component) gameObject.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = _buttons[index];
      // ISSUE: method pointer
      ((UnityEvent) gameObject.GetComponent<Button>().onClick).AddListener(new UnityAction((object) displayClass1771, __methodptr(\u003CCreateDialog\u003Eb__0)));
    }
  }

  public void CreateTextEffect(string _text, Vector2 _position)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(((Component) GameManager.instance.nicknamePrefab).gameObject, this.nicknameCanvas.transform);
    gameObject.transform.position = Vector2.op_Implicit(_position);
    Object.Destroy((Object) gameObject, 2f);
    TextEffect textEffect = new TextEffect();
    textEffect.text = _text;
    textEffect.tmp = gameObject.GetComponent<TextMeshProUGUI>();
    ((TMP_Text) textEffect.tmp).text = _text;
    for (int key = 0; key < int.MaxValue; ++key)
    {
      if (!this.textEffects.ContainsKey(key))
      {
        this.textEffects.Add(key, textEffect);
        break;
      }
    }
  }

  private void Awake()
  {
    if (Object.op_Equality((Object) UIController.instance, (Object) null))
      UIController.instance = this;
    else if (Object.op_Inequality((Object) UIController.instance, (Object) this))
    {
      Debug.Log((object) "Instance already exists, destroying object!");
      Object.Destroy((Object) this);
    }
    if (Application.platform == 2)
    {
      foreach (GameObject movementButton in this.movementButtons)
        movementButton.SetActive(false);
    }
    // ISSUE: method pointer
    ((UnityEvent<TouchScreenKeyboard.Status>) ((Component) this.input.transform.GetChild(0)).GetComponent<TMP_InputField>().onTouchScreenKeyboardStatusChanged).AddListener(new UnityAction<TouchScreenKeyboard.Status>((object) this, __methodptr(OnTouchScreenKeyboardStatusChanged)));
    for (int index = 0; index < this.shopCategoriesContainer.transform.childCount; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      ((UnityEvent) ((Component) this.shopCategoriesContainer.transform.GetChild(index)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) new UIController.\u003C\u003Ec__DisplayClass179_0()
      {
        \u003C\u003E4__this = this,
        x = index
      }, __methodptr(\u003CAwake\u003Eb__0)));
    }
    for (int index = 0; index < this.menuButtons.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      ((UnityEvent) this.menuButtons[index].GetComponent<Button>().onClick).AddListener(new UnityAction((object) new UIController.\u003C\u003Ec__DisplayClass179_1()
      {
        \u003C\u003E4__this = this,
        x = index
      }, __methodptr(\u003CAwake\u003Eb__2)));
    }
  }

  public void OnTouchScreenKeyboardStatusChanged(TouchScreenKeyboard.Status status) => this.ToggleChat();

  private void Start()
  {
    if (PlayerPrefs.HasKey("username"))
      this.loginUserNameInputField.text = PlayerPrefs.GetString("username");
    if (PlayerPrefs.HasKey("password"))
      this.loginPasswordInputField.text = PlayerPrefs.GetString("password");
    // ISSUE: method pointer
    ((UnityEvent) this.selectItemMarketplaceSlot.GetComponent<Button>().onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Eb__181_0)));
    // ISSUE: method pointer
    ((UnityEvent) ((Component) this.selectItemMarketplaceSlot.transform.parent.GetChild(7).GetChild(2)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Eb__181_1)));
  }

  private void Update()
  {
    for (int index = 0; index < this.iconEffects.Count; ++index)
    {
      GameObject key = ((IEnumerable<KeyValuePair<GameObject, IconEffect>>) this.iconEffects).ElementAt<KeyValuePair<GameObject, IconEffect>>(index).Key;
      IconEffect iconEffect = ((IEnumerable<KeyValuePair<GameObject, IconEffect>>) this.iconEffects).ElementAt<KeyValuePair<GameObject, IconEffect>>(index).Value;
      key.transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(key.transform.localScale), new Vector2(0.0f, 0.0f), this.iconAnimationSpeed * Time.deltaTime));
      ((Transform) key.GetComponent<RectTransform>()).position = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(key.transform.position), iconEffect.destination, this.iconAnimationSpeed * Time.deltaTime));
      if ((double) key.transform.localScale.x < 0.05000000074505806)
      {
        Object.Destroy((Object) key);
        this.iconEffects.Remove(key);
        break;
      }
    }
    if (Object.op_Inequality((Object) this.craftingSlotToFollow, (Object) null))
      this.craftingSelectedIndicator.transform.position = this.craftingSlotToFollow.transform.position;
    foreach (KeyValuePair<int, TextEffect> keyValuePair in this.textEffects.ToList<KeyValuePair<int, TextEffect>>())
    {
      if (Object.op_Inequality((Object) keyValuePair.Value.tmp, (Object) null))
      {
        ((Component) keyValuePair.Value.tmp).gameObject.transform.position = Vector2.op_Implicit(new Vector2(((Component) keyValuePair.Value.tmp).gameObject.transform.position.x, ((Component) keyValuePair.Value.tmp).gameObject.transform.position.y + 0.01f));
        ((Graphic) keyValuePair.Value.tmp).color = new Color(1f, 1f, 1f, Mathf.Lerp(((Graphic) keyValuePair.Value.tmp).color.a, 0.0f, Time.deltaTime * 1.25f));
      }
      else
        this.textEffects.Remove(keyValuePair.Key);
    }
    if (!this.logoSetActiveAnimation)
    {
      if (!this.logoSizeRound)
      {
        foreach (GameObject logo in this.logos)
          logo.transform.localScale = Vector2.op_Implicit(new Vector2(Mathf.Lerp(this.logos[0].transform.localScale.x, 1.1f, Time.deltaTime * 1f), Mathf.Lerp(this.logos[0].transform.localScale.y, 1.2f, Time.deltaTime * 1.1f)));
      }
      else
      {
        foreach (GameObject logo in this.logos)
          logo.transform.localScale = Vector2.op_Implicit(new Vector2(Mathf.Lerp(this.logos[0].transform.localScale.x, 1f, Time.deltaTime * 1f), Mathf.Lerp(this.logos[0].transform.localScale.y, 1f, Time.deltaTime * 1f)));
      }
    }
    if ((double) this.logos[0].transform.localScale.x > 1.0800000429153442 && !this.logoSizeRound)
      this.logoSizeRound = !this.logoSizeRound;
    else if ((double) this.logos[0].transform.localScale.x < 1.0199999809265137 && this.logoSizeRound)
      this.logoSizeRound = !this.logoSizeRound;
    float z = this.logos[0].transform.localEulerAngles.z;
    float num1 = (double) z > 180.0 ? z - 360f : z;
    if (!this.logoRotationRound)
    {
      foreach (GameObject logo in this.logos)
        logo.transform.eulerAngles = new Vector3(this.logos[0].transform.eulerAngles.x, this.logos[0].transform.eulerAngles.y, Mathf.Lerp(num1, 4f, Time.deltaTime * 1f));
    }
    else
    {
      foreach (GameObject logo in this.logos)
        logo.transform.eulerAngles = new Vector3(this.logos[0].transform.eulerAngles.x, this.logos[0].transform.eulerAngles.y, Mathf.Lerp(num1, -4f, Time.deltaTime * 1f));
    }
    if ((double) num1 > 3.0 && !this.logoRotationRound)
      this.logoRotationRound = !this.logoRotationRound;
    else if ((double) num1 < -2.0 && this.logoRotationRound)
      this.logoRotationRound = !this.logoRotationRound;
    if (GameManager.players.ContainsKey(Client.instance.myId) && Object.op_Implicit((Object) ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().playerRB) && !((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().isNoclip)
    {
      float num2 = 0.0f;
      if (this.isHoldingLeftButton)
        num2 -= ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().movementSpeed;
      if (this.isHoldingRightButton)
        num2 += ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().movementSpeed;
      ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().Move(new Vector2(num2, ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().playerRB.velocity.y));
    }
    if (Input.GetKeyDown((KeyCode) 13))
      this.ToggleChat();
    foreach (ChatBubble chatBubble in this.chatBubbles)
    {
      try
      {
        chatBubble.gameObject.transform.position = chatBubble.playerToFollow.transform.position;
      }
      catch
      {
      }
    }
    if (!this.isJumpButtonDown || !MovementManager.instance.waterPhysics)
      return;
    ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().Jump();
  }

  public void OnClickRLButton()
  {
    this.isLogin = !this.isLogin;
    if (this.isLogin)
      ((TMP_Text) ((Component) this.rlButton.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "I don't have account";
    else
      ((TMP_Text) ((Component) this.rlButton.transform.GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "I have an account";
  }

  public void CreatePack(Pack _pack)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIController.\u003C\u003Ec__DisplayClass184_0 displayClass1840 = new UIController.\u003C\u003Ec__DisplayClass184_0()
    {
      \u003C\u003E4__this = this,
      _pack = _pack
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    displayClass1840._tempPack = Object.Instantiate<GameObject>(this.pack, ((Component) this.packsContainer.transform.GetChild(displayClass1840._pack.categoryID)).transform);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    displayClass1840._pack.gameobject = displayClass1840._tempPack;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((Component) displayClass1840._tempPack.transform.GetChild(0).GetChild(0).GetChild(1)).GetComponent<Image>().sprite = GameManager.instance.blockSprites[displayClass1840._pack.logoTextureID];
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((TMP_Text) ((Component) displayClass1840._tempPack.transform.GetChild(2).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = displayClass1840._pack.title;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((TMP_Text) ((Component) displayClass1840._tempPack.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = "\"" + displayClass1840._pack.description + "\"";
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((TMP_Text) ((Component) displayClass1840._tempPack.transform.GetChild(3).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "<sprite index=0> " + displayClass1840._pack.cost.ToString();
    // ISSUE: reference to a compiler-generated field
    ((UnityEventBase) ((Component) displayClass1840._tempPack.transform.GetChild(3)).GetComponent<Button>().onClick).RemoveAllListeners();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((UnityEvent) ((Component) displayClass1840._tempPack.transform.GetChild(3)).GetComponent<Button>().onClick).AddListener(new UnityAction((object) displayClass1840, __methodptr(\u003CCreatePack\u003Eb__0)));
  }

  public void ShopCancelButton()
  {
    UIController.instance.SetActive(this.shopCategoriesContainer, true);
    foreach (Component component in this.packsContainer.transform)
      this.SetActive(component.gameObject, false);
    UIController.instance.SetActive(UIController.instance.backButton, false);
    ((Component) this.packsContainer.transform.parent).gameObject.GetComponent<ScrollRect>().content = this.shopCategoriesContainer.GetComponent<RectTransform>();
    UIController.instance.SetActive(this.scenes[2].gameObject, true);
  }

  public void ButtonClick(int _id) => ClientSend.ButtonClick(_id);

  public void LeftMovementButtonDown() => this.isHoldingLeftButton = true;

  public void LeftMovementButtonUp() => this.isHoldingLeftButton = false;

  public void RightMovementButtonDown() => this.isHoldingRightButton = true;

  public void RightMovementButtonUp() => this.isHoldingRightButton = false;

  public void JumpButtonDown()
  {
    this.isHoldingJumpButton = true;
    if (!MovementManager.instance.waterPhysics)
    {
      ((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().Jump();
      this.isJumpButtonDown = false;
    }
    else
      this.isJumpButtonDown = true;
  }

  public void JumpButtonUp()
  {
    this.isJumpButtonDown = false;
    this.isHoldingJumpButton = false;
  }

  public void OpenMenuScreen()
  {
    ((Component) ((Component) this.loginUserNameInputField).transform.parent).gameObject.SetActive(false);
    ((Component) ((Component) this.registerUserNameInputField).transform.parent).gameObject.SetActive(false);
    UIController.instance.SetActive(this.worldMenu);
    TMP_InputField worldNameInputField = this.worldNameInputField;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    worldNameInputField.onValidateInput = (TMP_InputField.OnValidateInput) Delegate.Combine((Delegate) worldNameInputField.onValidateInput, (Delegate) (UIController.\u003C\u003Ec.\u003C\u003E9__193_0 ?? (UIController.\u003C\u003Ec.\u003C\u003E9__193_0 = new TMP_InputField.OnValidateInput((object) UIController.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003COpenMenuScreen\u003Eb__193_0)))));
  }

  public void ToggleChat()
  {
    this.isChatOn = !this.isChatOn;
    if (this.isChatOn)
    {
      ((Transform) this.scrollerImage.GetComponent<RectTransform>()).position = ((Transform) this.ifOnPosition.GetComponent<RectTransform>()).position;
      this.input.SetActive(true);
      ((Selectable) ((Component) this.input.transform.GetChild(0)).GetComponent<TMP_InputField>()).Select();
      ((Component) this.input.transform.GetChild(0)).GetComponent<TMP_InputField>().ActivateInputField();
    }
    else
    {
      ((Transform) this.scrollerImage.GetComponent<RectTransform>()).position = ((Transform) this.input.GetComponent<RectTransform>()).position;
      this.input.SetActive(false);
      string text = ((Component) this.input.transform.GetChild(0)).GetComponent<TMP_InputField>().text;
      if (!(text != ""))
        return;
      ClientSend.SendChatMessage(text);
      ((Component) this.input.transform.GetChild(0)).GetComponent<TMP_InputField>().text = "";
    }
  }

  public string wrapString(string msg, int width)
  {
    this.lineCount = 0;
    string[] strArray1 = msg.Split(" "[0], StringSplitOptions.None);
    string str1 = "";
    string str2 = "";
    for (int index1 = 0; index1 < strArray1.Length; ++index1)
    {
      strArray1[index1].Trim();
      if (strArray1[index1].Length >= width + 2)
      {
        string[] strArray2 = new string[5];
        int index2 = 0;
        while (strArray1[index1].Length > width)
        {
          strArray2[index2] = strArray1[index1].Substring(0, width) + "\n";
          ++this.lineCount;
          strArray1[index1] = strArray1[index1].Substring(width);
          ++index2;
          if (strArray1[index1].Length <= width)
          {
            strArray2[index2] = strArray1[index1];
            str2 = strArray2[index2];
          }
        }
        str1 += "\n";
        ++this.lineCount;
        for (int index3 = 0; index3 < index2 + 1; ++index3)
          str1 += strArray2[index3];
      }
      else if (index1 == 0)
      {
        str1 = strArray1[0];
        str2 = str1;
      }
      else if (index1 > 0)
      {
        if (str2.Length + strArray1[index1].Length <= width)
        {
          str1 = str1 + " " + strArray1[index1];
          str2 = str2 + " " + strArray1[index1];
        }
        else if (str2.Length + strArray1[index1].Length > width)
        {
          str1 = str1 + "\n" + strArray1[index1];
          ++this.lineCount;
          str2 = strArray1[index1];
        }
      }
    }
    if (str1[0] == '\n')
      str1 = str1.Substring(1, str1.Length - 1);
    return str1;
  }

  public void SetActive(GameObject _gameObject)
  {
    if (_gameObject.activeSelf)
      this.SetActive(_gameObject, false);
    else
      this.SetActive(_gameObject, true);
  }

  public void SetActive(GameObject _gameObject, Vector2 _size)
  {
    if (_gameObject.activeSelf)
      this.SetActive(_gameObject, false, _size);
    else
      this.SetActive(_gameObject, true, _size);
  }

  public void SetActive(GameObject _gameObject, bool _status)
  {
    if (((Object) _gameObject).name == "Logo")
      this.logoSetActiveAnimation = true;
    if (_status)
    {
      _gameObject.SetActive(_status);
      _gameObject.transform.localScale = Vector2.op_Implicit(Vector2.zero);
    }
    else
      _gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(1f, 1f));
    this.StartCoroutine(this.SetActiveAnimation(_gameObject, 500, _status));
  }

  public void SetActive(GameObject _gameObject, bool _status, Vector2 _size)
  {
    if (((Object) _gameObject).name == "Logo")
      this.logoSetActiveAnimation = true;
    if (_status)
    {
      _gameObject.SetActive(_status);
      _gameObject.transform.localScale = Vector2.op_Implicit(Vector2.zero);
    }
    else
      _gameObject.transform.localScale = Vector2.op_Implicit(_size);
    this.StartCoroutine(this.SetActiveAnimation(_gameObject, 500, _status, _size));
  }

  public void SelectSkinTone(int _skinToneID) => ClientSend.SelectSkinTone(_skinToneID);

  public void ChangeAuthData(int _dataToChange)
  {
    if (_dataToChange != 0)
    {
      if (_dataToChange != 1)
        return;
      ClientSend.ChangeAuthData(this.changeEmailPassword.GetComponent<TMP_InputField>().text, this.changeEmailNewEmail.GetComponent<TMP_InputField>().text, 1);
    }
    else
      ClientSend.ChangeAuthData(this.changePasswordPassword.GetComponent<TMP_InputField>().text, this.changePasswordNewPassword.GetComponent<TMP_InputField>().text, 0);
  }

  public void StopFishing() => ClientSend.FishingAction(1);

  public void StopCatching() => ClientSend.FishingAction(2);

  public void ClaimWorld()
  {
    Action action = (Action) (() =>
    {
      ClientSend.SendChatMessage("/claim");
      UIController.instance.SetActive(this.inGameMenu, false);
    });
    this.CreateDialog("Claim", "Do you want to claim <color=green>" + WorldManager.instance.worldName + "</color> for <color=yellow>" + WorldManager.instance.worldValue.ToString() + "</color><sprite=0> ?", new string[2]
    {
      "Claim",
      "Cancel"
    }, new int[2]{ 0, 1 }, new Action[2]{ action, null });
  }

  public void GoToGame()
  {
    WorldManager.instance.worldName = this.worldNameInputField.text;
    ClientSend.SendIntoGame();
  }

  public void QuitGame() => Application.Quit();

  public void LeaveFromWorld()
  {
    if (!TutorialHandler.isTutorial)
      ClientSend.LeaveFromWorld();
    else
      UIController.instance.CreateCubotMessageFunction("<color=red>Error", "You are not allowed to <color=#FF1212>Leave from World</color>!", 3f);
  }

  public void CancelTrade() => ClientSend.TradeAction(1);

  public void AcceptTrade() => ClientSend.TradeAction(0);

  public void Login() => ClientSend.AuthUser();

  public void Register() => ClientSend.AuthUser();

  public void ChangeLoginState(bool _isLogin) => this.isLogin = _isLogin;

  public void OutOfService() => this.CreateNotificationFunction("<color=red>Error", "This button is out of service right now.", 2f);

  public void RequestFriendsData() => ClientSend.RequestData(0);
}
