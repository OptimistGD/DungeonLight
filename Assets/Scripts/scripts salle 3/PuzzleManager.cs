
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Objets à faire tourner")]
    public List<RotatingObject> rotatingObjects;

    [Header("Objet à déplacer quand le puzzle est réussi")]
    public Transform objectToMove;

    [Header("Position finale de cet objet")]
    public Vector3 targetPosition;

    [Header("Vitesse du déplacement (en secondes)")]
    public float moveDuration = 2f;

    private bool solved = false;

    private void Awake()
    {
        // Permet d’y accéder facilement depuis d’autres scripts
        Instance = this;
    }

    public static PuzzleManager Instance { get; private set; }

    public void CheckAllObjects()
    {
        if (solved) return;

        foreach (RotatingObject obj in rotatingObjects)
        {
            if (!obj.isCorrect)
                return; // un objet n’est pas bien placé
        }

        // Tous corrects !
        solved = true;
        StartCoroutine(MoveDoorSmoothly());
        Debug.Log("✅ Puzzle résolu !");
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
            yield return null; // attend la frame suivante
        }

        objectToMove.position = endPos;
    }
}