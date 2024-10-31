using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    #region UnityEvents
    private void Awake()
    {
        _image.fillAmount = 0f;
    }

    private void Start()
    {
        UpdateImage();
        LevelController.Instance.Timer.OnSecondsPassed += UpdateImage;
    }

    private void OnDestroy()
    {
        UpdateImage();
        LevelController.Instance.Timer.OnSecondsPassed -= UpdateImage;
    }
    #endregion

    private void UpdateImage()
    {
        var timer = LevelController.Instance.Timer;

        _image.fillAmount = timer.currentTime / timer.TargetTime;
    }
}