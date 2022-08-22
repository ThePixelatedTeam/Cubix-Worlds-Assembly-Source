// Decompiled with JetBrains decompiler
// Type: NotificationMovement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;

public class NotificationMovement : MonoBehaviour
{
  public Transform destinationPosition;
  public Transform staticPosition;
  public float speed;
  private bool state;

  private void Update()
  {
    if (this.state)
      ((Component) this).transform.position = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(((Component) this).transform.position), Vector2.op_Implicit(this.destinationPosition.position), this.speed * Time.deltaTime));
    else
      ((Component) this).transform.position = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(((Component) this).transform.position), Vector2.op_Implicit(this.staticPosition.position), this.speed * Time.deltaTime));
  }

  public void ToggleState() => this.state = !this.state;

  public void TrueState() => this.state = true;

  public void FalseState() => this.state = false;
}
