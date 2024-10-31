using CthulhuGame;
using UnityEngine;
using UnityEngine.UI;

public class ReputationUI : MonoBehaviour
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
        Player.Instance.Reputation.OnReputationChanged += UpdateImage;
    }

    private void OnDestroy()
    {
        UpdateImage();
        Player.Instance.Reputation.OnReputationChanged -= UpdateImage;
    }
    #endregion

    private void UpdateImage()
    {
        var reputation = Player.Instance.Reputation;

        _image.fillAmount = (float)reputation.CurrentRep / reputation.TargetRep;
    }
}