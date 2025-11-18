using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Lever : MonoBehaviour
{
    [FormerlySerializedAs("LeverManager")] [Header("ajouter le LeverManager au quel on souhaite\nassigner ce levier")][SerializeField]
    public LeverManager leverManager;
    [Header("ajouter la touche pour activer ce levier")][SerializeField]
    public KeyCode interactingKey = KeyCode.E;
    public bool state;
    

    void Start()
    {
        if (!leverManager.leverList.Contains(this))
        {
            leverManager.leverList.Add(this);
        }
        if (leverManager.overwriteKeyCode != KeyCode.None)
        {
            interactingKey = leverManager.overwriteKeyCode;
            Debug.Log(interactingKey.ToString());
        }
    } 
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(interactingKey))
            {
                SwitchLever();
            } 
        }
    }


    private void SwitchLever()
    {
        state = !state;
        leverManager.LeverSwitched(state);
    }
}