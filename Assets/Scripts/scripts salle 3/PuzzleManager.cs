using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    [Header("Objets à faire tourner")]
    public List<RotatingObject> rotatingObjects;

    [Header("Objet à déplacer quand le puzzle est réussi")]
    public Transform objectToMove;

    [Header("Position finale de cet objet")]
    public Vector3 targetPosition;

    [Header("Vitesse du déplacement (en secondes)")]
    public float moveDuration = 2f;

    [Header("Son de réussite")]
    public AudioClip successSound;
    private AudioSource audioSource;

    private bool solved = false;

    private void Awake()
    {
        Instance = this;

        // Récupère l'audio source (ou en ajoute une)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void CheckAllObjects()
    {
        if (solved) return;

        foreach (RotatingObject obj in rotatingObjects)
        {
            if (!obj.isCorrect)
                return;
        }

        // Puzzle réussi !
        solved = true;
        Debug.Log("✅ Puzzle résolu !");

        // 🔊 Jouer le son
        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        // Déplacer l'objet après
        StartCoroutine(MoveDoorSmoothly());
    }

    private IEnumerator MoveDoorSmoothly()
    {
        Vector3 startPos = objectToMove.position;
        Vector3 endPos = targetPosition;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            objectToMove.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        objectToMove.position = endPos;
    }
}