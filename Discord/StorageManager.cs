// Decompiled with JetBrains decompiler
// Type: Discord.StorageManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
  public class StorageManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private StorageManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (StorageManager.FFIMethods));
        return (StorageManager.FFIMethods) this.MethodsStructure;
      }
    }

    internal StorageManager(IntPtr ptr, IntPtr eventsPtr, ref StorageManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref StorageManager.FFIEvents events) => Marshal.StructureToPtr<StorageManager.FFIEvents>(events, eventsPtr, false);

    public uint Read(string name, byte[] data)
    {
      uint read = 0;
      Result result = this.Methods.Read(this.MethodsPtr, name, data, data.Length, ref read);
      if (result != Result.Ok)
        throw new ResultException(result);
      return read;
    }

    [MonoPInvokeCallback]
    private static void ReadAsyncCallbackImpl(
      IntPtr ptr,
      Result result,
      IntPtr dataPtr,
      int dataLen)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      StorageManager.ReadAsyncHandler target = (StorageManager.ReadAsyncHandler) gcHandle.Target;
      gcHandle.Free();
      byte[] destination = new byte[dataLen];
      Marshal.Copy(dataPtr, destination, 0, dataLen);
      int num = (int) result;
      byte[] data = destination;
      target((Result) num, data);
    }

    public void ReadAsync(string name, StorageManager.ReadAsyncHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.ReadAsync(this.MethodsPtr, name, GCHandle.ToIntPtr(gcHandle), new StorageManager.FFIMethods.ReadAsyncCallback(StorageManager.ReadAsyncCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void ReadAsyncPartialCallbackImpl(
      IntPtr ptr,
      Result result,
      IntPtr dataPtr,
      int dataLen)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      StorageManager.ReadAsyncPartialHandler target = (StorageManager.ReadAsyncPartialHandler) gcHandle.Target;
      gcHandle.Free();
      byte[] destination = new byte[dataLen];
      Marshal.Copy(dataPtr, destination, 0, dataLen);
      int num = (int) result;
      byte[] data = destination;
      target((Result) num, data);
    }

    public void ReadAsyncPartial(
      string name,
      ulong offset,
      ulong length,
      StorageManager.ReadAsyncPartialHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.ReadAsyncPartial(this.MethodsPtr, name, offset, length, GCHandle.ToIntPtr(gcHandle), new StorageManager.FFIMethods.ReadAsyncPartialCallback(StorageManager.ReadAsyncPartialCallbackImpl));
    }

    public void Write(string name, byte[] data)
    {
      Result result = this.Methods.Write(this.MethodsPtr, name, data, data.Length);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    [MonoPInvokeCallback]
    private static void WriteAsyncCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      StorageManager.WriteAsyncHandler target = (StorageManager.WriteAsyncHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void WriteAsync(string name, byte[] data, StorageManager.WriteAsyncHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.WriteAsync(this.MethodsPtr, name, data, data.Length, GCHandle.ToIntPtr(gcHandle), new StorageManager.FFIMethods.WriteAsyncCallback(StorageManager.WriteAsyncCallbackImpl));
    }

    public void Delete(string name)
    {
      Result result = this.Methods.Delete(this.MethodsPtr, name);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public bool Exists(string name)
    {
      bool exists = false;
      Result result = this.Methods.Exists(this.MethodsPtr, name, ref exists);
      if (result != Result.Ok)
        throw new ResultException(result);
      return exists;
    }

    public int Count()
    {
      int count = 0;
      this.Methods.Count(this.MethodsPtr, ref count);
      return count;
    }

    public FileStat Stat(string name)
    {
      FileStat stat = new FileStat();
      Result result = this.Methods.Stat(this.MethodsPtr, name, ref stat);
      if (result != Result.Ok)
        throw new ResultException(result);
      return stat;
    }

    public FileStat StatAt(int index)
    {
      FileStat stat = new FileStat();
      Result result = this.Methods.StatAt(this.MethodsPtr, index, ref stat);
      if (result != Result.Ok)
        throw new ResultException(result);
      return stat;
    }

    public string GetPath()
    {
      StringBuilder path = new StringBuilder(4096);
      Result result = this.Methods.GetPath(this.MethodsPtr, path);
      if (result != Result.Ok)
        throw new ResultException(result);
      return path.ToString();
    }

    public IEnumerable<FileStat> Files()
    {
      int num = this.Count();
      List<FileStat> fileStatList = new List<FileStat>();
      for (int index = 0; index < num; ++index)
        fileStatList.Add(this.StatAt(index));
      return (IEnumerable<FileStat>) fileStatList;
    }

    internal struct FFIEvents
    {
    }

    internal struct FFIMethods
    {
      internal StorageManager.FFIMethods.ReadMethod Read;
      internal StorageManager.FFIMethods.ReadAsyncMethod ReadAsync;
      internal StorageManager.FFIMethods.ReadAsyncPartialMethod ReadAsyncPartial;
      internal StorageManager.FFIMethods.WriteMethod Write;
      internal StorageManager.FFIMethods.WriteAsyncMethod WriteAsync;
      internal StorageManager.FFIMethods.DeleteMethod Delete;
      internal StorageManager.FFIMethods.ExistsMethod Exists;
      internal StorageManager.FFIMethods.CountMethod Count;
      internal StorageManager.FFIMethods.StatMethod Stat;
      internal StorageManager.FFIMethods.StatAtMethod StatAt;
      internal StorageManager.FFIMethods.GetPathMethod GetPath;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result ReadMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string name,
        byte[] data,
        int dataLen,
        ref uint read);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ReadAsyncCallback(
        IntPtr ptr,
        Result result,
        IntPtr dataPtr,
        int dataLen);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ReadAsyncMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string name,
        IntPtr callbackData,
        StorageManager.FFIMethods.ReadAsyncCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ReadAsyncPartialCallback(
        IntPtr ptr,
        Result result,
        IntPtr dataPtr,
        int dataLen);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ReadAsyncPartialMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string name,
        ulong offset,
        ulong length,
        IntPtr callbackData,
        StorageManager.FFIMethods.ReadAsyncPartialCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result WriteMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string name,
        byte[] data,
        int dataLen);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void WriteAsyncCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void WriteAsyncMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string name,
        byte[] data,
        int dataLen,
        IntPtr callbackData,
        StorageManager.FFIMethods.WriteAsyncCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result DeleteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result ExistsMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref bool exists);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void CountMethod(IntPtr methodsPtr, ref int count);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result StatMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref FileStat stat);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result StatAtMethod(IntPtr methodsPtr, int index, ref FileStat stat);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetPathMethod(IntPtr methodsPtr, StringBuilder path);
    }

    public delegate void ReadAsyncHandler(Result result, byte[] data);

    public delegate void ReadAsyncPartialHandler(Result result, byte[] data);

    public delegate void WriteAsyncHandler(Result result);
  }
}
