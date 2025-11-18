
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationStep = 90f; // angle à chaque rotation
    public Vector3 correctRotation;  // rotation correcte (en Euler)
    public bool isCorrect = false;   // état actuel

    private void Update()
    {
        // Exemple : clic gauche pour tourner
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                RotateObject();
            }
        }
    }

    void RotateObject()
    {
        transform.Rotate(Vector3.up, rotationStep); // tourne sur l’axe Y

        // Vérifie si la rotation correspond à la bonne
        Vector3 currentRot = new Vector3(
            Mathf.Round(transform.eulerAngles.x),
            Mathf.Round(transform.eulerAngles.y),
            Mathf.Round(transform.eulerAngles.z)
        );

        // Tolérance : on arrondit à 1° pour éviter les flottants
        isCorrect = Vector3.Distance(currentRot, correctRotation) < 1f;

        // Notifie le manager
        PuzzleManager.Instance.CheckAllObjects();
    }
}