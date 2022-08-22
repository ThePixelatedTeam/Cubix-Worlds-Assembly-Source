// Decompiled with JetBrains decompiler
// Type: PlayerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public int id;
  public string username;
  public string bio;
  public int level;
  public int currentXP;
  public int maxXP;
  public int health;
  public int accountLevel;
  public TextMeshProUGUI nicknameObject;
  public GameObject wrenchIcon;
  public int skinID;
  public AnimationSystem animationSystem;
  public Vector2 destination;
  public float positionSmoothness = 2f;
  public bool isArrived = true;
  public bool isMine;
  private Vector2 position;
  private int previousAnimation;
  private int currentFrame;
  private int petCurrentFrame;
  private int currentAnimation;
  private SpriteRenderer spriteRenderer;
  public Thread currentThread;
  public Dictionary<int, bool> animations = new Dictionary<int, bool>();
  private Vector2 lastPosition;
  private float checkIsGroundedLength = 0.1f;
  private bool lastIsGrounded;
  private bool isGrounded;
  private Collider2D collided;
  public int[] clothes = new int[8];
  public List<DropShadow> dropShadows = new List<DropShadow>();
  private bool updateSize;
  private Vector2 selectedSize;
  public bool preventUpdatingSize;
  private bool updateAlpha;
  private float selectedAlpha;
  public bool isDying;

  private void Awake()
  {
    this.spriteRenderer = ((Component) ((Component) this).transform).GetComponent<SpriteRenderer>();
    this.animationSystem = ((Component) ((Component) this).transform).GetComponent<AnimationSystem>();
  }

  private void Start()
  {
    this.destination = Vector2.op_Implicit(((Component) this).transform.position);
    this.isMine = this.id == Client.instance.myId;
    new Thread(new ThreadStart(this.PassPetFrame)).Start();
  }

  public void Update()
  {
    this.CheckIsGrounded();
    if (Object.op_Inequality((Object) this.wrenchIcon, (Object) null))
      this.wrenchIcon.transform.position = ((Component) this).transform.position;
    if (this.updateSize)
    {
      ((Component) this).transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(((Component) this).transform.localScale), this.selectedSize, Time.deltaTime * 5f));
      if ((double) MathF.Abs(this.selectedSize.x - ((Component) this).transform.localScale.x) < (double) this.selectedSize.x - (double) this.selectedSize.x * 0.9)
      {
        ((Component) this).transform.localScale = Vector2.op_Implicit(this.selectedSize);
        this.updateSize = false;
        this.preventUpdatingSize = false;
        if ((double) this.selectedSize.x == 1.0 && this.id == Client.instance.myId && !((Component) GameManager.players[Client.instance.myId]).GetComponent<MovementManager>().isNoclip)
          ((Component) this).GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D) 0;
        if ((double) this.selectedSize.x == 0.10000000149011612)
        {
          ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(0.0f, 0.0f));
          ((Component) this).gameObject.SetActive(false);
        }
      }
    }
    if (this.updateAlpha)
    {
      this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.spriteRenderer.color.a - 0.0156862754f);
      if ((double) this.spriteRenderer.color.a > ((double) this.selectedAlpha - (double) this.selectedAlpha / 20.0) / (double) byte.MaxValue)
      {
        this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.selectedAlpha / (float) byte.MaxValue);
        this.updateAlpha = false;
      }
    }
    if (!this.isArrived)
    {
      this.position = Vector2.Lerp(Vector2.op_Implicit(((Component) this).transform.position), this.destination, this.positionSmoothness * Time.deltaTime);
      ((Component) this).transform.position = Vector2.op_Implicit(this.position);
      if (Vector2.op_Equality(this.position, this.destination))
        this.isArrived = true;
    }
    if (this.isMine)
      ((Component) Camera.main).transform.position = ((Component) this).transform.position;
    ((TMP_Text) this.nicknameObject).transform.position = ((Component) ((Component) this).transform.GetChild(1)).transform.position;
    ((TMP_Text) this.nicknameObject).text = "<sprite index=" + this.accountLevel.ToString() + "> " + this.username;
    if (this.lastIsGrounded && !this.isGrounded && (double) ((Component) this).transform.position.y > (double) this.lastPosition.y && Object.op_Inequality((Object) this.collided, (Object) null))
      GameManager.instance.CreateEffect(0, new Vector2(((Component) this).transform.position.x, ((Component) this.collided).gameObject.transform.position.y + ((Component) this.collided).gameObject.transform.localScale.y));
    if (!this.lastIsGrounded && this.isGrounded == Object.op_Implicit((Object) ((Component) this).transform) && Object.op_Inequality((Object) this.collided, (Object) null))
      GameManager.instance.CreateEffect(4, new Vector2(((Component) this).transform.position.x, ((Component) this.collided).gameObject.transform.position.y + ((Component) this.collided).gameObject.transform.localScale.y));
    this.lastIsGrounded = this.isGrounded;
    this.lastPosition = Vector2.op_Implicit(((Component) this).transform.position);
  }

  public void SetSkin(int _skinID)
  {
    this.skinID = _skinID;
    if (_skinID == 1 || _skinID == 3)
    {
      ((Component) ((Component) this).transform.GetChild(1)).transform.localPosition = Vector2.op_Implicit(new Vector2(0.0f, 1.15f));
      ((Component) ((Component) this).transform.GetChild(0)).transform.localPosition = Vector2.op_Implicit(new Vector2(0.0f, 1.6f));
    }
    else
    {
      ((Component) ((Component) this).transform.GetChild(1)).transform.localPosition = Vector2.op_Implicit(new Vector2(0.0f, 0.758f));
      ((Component) ((Component) this).transform.GetChild(0)).transform.localPosition = Vector2.op_Implicit(new Vector2(0.0f, 1.203f));
    }
    for (int index1 = 0; index1 < this.animationSystem.animations.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.animationSystem.animations[index1].sprites.Count; ++index2)
      {
        Sprite sprite = Resources.Load("Images/Textures/Player/" + this.skinID.ToString() + "/" + index1.ToString() + "/" + (index2 + 1).ToString(), typeof (Sprite)) as Sprite;
        if (Object.op_Inequality((Object) sprite, (Object) null))
          this.animationSystem.animations[index1].sprites[index2] = sprite;
      }
    }
  }

  public void ChangeSize(Vector2 _newSize, bool _isPublic)
  {
    this.updateSize = true;
    this.selectedSize = _newSize;
    this.preventUpdatingSize = _isPublic;
  }

  public void ChangeAlpha(int _alpha, bool _isPublic)
  {
    this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, (float) ((int) byte.MaxValue - _alpha));
    this.updateAlpha = true;
    this.selectedAlpha = (float) _alpha;
    this.preventUpdatingSize = _isPublic;
  }

  private void CheckIsGrounded()
  {
    RaycastHit2D raycastHit2D1 = Physics2D.Raycast(new Vector2(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 5f, ((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2f), Vector2.down, this.checkIsGroundedLength);
    RaycastHit2D raycastHit2D2 = Physics2D.Raycast(new Vector2(((Component) this).transform.position.x - Mathf.Abs(((Component) this).transform.localScale.x) / 5f, ((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2f), Vector2.down, this.checkIsGroundedLength);
    if (Object.op_Inequality((Object) ((RaycastHit2D) ref raycastHit2D1).collider, (Object) null) || Object.op_Inequality((Object) ((RaycastHit2D) ref raycastHit2D2).collider, (Object) null))
    {
      if (Object.op_Inequality((Object) ((RaycastHit2D) ref raycastHit2D1).collider, (Object) null))
        this.collided = ((RaycastHit2D) ref raycastHit2D1).collider;
      if (Object.op_Inequality((Object) ((RaycastHit2D) ref raycastHit2D2).collider, (Object) null))
        this.collided = ((RaycastHit2D) ref raycastHit2D2).collider;
      this.isGrounded = true;
    }
    if (!Object.op_Equality((Object) ((RaycastHit2D) ref raycastHit2D1).collider, (Object) null) || !Object.op_Equality((Object) ((RaycastHit2D) ref raycastHit2D2).collider, (Object) null))
      return;
    this.isGrounded = false;
  }

  public void ChangeAnimation(int _animationID)
  {
    try
    {
      if (this.currentAnimation != 4)
        this.previousAnimation = this.currentAnimation;
      this.currentAnimation = _animationID;
      this.animationSystem.currentID = _animationID;
      this.currentFrame = 0;
      for (int i = 0; i < int.MaxValue; i++)
      {
        if (!this.animations.ContainsKey(i))
        {
          foreach (KeyValuePair<int, bool> keyValuePair in this.animations.ToList<KeyValuePair<int, bool>>())
            this.animations[keyValuePair.Key] = false;
          this.animations.Add(i, true);
          this.currentThread = new Thread((ThreadStart) (() => this.PassFrame(i)));
          this.currentThread.Start();
          break;
        }
      }
    }
    catch (Exception ex)
    {
      Debug.Log((object) ex.ToString());
    }
  }

  public void PassPetFrame()
  {
    ThreadManager.ExecuteOnMainThread((Action) (() =>
    {
      if (!GameManager.instance.pets.ContainsKey(this))
        return;
      GameManager.instance.pets[this].GetComponent<AnimationSystem>().spriteRenderer.sprite = GameManager.instance.pets[this].GetComponent<AnimationSystem>().animations[0].sprites[this.petCurrentFrame];
    }));
    Thread.Sleep(200);
    ++this.petCurrentFrame;
    if (this.petCurrentFrame >= 2)
    {
      this.petCurrentFrame = 0;
      this.PassPetFrame();
    }
    else
      this.PassPetFrame();
  }

  public void PassFrame(int _taskID)
  {
    while (this.animations[_taskID])
    {
      ThreadManager.ExecuteOnMainThread((Action) (() =>
      {
        if (!GameManager.players.ContainsKey(Client.instance.myId) || !Object.op_Inequality((Object) this.animationSystem.spriteRenderer, (Object) null))
          return;
        this.animationSystem.spriteRenderer.sprite = this.animationSystem.animations[this.currentAnimation].sprites[this.currentFrame];
        for (int index = 0; index < GameManager.players[Client.instance.myId].clothes.Length; ++index)
        {
          if (index <= 4)
            ((Component) ((Component) this).gameObject.transform.GetChild(2).GetChild(index)).GetComponent<AnimationSystem>().spriteRenderer.sprite = ((Component) ((Component) this).gameObject.transform.GetChild(2).GetChild(index)).GetComponent<AnimationSystem>().animations[this.currentAnimation].sprites[this.currentFrame];
          else
            ((Component) ((Component) this).gameObject.transform.GetChild(2).GetChild(index + 1)).GetComponent<AnimationSystem>().spriteRenderer.sprite = ((Component) ((Component) this).gameObject.transform.GetChild(2).GetChild(index + 1)).GetComponent<AnimationSystem>().animations[this.currentAnimation].sprites[this.currentFrame];
        }
      }));
      Thread.Sleep(this.animationSystem.animations[this.currentAnimation].updateFrameMS);
      if (this.animations[_taskID])
      {
        ++this.currentFrame;
        if (this.currentFrame >= this.animationSystem.animations[this.currentAnimation].sprites.Count && this.animationSystem.animations[this.currentAnimation].isLoop)
        {
          this.currentFrame = 0;
        }
        else
        {
          if (this.currentFrame >= this.animationSystem.animations[this.currentAnimation].sprites.Count && !this.animationSystem.animations[this.currentAnimation].isLoop && this.id == Client.instance.myId)
          {
            this.currentFrame = 0;
            this.animationSystem.currentID = this.previousAnimation;
            ClientSend.ChangeAnimation(this.previousAnimation);
            break;
          }
          if (this.currentFrame >= this.animationSystem.animations[this.currentAnimation].sprites.Count && !this.animationSystem.animations[this.currentAnimation].isLoop && this.id != Client.instance.myId)
          {
            this.currentFrame = 0;
            break;
          }
        }
      }
    }
    this.animations.Remove(_taskID);
  }
}
