using System;
using UnityEngine;


public class Levier : MonoBehaviour
{
    public event Action OnLevierChange;
    public bool isActive = false;
    
    public void Interacting()
    {
            if (isActive)
                    return;

            isActive = true;
            transform.localRotation = Quaternion.Euler(isActive ? 45f : -45f, 0f, 0f);

            OnLevierChange?.Invoke();
    }
}
