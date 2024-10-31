using CthulhuGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : SingletonBase<LevelController>
{
    [SerializeField] private GameObject _startGamePanel;
    [SerializeField] private GameObject _winGamePanel;
    [SerializeField] private GameObject _loseGamePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _storyPanel;
    [SerializeField] private PauseHandler _pauseHandler;

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        _startGamePanel.SetActive(true);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);

        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
        Player.Instance.Ship.Health.OnDeath += LoseGame; 
    }

    private void WinGame()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(true);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);

        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
        Player.Instance.Ship.Health.OnDeath -= LoseGame;
    }

    private void LoseGame()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(true);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);

        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
        Player.Instance.Ship.Health.OnDeath -= LoseGame;
    }

    #region PublicAPI
    public void StartGame()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);

        _pauseHandler.enabled = true;

        Player.Instance.Ship.gameObject.SetActive(true);
    }

    public void ShowStoryScreen()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(true);

        _pauseHandler.enabled = false;

        Player.Instance.Ship.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(true);
        _storyPanel.SetActive(false);

        _pauseHandler.enabled = true;

        Player.Instance.Ship.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);

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