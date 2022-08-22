// Decompiled with JetBrains decompiler
// Type: AudioManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;
  public Audio[] audios;

  private void Awake()
  {
    if (!Object.op_Equality((Object) AudioManager.instance, (Object) null))
      return;
    AudioManager.instance = this;
  }

  public void PlayAudio(string _name)
  {
    foreach (Audio audio in this.audios)
    {
      if (audio.name == _name)
      {
        AudioSource audioSource = ((Component) this).gameObject.AddComponent<AudioSource>();
        audioSource.clip = audio.audio;
        audioSource.Play();
        Object.Destroy((Object) audioSource, audio.audio.length);
        break;
      }
    }
  }
}
