using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpForce = 4f;
    private Rigidbody rb;

    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Jumping();
        Running();
        Walking();
        MoveCharacter();
        CheckGroundCollision();
    }

    private void MoveCharacter()
    {
        var xInpput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");
        var position = new Vector3(xInpput, 0, yInput);
        var runPressed = Input.GetKey(KeyCode.LeftShift);

        // Get the movemnt speed depeding on if we are walking or not.
        var movementSpeed = runPressed ? runSpeed : walkSpeed;

        // Character is moving
        if (position.magnitude > 0)
        {
            position.Normalize();

            // transform.Translate(position * Time.deltaTime * movementSpeed);
            rb.MovePosition(transform.position + position * Time.deltaTime * movementSpeed);
        }

        // Controls the animation blend tree
        AnimatorEventManager.Instance.SetMoveSpeed(new Vector2(xInpput, yInput));
    }

    private void Walking()
    {
        var runPressed = Input.GetKey(KeyCode.LeftShift);

        if (!runPressed)
        {
            AnimatorEventManager.Instance.PlayerRun(false);
            AnimatorEventManager.Instance.PlayerWalk(true);
            return;
        }

        AnimatorEventManager.Instance.PlayerWalk(false);
    }

    private void CheckGroundCollision()
    {
        if (!isOnGround)
        {
            if (transform.position.y <= 0 )
            {
                isOnGround = true;
            }
        }
    }

    private void Jumping()
    {
        var jumpPressed = Input.GetKeyDown(KeyCode.Space);

        // Check if jumping
        if (jumpPressed && isOnGround)
        {
            AnimatorEventManager.Instance.PlayerJump(true);
            AnimatorEventManager.Instance.PlayerRun(false);
            AnimatorEventManager.Instance.PlayerWalk(false);
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isOnGround = false;
            return;
        }

        AnimatorEventManager.Instance.PlayerJump(false);
    }

    private void Running()
    {
        var runPressed = Input.GetKey(KeyCode.LeftShift);

        // Check if running
        if (runPressed)
        {
            AnimatorEventManager.Instance.PlayerRun(true);
            AnimatorEventManager.Instance.PlayerWalk(false);
        }
    }
}

