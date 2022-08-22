// Decompiled with JetBrains decompiler
// Type: MovementManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
  public static MovementManager instance;
  public float movementSpeed = 3.5f;
  public float jumpPower = 165f;
  public float airJumpPower = 14f;
  private float airJumpAmount = 8f;
  public float airJumpMaxAmount = 10f;
  public bool isContinuous;
  public Rigidbody2D playerRB;
  public bool isNoclip;
  public bool isAirJumping;
  public bool isJumpingInWater;
  private AnimationSystem animationSystem;
  public float checkIsGroundedLength;
  public bool isGrounded;
  public bool waterPhysics;
  public bool isReadingSign;
  public bool isPassingEntrance;
  public Sprite currententrancesprite;
  public GameObject currentDoor;
  public Vector2 previousSize;
  public GameObject currentSignBar;
  public List<GameObject> signBars = new List<GameObject>();
  public int originalActualJumpCount;
  public int actualJumpCount;
  public int jumpCount = 1;
  public Vector2 lastPosition;
  public float gravityInWater = 0.5f;
  public float gravity = 1f;
  public float dragInWater = 1.5f;
  public float drag = 1f;
  public float lastSignUpdate;
  public float[] defaultVariables;

  private void Awake()
  {
    if (!Object.op_Equality((Object) MovementManager.instance, (Object) null))
      return;
    MovementManager.instance = this;
  }

  private void Start()
  {
    this.playerRB = ((Component) this).GetComponent<Rigidbody2D>();
    this.previousSize = Vector2.op_Implicit(((Component) this).transform.localScale);
    this.animationSystem = ((Component) this).gameObject.GetComponent<AnimationSystem>();
    this.defaultVariables = new float[6]
    {
      this.gravity,
      this.gravityInWater,
      this.dragInWater,
      this.drag,
      this.movementSpeed,
      (float) ((Component) this).GetComponent<PlayerManager>().skinID
    };
  }

  private void Update()
  {
    bool isGrounded = this.isGrounded;
    this.CheckIsGrounded();
    if (this.isGrounded != isGrounded)
      this.airJumpAmount = this.airJumpMaxAmount;
    if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 3 && Mathf.RoundToInt(((Component) this).transform.position.x) >= 65 && Mathf.RoundToInt(((Component) this).transform.position.x) <= 71)
    {
      TutorialHandler.isMoved = true;
      TutorialHandler.CheckForStep8();
    }
    if (GameManager.players.ContainsKey(Client.instance.myId) && GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y)].id].collisionType == 0 && GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y)].id].isSolid && !this.isNoclip && !GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y)].id].isEntrance && !GameManager.players[Client.instance.myId].isDying)
    {
      ClientSend.ButtonClick(0);
      GameManager.players[Client.instance.myId].isDying = true;
    }
    WorldManager.instance.lightingViewness = (double) ((Component) this).transform.position.y <= (double) WorldManager.instance.lightingMinPoint || (double) ((Component) this).transform.position.y >= (double) WorldManager.instance.lightingMaxPoint ? ((double) ((Component) this).transform.position.y >= (double) WorldManager.instance.lightingMinPoint ? 0.0f : 1f) : (((Component) this).transform.position.y - (float) WorldManager.instance.lightingMinPoint) / (float) (WorldManager.instance.lightingMaxPoint / WorldManager.instance.lightingMinPoint);
    if ((double) ((Component) this).transform.position.x > 0.0 && !this.isNoclip && GameManager.players.ContainsKey(Client.instance.myId))
    {
      if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2f)].id].waterPhysics || GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].waterPhysics)
      {
        if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2f)].id].waterPhysics && !this.waterPhysics)
          GameManager.instance.CreateEffect(10, new Vector2(((Component) this).transform.position.x, WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2f)].gameObject.transform.position.y + WorldManager.instance.worldLayers[0][Mathf.RoundToInt(((Component) this).transform.position.x), Mathf.RoundToInt(((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2f)].gameObject.transform.localScale.y));
        this.playerRB.gravityScale = this.gravityInWater;
        this.playerRB.drag = this.dragInWater;
        this.waterPhysics = true;
      }
      else if (GameManager.players[Client.instance.myId].clothes[4] != 0 && !this.isGrounded && (double) this.playerRB.velocity.y < 0.0)
      {
        this.playerRB.drag = this.dragInWater * 2f;
      }
      else
      {
        this.playerRB.gravityScale = this.gravity;
        this.playerRB.drag = this.drag;
        this.waterPhysics = false;
      }
    }
    if ((double) ((Component) this).transform.position.x > 0.0)
    {
      if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isSign && !this.isReadingSign)
      {
        this.isReadingSign = true;
        ClientSend.RequestSignData(new Vector2((float) Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), (float) Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)));
        this.lastSignUpdate = Time.timeSinceLevelLoad;
      }
      if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isSign && GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isHarvestable && this.isReadingSign && (double) Time.timeSinceLevelLoad - (double) this.lastSignUpdate >= 1.0)
      {
        ClientSend.RequestSignData(new Vector2((float) Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), (float) Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)));
        this.lastSignUpdate = Time.timeSinceLevelLoad;
      }
      else if (!GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isSign && this.isReadingSign)
      {
        if (this.signBars.Contains(this.currentSignBar))
          this.signBars.Remove(this.currentSignBar);
        if (this.signBars.Count > 0)
        {
          foreach (Object signBar in this.signBars)
            Object.Destroy(signBar);
          this.signBars.Clear();
        }
        this.isReadingSign = false;
        Object.Destroy((Object) this.currentSignBar);
        this.currentSignBar = (GameObject) null;
      }
      else if (Object.op_Inequality((Object) this.currentSignBar, (Object) null) && GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isSign && this.isReadingSign && Vector2.op_Inequality(new Vector2((float) Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), (float) Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f) + 0.75f), new Vector2(this.currentSignBar.transform.position.x, this.currentSignBar.transform.position.y)))
      {
        if (this.signBars.Contains(this.currentSignBar))
          this.signBars.Remove(this.currentSignBar);
        Object.Destroy((Object) this.currentSignBar);
        this.currentSignBar = (GameObject) null;
        ClientSend.RequestSignData(new Vector2((float) Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), (float) Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)));
      }
      else if (!GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isSign && this.signBars != null)
      {
        foreach (Object signBar in this.signBars)
          Object.Destroy(signBar);
        this.signBars.Clear();
      }
      if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isEntrance && !this.isPassingEntrance)
      {
        this.isPassingEntrance = true;
        WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.blockSprites[GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].id].textureID + 1];
        this.currentDoor = WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].gameObject;
        this.currententrancesprite = GameManager.instance.blockSprites[GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].id].textureID];
      }
      else if (!GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isEntrance && this.isPassingEntrance)
      {
        this.isPassingEntrance = false;
        this.currentDoor.gameObject.GetComponent<SpriteRenderer>().sprite = this.currententrancesprite;
        this.currentDoor = (GameObject) null;
      }
      else if (Object.op_Inequality((Object) this.currentDoor, (Object) null) && GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)].id].isEntrance && this.isPassingEntrance && Vector2.op_Inequality(new Vector2((float) Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), (float) Mathf.FloorToInt(((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2f)), new Vector2(this.currentDoor.transform.position.x, this.currentDoor.transform.position.y)))
      {
        this.currentDoor.gameObject.GetComponent<SpriteRenderer>().sprite = this.currententrancesprite;
        WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.blockSprites[GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].id].textureID + 1];
        this.currentDoor = WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].gameObject;
        this.currententrancesprite = GameManager.instance.blockSprites[GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 2f), Mathf.FloorToInt(((Component) this).transform.position.y + Mathf.Abs(((Component) this).transform.localScale.y) / 2f)].id].textureID];
      }
    }
    float axis = Input.GetAxis("Horizontal");
    if (this.animationSystem.currentID != 4)
    {
      if ((double) this.playerRB.velocity.y > 0.0 && this.animationSystem.currentID != 2 && !UIController.instance.isChatOn && !GameManager.instance.isMovementFreeze)
      {
        this.animationSystem.currentID = 2;
        ClientSend.ChangeAnimation(2);
      }
      else if ((double) this.playerRB.velocity.y < 0.0 && this.animationSystem.currentID != 3)
      {
        this.animationSystem.currentID = 3;
        ClientSend.ChangeAnimation(3);
      }
      else if ((double) Mathf.Abs(axis) > 0.0 && this.animationSystem.currentID != 1 && this.isGrounded && !UIController.instance.isChatOn && !GameManager.instance.isMovementFreeze)
      {
        if (Input.GetKey((KeyCode) 97) || Input.GetKey((KeyCode) 100) || Input.GetKey((KeyCode) 276) || Input.GetKey((KeyCode) 275))
        {
          this.animationSystem.currentID = 1;
          ClientSend.ChangeAnimation(1);
        }
        else if ((double) Mathf.Abs(axis) <= 0.10000000149011612 && this.animationSystem.currentID != 0 && this.isGrounded)
        {
          this.animationSystem.currentID = 0;
          ClientSend.ChangeAnimation(0);
        }
      }
      else if (UIController.instance.isHoldingLeftButton || UIController.instance.isHoldingRightButton && !UIController.instance.isChatOn && !GameManager.instance.isMovementFreeze)
      {
        if (this.animationSystem.currentID != 1 && this.isGrounded)
        {
          if (UIController.instance.isHoldingLeftButton || UIController.instance.isHoldingRightButton || Input.GetKey((KeyCode) 97) || Input.GetKey((KeyCode) 100) || Input.GetKey((KeyCode) 276) || Input.GetKey((KeyCode) 275))
          {
            this.animationSystem.currentID = 1;
            ClientSend.ChangeAnimation(1);
          }
          else if ((double) Mathf.Abs(axis) <= 0.10000000149011612 && this.animationSystem.currentID != 0 && this.isGrounded)
          {
            this.animationSystem.currentID = 0;
            ClientSend.ChangeAnimation(0);
          }
        }
      }
      else if ((double) Mathf.Abs(axis) == 0.0 && this.animationSystem.currentID != 0 && this.isGrounded)
      {
        this.animationSystem.currentID = 0;
        ClientSend.ChangeAnimation(0);
      }
    }
    if (!this.isNoclip)
    {
      if (Object.op_Inequality((Object) this.playerRB, (Object) null))
      {
        if (!UIController.instance.isChatOn && !GameManager.instance.isMovementFreeze && !UIController.instance.itemActionMenu.activeSelf && !UIController.instance.itemActionMenu.activeSelf)
        {
          if ((double) Mathf.Abs(axis) > 0.0)
            this.Move(new Vector2(axis * this.movementSpeed, this.playerRB.velocity.y));
          if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 2 && (Input.GetKeyDown((KeyCode) 97) || Input.GetKeyDown((KeyCode) 276) || UIController.instance.isHoldingLeftButton))
          {
            TutorialHandler.isLeftKeyPressed = true;
            TutorialHandler.CheckForStep2();
          }
          if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 2 && (Input.GetKeyDown((KeyCode) 100) || Input.GetKeyDown((KeyCode) 275) || UIController.instance.isHoldingRightButton))
          {
            TutorialHandler.isRightKeyPressed = true;
            TutorialHandler.CheckForStep2();
          }
          if ((double) this.playerRB.velocity.x > 0.0 && Vector2.op_Inequality(new Vector2(((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y), new Vector2(Mathf.Abs(((Component) this).transform.localScale.x), ((Component) this).transform.localScale.y)))
            ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(-((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y));
          else if ((double) this.playerRB.velocity.x < 0.0 && Vector2.op_Inequality(new Vector2(((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y), new Vector2(-Mathf.Abs(((Component) this).transform.localScale.x), ((Component) this).transform.localScale.y)))
            ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(-((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y));
          if (!this.waterPhysics)
          {
            this.isJumpingInWater = false;
            if (this.isGrounded)
            {
              if (Input.GetKeyDown((KeyCode) 119) || Input.GetKeyDown((KeyCode) 273) || UIController.instance.isJumpButtonDown)
              {
                if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 4)
                {
                  TutorialHandler.isJumped = true;
                  TutorialHandler.CheckForStep3();
                }
                this.Jump();
              }
            }
            else
            {
              if (!this.isContinuous && (double) this.airJumpAmount > 0.0 && !Input.GetKey((KeyCode) 119) && !Input.GetKey((KeyCode) 273) && !UIController.instance.isHoldingJumpButton)
                this.airJumpAmount = 0.0f;
              if ((double) this.airJumpAmount == 0.0 || !Input.GetKey((KeyCode) 119) && !Input.GetKey((KeyCode) 273) && !UIController.instance.isHoldingJumpButton)
                this.isAirJumping = false;
              if ((double) this.airJumpAmount > 0.0 && (Input.GetKey((KeyCode) 119) || Input.GetKey((KeyCode) 273) || UIController.instance.isHoldingJumpButton))
                this.isAirJumping = true;
              if ((double) this.airJumpAmount == 0.0 && (Input.GetKeyDown((KeyCode) 119) || Input.GetKeyDown((KeyCode) 273)))
                this.Jump();
            }
          }
          else
          {
            if (!Input.GetKey((KeyCode) 119) || Input.GetKey((KeyCode) 273))
              this.isJumpingInWater = false;
            if (Input.GetKey((KeyCode) 119) || Input.GetKey((KeyCode) 273))
              this.isJumpingInWater = true;
          }
        }
      }
      else if (Object.op_Inequality((Object) ((Component) this).GetComponent<Rigidbody2D>(), (Object) null))
        this.playerRB = ((Component) this).GetComponent<Rigidbody2D>();
    }
    else if (!UIController.instance.isChatOn && !GameManager.instance.isMovementFreeze)
    {
      if (Input.GetKey((KeyCode) 119) || Input.GetKey((KeyCode) 273))
        ((Component) this).transform.position = Vector2.op_Implicit(new Vector2(((Component) this).transform.position.x, ((Component) this).transform.position.y + 0.18f));
      if (Input.GetKey((KeyCode) 115) || Input.GetKey((KeyCode) 274))
        ((Component) this).transform.position = Vector2.op_Implicit(new Vector2(((Component) this).transform.position.x, ((Component) this).transform.position.y - 0.18f));
      if (Input.GetKey((KeyCode) 97) || Input.GetKey((KeyCode) 276))
      {
        if ((double) ((Component) this).transform.localScale.x > 0.0)
          ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(-((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y));
        ((Component) this).transform.position = Vector2.op_Implicit(new Vector2(((Component) this).transform.position.x - 0.18f, ((Component) this).transform.position.y));
      }
      if (Input.GetKey((KeyCode) 100) || Input.GetKey((KeyCode) 275))
      {
        if ((double) ((Component) this).transform.localScale.x < 0.0)
          ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(-((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y));
        ((Component) this).transform.position = Vector2.op_Implicit(new Vector2(((Component) this).transform.position.x + 0.18f, ((Component) this).transform.position.y));
      }
    }
    if (Vector2.op_Inequality(this.previousSize, new Vector2(((Component) this).transform.localScale.x, ((Component) this).transform.localScale.y)) && !GameManager.players[Client.instance.myId].preventUpdatingSize)
      ClientSend.PlayerSize();
    this.previousSize = Vector2.op_Implicit(((Component) this).transform.localScale);
    if (Object.op_Inequality((Object) this.playerRB, (Object) null) && ((double) this.lastPosition.x != (double) ((Component) this).transform.position.x || (double) this.lastPosition.y != (double) ((Component) this).transform.position.y))
      ClientSend.PlayerMovement();
    this.lastPosition = Vector2.op_Implicit(((Component) this).transform.position);
  }

  private void FixedUpdate()
  {
    if (this.isAirJumping && (UIController.instance.isChatOn || GameManager.instance.isMovementFreeze || UIController.instance.itemActionMenu.activeSelf || UIController.instance.itemActionMenu.activeSelf))
      this.isAirJumping = false;
    if (this.isAirJumping && (double) this.airJumpAmount > 0.0)
    {
      this.playerRB.AddForce(new Vector2(0.0f, this.airJumpPower));
      --this.airJumpAmount;
    }
    if (!this.isJumpingInWater)
      return;
    this.Jump();
  }

  public void Move(Vector2 _velocity) => this.playerRB.velocity = _velocity;

  public void Jump()
  {
    if (this.waterPhysics)
      this.playerRB.AddForce(new Vector2(0.0f, this.jumpPower / 20f));
    else if (this.isGrounded)
    {
      this.playerRB.AddForce(new Vector2(0.0f, this.jumpPower));
      this.jumpCount = this.actualJumpCount;
    }
    else
    {
      if (this.jumpCount <= 0 || this.actualJumpCount <= 0)
        return;
      this.playerRB.AddForce(new Vector2(0.0f, this.jumpPower));
      --this.jumpCount;
    }
  }

  private void CheckIsGrounded()
  {
    RaycastHit2D raycastHit2D1 = Physics2D.Raycast(new Vector2(((Component) this).transform.position.x + Mathf.Abs(((Component) this).transform.localScale.x) / 5f, ((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2.2f), Vector2.down, this.checkIsGroundedLength);
    RaycastHit2D raycastHit2D2 = Physics2D.Raycast(new Vector2(((Component) this).transform.position.x - Mathf.Abs(((Component) this).transform.localScale.x) / 5f, ((Component) this).transform.position.y - ((Component) this).transform.localScale.y / 2.2f), Vector2.down, this.checkIsGroundedLength);
    if (Object.op_Inequality((Object) ((RaycastHit2D) ref raycastHit2D1).collider, (Object) null) || Object.op_Inequality((Object) ((RaycastHit2D) ref raycastHit2D2).collider, (Object) null))
      this.isGrounded = true;
    if (!Object.op_Equality((Object) ((RaycastHit2D) ref raycastHit2D1).collider, (Object) null) || !Object.op_Equality((Object) ((RaycastHit2D) ref raycastHit2D2).collider, (Object) null))
      return;
    this.isGrounded = false;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!(((Component) collision).gameObject.tag == "Dropped") || !(((Component) ((Component) collision).gameObject.transform.parent).tag == "Dropped") || !WorldManager.instance.droppedItems.ContainsKey(int.Parse(((Object) ((Component) collision).gameObject.transform.parent).name)))
      return;
    AudioManager.instance.PlayAudio("DropTake");
    UIController.instance.CreateTextEffect("<size=75%>" + WorldManager.instance.droppedItems[int.Parse(((Object) ((Component) collision).gameObject.transform.parent).name)].item.quantity.ToString() + "x " + GameManager.instance.items[WorldManager.instance.droppedItems[int.Parse(((Object) ((Component) collision).gameObject.transform.parent).name)].item.id].name, Vector2.op_Implicit(WorldManager.instance.droppedItems[int.Parse(((Object) ((Component) collision).gameObject.transform.parent).name)].gameObject.transform.position));
    ClientSend.TakeDroppedItem(int.Parse(((Object) ((Component) collision).gameObject.transform.parent).name), WorldManager.instance.droppedItems[int.Parse(((Object) ((Component) collision).gameObject.transform.parent).name)]);
  }
}
