using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cinecamera;
    
    [Header("Stat")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnspeed = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 1;

    private float verticalVelocity; 
    
    [Header("Input")]
    private float turnInput;
    private float moveInput;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        InputManager();
        Mouvement();
    }

    void Mouvement()
    {
        GroundMovement();
        Turn();
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput)> 0 )
        {
        Vector3 currentLook = cinecamera.forward;
        currentLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(currentLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnspeed);
    }
}
    void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = transform.TransformDirection(move);
        move *= speed;
        move.y = VerticalForceCalculation();
        controller.Move (move*Time.deltaTime);
    }
    void InputManager()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * gravity*2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }
}
