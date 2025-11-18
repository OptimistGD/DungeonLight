using System;
using UnityEngine;


public class Levier : MonoBehaviour
{
        public float L;
        public float J;
        public float K;
    public event Action OnLevierChange;
    public bool isActive = false;
    
    public void Interacting()
    {
            if (isActive)
                    return;

            isActive = true;
            transform.localRotation = Quaternion.Euler(  J, -45f, -45f);

            OnLevierChange?.Invoke();
    }
}
