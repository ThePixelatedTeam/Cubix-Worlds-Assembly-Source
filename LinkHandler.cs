// Decompiled with JetBrains decompiler
// Type: LinkHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
  [SerializeField]
  private TextMeshProUGUI chatField;

  public void OnPointerClick(PointerEventData eventData)
  {
    int intersectingLink = TMP_TextUtilities.FindIntersectingLink((TMP_Text) this.chatField, Vector2.op_Implicit(eventData.position), eventData.pressEventCamera);
    int intersectingLine = TMP_TextUtilities.FindIntersectingLine((TMP_Text) this.chatField, Vector2.op_Implicit(eventData.position), eventData.pressEventCamera);
    if (intersectingLine == 0)
      return;
    string str1 = ((TMP_Text) this.chatField).GetParsedText().Substring(((TMP_Text) this.chatField).textInfo.lineInfo[intersectingLine - 1].firstCharacterIndex, ((TMP_Text) this.chatField).textInfo.lineInfo[intersectingLine - 1].lastCharacterIndex - ((TMP_Text) this.chatField).textInfo.lineInfo[intersectingLine - 1].firstCharacterIndex);
    if (intersectingLink == -1)
      return;
    string linkText = ((TMP_LinkInfo) ref ((TMP_Text) this.chatField).textInfo.linkInfo[intersectingLink]).GetLinkText();
    if (!(linkText == "[Click To Warp]"))
    {
      if (!(linkText == "[Click To Reply]"))
      {
        if (!(linkText == "[Accept]"))
        {
          if (!(linkText == "[Refuse]"))
            return;
          ClientSend.SendChatMessage("/refuse");
        }
        else
          ClientSend.SendChatMessage("/accept");
      }
      else
      {
        string str2 = str1.Split(' ', StringSplitOptions.None)[5];
        UIController.instance.ToggleChat();
        ((Component) UIController.instance.input.transform.GetChild(0)).GetComponent<TMP_InputField>().text = "/private " + str2 + " ";
      }
    }
    else
      ClientSend.SendChatMessage("/warp " + str1.Split(' ', StringSplitOptions.None)[7].Replace(":", ""));
  }
}
