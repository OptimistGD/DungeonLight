using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraEffects : MonoBehaviour
{
    [Header("Références UI")]
    public Image blackOverlay;           // Image noire UI en overlay

    [Header("Audio")]
    public AudioSource whispersAudio;    // murmures
    public AudioSource deathAudio;       // audio de mort

    [Header("Paramètres d'effet")]
    public float fadeDuration = 5f;      // durée avant mort (même que deathDelay)
    public float cameraShakeIntensity = 0.1f; // intensité légère
    public float cameraShakeSpeed = 30f;

    private bool isFading = false;
    private Vector3 originalCamPos;

    void Start()
    {
        if (blackOverlay != null)
        {
            Color c = blackOverlay.color;
            c.a = 0f;
            blackOverlay.color = c;
        }

        if (whispersAudio != null)
        {
            whispersAudio.volume = 0f;
            whispersAudio.loop = true; // important : les murmures doivent boucler
        }

        originalCamPos = transform.localPosition;
    }

    // Appelée depuis TorchController quand la torche s'éteint
    public void StartDarknessEffect()
    {
        if (!isFading)
            StartCoroutine(DarknessSequence());
    }

    public void StopDarknessEffect()
    {
        StopAllCoroutines();
        isFading = false;

        // Reset effets visuels et audio
        if (blackOverlay != null)
        {
            Color c = blackOverlay.color;
            c.a = 0f;
            blackOverlay.color = c;
        }

        transform.localPosition = originalCamPos;

        if (whispersAudio != null)
        {
            whispersAudio.volume = 0f;
            whispersAudio.Stop();
        }
    }

    private IEnumerator DarknessSequence()
    {
        isFading = true;
        float elapsed = 0f;

        if (whispersAudio != null)
        {
            whispersAudio.volume = 0f;
            if (!whispersAudio.isPlaying)
                whispersAudio.Play();
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            // Écran noir progressif
            if (blackOverlay != null)
            {
                Color c = blackOverlay.color;
                c.a = Mathf.Lerp(0f, 0.8f, t);
                blackOverlay.color = c;
            }

            // Volume murmures progressif
            if (whispersAudio != null)
                whispersAudio.volume = Mathf.Lerp(0f, 1f, t);

            // Caméra tremble légèrement
            transform.localPosition = originalCamPos + (Vector3)Random.insideUnitCircle * cameraShakeIntensity;

            yield return null;
        }

        // Fin de l’effet (l’écran est presque noir)
        transform.localPosition = originalCamPos;
        isFading = false;
    }

    // Appelée depuis TorchController lors de la mort
    public void PlayDeathEffect()
    {
        StopDarknessEffect();

        if (deathAudio != null)
            deathAudio.Play();
    }
}