using System;
using UnityEngine;

public class Levier : MonoBehaviour
{
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float rot;
    public event Action OnLevierChange;
    public bool isActive = false;
    
    public void Interacting()
    {
            if (isActive)
                    return;
            isActive = true;
            transform.rotation *= Quaternion.Euler(0f, rot, 0f); 
            OnLevierChange?.Invoke();
            
            audioSource.Play();
    }
}
