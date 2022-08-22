// Decompiled with JetBrains decompiler
// Type: Discord.ApplicationManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
  public class ApplicationManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private ApplicationManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (ApplicationManager.FFIMethods));
        return (ApplicationManager.FFIMethods) this.MethodsStructure;
      }
    }

    internal ApplicationManager(
      IntPtr ptr,
      IntPtr eventsPtr,
      ref ApplicationManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref ApplicationManager.FFIEvents events) => Marshal.StructureToPtr<ApplicationManager.FFIEvents>(events, eventsPtr, false);

    [MonoPInvokeCallback]
    private static void ValidateOrExitCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ApplicationManager.ValidateOrExitHandler target = (ApplicationManager.ValidateOrExitHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void ValidateOrExit(ApplicationManager.ValidateOrExitHandler callback) => this.Methods.ValidateOrExit(this.MethodsPtr, GCHandle.ToIntPtr(GCHandle.Alloc((object) callback)), new ApplicationManager.FFIMethods.ValidateOrExitCallback(ApplicationManager.ValidateOrExitCallbackImpl));

    public string GetCurrentLocale()
    {
      StringBuilder locale = new StringBuilder(128);
      this.Methods.GetCurrentLocale(this.MethodsPtr, locale);
      return locale.ToString();
    }

    public string GetCurrentBranch()
    {
      StringBuilder branch = new StringBuilder(4096);
      this.Methods.GetCurrentBranch(this.MethodsPtr, branch);
      return branch.ToString();
    }

    [MonoPInvokeCallback]
    private static void GetOAuth2TokenCallbackImpl(
      IntPtr ptr,
      Result result,
      ref OAuth2Token oauth2Token)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ApplicationManager.GetOAuth2TokenHandler target = (ApplicationManager.GetOAuth2TokenHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      ref OAuth2Token local = ref oauth2Token;
      target((Result) num, ref local);
    }

    public void GetOAuth2Token(ApplicationManager.GetOAuth2TokenHandler callback) => this.Methods.GetOAuth2Token(this.MethodsPtr, GCHandle.ToIntPtr(GCHandle.Alloc((object) callback)), new ApplicationManager.FFIMethods.GetOAuth2TokenCallback(ApplicationManager.GetOAuth2TokenCallbackImpl));

    [MonoPInvokeCallback]
    private static void GetTicketCallbackImpl(IntPtr ptr, Result result, ref string data)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      ApplicationManager.GetTicketHandler target = (ApplicationManager.GetTicketHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      ref string local = ref data;
      target((Result) num, ref local);
    }

    public void GetTicket(ApplicationManager.GetTicketHandler callback) => this.Methods.GetTicket(this.MethodsPtr, GCHandle.ToIntPtr(GCHandle.Alloc((object) callback)), new ApplicationManager.FFIMethods.GetTicketCallback(ApplicationManager.GetTicketCallbackImpl));

    internal struct FFIEvents
    {
    }

    internal struct FFIMethods
    {
      internal ApplicationManager.FFIMethods.ValidateOrExitMethod ValidateOrExit;
      internal ApplicationManager.FFIMethods.GetCurrentLocaleMethod GetCurrentLocale;
      internal ApplicationManager.FFIMethods.GetCurrentBranchMethod GetCurrentBranch;
      internal ApplicationManager.FFIMethods.GetOAuth2TokenMethod GetOAuth2Token;
      internal ApplicationManager.FFIMethods.GetTicketMethod GetTicket;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ValidateOrExitCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ValidateOrExitMethod(
        IntPtr methodsPtr,
        IntPtr callbackData,
        ApplicationManager.FFIMethods.ValidateOrExitCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetCurrentLocaleMethod(IntPtr methodsPtr, StringBuilder locale);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetCurrentBranchMethod(IntPtr methodsPtr, StringBuilder branch);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetOAuth2TokenCallback(
        IntPtr ptr,
        Result result,
        ref OAuth2Token oauth2Token);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetOAuth2TokenMethod(
        IntPtr methodsPtr,
        IntPtr callbackData,
        ApplicationManager.FFIMethods.GetOAuth2TokenCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetTicketCallback(IntPtr ptr, Result result, [MarshalAs(UnmanagedType.LPStr)] ref string data);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void GetTicketMethod(
        IntPtr methodsPtr,
        IntPtr callbackData,
        ApplicationManager.FFIMethods.GetTicketCallback callback);
    }

    public delegate void ValidateOrExitHandler(Result result);

    public delegate void GetOAuth2TokenHandler(Result result, ref OAuth2Token oauth2Token);

    public delegate void GetTicketHandler(Result result, ref string data);
  }
}
