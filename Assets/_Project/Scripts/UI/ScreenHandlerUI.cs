using UnityEngine;

public class ScreenHandlerUI : MonoBehaviour
{
    [SerializeField] private GameObject _startGamePanel;
    [SerializeField] private GameObject _winGamePanel;
    [SerializeField] private GameObject _loseGamePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _storyPanel;

    public void OpenMainMenu()
    {
        _startGamePanel.SetActive(true);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);
    }

    public void OpenWinGameScreen()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(true);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);
    }

    public void OpenLoseGameScreen()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(true);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);
    }

    public void CloseAllScreens()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(false);
    }

    public void ShowStoryScreen()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(false);
        _storyPanel.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        _startGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _pausePanel.SetActive(true);
        _storyPanel.SetActive(false);
    }
}
