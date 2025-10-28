using UnityEngine;

public class TorchPickup : MonoBehaviour
{
    public float pickupDistance = 3f; // distance maximale pour ramasser
    private Transform player;

    void Start()
    {
        // cherche le joueur dans la scène
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // clic gauche
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void TryPickup()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= pickupDistance)
        {
            TorchController playerTorch = FindObjectOfType<TorchController>();
            if (playerTorch != null)
            {
                playerTorch.GiveTorch();
            }
            
            Debug.Log("Torche ramassée !");
            Destroy(gameObject); // supprimer l’objet ramassé

        }
    }
}