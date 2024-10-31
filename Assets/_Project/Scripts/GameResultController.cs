using UnityEngine;
using CthulhuGame;

public class GameResultController : MonoBehaviour
{
    private bool _isTimerPassed;
    public bool IsTimerPassed => _isTimerPassed;
    
    private void Start()
    {
        Player.Instance.Ship.Health.OnDeath += DoLoseGameResult;
        Player.Instance.Reputation.OnReputationGained += DoWinGameResut;
        LevelController.Instance.Timer.OnCthulhuTimerPassed += CheckGameResult;
    }

    private void OnDestroy()
    {
        Player.Instance.Ship.Health.OnDeath -= DoLoseGameResult;
        Player.Instance.Reputation.OnReputationGained -= DoWinGameResut;
        LevelController.Instance.Timer.OnCthulhuTimerPassed -= CheckGameResult;
    }

    private void CheckGameResult()
    {
        var rep = Player.Instance.Reputation;

        if (rep.CurrentRep < rep.TargetRep)
        {
            _isTimerPassed = true;
            DoLoseGameResult();
        }
        else
        {
            DoWinGameResut();
        }
    }

    private void DoLoseGameResult()
    {
        LevelController.Instance.LoseGame();
    }

    private void DoWinGameResut()
    {
        LevelController.Instance.WinGame();
    }
}