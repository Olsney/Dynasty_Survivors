using System;
using Code.Data;
using Code.Services.PersistentProgress;

namespace Code.Services.Kill
{
  public class KillsHandlerService : IKillsHandlerService
  {
    private readonly IPersistentProgressService _progressService;
    
    private KillData KillData => _progressService.Progress?.WorldData?.KillData;

    public event Action KillCountChanged;
    
    public int KillsCount => KillData?.Count ?? 0;

    public KillsHandlerService(IPersistentProgressService progressService)
    {
      _progressService = progressService;
    }
    
    public void RegisterKill()
    {
      KillData?.Add();

      KillCountChanged?.Invoke();
    }
  }
}
