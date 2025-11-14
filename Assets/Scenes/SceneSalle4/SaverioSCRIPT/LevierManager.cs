using System;
using UnityEngine;
using UnityEngine.Events;

public class LevierManager : MonoBehaviour
{
        [SerializeField] private Levier[] leviers;
        [SerializeField] private UnityEvent<int, int> OnLevierChange;

        [SerializeField] private WaterLevelController waterController;

        private void OnEnable()
        {
                foreach (Levier levier in leviers)
                {
                        levier.OnLevierChange += CheckLeviers;
                }
        }

        private void OnDisable()
        {
                foreach (Levier levier in leviers)
                {
                        levier.OnLevierChange -= CheckLeviers;
                }
        }

        private void Start()
        {
                CheckLeviers();
        }

        private void CheckLeviers()
        {
                int active = 0;
                foreach (Levier levier in leviers)
                {
                        if (levier.isActive)
                                active++;
                }

                // 🔹 Baisse du niveau de l’eau selon le nombre de leviers activés
                if (waterController != null)
                {
                        float normalized = (float)active / leviers.Length;
                        waterController.SetWaterLevel(normalized);
                }

                OnLevierChange?.Invoke(active, leviers.Length);
        }
}