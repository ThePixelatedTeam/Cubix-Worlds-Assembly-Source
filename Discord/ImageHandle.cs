// Decompiled with JetBrains decompiler
// Type: Discord.ImageHandle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

namespace Discord
{
  public struct ImageHandle
  {
    public ImageType Type;
    public long Id;
    public uint Size;

    public static ImageHandle User(long id) => ImageHandle.User(id, 128U);

    public static ImageHandle User(long id, uint size) => new ImageHandle()
    {
      Type = ImageType.User,
      Id = id,
      Size = size
    };
  }
}
