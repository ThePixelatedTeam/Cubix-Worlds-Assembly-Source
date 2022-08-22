﻿// Decompiled with JetBrains decompiler
// Type: Discord.VoiceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace Discord
{
  public class VoiceManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private VoiceManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (VoiceManager.FFIMethods));
        return (VoiceManager.FFIMethods) this.MethodsStructure;
      }
    }

    public event VoiceManager.SettingsUpdateHandler OnSettingsUpdate;

    internal VoiceManager(IntPtr ptr, IntPtr eventsPtr, ref VoiceManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref VoiceManager.FFIEvents events)
    {
      events.OnSettingsUpdate = new VoiceManager.FFIEvents.SettingsUpdateHandler(VoiceManager.OnSettingsUpdateImpl);
      Marshal.StructureToPtr<VoiceManager.FFIEvents>(events, eventsPtr, false);
    }

    public InputMode GetInputMode()
    {
      InputMode inputMode = new InputMode();
      Result result = this.Methods.GetInputMode(this.MethodsPtr, ref inputMode);
      if (result != Result.Ok)
        throw new ResultException(result);
      return inputMode;
    }

    [MonoPInvokeCallback]
    private static void SetInputModeCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      VoiceManager.SetInputModeHandler target = (VoiceManager.SetInputModeHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void SetInputMode(InputMode inputMode, VoiceManager.SetInputModeHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.SetInputMode(this.MethodsPtr, inputMode, GCHandle.ToIntPtr(gcHandle), new VoiceManager.FFIMethods.SetInputModeCallback(VoiceManager.SetInputModeCallbackImpl));
    }

    public bool IsSelfMute()
    {
      bool mute = false;
      Result result = this.Methods.IsSelfMute(this.MethodsPtr, ref mute);
      if (result != Result.Ok)
        throw new ResultException(result);
      return mute;
    }

    public void SetSelfMute(bool mute)
    {
      Result result = this.Methods.SetSelfMute(this.MethodsPtr, mute);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public bool IsSelfDeaf()
    {
      bool deaf = false;
      Result result = this.Methods.IsSelfDeaf(this.MethodsPtr, ref deaf);
      if (result != Result.Ok)
        throw new ResultException(result);
      return deaf;
    }

    public void SetSelfDeaf(bool deaf)
    {
      Result result = this.Methods.SetSelfDeaf(this.MethodsPtr, deaf);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public bool IsLocalMute(long userId)
    {
      bool mute = false;
      Result result = this.Methods.IsLocalMute(this.MethodsPtr, userId, ref mute);
      if (result != Result.Ok)
        throw new ResultException(result);
      return mute;
    }

    public void SetLocalMute(long userId, bool mute)
    {
      Result result = this.Methods.SetLocalMute(this.MethodsPtr, userId, mute);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public byte GetLocalVolume(long userId)
    {
      byte volume = 0;
      Result result = this.Methods.GetLocalVolume(this.MethodsPtr, userId, ref volume);
      if (result != Result.Ok)
        throw new ResultException(result);
      return volume;
    }

    public void SetLocalVolume(long userId, byte volume)
    {
      Result result = this.Methods.SetLocalVolume(this.MethodsPtr, userId, volume);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    [MonoPInvokeCallback]
    private static void OnSettingsUpdateImpl(IntPtr ptr)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.VoiceManagerInstance.OnSettingsUpdate == null)
        return;
      target.VoiceManagerInstance.OnSettingsUpdate();
    }

    internal struct FFIEvents
    {
      internal VoiceManager.FFIEvents.SettingsUpdateHandler OnSettingsUpdate;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SettingsUpdateHandler(IntPtr ptr);
    }

    internal struct FFIMethods
    {
      internal VoiceManager.FFIMethods.GetInputModeMethod GetInputMode;
      internal VoiceManager.FFIMethods.SetInputModeMethod SetInputMode;
      internal VoiceManager.FFIMethods.IsSelfMuteMethod IsSelfMute;
      internal VoiceManager.FFIMethods.SetSelfMuteMethod SetSelfMute;
      internal VoiceManager.FFIMethods.IsSelfDeafMethod IsSelfDeaf;
      internal VoiceManager.FFIMethods.SetSelfDeafMethod SetSelfDeaf;
      internal VoiceManager.FFIMethods.IsLocalMuteMethod IsLocalMute;
      internal VoiceManager.FFIMethods.SetLocalMuteMethod SetLocalMute;
      internal VoiceManager.FFIMethods.GetLocalVolumeMethod GetLocalVolume;
      internal VoiceManager.FFIMethods.SetLocalVolumeMethod SetLocalVolume;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetInputModeMethod(IntPtr methodsPtr, ref InputMode inputMode);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SetInputModeCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SetInputModeMethod(
        IntPtr methodsPtr,
        InputMode inputMode,
        IntPtr callbackData,
        VoiceManager.FFIMethods.SetInputModeCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result IsSelfMuteMethod(IntPtr methodsPtr, ref bool mute);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result SetSelfMuteMethod(IntPtr methodsPtr, bool mute);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result IsSelfDeafMethod(IntPtr methodsPtr, ref bool deaf);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result SetSelfDeafMethod(IntPtr methodsPtr, bool deaf);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result IsLocalMuteMethod(
        IntPtr methodsPtr,
        long userId,
        ref bool mute);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result SetLocalMuteMethod(IntPtr methodsPtr, long userId, bool mute);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLocalVolumeMethod(
        IntPtr methodsPtr,
        long userId,
        ref byte volume);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result SetLocalVolumeMethod(
        IntPtr methodsPtr,
        long userId,
        byte volume);
    }

    public delegate void SetInputModeHandler(Result result);

    public delegate void SettingsUpdateHandler();
  }
}
