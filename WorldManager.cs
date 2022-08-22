// Decompiled with JetBrains decompiler
// Type: WorldManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
  public static WorldManager instance;
  public string worldName;
  public int worldValue;
  public bool worldDataFilled;
  public WorldManager.Block[][,] worldLayers = new WorldManager.Block[2][,];
  public List<Vector2> visibleBlocks = new List<Vector2>();
  public int[,] lightMap;
  public int worldWidth;
  public int worldHeight;
  public GameObject droppedItemPrefab;
  public GameObject worldSpaceCanvas;
  public Dictionary<int, DroppedItem> droppedItems = new Dictionary<int, DroppedItem>();
  public List<WorldManager.BreakingAnimationFrame> breakingAnimations = new List<WorldManager.BreakingAnimationFrame>();
  public Dictionary<Vector2, string> signs = new Dictionary<Vector2, string>();
  public Dictionary<Vector2, bool> entrances = new Dictionary<Vector2, bool>();
  public Dictionary<Vector2, string> doors = new Dictionary<Vector2, string>();
  public Sprite testSprite;
  public Texture2D testTexture;
  public Dictionary<GameObject, WorldManager.BlockAnimation> blockAnimations = new Dictionary<GameObject, WorldManager.BlockAnimation>();
  public int blockAnimationSpeed = 1;
  public Dictionary<Vector2, SpriteRenderer> rainbowBlocks = new Dictionary<Vector2, SpriteRenderer>();
  public Dictionary<Vector2, WorldManager.AnimatedBlock> animatedBlocks = new Dictionary<Vector2, WorldManager.AnimatedBlock>();
  public int currentBlockToPlaceID = 7;
  public Dictionary<int, float> lightingLevels = new Dictionary<int, float>()
  {
    {
      0,
      1f
    },
    {
      1,
      1f
    },
    {
      2,
      0.8f
    },
    {
      3,
      0.5f
    },
    {
      4,
      0.3f
    },
    {
      5,
      0.2f
    }
  };
  public float lightingViewness;
  public int lightingMaxPoint = 65;
  public int lightingMinPoint = 55;

  private void Awake()
  {
    if (Object.op_Equality((Object) WorldManager.instance, (Object) null))
    {
      WorldManager.instance = this;
    }
    else
    {
      if (!Object.op_Inequality((Object) WorldManager.instance, (Object) this))
        return;
      Debug.Log((object) "Instance already exists, destroying object!");
      Object.Destroy((Object) this);
    }
  }

  private void Start() => new Thread(new ThreadStart(this.AnimateBlocks)).Start();

  private void Update()
  {
    if (this.worldLayers[0] != null)
    {
      List<Vector2> vector2List = new List<Vector2>();
      foreach (Vector2 visibleBlock in this.visibleBlocks)
      {
        if ((double) visibleBlock.x < (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x || (double) visibleBlock.x > (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(1.2f, 0.0f))).x || (double) visibleBlock.y < (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y || (double) visibleBlock.y > (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, 1.2f))).y)
        {
          this.worldLayers[0][(int) visibleBlock.x, (int) visibleBlock.y].gameObject.SetActive(false);
          this.worldLayers[1][(int) visibleBlock.x, (int) visibleBlock.y].gameObject.SetActive(false);
          vector2List.Add(visibleBlock);
        }
      }
      foreach (Vector2 vector2 in vector2List)
        this.visibleBlocks.Remove(vector2);
      vector2List.Clear();
      for (int index1 = 0; (double) index1 < (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, 1.2f))).y - (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y; ++index1)
      {
        for (int index2 = 0; (double) index2 < (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(1.2f, 0.0f))).x - (double) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x; ++index2)
        {
          if (!this.visibleBlocks.Contains(new Vector2((float) ((int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x + index2), (float) ((int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y + index1))) && (int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x + index2 >= 0 && (int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x + index2 < this.worldLayers[0].GetLength(0) && (int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y + index1 >= 0 && (int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y + index1 < this.worldLayers[0].GetLength(1))
          {
            this.visibleBlocks.Add(new Vector2((float) ((int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x + index2), (float) ((int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y + index1)));
            this.worldLayers[0][(int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x + index2, (int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y + index1].gameObject.SetActive(true);
            this.worldLayers[1][(int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(-0.1f, 0.0f))).x + index2, (int) Camera.main.ViewportToWorldPoint(Vector2.op_Implicit(new Vector2(0.0f, -0.1f))).y + index1].gameObject.SetActive(true);
          }
        }
      }
    }
    for (int index = 0; index < this.blockAnimations.Count; ++index)
    {
      GameObject key = ((IEnumerable<KeyValuePair<GameObject, WorldManager.BlockAnimation>>) this.blockAnimations).ElementAt<KeyValuePair<GameObject, WorldManager.BlockAnimation>>(index).Key;
      WorldManager.BlockAnimation blockAnimation = ((IEnumerable<KeyValuePair<GameObject, WorldManager.BlockAnimation>>) this.blockAnimations).ElementAt<KeyValuePair<GameObject, WorldManager.BlockAnimation>>(index).Value;
      key.transform.localScale = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(key.transform.localScale), new Vector2(1f, 1f), (float) this.blockAnimationSpeed * Time.deltaTime));
      key.transform.position = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(key.transform.position), blockAnimation.destination, (float) this.blockAnimationSpeed * Time.deltaTime));
      if ((double) key.transform.localScale.x > 0.949999988079071)
      {
        if (Object.op_Equality((Object) blockAnimation.spriteRenderer.sprite, (Object) blockAnimation.previousSprite))
          blockAnimation.spriteRenderer.sprite = blockAnimation.sprite;
        Object.Destroy((Object) key);
        this.blockAnimations.Remove(key);
        break;
      }
    }
    foreach (SpriteRenderer spriteRenderer in this.rainbowBlocks.Values)
      spriteRenderer.color = GameManager.instance.RainbowColor();
  }

  public void SetWorldData(
    string _foregroundWorldData,
    string _backgroundWorldData,
    int _worldWidth,
    int _worldHeight)
  {
    this.worldLayers[0] = new WorldManager.Block[_worldWidth, _worldHeight];
    this.worldLayers[1] = new WorldManager.Block[_worldWidth, _worldHeight];
    this.lightMap = new int[_worldWidth, _worldHeight];
    this.worldWidth = _worldWidth;
    this.worldHeight = _worldHeight;
    this.CreateArea();
    for (int index1 = 0; index1 < this.lightMap.GetLength(0); ++index1)
    {
      for (int index2 = 0; index2 < this.lightMap.GetLength(1); ++index2)
      {
        this.lightMap[index1, index2] = 5;
        for (int index3 = 0; index3 < this.worldLayers.Length; ++index3)
          this.worldLayers[index3][index1, index2].spriteRenderer.color = new Color(this.lightingLevels[5], this.lightingLevels[5], this.lightingLevels[5], 1f);
      }
    }
    string[] strArray1 = _foregroundWorldData.Split('|', StringSplitOptions.None);
    string[] strArray2 = _backgroundWorldData.Split('|', StringSplitOptions.None);
    ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
    ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Loaded World Data 4/6)";
    for (int index = 1; index < strArray2.Length; ++index)
    {
      int num1 = (index - 1) % _worldWidth;
      int num2 = (int) Mathf.Floor((float) ((index - 1) / this.worldWidth));
      this.EditBlockData(int.Parse(strArray2[index].Split(':', StringSplitOptions.None)[0]), (float) int.Parse(strArray2[index].Split(':', StringSplitOptions.None)[1]), 1, new Vector2((float) num1, (float) num2), true, 0, false);
    }
    ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
    ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Loaded World Background Data 5/6)";
    for (int index = 1; index < strArray1.Length; ++index)
    {
      int num3 = (index - 1) % _worldWidth;
      int num4 = (int) Mathf.Floor((float) ((index - 1) / this.worldWidth));
      this.EditBlockData(int.Parse(strArray1[index].Split(':', StringSplitOptions.None)[0]), (float) int.Parse(strArray1[index].Split(':', StringSplitOptions.None)[1]), 0, new Vector2((float) num3, (float) num4), true, 0, false);
    }
    ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(0)).GetComponent<Slider>().value = 0.0f;
    ((TMP_Text) ((Component) UIController.instance.loadingScreen.transform.GetChild(1).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = "Loading... (Loaded World Foreground Data 6/6)";
    UIController.instance.SetActive(UIController.instance.loadingScreen);
  }

  public void CreateArea()
  {
    for (int index1 = 0; index1 < this.worldHeight; ++index1)
    {
      for (int index2 = 0; index2 < this.worldWidth; ++index2)
      {
        GameObject gameObject = new GameObject("Block");
        WorldManager.Block block = new WorldManager.Block();
        block.id = 0;
        block.gameObject = gameObject;
        block.position = new Vector2((float) index2, (float) index1);
        gameObject.transform.position = Vector2.op_Implicit(new Vector2((float) index2, (float) index1));
        gameObject.tag = "Block";
        gameObject.AddComponent<SpriteRenderer>().sprite = GameManager.instance.blockSprites[GameManager.instance.items[0].textureID];
        ((Renderer) block.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = 0;
        block.spriteRenderer = block.gameObject.GetComponent<SpriteRenderer>();
        ((Collider2D) gameObject.AddComponent<EdgeCollider2D>()).usedByEffector = true;
        gameObject.GetComponent<EdgeCollider2D>().points = new Vector2[5]
        {
          new Vector2(-0.5f, 0.5f),
          new Vector2(0.5f, 0.5f),
          new Vector2(0.5f, -0.5f),
          new Vector2(-0.5f, -0.5f),
          new Vector2(-0.5f, 0.5f)
        };
        gameObject.transform.SetParent(((Component) GameManager.instance).gameObject.transform);
        gameObject.SetActive(false);
        ((Behaviour) gameObject.GetComponent<EdgeCollider2D>()).enabled = GameManager.instance.items[0].isSolid;
        this.worldLayers[0][index2, index1] = block;
      }
    }
    for (int index3 = 0; index3 < this.worldHeight; ++index3)
    {
      for (int index4 = 0; index4 < this.worldWidth; ++index4)
      {
        GameObject gameObject = new GameObject("Background");
        WorldManager.Block block = new WorldManager.Block();
        block.id = 0;
        block.gameObject = gameObject;
        block.position = new Vector2((float) index4, (float) index3);
        gameObject.transform.position = Vector2.op_Implicit(new Vector2((float) index4, (float) index3));
        gameObject.tag = "Block";
        gameObject.AddComponent<SpriteRenderer>().sprite = GameManager.instance.blockSprites[GameManager.instance.items[0].textureID];
        ((Renderer) block.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = -2;
        block.spriteRenderer = block.gameObject.GetComponent<SpriteRenderer>();
        gameObject.transform.SetParent(((Component) GameManager.instance).gameObject.transform);
        gameObject.SetActive(false);
        this.worldLayers[1][index4, index3] = block;
      }
    }
  }

  public void EditBlockData(
    int _newID,
    float _blockHealth,
    int _layer,
    Vector2 _blockPosition,
    bool _isGenerating,
    int _clientID,
    bool _animation)
  {
    float health = (float) GameManager.instance.items[_newID].health;
    if (GameManager.players.ContainsKey(_clientID) && ((double) _blockHealth < (double) this.worldLayers[_layer][(int) _blockPosition.x, (int) _blockPosition.y].health || _newID != this.worldLayers[_layer][(int) _blockPosition.x, (int) _blockPosition.y].id))
    {
      if (GameManager.players[_clientID].skinID == 1)
      {
        GameManager.instance.CreateEffect(7, _blockPosition);
        if ((double) ((Component) GameManager.players[_clientID]).transform.localScale.x > 0.0)
          EffectManager.instance.ShootLaser(new Vector2(((Component) GameManager.players[_clientID]).transform.position.x + 0.07f, ((Component) GameManager.players[_clientID]).transform.position.y + 0.2f), _blockPosition);
        else
          EffectManager.instance.ShootLaser(new Vector2(((Component) GameManager.players[_clientID]).transform.position.x - 0.1f, ((Component) GameManager.players[_clientID]).transform.position.y + 0.25f), _blockPosition);
      }
      else if (GameManager.players[_clientID].skinID == 3)
      {
        GameManager.instance.CreateEffect(7, _blockPosition);
        if ((double) ((Component) GameManager.players[_clientID]).transform.localScale.x > 0.0)
          EffectManager.instance.Punch(new Vector2(((Component) GameManager.players[_clientID]).transform.position.x + 0.27f, ((Component) GameManager.players[_clientID]).transform.position.y + 0.1f), _blockPosition);
        else
          EffectManager.instance.Punch(new Vector2(((Component) GameManager.players[_clientID]).transform.position.x - 0.3f, ((Component) GameManager.players[_clientID]).transform.position.y + 0.15f), _blockPosition);
      }
    }
    if (_newID != 0 && (double) _blockHealth != (double) GameManager.instance.items[_newID].health && (double) _blockHealth != 0.0)
    {
      bool flag = false;
      foreach (WorldManager.BreakingAnimationFrame breakingAnimation in this.breakingAnimations)
      {
        if (Vector2.op_Equality(breakingAnimation.blockPosition, _blockPosition))
        {
          breakingAnimation.frame = Mathf.FloorToInt((float) GameManager.instance.breakingAnimationSprites.Count - _blockHealth / (health / (float) GameManager.instance.breakingAnimationSprites.Count));
          breakingAnimation.gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.breakingAnimationSprites[breakingAnimation.frame];
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        WorldManager.BreakingAnimationFrame breakingAnimationFrame = new WorldManager.BreakingAnimationFrame();
        breakingAnimationFrame.gameObject = new GameObject();
        ((Object) breakingAnimationFrame.gameObject).name = "Animation";
        breakingAnimationFrame.frame = Mathf.FloorToInt((float) GameManager.instance.breakingAnimationSprites.Count - _blockHealth / (health / (float) GameManager.instance.breakingAnimationSprites.Count));
        breakingAnimationFrame.gameObject.AddComponent<SpriteRenderer>().sprite = GameManager.instance.breakingAnimationSprites[breakingAnimationFrame.frame];
        breakingAnimationFrame.blockPosition = _blockPosition;
        breakingAnimationFrame.gameObject.transform.position = Vector2.op_Implicit(_blockPosition);
        ((Renderer) breakingAnimationFrame.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = 9 - _layer;
        this.breakingAnimations.Add(breakingAnimationFrame);
      }
    }
    if (!GameManager.instance.items[_newID].isSolid && _layer == 0)
      this.UpdateLightMap(_blockPosition);
    else if (GameManager.instance.items[_newID].isSolid && !_isGenerating && _newID != this.worldLayers[_layer][(int) _blockPosition.x, (int) _blockPosition.y].id)
      this.RemoveLight(_blockPosition);
    if (_newID == 0 || (double) _blockHealth == (double) GameManager.instance.items[_newID].health)
    {
      foreach (WorldManager.BreakingAnimationFrame breakingAnimation in this.breakingAnimations)
      {
        if (Vector2.op_Equality(breakingAnimation.blockPosition, _blockPosition))
        {
          Object.Destroy((Object) breakingAnimation.gameObject);
          this.breakingAnimations.Remove(breakingAnimation);
          break;
        }
      }
    }
    WorldManager.Block block = this.worldLayers[_layer][(int) _blockPosition.x, (int) _blockPosition.y];
    Sprite blockSprite = GameManager.instance.blockSprites[GameManager.instance.items[_newID].textureID];
    Sprite _sprite = this.CheckBlockTexture(_blockPosition, _layer, _newID);
    if (_newID == 0)
    {
      if (GameManager.instance.items[block.id].animationTextureIDs != null)
      {
        this.animatedBlocks.Remove(new Vector2((float) (int) _blockPosition.x, (float) (int) _blockPosition.y));
        block.isAnimationRunning = false;
      }
    }
    else if (GameManager.instance.items[_newID].animationTextureIDs != null && GameManager.instance.items[_newID].animationTextureIDs.Length != 0 && block.id != _newID)
    {
      WorldManager.AnimatedBlock animatedBlock = new WorldManager.AnimatedBlock();
      animatedBlock.block = block;
      animatedBlock.sprites = new Sprite[GameManager.instance.items[_newID].animationTextureIDs.Length];
      block.isAnimationRunning = true;
      for (int index = 0; index < animatedBlock.sprites.Length; ++index)
        animatedBlock.sprites[index] = GameManager.instance.blockSprites[GameManager.instance.items[_newID].animationTextureIDs[index]];
      this.animatedBlocks.Add(_blockPosition, animatedBlock);
    }
    if (_newID == 154 && block.id != 154)
      this.rainbowBlocks.Add(_blockPosition, block.gameObject.GetComponent<SpriteRenderer>());
    else if (block.id == 154 && _newID != 154)
      this.rainbowBlocks.Remove(_blockPosition);
    if (block.id != _newID && !_isGenerating)
    {
      AudioManager.instance.PlayAudio("Break");
      GameManager.instance.CreateEffect(1, _blockPosition);
      if (_newID != 0)
      {
        if (_animation)
        {
          GameObject key = new GameObject("Block");
          key.transform.localScale = Vector2.op_Implicit(new Vector2(0.0f, 0.0f));
          key.transform.position = ((Component) GameManager.players[_clientID]).transform.position;
          key.AddComponent<SpriteRenderer>().sprite = _sprite;
          this.blockAnimations.Add(key, new WorldManager.BlockAnimation(_blockPosition, block.gameObject.GetComponent<SpriteRenderer>(), _sprite, block.gameObject.GetComponent<SpriteRenderer>().sprite));
        }
      }
      else
        block.gameObject.GetComponent<SpriteRenderer>().sprite = this.CheckBlockTexture(_blockPosition, _layer, _newID);
    }
    else
      block.gameObject.GetComponent<SpriteRenderer>().sprite = this.CheckBlockTexture(_blockPosition, _layer, _newID);
    if (_layer == 0 && block.id != _newID && _newID != 0)
      block.gameObject.AddComponent<DropShadow>().ShadowOffset = new Vector2(0.1f, -0.1f);
    else if (_layer == 0 && _newID == 0 && block.id != _newID)
    {
      GameManager.instance.dropShadows.Remove(block.gameObject.GetComponent<DropShadow>());
      Object.Destroy((Object) block.gameObject.GetComponent<DropShadow>().shadowGameobject);
      Object.Destroy((Object) block.gameObject.GetComponent<DropShadow>());
    }
    else if (_layer == 1 && Object.op_Inequality((Object) block.gameObject.GetComponent<DropShadow>(), (Object) null))
    {
      GameManager.instance.dropShadows.Remove(block.gameObject.GetComponent<DropShadow>());
      Object.Destroy((Object) block.gameObject.GetComponent<DropShadow>().shadowGameobject);
      Object.Destroy((Object) block.gameObject.GetComponent<DropShadow>());
    }
    if (GameManager.instance.items[_newID].collisionType == 0 && GameManager.instance.items[block.id].collisionType == 1 && _layer == 0)
    {
      Object.Destroy((Object) block.gameObject.GetComponent<PlatformEffector2D>());
      block.gameObject.GetComponent<EdgeCollider2D>().points = new Vector2[5]
      {
        new Vector2(-0.5f, 0.5f),
        new Vector2(0.5f, 0.5f),
        new Vector2(0.5f, -0.5f),
        new Vector2(-0.5f, -0.5f),
        new Vector2(-0.5f, 0.5f)
      };
    }
    else if (GameManager.instance.items[_newID].collisionType == 1 && GameManager.instance.items[block.id].collisionType == 0 && _layer == 0)
    {
      block.gameObject.AddComponent<PlatformEffector2D>();
      block.gameObject.GetComponent<EdgeCollider2D>().points = new Vector2[2]
      {
        new Vector2(-0.5f, 0.5f),
        new Vector2(0.5f, 0.5f)
      };
    }
    if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 6 && _newID == 0 && block.id == 245)
    {
      ++TutorialHandler.brokenBlockCount;
      TutorialHandler.CheckForStep4();
    }
    if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 10 && _newID == 245 && block.id == 0)
    {
      ++TutorialHandler.placedBlockCount;
      TutorialHandler.CheckForStep6();
    }
    if (!_animation)
      block.gameObject.GetComponent<SpriteRenderer>().sprite = this.CheckBlockTexture(_blockPosition, _layer, _newID);
    block.id = _newID;
    block.health = _blockHealth;
    if (this.DoesExist((int) block.position.x, (int) block.position.y - 1))
      this.worldLayers[_layer][(int) block.position.x, (int) block.position.y - 1].gameObject.GetComponent<SpriteRenderer>().sprite = this.CheckBlockTexture(new Vector2((float) (int) block.position.x, (float) ((int) block.position.y - 1)), _layer, this.worldLayers[_layer][(int) block.position.x, (int) block.position.y - 1].id);
    if (_layer == 0)
      ((Behaviour) block.gameObject.GetComponent<EdgeCollider2D>()).enabled = GameManager.instance.items[_newID].isSolid;
    if (_layer == 0 && GameManager.instance.items[_newID].waterPhysics)
    {
      ((Object) block.gameObject).name = "Liquid";
      ((Renderer) block.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = 7;
    }
    else
    {
      if (_layer != 0)
        return;
      ((Object) block.gameObject).name = "Other";
      ((Renderer) block.gameObject.GetComponent<SpriteRenderer>()).sortingOrder = 0;
    }
  }

  public void UpdateLightMap(Vector2 _lightPosition)
  {
    if (!this.DoesExistInLightMap((int) _lightPosition.x, (int) _lightPosition.y))
      return;
    for (int index1 = 0; index1 < 11; ++index1)
    {
      for (int index2 = 0; index2 < 11; ++index2)
      {
        int _x = (int) _lightPosition.x - 5 + index1;
        int _y = (int) _lightPosition.y - 5 + index2;
        if (this.DoesExistInLightMap(_x, _y))
        {
          int key = Mathf.Clamp((int) Vector2.Distance(_lightPosition, new Vector2((float) _x, (float) _y)), 0, 5);
          if (key < this.lightMap[_x, _y])
          {
            this.lightMap[_x, _y] = key;
            for (int index3 = 0; index3 < this.worldLayers.Length; ++index3)
              this.worldLayers[index3][_x, _y].spriteRenderer.color = new Color(this.lightingLevels[key], this.lightingLevels[key], this.lightingLevels[key], 1f);
          }
        }
      }
    }
  }

  public void RemoveLight(Vector2 _lightPosition)
  {
    if (!this.DoesExistInLightMap((int) _lightPosition.x, (int) _lightPosition.y))
      return;
    this.lightMap[(int) _lightPosition.x, (int) _lightPosition.y] = 5;
    for (int index1 = 0; index1 < 11; ++index1)
    {
      for (int index2 = 0; index2 < 11; ++index2)
      {
        int _x = (int) _lightPosition.x - 5 + index1;
        int _y = (int) _lightPosition.y - 5 + index2;
        if (this.DoesExistInLightMap(_x, _y) && this.lightMap[_x, _y] != 0)
        {
          this.lightMap[_x, _y] = 5;
          for (int index3 = 0; index3 < this.worldLayers.Length; ++index3)
            this.worldLayers[index3][_x, _y].spriteRenderer.color = new Color(this.lightingLevels[5], this.lightingLevels[5], this.lightingLevels[5], 1f);
        }
      }
    }
    for (int index4 = 0; index4 < 21; ++index4)
    {
      for (int index5 = 0; index5 < 21; ++index5)
      {
        int _x = (int) _lightPosition.x - 10 + index4;
        int _y = (int) _lightPosition.y - 10 + index5;
        if (this.DoesExistInLightMap(_x, _y) && this.lightMap[_x, _y] == 0)
          this.UpdateLightMap(new Vector2((float) _x, (float) _y));
      }
    }
  }

  public void CreateDroppedItem(int _id, GameItem _gameItem, Vector2 _position)
  {
    DroppedItem droppedItem = new DroppedItem();
    droppedItem.item = _gameItem;
    droppedItem.position = _position;
    GameObject gameObject = Object.Instantiate<GameObject>(this.droppedItemPrefab);
    gameObject.transform.SetParent(this.worldSpaceCanvas.transform);
    ((Component) gameObject.transform.GetChild(0).GetChild(1)).GetComponent<Image>().sprite = GameManager.instance.items[_gameItem.id].texture;
    ((TMP_Text) ((Component) gameObject.transform.GetChild(0).GetChild(2)).GetComponent<TextMeshProUGUI>()).text = _gameItem.quantity.ToString();
    ((Transform) gameObject.GetComponent<RectTransform>()).localPosition = Vector2.op_Implicit(new Vector2(_position.x, _position.y - 0.25f));
    droppedItem.id = _id;
    ((Object) gameObject).name = droppedItem.id.ToString();
    droppedItem.gameObject = gameObject;
    this.droppedItems.Add(droppedItem.id, droppedItem);
  }

  public void RemoveDroppedItem(int _id)
  {
    Object.Destroy((Object) this.droppedItems[_id].gameObject);
    this.droppedItems.Remove(_id);
  }

  public bool DoesExist(int _x, int _y) => _x >= 0 && _x < this.worldLayers[0].GetLength(0) && _y >= 0 && _y < this.worldLayers[0].GetLength(1);

  public bool DoesExistInLightMap(int _x, int _y) => _x >= 0 && _x < this.lightMap.GetLength(0) && _y >= 0 && _y < this.lightMap.GetLength(1);

  public void AnimateBlocks()
  {
    while (true)
    {
      Thread.Sleep(250);
      ThreadManager.ExecuteOnMainThread((Action) (() =>
      {
        if (this.animatedBlocks.Values.Count <= 0)
          return;
        foreach (WorldManager.AnimatedBlock animatedBlock in ((IEnumerable<WorldManager.AnimatedBlock>) this.animatedBlocks.Values).ToList<WorldManager.AnimatedBlock>())
        {
          if (this.worldLayers[GameManager.instance.items[animatedBlock.block.id].layer][(int) animatedBlock.block.position.x, (int) animatedBlock.block.position.y].isAnimationRunning)
            animatedBlock.block.spriteRenderer.sprite = animatedBlock.sprites[animatedBlock.currentFrame];
          ++animatedBlock.currentFrame;
          if (animatedBlock.currentFrame == animatedBlock.sprites.Length)
            animatedBlock.currentFrame = 0;
        }
      }));
    }
  }

  public Sprite CheckBlockTexture(Vector2 _block, int _layer, int _newID)
  {
    if ((double) _block.x < 0.0 || (double) _block.x >= (double) this.worldWidth || (double) _block.y < 0.0 || (double) _block.y >= (double) this.worldHeight)
      return (Sprite) null;
    WorldManager.Block block = this.worldLayers[_layer][(int) _block.x, (int) _block.y];
    for (int index = 0; index < GameManager.instance.items[_newID].textureIDs.Length; ++index)
    {
      int textureId = GameManager.instance.items[_newID].textureIDs[index];
      if (textureId != 0 && index == 0 && (double) _block.y - 1.0 >= 0.0)
        return !GameManager.instance.items[this.worldLayers[_layer][(int) _block.x, (int) _block.y + 1].id].isSolid && !GameManager.instance.items[this.worldLayers[_layer][(int) _block.x, (int) _block.y + 1].id].waterPhysics ? GameManager.instance.blockSprites[textureId] : GameManager.instance.blockSprites[GameManager.instance.items[_newID].textureID];
    }
    return block.id == _newID ? block.gameObject.GetComponent<SpriteRenderer>().sprite : GameManager.instance.blockSprites[GameManager.instance.items[_newID].textureID];
  }

  public class Block
  {
    public int id;
    public float health;
    public Vector2 position;
    public GameObject gameObject;
    public SpriteRenderer spriteRenderer;
    public bool isAnimationRunning;
  }

  public class AnimatedBlock
  {
    public WorldManager.Block block;
    public int currentFrame;
    public Sprite[] sprites;
  }

  public class BreakingAnimationFrame
  {
    public int frame;
    public Vector2 blockPosition;
    public GameObject gameObject;
  }

  public class BlockAnimation
  {
    public Vector2 destination;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public Sprite previousSprite;

    public BlockAnimation(
      Vector2 _destination,
      SpriteRenderer _spriteRenderer,
      Sprite _sprite,
      Sprite _previousSprite)
    {
      this.destination = _destination;
      this.spriteRenderer = _spriteRenderer;
      this.sprite = _sprite;
      this.previousSprite = _previousSprite;
    }
  }
}
