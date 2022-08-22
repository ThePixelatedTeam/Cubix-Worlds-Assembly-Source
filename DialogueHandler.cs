// Decompiled with JetBrains decompiler
// Type: DialogueHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
  public DialogueHandler.Dialogue[] dialogues;
  public int cooldownMS;
  public TextMeshProUGUI text;
  public TextMeshProUGUI infoText;
  public Sprite[] cubotExpressions;
  public Image cubotIcon;

  public IEnumerator ShowText(int _dialogueID)
  {
    DialogueHandler dialogueHandler = this;
    if (_dialogueID == 0)
    {
      yield return (object) new WaitForSeconds(4f);
      UIController.instance.SetActive(((Component) dialogueHandler).gameObject, true);
    }
    DialogueHandler.Dialogue _dialogue = dialogueHandler.dialogues[_dialogueID];
    dialogueHandler.cubotIcon.sprite = dialogueHandler.cubotExpressions[_dialogue.expressionID];
    TextMeshProUGUI infoText = dialogueHandler.infoText;
    int num = _dialogueID + 1;
    string str1 = num.ToString();
    num = dialogueHandler.dialogues.Length;
    string str2 = num.ToString();
    string str3 = "Step " + str1 + "/" + str2;
    ((TMP_Text) infoText).text = str3;
    bool _isTag = false;
    for (int i = 0; i < _dialogue.dialogue.Length; num = i++)
    {
      if (_dialogueID == TutorialHandler.currentStep)
      {
        if (_isTag && _dialogue.dialogue[i - 1] == '>')
          _isTag = false;
        if (_dialogue.dialogue[i] == '<')
          _isTag = true;
        if (!_isTag)
        {
          ((TMP_Text) dialogueHandler.text).text = _dialogue.dialogue.Substring(0, i + 1);
          LayoutRebuilder.ForceRebuildLayoutImmediate(((TMP_Text) dialogueHandler.text).transform.parent as RectTransform);
          LayoutRebuilder.ForceRebuildLayoutImmediate(((TMP_Text) dialogueHandler.text).transform.parent.parent as RectTransform);
        }
        if (!_isTag)
          yield return (object) new WaitForSeconds((float) dialogueHandler.cooldownMS / 1000f);
      }
      else
        yield return (object) null;
    }
    if (_dialogue.autoSkip)
    {
      yield return (object) new WaitForSeconds(_dialogue.autoSkipMS / 1000f);
      if (_dialogueID < dialogueHandler.dialogues.Length - 1)
      {
        TutorialHandler.currentStep = _dialogueID + 1;
        dialogueHandler.StartCoroutine(dialogueHandler.ShowText(_dialogueID + 1));
      }
      else
      {
        if (((Object) ((Component) dialogueHandler).gameObject).name == "Tutorial Manager")
        {
          TutorialHandler.isTutorial = false;
          ClientSend.RequestData(4);
        }
        UIController.instance.SetActive(((Component) dialogueHandler).gameObject, false);
      }
    }
  }

  public IEnumerator ShowText(int _dialogueID, string _title)
  {
    DialogueHandler dialogueHandler = this;
    DialogueHandler.Dialogue _dialogue = dialogueHandler.dialogues[_dialogueID];
    dialogueHandler.cubotIcon.sprite = dialogueHandler.cubotExpressions[_dialogue.expressionID];
    ((TMP_Text) dialogueHandler.infoText).text = _title;
    bool _isTag = false;
    for (int i = 0; i < _dialogue.dialogue.Length; ++i)
    {
      if (_isTag && _dialogue.dialogue[i - 1] == '>')
        _isTag = false;
      if (_dialogue.dialogue[i] == '<')
        _isTag = true;
      if (!_isTag)
      {
        ((TMP_Text) dialogueHandler.text).text = _dialogue.dialogue.Substring(0, i + 1);
        LayoutRebuilder.ForceRebuildLayoutImmediate(((TMP_Text) dialogueHandler.text).transform.parent as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(((TMP_Text) dialogueHandler.text).transform.parent.parent as RectTransform);
      }
      if (!_isTag)
        yield return (object) new WaitForSeconds((float) dialogueHandler.cooldownMS / 1000f);
    }
    if (_dialogue.autoSkip)
    {
      yield return (object) new WaitForSeconds(_dialogue.autoSkipMS / 1000f);
      if (_dialogueID < dialogueHandler.dialogues.Length - 1)
      {
        TutorialHandler.currentStep = _dialogueID + 1;
        dialogueHandler.StartCoroutine(dialogueHandler.ShowText(_dialogueID + 1));
      }
      else
        UIController.instance.SetActive(((Component) dialogueHandler).gameObject, false);
    }
  }

  public void ShowTextFunction(int _dialogueID) => this.StartCoroutine(this.ShowText(_dialogueID));

  public void ShowTextFunction(int _dialogueID, string _title) => this.StartCoroutine(this.ShowText(_dialogueID, _title));

  private void Start()
  {
  }

  private void Update()
  {
    if (!Input.GetKeyDown((KeyCode) 98))
      return;
    this.ShowTextFunction(0, "Trailer");
  }

  [Serializable]
  public class Dialogue
  {
    public int expressionID;
    public string dialogue;
    public bool autoSkip;
    public float autoSkipMS;
  }
}
