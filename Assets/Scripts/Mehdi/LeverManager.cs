using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Serialization;

public class LeverManager : MonoBehaviour
{
    [Header("Ajouter tout les levier à assigner a ce LeverManager")] [SerializeField] public Lever[] LeverList;
    private int leverCounter = 0;
    private int activatedLevers = 0;
    [Header("ajouter la touche pour activer au leviers\nsi laissé vide prend la valeur par defaut (E)\nou celles assigné a chaque levier individuellement")][SerializeField]
    public KeyCode overwriteKeyCode;
    
    
    
    [Header("ajouter la/les fonctions a effectuer\nlorsque tout les levier assigné sont activé")] [SerializeField]
    private UnityEvent functionToCall;
    

    void Start()
    {
        leverCounter = LeverList.Length;
    }

    public void LeverSwitched(bool value)
    {
        if (value)
        {
            activatedLevers += 1;
        }
        else
        {
            activatedLevers -= 1;
        }

        if (activatedLevers == leverCounter)
        {
            ActionToTrigger();
        }
    }
    private void ActionToTrigger()
    {
        functionToCall.Invoke();
    }
}