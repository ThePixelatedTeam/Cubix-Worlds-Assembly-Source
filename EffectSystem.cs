// Decompiled with JetBrains decompiler
// Type: EffectSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Threading;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  public int currentID;
  [SerializeField]
  public Animation[] animations;
  private int currentFrame;
  private Sprite currentSprite;
  private bool isMoving;
  private bool isTransparent;

  private void Awake()
  {
  }

  private void Start()
  {
  }

  private void Update()
  {
    if (!this.isMoving)
      return;
    ((Component) this).transform.position = Vector2.op_Implicit(new Vector2(((Component) this).transform.position.x, ((Component) this).transform.position.y + 0.018f));
    this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.spriteRenderer.color.a - 0.0156862754f);
    if ((double) this.spriteRenderer.color.a >= 0.039215687662363052)
      return;
    this.isMoving = false;
  }

  public void PlayEffect(int _effectID)
  {
    this.currentFrame = 0;
    this.currentID = _effectID;
    new Thread((ThreadStart) (() => this.PassFrame(true))).Start();
  }

  public void PlayEffect(int _effectID, int _secondsToDestroy, bool _isMove)
  {
    this.currentFrame = 0;
    this.currentID = _effectID;
    Object.Destroy((Object) ((Component) this).gameObject, (float) _secondsToDestroy);
    new Thread((ThreadStart) (() => this.PassFrame(false))).Start();
    if (!_isMove)
      return;
    this.isMoving = true;
  }

  private void PassFrame(bool _destroy)
  {
    this.currentSprite = this.animations[this.currentID].sprites[this.currentFrame];
    ThreadManager.ExecuteOnMainThread((Action) (() =>
    {
      this.spriteRenderer.sprite = this.currentSprite;
      ((Renderer) this.spriteRenderer).sortingOrder = 10000;
    }));
    Thread.Sleep(this.animations[this.currentID].updateFrameMS);
    ++this.currentFrame;
    if (this.currentFrame >= this.animations[this.currentID].sprites.Count)
    {
      if (!_destroy)
        return;
      ThreadManager.ExecuteOnMainThread((Action) (() => Object.Destroy((Object) ((Component) this).gameObject)));
    }
    else
      this.PassFrame(_destroy);
  }
}
