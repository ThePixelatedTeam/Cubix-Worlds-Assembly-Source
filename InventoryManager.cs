// Decompiled with JetBrains decompiler
// Type: InventoryManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
  public static InventoryManager instance;
  public Inventory uiInventory;
  public GameObject slot;
  public GameObject slotsBase;
  public List<Slot> slots = new List<Slot>();
  public Deal deal;
  public GameObject indicator;
  public GameObject slotToFollow;
  public float doubleClickTime;

  private void Awake()
  {
    if (Object.op_Equality((Object) InventoryManager.instance, (Object) null))
    {
      InventoryManager.instance = this;
    }
    else
    {
      if (!Object.op_Inequality((Object) InventoryManager.instance, (Object) this))
        return;
      Debug.Log((object) "Instance already exists, destroying object!");
      Object.Destroy((Object) this);
    }
  }

  private void Update()
  {
    if (Object.op_Inequality((Object) this.slotToFollow, (Object) null))
      this.indicator.transform.position = this.slotToFollow.transform.position;
    if (this.slots != null)
    {
      if (!UIController.instance.isChatOn && Input.GetKeyDown((KeyCode) 113) && !GameManager.instance.isMovementFreeze)
      {
        foreach (Slot slot in this.slots)
        {
          if (slot.item.id == 7)
          {
            WorldManager.instance.currentBlockToPlaceID = 7;
            this.indicator.SetActive(true);
            this.slotToFollow = slot.gameObject;
            if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].typeID != 1)
            {
              if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isHarvestable)
                PutGridManager.instance.SetGridOn();
              else
                PutGridManager.instance.SetGridOff();
            }
            else
              PutGridManager.instance.SetGridOn();
          }
        }
      }
      if (!UIController.instance.isChatOn && Input.GetKeyDown((KeyCode) 101) && !GameManager.instance.isMovementFreeze)
      {
        foreach (Slot slot in this.slots)
        {
          if (slot.item.id == 8)
          {
            WorldManager.instance.currentBlockToPlaceID = 8;
            this.indicator.SetActive(true);
            this.slotToFollow = slot.gameObject;
            if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].typeID != 1)
            {
              if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isHarvestable)
                PutGridManager.instance.SetGridOn();
              else
                PutGridManager.instance.SetGridOff();
            }
            else
              PutGridManager.instance.SetGridOn();
          }
        }
      }
    }
    foreach (Slot slot in this.slots)
    {
      if (slot.isUsed)
        ((Graphic) slot.gameObject.GetComponent<Image>()).color = Color32.op_Implicit(new Color32((byte) 80, byte.MaxValue, (byte) 80, byte.MaxValue));
      else
        ((Graphic) slot.gameObject.GetComponent<Image>()).color = Color32.op_Implicit(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
    }
    if (((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).gameObject.activeSelf)
    {
      if (((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text == "Convert")
      {
        try
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will convert " + ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text + " " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s) to " + (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].rarity * int.Parse(((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text) / 3).ToString() + " Cubix(s).";
        }
        catch
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will convert 0 " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s) to 0 Cubix(s).";
        }
      }
    }
    if (((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).gameObject.activeSelf && ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text == "Sell")
    {
      if (this.deal != null)
      {
        try
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will sell " + ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text + " " + GameManager.instance.items[this.deal.dealItemID].name + "(s) to " + ((int) ((double) int.Parse(((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text) * ((double) this.deal.dealReward * 1.0 / (double) this.deal.dealItemQuantity))).ToString() + " Cubix(s).";
        }
        catch
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will sell 0 " + GameManager.instance.items[this.deal.dealItemID].name + "(s) to 0 Cubix(s).";
        }
      }
    }
    if (((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).gameObject.activeSelf)
    {
      if (((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text == "Drop")
      {
        try
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will drop " + ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text + " " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s).";
        }
        catch
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will drop 0 " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s)";
        }
      }
    }
    if (((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).gameObject.activeSelf)
    {
      if (((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text == "Trash")
      {
        try
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will trash " + ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text + " " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s).";
        }
        catch
        {
          ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will trash 0 " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s).";
        }
      }
    }
    if (!((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).gameObject.activeSelf)
      return;
    if (!(((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(0).GetChild(0)).GetComponent<TextMeshProUGUI>()).text == "Trade"))
      return;
    try
    {
      ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will trade " + ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(1)).GetComponent<TMP_InputField>().text + " " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s).";
    }
    catch
    {
      ((TMP_Text) ((Component) UIController.instance.itemActionMenu.transform.GetChild(1).GetChild(0)).GetComponent<TextMeshProUGUI>()).text = "You will trade 0 " + GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].name + "(s)";
    }
  }

  public void SetMainInventory(Inventory _inventory, Inventory _uiInventory) => this.uiInventory = _uiInventory;

  public void SetInventory()
  {
    foreach (GameItem gameItem in this.uiInventory.items)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      InventoryManager.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new InventoryManager.\u003C\u003Ec__DisplayClass12_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120._item = gameItem;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass120._item.quantity > 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.slot, this.slotsBase.transform);
        // ISSUE: reference to a compiler-generated field
        if (GameManager.instance.items[cDisplayClass120._item.id].textureID != 0)
        {
          // ISSUE: reference to a compiler-generated field
          ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[cDisplayClass120._item.id].texture;
        }
        else
          ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.blockSprites[1];
        // ISSUE: reference to a compiler-generated field
        ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass120._item.quantity.ToString();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass120.slot2 = new Slot();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass120.slot2.gameObject = gameObject;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass120.slot2.item = cDisplayClass120._item;
        if (this.slots.Count == 0)
        {
          // ISSUE: reference to a compiler-generated field
          WorldManager.instance.currentBlockToPlaceID = cDisplayClass120._item.id;
          this.indicator.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          this.slotToFollow = cDisplayClass120.slot2.gameObject;
          // ISSUE: reference to a compiler-generated field
          if (TutorialHandler.isTutorial && TutorialHandler.currentStep == 9 && cDisplayClass120._item.id == 245)
          {
            TutorialHandler.isItemSelected = true;
            TutorialHandler.CheckForStep5();
          }
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((UnityEvent) cDisplayClass120.slot2.gameObject.GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass120, __methodptr(\u003CSetInventory\u003Eb__0)));
        // ISSUE: reference to a compiler-generated field
        this.slots.Add(cDisplayClass120.slot2);
      }
    }
    RectTransform component = this.slotsBase.GetComponent<RectTransform>();
    double x1 = (double) this.slotsBase.GetComponent<RectTransform>().sizeDelta.x;
    double num1 = (double) (135 * this.slots.Count);
    Rect rect = this.slotsBase.GetComponent<RectTransform>().rect;
    double x2 = (double) ((Rect) ref rect).size.x;
    double num2 = (double) ((Mathf.CeilToInt((float) (num1 / x2)) + 1) * 135 + 120);
    Vector2 vector2 = new Vector2((float) x1, (float) num2);
    component.sizeDelta = vector2;
  }

  public void UpdateInventorySlot(int _itemID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    InventoryManager.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new InventoryManager.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.\u003C\u003E4__this = this;
    foreach (Slot slot in this.slots)
    {
      if (slot.item.id == _itemID)
      {
        if (slot.item.quantity > 0)
        {
          ((Component) slot.gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[_itemID].texture;
          foreach (GameItem gameItem in this.uiInventory.items)
          {
            if (gameItem.id == _itemID)
              ((TMP_Text) ((Component) slot.gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = gameItem.quantity.ToString();
          }
          return;
        }
        WorldManager.instance.currentBlockToPlaceID = this.slots[0].item.id;
        this.indicator.SetActive(true);
        this.slotToFollow = this.slots[0].gameObject;
        if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].typeID != 1)
        {
          if (GameManager.instance.items[WorldManager.instance.currentBlockToPlaceID].isHarvestable)
            PutGridManager.instance.SetGridOn();
          else
            PutGridManager.instance.SetGridOff();
        }
        else
          PutGridManager.instance.SetGridOn();
        Object.Destroy((Object) slot.gameObject);
        this.slots.Remove(slot);
        return;
      }
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130._item = (GameItem) null;
    foreach (GameItem gameItem in this.uiInventory.items)
    {
      if (gameItem.id == _itemID)
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass130._item = gameItem;
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass130._item.quantity > 0)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.slot, this.slotsBase.transform);
      // ISSUE: reference to a compiler-generated field
      if (GameManager.instance.items[cDisplayClass130._item.id].textureID != 0)
      {
        // ISSUE: reference to a compiler-generated field
        ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.items[cDisplayClass130._item.id].texture;
      }
      else
        ((Component) gameObject.transform.GetChild(0).GetChild(0)).GetComponent<Image>().sprite = GameManager.instance.blockSprites[1];
      // ISSUE: reference to a compiler-generated field
      ((TMP_Text) ((Component) gameObject.transform.GetChild(1)).GetComponent<TextMeshProUGUI>()).text = cDisplayClass130._item.quantity.ToString();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass130.slot2 = new Slot();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass130.slot2.gameObject = gameObject;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass130.slot2.item = cDisplayClass130._item;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) cDisplayClass130.slot2.gameObject.GetComponent<Button>().onClick).AddListener(new UnityAction((object) cDisplayClass130, __methodptr(\u003CUpdateInventorySlot\u003Eb__0)));
      // ISSUE: reference to a compiler-generated field
      this.slots.Add(cDisplayClass130.slot2);
    }
    RectTransform component = this.slotsBase.GetComponent<RectTransform>();
    double x1 = (double) this.slotsBase.GetComponent<RectTransform>().sizeDelta.x;
    double num1 = (double) (135 * this.slots.Count);
    Rect rect = this.slotsBase.GetComponent<RectTransform>().rect;
    double x2 = (double) ((Rect) ref rect).size.x;
    double num2 = (double) ((Mathf.CeilToInt((float) (num1 / x2)) + 1) * 135 + 120);
    Vector2 vector2 = new Vector2((float) x1, (float) num2);
    component.sizeDelta = vector2;
  }

  public Slot FindSlotFromItemID(int _itemID)
  {
    foreach (Slot slot in this.slots)
    {
      if (slot.item.id == _itemID)
        return slot;
    }
    return (Slot) null;
  }
}
