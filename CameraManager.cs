// Decompiled with JetBrains decompiler
// Type: CameraManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public static CameraManager instance;
  public CinemachineVirtualCamera camera;
  public float cameraDistanceMax = 7f;
  public float cameraDistanceMin = 2f;
  private float cameraDistance = 3f;
  private float scrollSpeed = -3f;
  public float lerpSpeed = 5f;
  public float aimZoom;

  private void Awake()
  {
    if (Object.op_Equality((Object) CameraManager.instance, (Object) null))
      CameraManager.instance = this;
    else if (Object.op_Inequality((Object) CameraManager.instance, (Object) this))
    {
      Debug.Log((object) "Instance already exists, destroying object!");
      Object.Destroy((Object) this);
    }
    this.aimZoom = this.cameraDistance;
  }

  private void Start()
  {
  }

  private void Update() => this.CameraZoom();

  public void ZoomIn() => this.aimZoom += 0.5f * this.scrollSpeed;

  public void ZoomOut() => this.aimZoom -= 0.5f * this.scrollSpeed;

  public void CameraZoom()
  {
    this.aimZoom += Input.GetAxis("Mouse ScrollWheel") * this.scrollSpeed;
    this.cameraDistance = Mathf.Lerp(this.cameraDistance, Mathf.Clamp(this.aimZoom, this.cameraDistanceMin, this.cameraDistanceMax), this.lerpSpeed * Time.deltaTime);
    if ((double) this.cameraDistance == 0.0)
      return;
    if (GameManager.players.ContainsKey(Client.instance.myId))
      this.camera.m_Lens.OrthographicSize = this.cameraDistance * Mathf.Clamp(((Component) GameManager.players[Client.instance.myId]).transform.localScale.y + 0.01f, 0.5f, 10f);
    else
      this.camera.m_Lens.OrthographicSize = this.cameraDistance;
  }

  public void SetFollowObject(GameObject obj) => ((CinemachineVirtualCameraBase) this.camera).Follow = obj.transform;
}
