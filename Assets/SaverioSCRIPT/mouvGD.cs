using UnityEngine;

public class mouvGD : MonoBehaviour
{
    public Transform centerPoint; // Point autour duquel l'objet tourne
    public float radius = 5f;     // Rayon du cercle
    public float speed = 2f;      // Vitesse de rotation

    private float angle = 0f;

    void Update()
    {
        if (centerPoint == null) return;

        // Incrémenter l'angle selon la vitesse et le temps
        angle += speed * Time.deltaTime;

        // Calculer la position x et z sur le cercle
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Appliquer la position relative au centre
        transform.position = centerPoint.position + new Vector3(x, 0, z);
    }
}
