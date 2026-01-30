using System;

namespace Code.Data
{
  [Serializable]
  public class PlayTimeData
  {
    public float Seconds;

    public void Add(float deltaSeconds)
    {
      if (deltaSeconds <= 0f)
        return;

      Seconds += deltaSeconds;
    }

    public void Reset()
    {
      Seconds = 0f;
    }
  }
}
