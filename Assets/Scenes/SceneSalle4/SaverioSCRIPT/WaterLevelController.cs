using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
  // Hauteur quand tout est activé
    [SerializeField] private float moveSpeed = 2f;       // Vitesse du mouvement de l’eau
    [SerializeField] private GameObject startTarget;    // Vitesse du mouvement de l’eau
    [SerializeField] private GameObject endTarget ;       // Vitesse du mouvement de l’eau

    private float targetHeight;

    private void Start()
    {
        targetHeight = startTarget.transform.position.y;
        Vector3 pos = transform.position;
        pos.y = targetHeight;
        transform.position = pos;
    }

    private void Update()
    {
        // Déplacement fluide vers la hauteur cible
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetHeight, Time.deltaTime * moveSpeed);
        transform.position = pos;
    }

    public void SetWaterLevel(float normalizedValue)
    {
        targetHeight = Mathf.Lerp(startTarget.transform.position.y, endTarget.transform.position.y, Mathf.Clamp01(normalizedValue)
        );
    }
}