using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathZone : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
        {
                // Vérifie si c’est le joueur
                if (other.CompareTag("Player"))
                {
                        // Si ton joueur a un script PlayerController, tu peux appeler une méthode Ded
                        PlayerController player = other.GetComponent<PlayerController>();
                        if (player != null)
                        {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        }
                        else
                        {
                                // Sinon, tu peux simplement désactiver le joueur
                                other.gameObject.SetActive(false);
                        }
                }
        }
}