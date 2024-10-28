using UnityEngine;

public class BoatEngineSound : MonoBehaviour
{
    public AudioSource engineAudioSource;
    public Rigidbody boatRigidbody;

    public float minPitch = 0.5f;
    public float maxPitch = 2.0f;
    public float maxSpeed = 20f;
    public float maxVolume = 1.0f;

    void Update()
    {
        if (engineAudioSource != null && boatRigidbody != null)
        {
            float speed = boatRigidbody.linearVelocity.magnitude;
            float normalizedSpeed = speed / maxSpeed;
            engineAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
            engineAudioSource.volume = Mathf.Lerp(0.05f, maxVolume, normalizedSpeed);
        }
    }
}