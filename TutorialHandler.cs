// Decompiled with JetBrains decompiler
// Type: TutorialHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;

public static class TutorialHandler
{
  public static DialogueHandler instance;
  public static bool isTutorial;
  public static int currentStep;
  public static bool isRightKeyPressed;
  public static bool isLeftKeyPressed;
  public static bool isJumped;
  public static int brokenBlockCount;
  public static int placedBlockCount;
  public static bool isItemSelected;
  public static bool isPortalTapped;
  public static bool isMoved;

  public static void StartTutorial()
  {
    TutorialHandler.isTutorial = true;
    TutorialHandler.currentStep = 0;
    UIController.instance.tutorialManager.SetActive(true);
    UIController.instance.tutorialManager.transform.localScale = Vector2.op_Implicit(Vector2.zero);
    TutorialHandler.instance = UIController.instance.tutorialManager.GetComponent<DialogueHandler>();
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }

  public static void CheckForStep2()
  {
    if (!TutorialHandler.isRightKeyPressed || !TutorialHandler.isLeftKeyPressed)
      return;
    TutorialHandler.currentStep = 3;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }

  public static void CheckForStep3()
  {
    if (!TutorialHandler.isJumped)
      return;
    TutorialHandler.currentStep = 5;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }

  public static void CheckForStep4()
  {
    if (TutorialHandler.brokenBlockCount < 4)
      return;
    TutorialHandler.currentStep = 7;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }

  public static void CheckForStep5()
  {
    if (!TutorialHandler.isItemSelected)
      return;
    TutorialHandler.currentStep = 10;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }

  public static void CheckForStep6()
  {
    if (TutorialHandler.placedBlockCount < 4)
      return;
    TutorialHandler.currentStep = 11;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
    foreach (Slot slot in InventoryManager.instance.slots)
    {
      if (slot.item.id == 7)
      {
        WorldManager.instance.currentBlockToPlaceID = 7;
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
      }
    }
  }

  public static void CheckForStep7()
  {
    if (!TutorialHandler.isPortalTapped)
      return;
    TutorialHandler.currentStep = 12;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }

  public static void CheckForStep8()
  {
    if (!TutorialHandler.isMoved)
      return;
    TutorialHandler.currentStep = 4;
    TutorialHandler.instance.ShowTextFunction(TutorialHandler.currentStep);
  }
}
