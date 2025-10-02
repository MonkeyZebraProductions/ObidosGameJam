using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    //Component Variables
    private Rigidbody2D rb2D;
    private float currentSpeed;

    public float PlayerSpeed = 10f;
    public float SlowSpeed = 5f;
    public float slowDuration = 5.0f;

    private void Awake()
    {
        //Assigns variables at start of Runtime
        playerInput = GetComponent<PlayerInput>();
        rb2D = GetComponent<Rigidbody2D>();
        currentSpeed = PlayerSpeed;
    }

    private void FixedUpdate()
    {
        //Gets the movement action value
        moveAction = playerInput.actions["Move"];
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        rb2D.linearVelocityY = moveInput.y*currentSpeed;
    }

    public void SlowPlayer()
    {
        StopAllCoroutines();
        currentSpeed = SlowSpeed;
        StartCoroutine(ResetPlayerSpeed());
    }

    IEnumerator ResetPlayerSpeed()
    {
        yield return new WaitForSeconds(slowDuration);
        ResetSpeed();
    }

    void ResetSpeed()
    {
        currentSpeed = PlayerSpeed;
    }
}
