using CthulhuGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : SingletonBase<LevelController>
{
    [SerializeField] private PauseHandler _pauseHandler;
    [SerializeField] private CthulhuTimer _timer;
    [SerializeField] private ScreenHandlerUI _screenHandler;
    public CthulhuTimer Timer => _timer;

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        _screenHandler.OpenMainMenu();
        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
        Player.Instance.Ship.Health.OnDeath += LoseGame; 
    }

    private void WinGame()
    {
        _screenHandler.OpenWinGameScreen();
        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
        Player.Instance.Ship.Health.OnDeath -= LoseGame;
    }

    private void LoseGame()
    {
        _screenHandler.OpenLoseGameScreen();    
        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
        Player.Instance.Ship.Health.OnDeath -= LoseGame;
    }

    #region PublicAPI
    public void StartGame()
    {
        _timer.ResetTime();
        _screenHandler.CloseAllScreens();
        _pauseHandler.enabled = true;

        Player.Instance.Ship.gameObject.SetActive(true);
    }

    public void ShowStory()
    {
        _screenHandler.ShowStoryScreen();
        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        _screenHandler.ShowPauseScreen();
        _pauseHandler.enabled = true;

        Player.Instance.Ship.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        _screenHandler.CloseAllScreens();
        _pauseHandler.enabled = true;

        Player.Instance.Ship.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  

        LoadLevel();
        StartGame();
    }
    #endregion
}