using UnityEngine;

public class TorchVisual : MonoBehaviour
{
    [Header("Positions de la torche")]
    public Vector3 offPosition = new Vector3(0.4f, -0.6f, 0.5f); //position eteinte
    public Vector3 onPosition = new Vector3(0.4f, -0.3f, 0.6f); //position allumée

    [Header("Paramètres")] 
    public float moveSpeed = 4f; //vitesse de mouvement de transition

    private bool isVisible = false;

    void Start()
    {
        //commence cachée 
        transform.localPosition = offPosition;
    }

    void Update()
    {
        //interpoler entre la position allumée et éteinte
        Vector3 targetPos = isVisible ? onPosition : offPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
        
    }
    
    public void ShowTorch(bool show)
    {
        isVisible = show;
    }
}    
