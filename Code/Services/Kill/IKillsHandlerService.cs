using System;
using Code.Infrastructure.Services;
using Code.UI.Elements;

namespace Code.Services.Kill
{
  public interface IKillsHandlerService : IService
  {
    void RegisterKill();
    int KillsCount { get; }
    event Action KillCountChanged;
  }
}