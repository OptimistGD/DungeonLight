using JetBrains.Annotations;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
        [SerializeField] private float interactDistance = 3f; // distance max pour activer un levier
        
        private Camera playerCamera;

        private void Start()
        {
                playerCamera = Camera.main;
        }
        

        [UsedImplicitly]
        public void TryInteract()
        {
                // Lance un rayon devant le joueur
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance))
                {
                        // Vérifie si l’objet a un script Levier
                        Levier levier = hit.collider.GetComponent<Levier>();
                        if (levier != null)
                        {
                                levier.Interacting();
                        }
                }
        }
}