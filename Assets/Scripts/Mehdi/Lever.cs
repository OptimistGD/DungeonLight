using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Lever : MonoBehaviour
{
    [FormerlySerializedAs("LeverManager")] [Header("ajouter le LeverManager au quel on souhaite\nassigner ce levier")][SerializeField]
    private LeverManager leverManager;
    [Header("ajouter la touche pour activer ce levier")][SerializeField]
    public KeyCode interactingKey = KeyCode.E;
    private bool state;
    private bool isInRange;

    void Awake()
    {
        if (!leverManager.LeverList.Contains(this))
        {
            leverManager.LeverList.Add(this);
        }
    }

    void Start()
    {
        if (leverManager.overwriteKeyCode != KeyCode.None)
        {
            interactingKey = leverManager.overwriteKeyCode;
        }
    } 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(interactingKey) && isInRange)
        {
            SwitchLever();
        }
    }

    private void SwitchLever()
    {
        state = !state;
        leverManager.LeverSwitched(state);
    }
}