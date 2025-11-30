using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationStep = 90f;
    public Vector3 correctRotation;
    public float rotationTolerance = 1f;

    private bool playerInside = false;
    public bool isCorrect = false;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            RotateObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }

    void RotateObject()
    {
        transform.Rotate(Vector3.up, rotationStep);

        Vector3 currentRot = transform.eulerAngles;
        bool xOk = Mathf.Abs(Mathf.DeltaAngle(currentRot.x, correctRotation.x)) < rotationTolerance;
        bool yOk = Mathf.Abs(Mathf.DeltaAngle(currentRot.y, correctRotation.y)) < rotationTolerance;
        bool zOk = Mathf.Abs(Mathf.DeltaAngle(currentRot.z, correctRotation.z)) < rotationTolerance;

        isCorrect = xOk && yOk && zOk;

        PuzzleManager.Instance?.CheckAllObjects();
    }
}