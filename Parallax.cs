// Decompiled with JetBrains decompiler
// Type: Parallax
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;

public class Parallax : MonoBehaviour
{
  private float length;
  public Vector2 startPosition;
  public GameObject cam;
  public float parallaxEffect;

  private void Awake()
  {
    this.startPosition = Vector2.op_Implicit(((Component) this).transform.position);
    Bounds bounds = ((Renderer) ((Component) this).GetComponent<SpriteRenderer>()).bounds;
    this.length = ((Bounds) ref bounds).size.x;
  }

  private void LateUpdate() => ((Component) this).transform.position = new Vector3(this.startPosition.x + this.cam.transform.position.x * this.parallaxEffect, this.cam.transform.position.y + this.startPosition.y, ((Component) this).transform.position.z);
}
