using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    //Component Variables
    private Rigidbody2D rb2D;

    public float PlayerSpeed = 10;

    private void Awake()
    {
        //Asigns variables at start of Runtime
        playerInput = GetComponent<PlayerInput>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Gets the movement action value
        moveAction = playerInput.actions["Move"];
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        rb2D.linearVelocityY = moveInput.y*PlayerSpeed;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
