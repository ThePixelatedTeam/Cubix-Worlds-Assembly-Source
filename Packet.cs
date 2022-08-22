// Decompiled with JetBrains decompiler
// Type: Packet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Packet : IDisposable
{
  private List<byte> buffer;
  private byte[] readableBuffer;
  private int readPos;
  private bool disposed;

  public Packet()
  {
    this.buffer = new List<byte>();
    this.readPos = 0;
  }

  public Packet(int _id)
  {
    this.buffer = new List<byte>();
    this.readPos = 0;
    this.Write(_id);
  }

  public Packet(byte[] _data)
  {
    this.buffer = new List<byte>();
    this.readPos = 0;
    this.SetBytes(_data);
  }

  public void SetBytes(byte[] _data)
  {
    this.Write(_data);
    this.readableBuffer = this.buffer.ToArray();
  }

  public void WriteLength() => this.buffer.InsertRange(0, (IEnumerable<byte>) BitConverter.GetBytes(this.buffer.Count));

  public void InsertInt(int _value) => this.buffer.InsertRange(0, (IEnumerable<byte>) BitConverter.GetBytes(_value));

  public byte[] ToArray()
  {
    this.readableBuffer = this.buffer.ToArray();
    return this.readableBuffer;
  }

  public int Length() => this.buffer.Count;

  public int UnreadLength() => this.Length() - this.readPos;

  public void Reset(bool _shouldReset = true)
  {
    if (_shouldReset)
    {
      this.buffer.Clear();
      this.readableBuffer = (byte[]) null;
      this.readPos = 0;
    }
    else
      this.readPos -= 4;
  }

  public void Write(byte _value) => this.buffer.Add(_value);

  public void Write(byte[] _value) => this.buffer.AddRange((IEnumerable<byte>) _value);

  public void Write(short _value) => this.buffer.AddRange((IEnumerable<byte>) BitConverter.GetBytes(_value));

  public void Write(int _value) => this.buffer.AddRange((IEnumerable<byte>) BitConverter.GetBytes(_value));

  public void Write(long _value) => this.buffer.AddRange((IEnumerable<byte>) BitConverter.GetBytes(_value));

  public void Write(float _value) => this.buffer.AddRange((IEnumerable<byte>) BitConverter.GetBytes(_value));

  public void Write(bool _value) => this.buffer.AddRange((IEnumerable<byte>) BitConverter.GetBytes(_value));

  public void Write(string _value)
  {
    this.Write(Encoding.UTF8.GetByteCount(_value));
    this.buffer.AddRange((IEnumerable<byte>) Encoding.UTF8.GetBytes(_value));
  }

  public void Write(Vector3 _value)
  {
    this.Write(_value.x);
    this.Write(_value.y);
    this.Write(_value.z);
  }

  public void Write(Quaternion _value)
  {
    this.Write(_value.x);
    this.Write(_value.y);
    this.Write(_value.z);
    this.Write(_value.w);
  }

  public void Write(Vector2 _value)
  {
    this.Write(_value.x);
    this.Write(_value.y);
  }

  public byte ReadByte(bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'byte'!");
    int num = (int) this.readableBuffer[this.readPos];
    if (!_moveReadPos)
      return (byte) num;
    ++this.readPos;
    return (byte) num;
  }

  public byte[] ReadBytes(int _length, bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'byte[]'!");
    byte[] array = this.buffer.GetRange(this.readPos, _length).ToArray();
    if (!_moveReadPos)
      return array;
    this.readPos += _length;
    return array;
  }

  public short ReadShort(bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'short'!");
    int int16 = (int) BitConverter.ToInt16(this.readableBuffer, this.readPos);
    if (!_moveReadPos)
      return (short) int16;
    this.readPos += 2;
    return (short) int16;
  }

  public int ReadInt(bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'int'!");
    int int32 = BitConverter.ToInt32(this.readableBuffer, this.readPos);
    if (!_moveReadPos)
      return int32;
    this.readPos += 4;
    return int32;
  }

  public long ReadLong(bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'long'!");
    long int64 = BitConverter.ToInt64(this.readableBuffer, this.readPos);
    if (!_moveReadPos)
      return int64;
    this.readPos += 8;
    return int64;
  }

  public float ReadFloat(bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'float'!");
    double single = (double) BitConverter.ToSingle(this.readableBuffer, this.readPos);
    if (!_moveReadPos)
      return (float) single;
    this.readPos += 4;
    return (float) single;
  }

  public bool ReadBool(bool _moveReadPos = true)
  {
    if (this.buffer.Count <= this.readPos)
      throw new Exception("Could not read value of type 'bool'!");
    int num = BitConverter.ToBoolean(this.readableBuffer, this.readPos) ? 1 : 0;
    if (!_moveReadPos)
      return num != 0;
    ++this.readPos;
    return num != 0;
  }

  public string ReadString(bool _moveReadPos = true)
  {
    try
    {
      int count = this.ReadInt();
      string str = Encoding.UTF8.GetString(this.readableBuffer, this.readPos, count);
      if (_moveReadPos && str.Length > 0)
        this.readPos += count;
      return str;
    }
    catch (Exception ex)
    {
      throw new Exception("Could not read value of type 'string'!" + ex.ToString());
    }
  }

  public Vector3 ReadVector3(bool _moveReadPos = true) => new Vector3(this.ReadFloat(_moveReadPos), this.ReadFloat(_moveReadPos), this.ReadFloat(_moveReadPos));

  public Quaternion ReadQuaternion(bool _moveReadPos = true) => new Quaternion(this.ReadFloat(_moveReadPos), this.ReadFloat(_moveReadPos), this.ReadFloat(_moveReadPos), this.ReadFloat(_moveReadPos));

  public Vector2 ReadVector2(bool _moveReadPos = true) => new Vector2(this.ReadFloat(_moveReadPos), this.ReadFloat(_moveReadPos));

  protected virtual void Dispose(bool _disposing)
  {
    if (this.disposed)
      return;
    if (_disposing)
    {
      this.buffer = (List<byte>) null;
      this.readableBuffer = (byte[]) null;
      this.readPos = 0;
    }
    this.disposed = true;
  }

  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
