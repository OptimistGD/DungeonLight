using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private bool hasShaken = false; // 👈 pour éviter le tremblement au démarrage

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            hasShaken = true; // la caméra a tremblé au moins une fois
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
        else if (hasShaken)
        {
            // on ne recentre que si la caméra a déjà bougé une fois
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 8f);
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    // méthode utile pour le respawn
    public void ResetPosition()
    {
        transform.localPosition = originalPosition;
        shakeDuration = 0f;
    }
}