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
    [SerializeField] private GameObject root;
    public bool isInRange { get; private set; }
    
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

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactingKey))
            {
                SwitchLever();
            } 
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
            if (other.gameObject.CompareTag("Player"))
            {
                isInRange = false;
            }
        }

    

    private void SwitchLever()
    {
        state = !state;
        /*var x = 0f;
        var y = 90f;
        var z = -90f;*/
        
        switch (state)
        {
            case false:
                root.transform.eulerAngles = new Vector3(0, root.transform.eulerAngles.y, root.transform.eulerAngles.z);
                break;
            case true:
                root.transform.eulerAngles = new Vector3(180, root.transform.eulerAngles.y, root.transform.eulerAngles.z);
                break;
        }
        leverManager.LeverSwitched(state);
    }
}