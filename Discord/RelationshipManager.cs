// Decompiled with JetBrains decompiler
// Type: Discord.RelationshipManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace Discord
{
  public class RelationshipManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private RelationshipManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (RelationshipManager.FFIMethods));
        return (RelationshipManager.FFIMethods) this.MethodsStructure;
      }
    }

    public event RelationshipManager.RefreshHandler OnRefresh;

    public event RelationshipManager.RelationshipUpdateHandler OnRelationshipUpdate;

    internal RelationshipManager(
      IntPtr ptr,
      IntPtr eventsPtr,
      ref RelationshipManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref RelationshipManager.FFIEvents events)
    {
      events.OnRefresh = new RelationshipManager.FFIEvents.RefreshHandler(RelationshipManager.OnRefreshImpl);
      events.OnRelationshipUpdate = new RelationshipManager.FFIEvents.RelationshipUpdateHandler(RelationshipManager.OnRelationshipUpdateImpl);
      Marshal.StructureToPtr<RelationshipManager.FFIEvents>(events, eventsPtr, false);
    }

    [MonoPInvokeCallback]
    private static bool FilterCallbackImpl(IntPtr ptr, ref Relationship relationship) => ((RelationshipManager.FilterHandler) GCHandle.FromIntPtr(ptr).Target)(ref relationship);

    public void Filter(RelationshipManager.FilterHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.Filter(this.MethodsPtr, GCHandle.ToIntPtr(gcHandle), new RelationshipManager.FFIMethods.FilterCallback(RelationshipManager.FilterCallbackImpl));
      gcHandle.Free();
    }

    public int Count()
    {
      int count = 0;
      Result result = this.Methods.Count(this.MethodsPtr, ref count);
      if (result != Result.Ok)
        throw new ResultException(result);
      return count;
    }

    public Relationship Get(long userId)
    {
      Relationship relationship = new Relationship();
      Result result = this.Methods.Get(this.MethodsPtr, userId, ref relationship);
      if (result != Result.Ok)
        throw new ResultException(result);
      return relationship;
    }

    public Relationship GetAt(uint index)
    {
      Relationship relationship = new Relationship();
      Result result = this.Methods.GetAt(this.MethodsPtr, index, ref relationship);
      if (result != Result.Ok)
        throw new ResultException(result);
      return relationship;
    }

    [MonoPInvokeCallback]
    private static void OnRefreshImpl(IntPtr ptr)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.RelationshipManagerInstance.OnRefresh == null)
        return;
      target.RelationshipManagerInstance.OnRefresh();
    }

    [MonoPInvokeCallback]
    private static void OnRelationshipUpdateImpl(IntPtr ptr, ref Relationship relationship)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.RelationshipManagerInstance.OnRelationshipUpdate == null)
        return;
      target.RelationshipManagerInstance.OnRelationshipUpdate(ref relationship);
    }

    internal struct FFIEvents
    {
      internal RelationshipManager.FFIEvents.RefreshHandler OnRefresh;
      internal RelationshipManager.FFIEvents.RelationshipUpdateHandler OnRelationshipUpdate;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void RefreshHandler(IntPtr ptr);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void RelationshipUpdateHandler(IntPtr ptr, ref Relationship relationship);
    }

    internal struct FFIMethods
    {
      internal RelationshipManager.FFIMethods.FilterMethod Filter;
      internal RelationshipManager.FFIMethods.CountMethod Count;
      internal RelationshipManager.FFIMethods.GetMethod Get;
      internal RelationshipManager.FFIMethods.GetAtMethod GetAt;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate bool FilterCallback(IntPtr ptr, ref Relationship relationship);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void FilterMethod(
        IntPtr methodsPtr,
        IntPtr callbackData,
        RelationshipManager.FFIMethods.FilterCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result CountMethod(IntPtr methodsPtr, ref int count);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetMethod(
        IntPtr methodsPtr,
        long userId,
        ref Relationship relationship);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetAtMethod(
        IntPtr methodsPtr,
        uint index,
        ref Relationship relationship);
    }

    public delegate bool FilterHandler(ref Relationship relationship);

    public delegate void RefreshHandler();

    public delegate void RelationshipUpdateHandler(ref Relationship relationship);
  }
}
