// Decompiled with JetBrains decompiler
// Type: EffectManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;

public class EffectManager : MonoBehaviour
{
  public static EffectManager instance;
  public GameObject laser;
  public GameObject punch;

  private void Awake()
  {
    if (!Object.op_Equality((Object) EffectManager.instance, (Object) null))
      return;
    EffectManager.instance = this;
  }

  private void Start()
  {
  }

  private void Update()
  {
  }

  public void ShootLaser(Vector2 _point1, Vector2 _point2)
  {
    float num = Vector2.Distance(_point1, _point2);
    GameObject gameObject = Object.Instantiate<GameObject>(this.laser, GameManager.instance.effects.transform);
    gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(num, 0.5f));
    gameObject.transform.position = Vector2.op_Implicit(this.GetMiddlePoint(_point1, _point2));
    Vector2 vector2 = Vector2.op_Subtraction(_point2, _point1);
    gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(vector2.y, vector2.x) * 57.29578f);
    Object.Destroy((Object) gameObject, 0.5f);
  }

  public void Punch(Vector2 _point1, Vector2 _point2)
  {
    float num = Vector2.Distance(_point1, _point2);
    GameObject gameObject = Object.Instantiate<GameObject>(this.punch, GameManager.instance.effects.transform);
    gameObject.transform.localScale = Vector2.op_Implicit(new Vector2(num, 1f));
    gameObject.transform.position = Vector2.op_Implicit(this.GetMiddlePoint(_point1, _point2));
    Vector2 vector2 = Vector2.op_Subtraction(_point2, _point1);
    gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(vector2.y, vector2.x) * 57.29578f);
    Object.Destroy((Object) gameObject, 0.5f);
  }

  private Vector2 GetMiddlePoint(Vector2 _a, Vector2 _b)
  {
    Vector2 middlePoint;
    middlePoint.x = (float) (((double) _a.x + (double) _b.x) / 2.0);
    middlePoint.y = (float) (((double) _a.y + (double) _b.y) / 2.0);
    return middlePoint;
  }
}
