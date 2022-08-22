// Decompiled with JetBrains decompiler
// Type: ThreadManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
  private static readonly List<Action> executeOnMainThread = new List<Action>();
  private static readonly List<Action> executeCopiedOnMainThread = new List<Action>();
  private static bool actionToExecuteOnMainThread = false;

  private void Update() => ThreadManager.UpdateMain();

  public static void ExecuteOnMainThread(Action _action)
  {
    if (_action == null)
    {
      Debug.Log((object) "No action to execute on main thread!");
    }
    else
    {
      lock (ThreadManager.executeOnMainThread)
      {
        ThreadManager.executeOnMainThread.Add(_action);
        ThreadManager.actionToExecuteOnMainThread = true;
      }
    }
  }

  public static void UpdateMain()
  {
    try
    {
      if (!ThreadManager.actionToExecuteOnMainThread)
        return;
      ThreadManager.executeCopiedOnMainThread.Clear();
      lock (ThreadManager.executeOnMainThread)
      {
        ThreadManager.executeCopiedOnMainThread.AddRange((IEnumerable<Action>) ThreadManager.executeOnMainThread);
        ThreadManager.executeOnMainThread.Clear();
        ThreadManager.actionToExecuteOnMainThread = false;
      }
      for (int index = 0; index < ThreadManager.executeCopiedOnMainThread.Count; ++index)
        ThreadManager.executeCopiedOnMainThread[index]();
    }
    catch (Exception ex)
    {
      Debug.Log((object) ex.ToString());
    }
  }
}
