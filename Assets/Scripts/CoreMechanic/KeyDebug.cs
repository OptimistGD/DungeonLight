using UnityEngine;

public class KeyDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(k))
                {
                    Debug.Log("Touche détectée : " + k);
                }
            }
        }
    }
}