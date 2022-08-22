﻿// Decompiled with JetBrains decompiler
// Type: Discord.LobbyManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
  public class LobbyManager
  {
    private IntPtr MethodsPtr;
    private object MethodsStructure;

    private LobbyManager.FFIMethods Methods
    {
      get
      {
        if (this.MethodsStructure == null)
          this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof (LobbyManager.FFIMethods));
        return (LobbyManager.FFIMethods) this.MethodsStructure;
      }
    }

    public event LobbyManager.LobbyUpdateHandler OnLobbyUpdate;

    public event LobbyManager.LobbyDeleteHandler OnLobbyDelete;

    public event LobbyManager.MemberConnectHandler OnMemberConnect;

    public event LobbyManager.MemberUpdateHandler OnMemberUpdate;

    public event LobbyManager.MemberDisconnectHandler OnMemberDisconnect;

    public event LobbyManager.LobbyMessageHandler OnLobbyMessage;

    public event LobbyManager.SpeakingHandler OnSpeaking;

    public event LobbyManager.NetworkMessageHandler OnNetworkMessage;

    internal LobbyManager(IntPtr ptr, IntPtr eventsPtr, ref LobbyManager.FFIEvents events)
    {
      if (eventsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
      this.InitEvents(eventsPtr, ref events);
      this.MethodsPtr = ptr;
      if (this.MethodsPtr == IntPtr.Zero)
        throw new ResultException(Result.InternalError);
    }

    private void InitEvents(IntPtr eventsPtr, ref LobbyManager.FFIEvents events)
    {
      events.OnLobbyUpdate = new LobbyManager.FFIEvents.LobbyUpdateHandler(LobbyManager.OnLobbyUpdateImpl);
      events.OnLobbyDelete = new LobbyManager.FFIEvents.LobbyDeleteHandler(LobbyManager.OnLobbyDeleteImpl);
      events.OnMemberConnect = new LobbyManager.FFIEvents.MemberConnectHandler(LobbyManager.OnMemberConnectImpl);
      events.OnMemberUpdate = new LobbyManager.FFIEvents.MemberUpdateHandler(LobbyManager.OnMemberUpdateImpl);
      events.OnMemberDisconnect = new LobbyManager.FFIEvents.MemberDisconnectHandler(LobbyManager.OnMemberDisconnectImpl);
      events.OnLobbyMessage = new LobbyManager.FFIEvents.LobbyMessageHandler(LobbyManager.OnLobbyMessageImpl);
      events.OnSpeaking = new LobbyManager.FFIEvents.SpeakingHandler(LobbyManager.OnSpeakingImpl);
      events.OnNetworkMessage = new LobbyManager.FFIEvents.NetworkMessageHandler(LobbyManager.OnNetworkMessageImpl);
      Marshal.StructureToPtr<LobbyManager.FFIEvents>(events, eventsPtr, false);
    }

    public LobbyTransaction GetLobbyCreateTransaction()
    {
      LobbyTransaction createTransaction = new LobbyTransaction();
      Result result = this.Methods.GetLobbyCreateTransaction(this.MethodsPtr, ref createTransaction.MethodsPtr);
      if (result != Result.Ok)
        throw new ResultException(result);
      return createTransaction;
    }

    public LobbyTransaction GetLobbyUpdateTransaction(long lobbyId)
    {
      LobbyTransaction updateTransaction = new LobbyTransaction();
      Result result = this.Methods.GetLobbyUpdateTransaction(this.MethodsPtr, lobbyId, ref updateTransaction.MethodsPtr);
      if (result != Result.Ok)
        throw new ResultException(result);
      return updateTransaction;
    }

    public LobbyMemberTransaction GetMemberUpdateTransaction(
      long lobbyId,
      long userId)
    {
      LobbyMemberTransaction updateTransaction = new LobbyMemberTransaction();
      Result result = this.Methods.GetMemberUpdateTransaction(this.MethodsPtr, lobbyId, userId, ref updateTransaction.MethodsPtr);
      if (result != Result.Ok)
        throw new ResultException(result);
      return updateTransaction;
    }

    [MonoPInvokeCallback]
    private static void CreateLobbyCallbackImpl(IntPtr ptr, Result result, ref Lobby lobby)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.CreateLobbyHandler target = (LobbyManager.CreateLobbyHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      ref Lobby local = ref lobby;
      target((Result) num, ref local);
    }

    public void CreateLobby(LobbyTransaction transaction, LobbyManager.CreateLobbyHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.CreateLobby(this.MethodsPtr, transaction.MethodsPtr, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.CreateLobbyCallback(LobbyManager.CreateLobbyCallbackImpl));
      transaction.MethodsPtr = IntPtr.Zero;
    }

    [MonoPInvokeCallback]
    private static void UpdateLobbyCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.UpdateLobbyHandler target = (LobbyManager.UpdateLobbyHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void UpdateLobby(
      long lobbyId,
      LobbyTransaction transaction,
      LobbyManager.UpdateLobbyHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.UpdateLobby(this.MethodsPtr, lobbyId, transaction.MethodsPtr, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.UpdateLobbyCallback(LobbyManager.UpdateLobbyCallbackImpl));
      transaction.MethodsPtr = IntPtr.Zero;
    }

    [MonoPInvokeCallback]
    private static void DeleteLobbyCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.DeleteLobbyHandler target = (LobbyManager.DeleteLobbyHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void DeleteLobby(long lobbyId, LobbyManager.DeleteLobbyHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.DeleteLobby(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.DeleteLobbyCallback(LobbyManager.DeleteLobbyCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void ConnectLobbyCallbackImpl(IntPtr ptr, Result result, ref Lobby lobby)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.ConnectLobbyHandler target = (LobbyManager.ConnectLobbyHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      ref Lobby local = ref lobby;
      target((Result) num, ref local);
    }

    public void ConnectLobby(
      long lobbyId,
      string secret,
      LobbyManager.ConnectLobbyHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.ConnectLobby(this.MethodsPtr, lobbyId, secret, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.ConnectLobbyCallback(LobbyManager.ConnectLobbyCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void ConnectLobbyWithActivitySecretCallbackImpl(
      IntPtr ptr,
      Result result,
      ref Lobby lobby)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.ConnectLobbyWithActivitySecretHandler target = (LobbyManager.ConnectLobbyWithActivitySecretHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      ref Lobby local = ref lobby;
      target((Result) num, ref local);
    }

    public void ConnectLobbyWithActivitySecret(
      string activitySecret,
      LobbyManager.ConnectLobbyWithActivitySecretHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.ConnectLobbyWithActivitySecret(this.MethodsPtr, activitySecret, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback(LobbyManager.ConnectLobbyWithActivitySecretCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void DisconnectLobbyCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.DisconnectLobbyHandler target = (LobbyManager.DisconnectLobbyHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void DisconnectLobby(long lobbyId, LobbyManager.DisconnectLobbyHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.DisconnectLobby(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.DisconnectLobbyCallback(LobbyManager.DisconnectLobbyCallbackImpl));
    }

    public Lobby GetLobby(long lobbyId)
    {
      Lobby lobby = new Lobby();
      Result result = this.Methods.GetLobby(this.MethodsPtr, lobbyId, ref lobby);
      if (result != Result.Ok)
        throw new ResultException(result);
      return lobby;
    }

    public string GetLobbyActivitySecret(long lobbyId)
    {
      StringBuilder secret = new StringBuilder(128);
      Result result = this.Methods.GetLobbyActivitySecret(this.MethodsPtr, lobbyId, secret);
      if (result != Result.Ok)
        throw new ResultException(result);
      return secret.ToString();
    }

    public string GetLobbyMetadataValue(long lobbyId, string key)
    {
      StringBuilder stringBuilder = new StringBuilder(4096);
      Result result = this.Methods.GetLobbyMetadataValue(this.MethodsPtr, lobbyId, key, stringBuilder);
      if (result != Result.Ok)
        throw new ResultException(result);
      return stringBuilder.ToString();
    }

    public string GetLobbyMetadataKey(long lobbyId, int index)
    {
      StringBuilder key = new StringBuilder(256);
      Result result = this.Methods.GetLobbyMetadataKey(this.MethodsPtr, lobbyId, index, key);
      if (result != Result.Ok)
        throw new ResultException(result);
      return key.ToString();
    }

    public int LobbyMetadataCount(long lobbyId)
    {
      int count = 0;
      Result result = this.Methods.LobbyMetadataCount(this.MethodsPtr, lobbyId, ref count);
      if (result != Result.Ok)
        throw new ResultException(result);
      return count;
    }

    public int MemberCount(long lobbyId)
    {
      int count = 0;
      Result result = this.Methods.MemberCount(this.MethodsPtr, lobbyId, ref count);
      if (result != Result.Ok)
        throw new ResultException(result);
      return count;
    }

    public long GetMemberUserId(long lobbyId, int index)
    {
      long userId = 0;
      Result result = this.Methods.GetMemberUserId(this.MethodsPtr, lobbyId, index, ref userId);
      if (result != Result.Ok)
        throw new ResultException(result);
      return userId;
    }

    public User GetMemberUser(long lobbyId, long userId)
    {
      User user = new User();
      Result result = this.Methods.GetMemberUser(this.MethodsPtr, lobbyId, userId, ref user);
      if (result != Result.Ok)
        throw new ResultException(result);
      return user;
    }

    public string GetMemberMetadataValue(long lobbyId, long userId, string key)
    {
      StringBuilder stringBuilder = new StringBuilder(4096);
      Result result = this.Methods.GetMemberMetadataValue(this.MethodsPtr, lobbyId, userId, key, stringBuilder);
      if (result != Result.Ok)
        throw new ResultException(result);
      return stringBuilder.ToString();
    }

    public string GetMemberMetadataKey(long lobbyId, long userId, int index)
    {
      StringBuilder key = new StringBuilder(256);
      Result result = this.Methods.GetMemberMetadataKey(this.MethodsPtr, lobbyId, userId, index, key);
      if (result != Result.Ok)
        throw new ResultException(result);
      return key.ToString();
    }

    public int MemberMetadataCount(long lobbyId, long userId)
    {
      int count = 0;
      Result result = this.Methods.MemberMetadataCount(this.MethodsPtr, lobbyId, userId, ref count);
      if (result != Result.Ok)
        throw new ResultException(result);
      return count;
    }

    [MonoPInvokeCallback]
    private static void UpdateMemberCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.UpdateMemberHandler target = (LobbyManager.UpdateMemberHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void UpdateMember(
      long lobbyId,
      long userId,
      LobbyMemberTransaction transaction,
      LobbyManager.UpdateMemberHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.UpdateMember(this.MethodsPtr, lobbyId, userId, transaction.MethodsPtr, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.UpdateMemberCallback(LobbyManager.UpdateMemberCallbackImpl));
      transaction.MethodsPtr = IntPtr.Zero;
    }

    [MonoPInvokeCallback]
    private static void SendLobbyMessageCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.SendLobbyMessageHandler target = (LobbyManager.SendLobbyMessageHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void SendLobbyMessage(
      long lobbyId,
      byte[] data,
      LobbyManager.SendLobbyMessageHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.SendLobbyMessage(this.MethodsPtr, lobbyId, data, data.Length, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.SendLobbyMessageCallback(LobbyManager.SendLobbyMessageCallbackImpl));
    }

    public LobbySearchQuery GetSearchQuery()
    {
      LobbySearchQuery searchQuery = new LobbySearchQuery();
      Result result = this.Methods.GetSearchQuery(this.MethodsPtr, ref searchQuery.MethodsPtr);
      if (result != Result.Ok)
        throw new ResultException(result);
      return searchQuery;
    }

    [MonoPInvokeCallback]
    private static void SearchCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.SearchHandler target = (LobbyManager.SearchHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void Search(LobbySearchQuery query, LobbyManager.SearchHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.Search(this.MethodsPtr, query.MethodsPtr, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.SearchCallback(LobbyManager.SearchCallbackImpl));
      query.MethodsPtr = IntPtr.Zero;
    }

    public int LobbyCount()
    {
      int count = 0;
      this.Methods.LobbyCount(this.MethodsPtr, ref count);
      return count;
    }

    public long GetLobbyId(int index)
    {
      long lobbyId = 0;
      Result result = this.Methods.GetLobbyId(this.MethodsPtr, index, ref lobbyId);
      if (result != Result.Ok)
        throw new ResultException(result);
      return lobbyId;
    }

    [MonoPInvokeCallback]
    private static void ConnectVoiceCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.ConnectVoiceHandler target = (LobbyManager.ConnectVoiceHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void ConnectVoice(long lobbyId, LobbyManager.ConnectVoiceHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.ConnectVoice(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.ConnectVoiceCallback(LobbyManager.ConnectVoiceCallbackImpl));
    }

    [MonoPInvokeCallback]
    private static void DisconnectVoiceCallbackImpl(IntPtr ptr, Result result)
    {
      GCHandle gcHandle = GCHandle.FromIntPtr(ptr);
      LobbyManager.DisconnectVoiceHandler target = (LobbyManager.DisconnectVoiceHandler) gcHandle.Target;
      gcHandle.Free();
      int num = (int) result;
      target((Result) num);
    }

    public void DisconnectVoice(long lobbyId, LobbyManager.DisconnectVoiceHandler callback)
    {
      GCHandle gcHandle = GCHandle.Alloc((object) callback);
      this.Methods.DisconnectVoice(this.MethodsPtr, lobbyId, GCHandle.ToIntPtr(gcHandle), new LobbyManager.FFIMethods.DisconnectVoiceCallback(LobbyManager.DisconnectVoiceCallbackImpl));
    }

    public void ConnectNetwork(long lobbyId)
    {
      Result result = this.Methods.ConnectNetwork(this.MethodsPtr, lobbyId);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void DisconnectNetwork(long lobbyId)
    {
      Result result = this.Methods.DisconnectNetwork(this.MethodsPtr, lobbyId);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void FlushNetwork()
    {
      Result result = this.Methods.FlushNetwork(this.MethodsPtr);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void OpenNetworkChannel(long lobbyId, byte channelId, bool reliable)
    {
      Result result = this.Methods.OpenNetworkChannel(this.MethodsPtr, lobbyId, channelId, reliable);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    public void SendNetworkMessage(long lobbyId, long userId, byte channelId, byte[] data)
    {
      Result result = this.Methods.SendNetworkMessage(this.MethodsPtr, lobbyId, userId, channelId, data, data.Length);
      if (result != Result.Ok)
        throw new ResultException(result);
    }

    [MonoPInvokeCallback]
    private static void OnLobbyUpdateImpl(IntPtr ptr, long lobbyId)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnLobbyUpdate == null)
        return;
      target.LobbyManagerInstance.OnLobbyUpdate(lobbyId);
    }

    [MonoPInvokeCallback]
    private static void OnLobbyDeleteImpl(IntPtr ptr, long lobbyId, uint reason)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnLobbyDelete == null)
        return;
      target.LobbyManagerInstance.OnLobbyDelete(lobbyId, reason);
    }

    [MonoPInvokeCallback]
    private static void OnMemberConnectImpl(IntPtr ptr, long lobbyId, long userId)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnMemberConnect == null)
        return;
      target.LobbyManagerInstance.OnMemberConnect(lobbyId, userId);
    }

    [MonoPInvokeCallback]
    private static void OnMemberUpdateImpl(IntPtr ptr, long lobbyId, long userId)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnMemberUpdate == null)
        return;
      target.LobbyManagerInstance.OnMemberUpdate(lobbyId, userId);
    }

    [MonoPInvokeCallback]
    private static void OnMemberDisconnectImpl(IntPtr ptr, long lobbyId, long userId)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnMemberDisconnect == null)
        return;
      target.LobbyManagerInstance.OnMemberDisconnect(lobbyId, userId);
    }

    [MonoPInvokeCallback]
    private static void OnLobbyMessageImpl(
      IntPtr ptr,
      long lobbyId,
      long userId,
      IntPtr dataPtr,
      int dataLen)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnLobbyMessage == null)
        return;
      byte[] numArray = new byte[dataLen];
      Marshal.Copy(dataPtr, numArray, 0, dataLen);
      target.LobbyManagerInstance.OnLobbyMessage(lobbyId, userId, numArray);
    }

    [MonoPInvokeCallback]
    private static void OnSpeakingImpl(IntPtr ptr, long lobbyId, long userId, bool speaking)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnSpeaking == null)
        return;
      target.LobbyManagerInstance.OnSpeaking(lobbyId, userId, speaking);
    }

    [MonoPInvokeCallback]
    private static void OnNetworkMessageImpl(
      IntPtr ptr,
      long lobbyId,
      long userId,
      byte channelId,
      IntPtr dataPtr,
      int dataLen)
    {
      Discord.Discord target = (Discord.Discord) GCHandle.FromIntPtr(ptr).Target;
      if (target.LobbyManagerInstance.OnNetworkMessage == null)
        return;
      byte[] numArray = new byte[dataLen];
      Marshal.Copy(dataPtr, numArray, 0, dataLen);
      target.LobbyManagerInstance.OnNetworkMessage(lobbyId, userId, channelId, numArray);
    }

    public IEnumerable<User> GetMemberUsers(long lobbyID)
    {
      int num = this.MemberCount(lobbyID);
      List<User> memberUsers = new List<User>();
      for (int index = 0; index < num; ++index)
        memberUsers.Add(this.GetMemberUser(lobbyID, this.GetMemberUserId(lobbyID, index)));
      return (IEnumerable<User>) memberUsers;
    }

    public void SendLobbyMessage(
      long lobbyID,
      string data,
      LobbyManager.SendLobbyMessageHandler handler)
    {
      this.SendLobbyMessage(lobbyID, Encoding.UTF8.GetBytes(data), handler);
    }

    internal struct FFIEvents
    {
      internal LobbyManager.FFIEvents.LobbyUpdateHandler OnLobbyUpdate;
      internal LobbyManager.FFIEvents.LobbyDeleteHandler OnLobbyDelete;
      internal LobbyManager.FFIEvents.MemberConnectHandler OnMemberConnect;
      internal LobbyManager.FFIEvents.MemberUpdateHandler OnMemberUpdate;
      internal LobbyManager.FFIEvents.MemberDisconnectHandler OnMemberDisconnect;
      internal LobbyManager.FFIEvents.LobbyMessageHandler OnLobbyMessage;
      internal LobbyManager.FFIEvents.SpeakingHandler OnSpeaking;
      internal LobbyManager.FFIEvents.NetworkMessageHandler OnNetworkMessage;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void LobbyUpdateHandler(IntPtr ptr, long lobbyId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void LobbyDeleteHandler(IntPtr ptr, long lobbyId, uint reason);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void MemberConnectHandler(IntPtr ptr, long lobbyId, long userId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void MemberUpdateHandler(IntPtr ptr, long lobbyId, long userId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void MemberDisconnectHandler(IntPtr ptr, long lobbyId, long userId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void LobbyMessageHandler(
        IntPtr ptr,
        long lobbyId,
        long userId,
        IntPtr dataPtr,
        int dataLen);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SpeakingHandler(IntPtr ptr, long lobbyId, long userId, bool speaking);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void NetworkMessageHandler(
        IntPtr ptr,
        long lobbyId,
        long userId,
        byte channelId,
        IntPtr dataPtr,
        int dataLen);
    }

    internal struct FFIMethods
    {
      internal LobbyManager.FFIMethods.GetLobbyCreateTransactionMethod GetLobbyCreateTransaction;
      internal LobbyManager.FFIMethods.GetLobbyUpdateTransactionMethod GetLobbyUpdateTransaction;
      internal LobbyManager.FFIMethods.GetMemberUpdateTransactionMethod GetMemberUpdateTransaction;
      internal LobbyManager.FFIMethods.CreateLobbyMethod CreateLobby;
      internal LobbyManager.FFIMethods.UpdateLobbyMethod UpdateLobby;
      internal LobbyManager.FFIMethods.DeleteLobbyMethod DeleteLobby;
      internal LobbyManager.FFIMethods.ConnectLobbyMethod ConnectLobby;
      internal LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretMethod ConnectLobbyWithActivitySecret;
      internal LobbyManager.FFIMethods.DisconnectLobbyMethod DisconnectLobby;
      internal LobbyManager.FFIMethods.GetLobbyMethod GetLobby;
      internal LobbyManager.FFIMethods.GetLobbyActivitySecretMethod GetLobbyActivitySecret;
      internal LobbyManager.FFIMethods.GetLobbyMetadataValueMethod GetLobbyMetadataValue;
      internal LobbyManager.FFIMethods.GetLobbyMetadataKeyMethod GetLobbyMetadataKey;
      internal LobbyManager.FFIMethods.LobbyMetadataCountMethod LobbyMetadataCount;
      internal LobbyManager.FFIMethods.MemberCountMethod MemberCount;
      internal LobbyManager.FFIMethods.GetMemberUserIdMethod GetMemberUserId;
      internal LobbyManager.FFIMethods.GetMemberUserMethod GetMemberUser;
      internal LobbyManager.FFIMethods.GetMemberMetadataValueMethod GetMemberMetadataValue;
      internal LobbyManager.FFIMethods.GetMemberMetadataKeyMethod GetMemberMetadataKey;
      internal LobbyManager.FFIMethods.MemberMetadataCountMethod MemberMetadataCount;
      internal LobbyManager.FFIMethods.UpdateMemberMethod UpdateMember;
      internal LobbyManager.FFIMethods.SendLobbyMessageMethod SendLobbyMessage;
      internal LobbyManager.FFIMethods.GetSearchQueryMethod GetSearchQuery;
      internal LobbyManager.FFIMethods.SearchMethod Search;
      internal LobbyManager.FFIMethods.LobbyCountMethod LobbyCount;
      internal LobbyManager.FFIMethods.GetLobbyIdMethod GetLobbyId;
      internal LobbyManager.FFIMethods.ConnectVoiceMethod ConnectVoice;
      internal LobbyManager.FFIMethods.DisconnectVoiceMethod DisconnectVoice;
      internal LobbyManager.FFIMethods.ConnectNetworkMethod ConnectNetwork;
      internal LobbyManager.FFIMethods.DisconnectNetworkMethod DisconnectNetwork;
      internal LobbyManager.FFIMethods.FlushNetworkMethod FlushNetwork;
      internal LobbyManager.FFIMethods.OpenNetworkChannelMethod OpenNetworkChannel;
      internal LobbyManager.FFIMethods.SendNetworkMessageMethod SendNetworkMessage;

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyCreateTransactionMethod(
        IntPtr methodsPtr,
        ref IntPtr transaction);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyUpdateTransactionMethod(
        IntPtr methodsPtr,
        long lobbyId,
        ref IntPtr transaction);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetMemberUpdateTransactionMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        ref IntPtr transaction);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void CreateLobbyCallback(IntPtr ptr, Result result, ref Lobby lobby);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void CreateLobbyMethod(
        IntPtr methodsPtr,
        IntPtr transaction,
        IntPtr callbackData,
        LobbyManager.FFIMethods.CreateLobbyCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void UpdateLobbyCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void UpdateLobbyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        IntPtr transaction,
        IntPtr callbackData,
        LobbyManager.FFIMethods.UpdateLobbyCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void DeleteLobbyCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void DeleteLobbyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        IntPtr callbackData,
        LobbyManager.FFIMethods.DeleteLobbyCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ConnectLobbyCallback(IntPtr ptr, Result result, ref Lobby lobby);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ConnectLobbyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        [MarshalAs(UnmanagedType.LPStr)] string secret,
        IntPtr callbackData,
        LobbyManager.FFIMethods.ConnectLobbyCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ConnectLobbyWithActivitySecretCallback(
        IntPtr ptr,
        Result result,
        ref Lobby lobby);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ConnectLobbyWithActivitySecretMethod(
        IntPtr methodsPtr,
        [MarshalAs(UnmanagedType.LPStr)] string activitySecret,
        IntPtr callbackData,
        LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void DisconnectLobbyCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void DisconnectLobbyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        IntPtr callbackData,
        LobbyManager.FFIMethods.DisconnectLobbyCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        ref Lobby lobby);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyActivitySecretMethod(
        IntPtr methodsPtr,
        long lobbyId,
        StringBuilder secret);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyMetadataValueMethod(
        IntPtr methodsPtr,
        long lobbyId,
        [MarshalAs(UnmanagedType.LPStr)] string key,
        StringBuilder value);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyMetadataKeyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        int index,
        StringBuilder key);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result LobbyMetadataCountMethod(
        IntPtr methodsPtr,
        long lobbyId,
        ref int count);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result MemberCountMethod(
        IntPtr methodsPtr,
        long lobbyId,
        ref int count);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetMemberUserIdMethod(
        IntPtr methodsPtr,
        long lobbyId,
        int index,
        ref long userId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetMemberUserMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        ref User user);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetMemberMetadataValueMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        [MarshalAs(UnmanagedType.LPStr)] string key,
        StringBuilder value);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetMemberMetadataKeyMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        int index,
        StringBuilder key);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result MemberMetadataCountMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        ref int count);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void UpdateMemberCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void UpdateMemberMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        IntPtr transaction,
        IntPtr callbackData,
        LobbyManager.FFIMethods.UpdateMemberCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SendLobbyMessageCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SendLobbyMessageMethod(
        IntPtr methodsPtr,
        long lobbyId,
        byte[] data,
        int dataLen,
        IntPtr callbackData,
        LobbyManager.FFIMethods.SendLobbyMessageCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetSearchQueryMethod(IntPtr methodsPtr, ref IntPtr query);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SearchCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void SearchMethod(
        IntPtr methodsPtr,
        IntPtr query,
        IntPtr callbackData,
        LobbyManager.FFIMethods.SearchCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void LobbyCountMethod(IntPtr methodsPtr, ref int count);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result GetLobbyIdMethod(
        IntPtr methodsPtr,
        int index,
        ref long lobbyId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ConnectVoiceCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void ConnectVoiceMethod(
        IntPtr methodsPtr,
        long lobbyId,
        IntPtr callbackData,
        LobbyManager.FFIMethods.ConnectVoiceCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void DisconnectVoiceCallback(IntPtr ptr, Result result);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void DisconnectVoiceMethod(
        IntPtr methodsPtr,
        long lobbyId,
        IntPtr callbackData,
        LobbyManager.FFIMethods.DisconnectVoiceCallback callback);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result ConnectNetworkMethod(IntPtr methodsPtr, long lobbyId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result DisconnectNetworkMethod(IntPtr methodsPtr, long lobbyId);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result FlushNetworkMethod(IntPtr methodsPtr);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result OpenNetworkChannelMethod(
        IntPtr methodsPtr,
        long lobbyId,
        byte channelId,
        bool reliable);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate Result SendNetworkMessageMethod(
        IntPtr methodsPtr,
        long lobbyId,
        long userId,
        byte channelId,
        byte[] data,
        int dataLen);
    }

    public delegate void CreateLobbyHandler(Result result, ref Lobby lobby);

    public delegate void UpdateLobbyHandler(Result result);

    public delegate void DeleteLobbyHandler(Result result);

    public delegate void ConnectLobbyHandler(Result result, ref Lobby lobby);

    public delegate void ConnectLobbyWithActivitySecretHandler(Result result, ref Lobby lobby);

    public delegate void DisconnectLobbyHandler(Result result);

    public delegate void UpdateMemberHandler(Result result);

    public delegate void SendLobbyMessageHandler(Result result);

    public delegate void SearchHandler(Result result);

    public delegate void ConnectVoiceHandler(Result result);

    public delegate void DisconnectVoiceHandler(Result result);

    public delegate void LobbyUpdateHandler(long lobbyId);

    public delegate void LobbyDeleteHandler(long lobbyId, uint reason);

    public delegate void MemberConnectHandler(long lobbyId, long userId);

    public delegate void MemberUpdateHandler(long lobbyId, long userId);

    public delegate void MemberDisconnectHandler(long lobbyId, long userId);

    public delegate void LobbyMessageHandler(long lobbyId, long userId, byte[] data);

    public delegate void SpeakingHandler(long lobbyId, long userId, bool speaking);

    public delegate void NetworkMessageHandler(
      long lobbyId,
      long userId,
      byte channelId,
      byte[] data);
  }
}
