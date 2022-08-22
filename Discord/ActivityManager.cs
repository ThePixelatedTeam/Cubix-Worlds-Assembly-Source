// Decompiled with JetBrains decompiler
// Type: Discord.ActivityManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace Discord
{
  public class ActivityManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    public void RegisterCommand() => this.RegisterCommand((string) null);

    private ActivityManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (ActivityManager.FFIMethods));
        return (ActivityManager.FFIMethods) this.MethodsStructure;
      }
    }

    public event ActivityManager.ActivityJoinHandler OnActivityJoin;

    public event ActivityManager.ActivitySpectateHandler OnActivitySpectate;

    public event ActivityManager.ActivityJoinRequestHandler OnActivityJoinRequest;

    public event ActivityManager.ActivityInviteHandler OnActivityInvite;

    internal ActivityManager(IntPtr ptr, IntPtr eventsPtr, ref ActivityManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref ActivityManager.FFIEvents events)
    {
      events.OnActivityJoin = new ActivityManager.FFIEvents.ActivityJoinHandler(ActivityManager.OnActivityJoinImpl);
      events.OnActivitySpectate = new ActivityManager.FFIEvents.ActivitySpectateHandler(ActivityManager.OnActivitySpectateImpl);
      events.OnActivityJoinRequest = new ActivityManager.FFIEvents.ActivityJoinRequestHandler(ActivityManager.OnActivityJoinRequestImpl);
      events.OnActivityInvite = new ActivityManager.FFIEvents.ActivityInviteHandler(ActivityManager.OnActivityInviteImpl);
      Marshal.StructureToPtr<ActivityManager.FFIEvents>(events, eventsPtr, false);
    }

    public void RegisterCommand(string command)
    {
      Result result = this.Methods.RegisterCommand(this.MethodsPtr, command);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void RegisterSteam(uint steamId)
    {
      Result result = this.Methods.RegisterSteam(this.MethodsPtr, steamId);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    [MonoPInvokeCallback]
    private static void UpdateActivityCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ActivityManager.UpdateActivityHandler target = (ActivityManager.UpdateActivityHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void UpdateActivity(Activity activity, ActivityManager.UpdateActivityHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.UpdateActivity(this.MethodsPtr, ref activity, GCHandle.ToIntPtr(gcHandle), new ActivityManager.FFIMethods.UpdateActivityCallback(ActivityManager.UpdateActivityCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void ClearActivityCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ActivityManager.ClearActivityHandler target = (ActivityManager.ClearActivityHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void ClearActivity(ActivityManager.ClearActivityHandler callback) => this.Methods.ClearActivity(this.MethodsPtr, GCHandle.ToIntPtr(GCHandle.Alloc((object) callback)), new ActivityManager.FFIMethods.ClearActivityCallback(ActivityManager.ClearActivityCallbackImpl));

    [MonoPInvokeCallback]
    private static void SendRequestReplyCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ActivityManager.SendRequestReplyHandler target = (ActivityManager.SendRequestReplyHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void SendRequestReply(
      long userId,
      ActivityJoinRequestReply reply,
      ActivityManager.SendRequestReplyHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.SendRequestReply(this.MethodsPtr, userId, reply, GCHandle.ToIntPtr(gcHandle), new ActivityManager.FFIMethods.SendRequestReplyCallback(ActivityManager.SendRequestReplyCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void SendInviteCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ActivityManager.SendInviteHandler target = (ActivityManager.SendInviteHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void SendInvite(
      long userId,
      ActivityActionType type,
      string content,
      ActivityManager.SendInviteHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.SendInvite(this.MethodsPtr, userId, type, content, GCHandle.ToIntPtr(gcHandle), new ActivityManager.FFIMethods.SendInviteCallback(ActivityManager.SendInviteCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void AcceptInviteCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ActivityManager.AcceptInviteHandler target = (ActivityManager.AcceptInviteHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void AcceptInvite(long userId, ActivityManager.AcceptInviteHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.AcceptInvite(this.MethodsPtr, userId, GCHandle.ToIntPtr(gcHandle), new ActivityManager.FFIMethods.AcceptInviteCallback(ActivityManager.AcceptInviteCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void OnActivityJoinImpl(IntPtr ptr, string secret)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.ActivityManagerInstance.OnActivityJoin == null)
        return;
      target.ActivityManagerInstance.OnActivityJoin(secret);
    }

    [MonoPInvokeCallback]
    private static void OnActivitySpectateImpl(IntPtr ptr, string secret)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.ActivityManagerInstance.OnActivitySpectate == null)
        return;
      target.ActivityManagerInstance.OnActivitySpectate(secret);
    }

    [MonoPInvokeCallback]
    private static void OnActivityJoinRequestImpl(IntPtr ptr, ref User user)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.ActivityManagerInstance.OnActivityJoinRequest == null)
        return;
      target.ActivityManagerInstance.OnActivityJoinRequest(ref user);
    }

    [MonoPInvokeCallback]
    private static void OnActivityInviteImpl(
      IntPtr ptr,
      ActivityActionType type,
      ref User user,
      ref Activity activity)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.ActivityManagerInstance.OnActivityInvite == null)
        return;
      target.ActivityManagerInstance.OnActivityInvite(type, ref user, ref activity);
    }

    internal struct FFIEvents
    {
      internal ActivityManager.FFIEvents.ActivityJoinHandler OnActivityJoin;
      internal ActivityManager.FFIEvents.ActivitySpectateHandler OnActivitySpectate;
      internal ActivityManager.FFIEvents.ActivityJoinRequestHandler OnActivityJoinRequest;
      internal ActivityManager.FFIEvents.ActivityInviteHandler OnActivityInvite;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ActivityJoinHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string secret);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ActivitySpectateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string secret);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ActivityJoinRequestHandler(IntPtr ptr, ref User user);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ActivityInviteHandler(
        IntPtr ptr,
        ActivityActionType type,
        ref User user,
        ref Activity activity);
    }

    internal struct FFIMethods
    {
      internal ActivityManager.FFIMethods.RegisterCommandMethod RegisterCommand;
      internal ActivityManager.FFIMethods.RegisterSteamMethod RegisterSteam;
      internal ActivityManager.FFIMethods.UpdateActivityMethod UpdateActivity;
      internal ActivityManager.FFIMethods.ClearActivityMethod ClearActivity;
      internal ActivityManager.FFIMethods.SendRequestReplyMethod SendRequestReply;
      internal ActivityManager.FFIMethods.SendInviteMethod SendInvite;
      internal ActivityManager.FFIMethods.AcceptInviteMethod AcceptInvite;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result RegisterCommandMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string command);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result RegisterSteamMethod(IntPtr methodsPtr, uint steamId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void UpdateActivityCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void UpdateActivityMethod(
        IntPtr methodsPtr,
        ref Activity activity,
        IntPtr callbackData,
        ActivityManager.FFIMethods.UpdateActivityCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ClearActivityCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ClearActivityMethod(
        IntPtr methodsPtr,
        IntPtr callbackData,
        ActivityManager.FFIMethods.ClearActivityCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SendRequestReplyCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SendRequestReplyMethod(
        IntPtr methodsPtr,
        long userId,
        ActivityJoinRequestReply reply,
        IntPtr callbackData,
        ActivityManager.FFIMethods.SendRequestReplyCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SendInviteCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SendInviteMethod(
        IntPtr methodsPtr,
        long userId,
        ActivityActionType type,
        [MarshalAs(UnmanagedType.LPStr)] string content,
        IntPtr callbackData,
        ActivityManager.FFIMethods.SendInviteCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void AcceptInviteCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void AcceptInviteMethod(
        IntPtr methodsPtr,
        long userId,
        IntPtr callbackData,
        ActivityManager.FFIMethods.AcceptInviteCallback callback);
    }

    public delegate void UpdateActivityHandler(Result result);

    public delegate void ClearActivityHandler(Result result);

    public delegate void SendRequestReplyHandler(Result result);

    public delegate void SendInviteHandler(Result result);

    public delegate void AcceptInviteHandler(Result result);

    public delegate void ActivityJoinHandler(string secret);

    public delegate void ActivitySpectateHandler(string secret);

    public delegate void ActivityJoinRequestHandler(ref User user);

    public delegate void ActivityInviteHandler(
      ActivityActionType type,
      ref User user,
      ref Activity activity);
  }
}
