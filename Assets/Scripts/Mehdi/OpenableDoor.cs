using System.Collections;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    [SerializeField] private GameObject root;

    private bool state = false; // false = closed, true = open
    public float openingSpeed = 2f;
    public float openAngle = 90f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine doorRoutine;

    void Start()
    {
        closedRotation = root.transform.rotation;
        openRotation = Quaternion.Euler(root.transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void InteractDoor()
    {
        state = !state;
        Quaternion targetRot = state ? openRotation : closedRotation;
        if (doorRoutine != null)
            StopCoroutine(doorRoutine);
        doorRoutine = StartCoroutine(RotateDoor(targetRot));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(root.transform.rotation, targetRotation) > 0.1f)
        {
            root.transform.rotation = Quaternion.Lerp(root.transform.rotation, targetRotation, Time.deltaTime * openingSpeed);
            yield return null;
        }
        root.transform.rotation = targetRotation;
        doorRoutine = null;
    }
}