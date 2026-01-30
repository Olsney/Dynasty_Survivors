using System;
using UnityEngine;

namespace Code.Data
{
  [Serializable]
  public class KillData
  {
    public int Count;

    public void Add()
    {
      Count++;
    }

    public void Reset()
    {
      Count = 0;
    }
  }
}