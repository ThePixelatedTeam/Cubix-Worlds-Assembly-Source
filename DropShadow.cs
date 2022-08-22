// Decompiled with JetBrains decompiler
// Type: DropShadow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
  public Vector2 ShadowOffset;
  private SpriteRenderer spriteRenderer;
  private SpriteRenderer shadowSpriteRenderer;
  public GameObject shadowGameobject;

  private void Start()
  {
    GameManager.instance.dropShadows.Add(this);
    this.spriteRenderer = ((Component) this).GetComponent<SpriteRenderer>();
    this.shadowGameobject = new GameObject("Shadow");
    this.shadowGameobject.transform.parent = ((Component) GameManager.instance).gameObject.transform;
    this.shadowSpriteRenderer = this.shadowGameobject.AddComponent<SpriteRenderer>();
    this.shadowSpriteRenderer.sprite = this.spriteRenderer.sprite;
    this.shadowSpriteRenderer.color = Color32.op_Implicit(new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 80));
    ((Renderer) this.shadowSpriteRenderer).sortingLayerName = ((Renderer) this.spriteRenderer).sortingLayerName;
    if (((Component) this).gameObject.tag == "Player" || ((Component) this).gameObject.tag == "Costume")
      ((Renderer) this.shadowSpriteRenderer).sortingOrder = -1;
    else if (((Object) ((Component) this).gameObject).name == "Liquid")
      ((Renderer) this.shadowSpriteRenderer).sortingOrder = -1;
    else
      ((Renderer) this.shadowSpriteRenderer).sortingOrder = ((Renderer) this.spriteRenderer).sortingOrder - 1;
  }

  private void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.shadowSpriteRenderer, (Object) null) || !Object.op_Inequality((Object) this.spriteRenderer, (Object) null))
      return;
    if (Object.op_Inequality((Object) this.shadowSpriteRenderer.sprite, (Object) this.spriteRenderer.sprite))
      this.shadowSpriteRenderer.sprite = this.spriteRenderer.sprite;
    if (((Component) this).gameObject.tag == "Costume")
    {
      this.shadowGameobject.transform.localPosition = Vector3.op_Addition(((Component) this).transform.parent.parent.localPosition, Vector2.op_Implicit(this.ShadowOffset));
      this.shadowGameobject.transform.localRotation = ((Component) this).transform.parent.parent.localRotation;
      this.shadowGameobject.transform.localScale = ((Component) this).transform.parent.parent.localScale;
    }
    else
    {
      this.shadowGameobject.transform.localPosition = Vector3.op_Addition(((Component) this).transform.localPosition, Vector2.op_Implicit(this.ShadowOffset));
      this.shadowGameobject.transform.localRotation = ((Component) this).transform.localRotation;
      this.shadowGameobject.transform.localScale = ((Component) this).transform.localScale;
    }
  }
}
