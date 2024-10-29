using CthulhuGame;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CastingAFishingRodSound : MonoBehaviour
{
    [SerializeField] private AudioClip castingAFishingRod;
    [SerializeField] private AudioClip pullTheFishingRod;
    [SerializeField] private AudioSource audioSource;

    #region UnityEvents
    void Start()
    {
        ActionButton.Instance.OnActionButtonClicked += OnButtonClick;
    }

    private void OnDestroy()
    {
        ActionButton.Instance.OnActionButtonClicked -= OnButtonClick;
    }
    #endregion

    void OnButtonClick()
    {
        if (ActionButton.Instance.Type == ActionButton.ActionType.FishingChallenge)
        {
            audioSource.PlayOneShot(castingAFishingRod);
        }

        if (ActionButton.Instance.Type == ActionButton.ActionType.CatchFish)
        {
            audioSource.PlayOneShot(pullTheFishingRod);
        }
    }    
}
