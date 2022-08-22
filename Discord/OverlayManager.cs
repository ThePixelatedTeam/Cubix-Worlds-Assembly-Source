// Decompiled with JetBrains decompiler
// Type: Discord.OverlayManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace Discord
{
  public class OverlayManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private OverlayManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (OverlayManager.FFIMethods));
        return (OverlayManager.FFIMethods) this.MethodsStructure;
      }
    }

    public event OverlayManager.ToggleHandler OnToggle;

    internal OverlayManager(IntPtr ptr, IntPtr eventsPtr, ref OverlayManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref OverlayManager.FFIEvents events)
    {
      events.OnToggle = new OverlayManager.FFIEvents.ToggleHandler(OverlayManager.OnToggleImpl);
      Marshal.StructureToPtr<OverlayManager.FFIEvents>(events, eventsPtr, false);
    }

    public bool IsEnabled()
    {
      bool enabled = false;
      this.Methods.IsEnabled(this.MethodsPtr, ref enabled);
      return enabled;
    }

    public bool IsLocked()
    {
      bool locked = false;
      this.Methods.IsLocked(this.MethodsPtr, ref locked);
      return locked;
    }

    [MonoPInvokeCallback]
    private static void SetLockedCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      OverlayManager.SetLockedHandler target = (OverlayManager.SetLockedHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void SetLocked(bool locked, OverlayManager.SetLockedHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.SetLocked(this.MethodsPtr, locked, GCHandle.ToIntPtr(gcHandle), new OverlayManager.FFIMethods.SetLockedCallback(OverlayManager.SetLockedCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void OpenActivityInviteCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      OverlayManager.OpenActivityInviteHandler target = (OverlayManager.OpenActivityInviteHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void OpenActivityInvite(
      ActivityActionType type,
      OverlayManager.OpenActivityInviteHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.OpenActivityInvite(this.MethodsPtr, type, GCHandle.ToIntPtr(gcHandle), new OverlayManager.FFIMethods.OpenActivityInviteCallback(OverlayManager.OpenActivityInviteCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void OpenGuildInviteCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      OverlayManager.OpenGuildInviteHandler target = (OverlayManager.OpenGuildInviteHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void OpenGuildInvite(string code, OverlayManager.OpenGuildInviteHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.OpenGuildInvite(this.MethodsPtr, code, GCHandle.ToIntPtr(gcHandle), new OverlayManager.FFIMethods.OpenGuildInviteCallback(OverlayManager.OpenGuildInviteCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void OpenVoiceSettingsCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      OverlayManager.OpenVoiceSettingsHandler target = (OverlayManager.OpenVoiceSettingsHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void OpenVoiceSettings(OverlayManager.OpenVoiceSettingsHandler callback) => this.Methods.OpenVoiceSettings(this.MethodsPtr, GCHandle.ToIntPtr(GCHandle.Alloc((object) callback)), new OverlayManager.FFIMethods.OpenVoiceSettingsCallback(OverlayManager.OpenVoiceSettingsCallbackImpl));

    [MonoPInvokeCallback]
    private static void OnToggleImpl(IntPtr ptr, bool locked)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.OverlayManagerInstance.OnToggle == null)
        return;
      target.OverlayManagerInstance.OnToggle(locked);
    }

    internal struct FFIEvents
    {
      internal OverlayManager.FFIEvents.ToggleHandler OnToggle;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ToggleHandler(IntPtr ptr, bool locked);
    }

    internal struct FFIMethods
    {
      internal OverlayManager.FFIMethods.IsEnabledMethod IsEnabled;
      internal OverlayManager.FFIMethods.IsLockedMethod IsLocked;
      internal OverlayManager.FFIMethods.SetLockedMethod SetLocked;
      internal OverlayManager.FFIMethods.OpenActivityInviteMethod OpenActivityInvite;
      internal OverlayManager.FFIMethods.OpenGuildInviteMethod OpenGuildInvite;
      internal OverlayManager.FFIMethods.OpenVoiceSettingsMethod OpenVoiceSettings;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void IsEnabledMethod(IntPtr methodsPtr, ref bool enabled);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void IsLockedMethod(IntPtr methodsPtr, ref bool locked);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SetLockedCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SetLockedMethod(
        IntPtr methodsPtr,
        bool locked,
        IntPtr callbackData,
        OverlayManager.FFIMethods.SetLockedCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void OpenActivityInviteCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void OpenActivityInviteMethod(
        IntPtr methodsPtr,
        ActivityActionType type,
        IntPtr callbackData,
        OverlayManager.FFIMethods.OpenActivityInviteCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void OpenGuildInviteCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void OpenGuildInviteMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string code,
        IntPtr callbackData,
        OverlayManager.FFIMethods.OpenGuildInviteCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void OpenVoiceSettingsCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void OpenVoiceSettingsMethod(
        IntPtr methodsPtr,
        IntPtr callbackData,
        OverlayManager.FFIMethods.OpenVoiceSettingsCallback callback);
    }

    public delegate void SetLockedHandler(Result result);

    public delegate void OpenActivityInviteHandler(Result result);

    public delegate void OpenGuildInviteHandler(Result result);

    public delegate void OpenVoiceSettingsHandler(Result result);

    public delegate void ToggleHandler(bool locked);
  }
}
