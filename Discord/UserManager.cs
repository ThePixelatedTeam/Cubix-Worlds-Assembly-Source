// Decompiled with JetBrains decompiler
// Type: Discord.UserManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace Discord
{
  public class UserManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private UserManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (UserManager.FFIMethods));
        return (UserManager.FFIMethods) this.MethodsStructure;
      }
    }

    public event UserManager.CurrentUserUpdateHandler OnCurrentUserUpdate;

    internal UserManager(IntPtr ptr, IntPtr eventsPtr, ref UserManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref UserManager.FFIEvents events)
    {
      events.OnCurrentUserUpdate = new UserManager.FFIEvents.CurrentUserUpdateHandler(UserManager.OnCurrentUserUpdateImpl);
      Marshal.StructureToPtr<UserManager.FFIEvents>(events, eventsPtr, false);
    }

    public User GetCurrentUser()
    {
      User currentUser = new User();
      Result result = this.Methods.GetCurrentUser(this.MethodsPtr, ref currentUser);
      if (result != Result.Ok)
        throw new ResultException(result);
      return currentUser;
    }

    [MonoPInvokeCallback]
    private static void GetUserCallbackImpl(IntPtr ptr, Result result, ref User user)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      UserManager.GetUserHandler target = (UserManager.GetUserHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      ref User local = ref user;
      target((Result) num, ref local);
    }

    public void GetUser(long userId, UserManager.GetUserHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.GetUser(this.MethodsPtr, userId, GCHandle.ToIntPtr(gcHandle), new UserManager.FFIMethods.GetUserCallback(UserManager.GetUserCallbackImpl));
    }

    public PremiumType GetCurrentUserPremiumType()
    {
      PremiumType premiumType = PremiumType.None;
      Result result = this.Methods.GetCurrentUserPremiumType(this.MethodsPtr, ref premiumType);
      if (result != Result.Ok)
        throw new ResultException(result);
      return premiumType;
    }

    public bool CurrentUserHasFlag(UserFlag flag)
    {
      bool hasFlag = false;
      Result result = this.Methods.CurrentUserHasFlag(this.MethodsPtr, flag, ref hasFlag);
      if (result != Result.Ok)
        throw new ResultException(result);
      return hasFlag;
    }

    [MonoPInvokeCallback]
    private static void OnCurrentUserUpdateImpl(IntPtr ptr)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.UserManagerInstance.OnCurrentUserUpdate == null)
        return;
      target.UserManagerInstance.OnCurrentUserUpdate();
    }

    internal struct FFIEvents
    {
      internal UserManager.FFIEvents.CurrentUserUpdateHandler OnCurrentUserUpdate;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void CurrentUserUpdateHandler(IntPtr ptr);
    }

    internal struct FFIMethods
    {
      internal UserManager.FFIMethods.GetCurrentUserMethod GetCurrentUser;
      internal UserManager.FFIMethods.GetUserMethod GetUser;
      internal UserManager.FFIMethods.GetCurrentUserPremiumTypeMethod GetCurrentUserPremiumType;
      internal UserManager.FFIMethods.CurrentUserHasFlagMethod CurrentUserHasFlag;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetCurrentUserMethod(IntPtr methodsPtr, ref User currentUser);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetUserCallback(IntPtr ptr, Result result, ref User user);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetUserMethod(
        IntPtr methodsPtr,
        long userId,
        IntPtr callbackData,
        UserManager.FFIMethods.GetUserCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetCurrentUserPremiumTypeMethod(
        IntPtr methodsPtr,
        ref PremiumType premiumType);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result CurrentUserHasFlagMethod(
        IntPtr methodsPtr,
        UserFlag flag,
        ref bool hasFlag);
    }

    public delegate void GetUserHandler(Result result, ref User user);

    public delegate void CurrentUserUpdateHandler();
  }
}
