using CthulhuGame;
using UnityEngine;
using UnityEngine.UI;

public class CastingAFishingRodSound : MonoBehaviour
{
    public AudioClip castingAFishingRod;
    public AudioClip pullTheFishingRod;
    public AudioSource audioSource;
    public Button playButton;

    private bool playFirstSound = true; // Флаг для определения, какой звук воспроизводить

    void Start()
    {
        if (playButton != null && audioSource != null)
        {
            playButton.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (playFirstSound)
        {
            audioSource.PlayOneShot(castingAFishingRod);
        }
        else
        {
            audioSource.PlayOneShot(pullTheFishingRod);
        }

        playFirstSound = !playFirstSound;
    }
}
