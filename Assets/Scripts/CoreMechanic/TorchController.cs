using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TorchController : MonoBehaviour
{
    [Header("References de la torche")] 
    public Light torchLight; //assigne la Light (component) de la torche

    [Header("Parameters")] 
    public float deathDelay = 5f; //délai avant la mort si torche éteinte

    [Header("Etat du joueur")] 
    public static bool HasTorch = false; // le joueur commence sans la torche
    private bool isOn = false; //etat debut de la torche
    private Coroutine deathCoroutine = null; // etat debut du timer
    
    

    void Start()
    {
        //désactiver les etats de la torche (le joueur ne l'a pas encore')
        SetTorch(false);
        if (torchLight != null) torchLight.enabled = false;
    }
    
    
    void Update()
    {
        // si le joueur n'a pas de torche, on ne fait rien
        if (!HasTorch)
            return;
        
        //gestion de l'allumage / extinction avec F
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
    
    public static void GiveTorch()
    {
        HasTorch = true;
        Debug.Log("le joueur a récupéré la torche !");
    }

    
    private void SetTorch(bool on)
    {
        isOn = on;
        
        if (torchLight != null) torchLight.enabled = on;

        if (!HasTorch)
            return;
        
        if (!on)
        {
            if (deathCoroutine != null) StopCoroutine(deathCoroutine);
            deathCoroutine = StartCoroutine(DeathTimer());
        }
        
        else
        {
            //si rallume, annuler le timer 
            if (deathCoroutine != null)
            {
                StopCoroutine(deathCoroutine);
                deathCoroutine = null;
            }
        }    
    }

    // coroutine : compte jusqu'a deathDelay puis appelle Die
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator DeathTimer()
    {
        float elapsed = 0f;
        while (elapsed < deathDelay)
        {
            // si la torche a été rallumée ailleurs, on arrête (sécurité debug)
            if (isOn || !HasTorch)
            {
                deathCoroutine = null;
                yield break;
            }
        
            elapsed += Time.deltaTime;
            yield return null;
        }

        Die();
    }
    
    //comportement de la mort : relance la scène courante
    private void Die()
    {
        Debug.Log("Mort : la torche est restée éteinte trop longtemps. Reload Current Scene.");
        // ici tu peux ajouter animation / son / UI avant reload 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}