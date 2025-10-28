using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TorchController : MonoBehaviour
{
    [Header("Références de la torche")]
    public Light torchLight;
    public CanvasGroup darknessOverlay;
    public AudioSource whispersAudio;
    public CameraEffects cameraEffects; // 👈 référence au script CameraEffects

    [Header("Paramètres")]
    public float deathDelay = 5f;

    [Header("Écran de mort")]
    public CanvasGroup deathScreen;
    public Text deathText; // texte visible à l’écran
    public AudioSource deathAudio; // son joué à la mort

    [Header("État du joueur")]
    public bool HasTorch = false;
    private bool isOn = false;
    private Coroutine deathCoroutine = null;

    public TorchVisual torchVisual;

    void Start()
    {
        SetTorch(false);
        if (torchLight != null) torchLight.enabled = false;
        if (darknessOverlay != null) darknessOverlay.alpha = 0f;
        if (whispersAudio != null) whispersAudio.volume = 0f;
    }

    void Update()
    {
        if (!HasTorch) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleTorch();
        }
    }

    public void ToggleTorch()
    {
        if (!HasTorch) return;
        SetTorch(!isOn);
    }

    public void GiveTorch()
    {
        HasTorch = true;
        Debug.Log("Le joueur a récupéré la torche !");
    }

    private void SetTorch(bool on)
    {
        isOn = on;
        if (torchLight != null) torchLight.enabled = on;
        if (torchVisual != null) torchVisual.ShowTorch(on);

        if (!HasTorch) return;

        if (!on)
        {
            if (deathCoroutine != null) StopCoroutine(deathCoroutine);
            deathCoroutine = StartCoroutine(DeathTimer());
        }
        else
        {
            if (deathCoroutine != null)
            {
                StopCoroutine(deathCoroutine);
                deathCoroutine = null;
            }

            // Réinitialise les effets visuels/sonores quand la torche se rallume
            if (darknessOverlay != null) darknessOverlay.alpha = 0f;
            if (whispersAudio != null) whispersAudio.volume = 0f;
            if (cameraEffects != null) cameraEffects.ResetPosition();
        }
    }

    private IEnumerator DeathTimer()
    {
        float elapsed = 0f;
        while (elapsed < deathDelay)
        {
            if (isOn || !HasTorch)
            {
                deathCoroutine = null;
                yield break;
            }

            // progressif : noirceur + murmures + caméra
            float t = elapsed / deathDelay;
            if (darknessOverlay != null)
                darknessOverlay.alpha = Mathf.Lerp(0f, 1f, t);

            if (whispersAudio != null)
                whispersAudio.volume = Mathf.Lerp(0f, 1f, t);

            if (cameraEffects != null)
                cameraEffects.Shake(0.1f, Mathf.Lerp(0.01f, 0.04f, t)); // 👈 tremblement doux et progressif

            elapsed += Time.deltaTime;
            yield return null;
        }

        Die();
    }

    private void Die()
    {
        Debug.Log("Mort : la torche est restée éteinte trop longtemps.");
        StartCoroutine(ShowDeathScreen());
    }

    private IEnumerator ShowDeathScreen()
    {
        if (deathScreen != null)
        {
            deathScreen.alpha = 1f;
            if (deathText != null)
                deathText.enabled = true; // 👈 on affiche le texte visible de l’UI
        }

        if (deathAudio != null)
            deathAudio.Play();

        yield return new WaitForSeconds(3f);

        if (cameraEffects != null)
            cameraEffects.ResetPosition();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}