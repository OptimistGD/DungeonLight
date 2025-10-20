using UnityEngine;

public class MovUP : MonoBehaviour
{
    public float moveSpeed = 2f;        // Vitesse du mouvement
    public float moveHeight = 3f;       // Distance de montée/descente
    public bool startGoingUp = true;    // Sens initial

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool goingUp;

    void Start()
    {
        startPos = transform.position;
        goingUp = startGoingUp;
        targetPos = startPos + (goingUp ? Vector3.up * moveHeight : Vector3.down * moveHeight);
    }

    void Update()
    {
        // Déplacement vers la cible
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Quand la plateforme atteint la cible → on inverse la direction
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            goingUp = !goingUp;
            targetPos = startPos + (goingUp ? Vector3.up * moveHeight : Vector3.down * moveHeight);
        }
    }
}
