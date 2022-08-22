// Decompiled with JetBrains decompiler
// Type: Discord.LobbySearchQuery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace Discord
{
  public struct LobbySearchQuery
  {
    internal IntPtr MethodsPtr;
    internal object MethodsStructure;

    private LobbySearchQuery.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (LobbySearchQuery.FFIMethods));
        return (LobbySearchQuery.FFIMethods) this.MethodsStructure;
      }
    }

    public void Filter(
      string key,
      LobbySearchComparison comparison,
      LobbySearchCast cast,
      string value)
    {
      if (!(this.MethodsPtr != IntPtr.Zero))
        return;
      Result result = this.Methods.Filter(this.MethodsPtr, key, comparison, cast, value);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void Sort(string key, LobbySearchCast cast, string value)
    {
      if (!(this.MethodsPtr != IntPtr.Zero))
        return;
      Result result = this.Methods.Sort(this.MethodsPtr, key, cast, value);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void Limit(uint limit)
    {
      if (!(this.MethodsPtr != IntPtr.Zero))
        return;
      Result result = this.Methods.Limit(this.MethodsPtr, limit);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void Distance(LobbySearchDistance distance)
    {
      if (!(this.MethodsPtr != IntPtr.Zero))
        return;
      Result result = this.Methods.Distance(this.MethodsPtr, distance);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    internal struct FFIMethods
    {
      internal LobbySearchQuery.FFIMethods.FilterMethod Filter;
      internal LobbySearchQuery.FFIMethods.SortMethod Sort;
      internal LobbySearchQuery.FFIMethods.LimitMethod Limit;
      internal LobbySearchQuery.FFIMethods.DistanceMethod Distance;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result FilterMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string key,
        LobbySearchComparison comparison,
        LobbySearchCast cast,
        [MarshalAs(UnmanagedType.LPStr)] string value);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result SortMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string key,
        LobbySearchCast cast,
        [MarshalAs(UnmanagedType.LPStr)] string value);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result LimitMethod(IntPtr methodsPtr, uint limit);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result DistanceMethod(IntPtr methodsPtr, LobbySearchDistance distance);
    }
  }
}
