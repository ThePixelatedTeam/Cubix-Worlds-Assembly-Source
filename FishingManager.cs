// Decompiled with JetBrains decompiler
// Type: FishingManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
  public static FishingManager instance;
  public GameObject fish;
  public Vector2 fishPosition;
  public float movementSpeed = 5f;
  public GameObject container;
  public GameObject bar;
  public float barGravity;
  public float minBarVelocity;
  public float maxBarVelocity;
  public float barForce;
  public float barVelocity;
  public float barAnimationSpeed = 5f;
  public Slider slider;
  public float sliderSpeed = 0.01f;
  public float sliderValue = 0.5f;
  public float sliderAnimationSpeed = 5f;
  public bool isCatching;
  private bool previousIsCatchingFish;
  public Sprite[] animationSprites;

  private void Awake()
  {
    if (!Object.op_Equality((Object) FishingManager.instance, (Object) null))
      return;
    FishingManager.instance = this;
  }

  private void Start() => this.fishPosition = Vector2.op_Implicit(this.fish.transform.localPosition);

  private void Update()
  {
    if (!this.isCatching)
      return;
    this.fish.transform.localPosition = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(this.fish.transform.localPosition), this.fishPosition, this.movementSpeed * Time.deltaTime));
    this.barVelocity -= this.barGravity;
    if (this.previousIsCatchingFish != this.isCatchingFish())
      this.fish.GetComponent<Image>().sprite = this.isCatchingFish() ? this.animationSprites[1] : this.animationSprites[0];
    this.sliderValue += this.isCatchingFish() ? this.sliderSpeed : -this.sliderSpeed;
    this.sliderValue = Mathf.Clamp(this.sliderValue, -0.1f, 1.1f);
    this.slider.value = Mathf.Lerp(this.slider.value, this.sliderValue, this.sliderAnimationSpeed * Time.deltaTime);
    this.previousIsCatchingFish = this.isCatchingFish();
    if (Input.GetMouseButton(0))
      this.barVelocity += this.barForce;
    this.barVelocity = Mathf.Clamp(this.barVelocity, this.minBarVelocity, this.maxBarVelocity);
    Transform transform = this.bar.transform;
    Vector2 vector2_1 = Vector2.op_Implicit(this.bar.transform.localPosition);
    double num1 = (double) this.bar.transform.localPosition.x + (double) this.barVelocity;
    Rect rect = ((RectTransform) this.container.transform).rect;
    double num2 = -((double) ((Rect) ref rect).width / 2.0);
    rect = ((RectTransform) this.bar.transform).rect;
    double num3 = (double) ((Rect) ref rect).width / 2.0;
    double num4 = num2 + num3;
    rect = ((RectTransform) this.container.transform).rect;
    double num5 = (double) ((Rect) ref rect).width / 2.0;
    rect = ((RectTransform) this.bar.transform).rect;
    double num6 = (double) ((Rect) ref rect).width / 2.0;
    double num7 = num5 - num6;
    Vector2 vector2_2 = new Vector2(Mathf.Clamp((float) num1, (float) num4, (float) num7), this.bar.transform.localPosition.y);
    double num8 = (double) this.barAnimationSpeed * (double) Time.deltaTime;
    Vector3 vector3 = Vector2.op_Implicit(Vector2.Lerp(vector2_1, vector2_2, (float) num8));
    transform.localPosition = vector3;
    if ((double) this.slider.value == 1.0)
    {
      this.isCatching = false;
      ClientSend.FishingAction(3);
    }
    if ((double) this.slider.value != 0.0)
      return;
    this.isCatching = false;
    ClientSend.FishingAction(4);
  }

  private bool isCatchingFish()
  {
    double x1 = (double) this.fish.transform.localPosition.x;
    double x2 = (double) this.bar.transform.localPosition.x;
    Rect rect1 = ((RectTransform) this.bar.transform).rect;
    double num1 = (double) ((Rect) ref rect1).width / 2.0;
    double num2 = x2 - num1;
    if (x1 >= num2)
    {
      double x3 = (double) this.fish.transform.localPosition.x;
      double x4 = (double) this.bar.transform.localPosition.x;
      Rect rect2 = ((RectTransform) this.bar.transform).rect;
      double num3 = (double) ((Rect) ref rect2).width / 2.0;
      double num4 = x4 + num3;
      if (x3 <= num4)
        return true;
    }
    return false;
  }
}
