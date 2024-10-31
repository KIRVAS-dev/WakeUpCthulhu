using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;

    private bool _isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        LevelController.Instance.PauseGame();
        _isPaused = true;
    }

    public void ResumeGame()
    {
        LevelController.Instance.ResumeGame();
        _isPaused = false;
    }
}
