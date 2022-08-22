// Decompiled with JetBrains decompiler
// Type: PutGridManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PutGridManager : MonoBehaviour
{
  public static PutGridManager instance;
  public Sprite putIndicatorTexture;
  public Sprite editIndicatorTexture;
  public Sprite baitIndicatorTexture;
  public List<PutGridManager.Grid> grids = new List<PutGridManager.Grid>();
  public bool isGridOn;

  private void Awake() => PutGridManager.instance = this;

  private void Start()
  {
  }

  private void Update()
  {
    if (GameManager.players.Count <= 0)
      return;
    this.UpdateGridPositions();
  }

  public void CreateGrid(int _radius)
  {
    int num = (_radius - 1) / 2;
    for (int index1 = 0; index1 < _radius; ++index1)
    {
      for (int index2 = 0; index2 < _radius; ++index2)
      {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(GameManager.instance.grids.transform);
        gameObject.AddComponent<SpriteRenderer>().sprite = this.putIndicatorTexture;
        ((Renderer) gameObject.GetComponent<SpriteRenderer>()).sortingOrder = 10;
        PutGridManager.Grid grid = new PutGridManager.Grid();
        grid.sprite = gameObject.GetComponent<SpriteRenderer>();
        grid.grid = gameObject;
        grid.offsetX = index1 - num;
        grid.offsetY = index2 - num;
        if (GameManager.players.ContainsKey(Client.instance.myId))
          grid.grid.transform.position = Vector2.op_Implicit(new Vector2((float) (Mathf.FloorToInt(((Component) GameManager.players[Client.instance.myId]).transform.position.x + Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).transform.localScale.x) / 2f) + grid.offsetX), (float) (Mathf.FloorToInt(((Component) GameManager.players[Client.instance.myId]).transform.position.y + ((Component) GameManager.players[Client.instance.myId]).transform.localScale.y / 2f) + grid.offsetY)));
        this.grids.Add(grid);
      }
    }
  }

  private void UpdateGridPositions()
  {
    foreach (PutGridManager.Grid grid in this.grids)
    {
      if (GameManager.players.ContainsKey(Client.instance.myId))
        grid.grid.transform.position = Vector2.op_Implicit(new Vector2((float) (Mathf.FloorToInt(((Component) GameManager.players[Client.instance.myId]).transform.position.x + Mathf.Abs(((Component) GameManager.players[Client.instance.myId]).transform.localScale.x) / 2f) + grid.offsetX), (float) (Mathf.FloorToInt(((Component) GameManager.players[Client.instance.myId]).transform.position.y + ((Component) GameManager.players[Client.instance.myId]).transform.localScale.y / 2f) + grid.offsetY)));
      if (Mathf.FloorToInt(grid.grid.transform.position.x) >= 0 && Mathf.FloorToInt(grid.grid.transform.position.x) < WorldManager.instance.worldWidth && Mathf.FloorToInt(grid.grid.transform.position.y) >= 0 && Mathf.FloorToInt(grid.grid.transform.position.y) < WorldManager.instance.worldHeight)
      {
        if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id].waterPhysics)
          grid.grid.GetComponent<SpriteRenderer>().sprite = this.baitIndicatorTexture;
        else if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id].isWrenchable)
          grid.grid.GetComponent<SpriteRenderer>().sprite = this.editIndicatorTexture;
        else
          grid.grid.GetComponent<SpriteRenderer>().sprite = this.putIndicatorTexture;
        if (this.isGridOn)
        {
          if (WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id != 0)
          {
            if (GameManager.instance.items[GameManager.players[Client.instance.myId].clothes[6]].isRod && GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isBait)
            {
              if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id].waterPhysics)
                grid.grid.SetActive(true);
              else
                grid.grid.SetActive(false);
            }
            else if (WorldManager.instance.currentBlockToPlaceID == 8)
            {
              if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id].isWrenchable)
                grid.grid.SetActive(true);
              else
                grid.grid.SetActive(false);
            }
            else
              grid.grid.SetActive(false);
          }
          else if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isHarvestable)
          {
            if (WorldManager.instance.DoesExist(Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y) - 1))
            {
              if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y) - 1].id].isSolid)
                grid.grid.SetActive(true);
              else
                grid.grid.SetActive(false);
            }
            else
              grid.grid.SetActive(false);
          }
          else
            grid.grid.SetActive(true);
        }
        else if (GameManager.instance.items[GameManager.players[Client.instance.myId].clothes[6]].isRod && GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isBait)
        {
          if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id].waterPhysics)
            grid.grid.SetActive(true);
          else
            grid.grid.SetActive(false);
        }
        else if (WorldManager.instance.currentBlockToPlaceID == 8)
        {
          foreach (GameObject wrenchIcon in GameManager.instance.wrenchIcons)
            wrenchIcon.SetActive(true);
          if (GameManager.instance.items[WorldManager.instance.worldLayers[0][Mathf.FloorToInt(grid.grid.transform.position.x), Mathf.FloorToInt(grid.grid.transform.position.y)].id].isWrenchable)
            grid.grid.SetActive(true);
          else
            grid.grid.SetActive(false);
        }
        else
        {
          foreach (GameObject wrenchIcon in GameManager.instance.wrenchIcons)
            wrenchIcon.SetActive(false);
          grid.grid.SetActive(false);
        }
      }
    }
  }

  public void SetGridOn()
  {
    this.isGridOn = true;
    foreach (PutGridManager.Grid grid in this.grids)
      grid.grid.SetActive(true);
  }

  public void SetGridOff()
  {
    this.isGridOn = false;
    foreach (PutGridManager.Grid grid in this.grids)
      grid.grid.SetActive(false);
  }

  public class Grid
  {
    public GameObject grid;
    public SpriteRenderer sprite;
    public int offsetX;
    public int offsetY;
  }
}
