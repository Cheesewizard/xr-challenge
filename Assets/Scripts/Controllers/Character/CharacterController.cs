using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpForce = 15f;
    public float turnSpeed = 2f;
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

             transform.Translate(position *= movementSpeed * Time.deltaTime, Space.World);
            //rb.MovePosition(transform.position + position * Time.deltaTime * movementSpeed);

            //Quaternion newDir = Quaternion.LookRotation(position);
           // Quaternion.Slerp(transform.rotation, newDir, Time.deltaTime * turnSpeed);
        }

        var velocityX = Vector3.Dot(position.normalized, transform.forward);
        var velocityZ = Vector3.Dot(position.normalized, transform.right);

        // Controls the animation blend tree
        AnimatorEventManager.Instance.SetMoveSpeed(new Vector3(velocityX, 0, velocityZ));
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


 
    // Check if the character is on the ground
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Floor"))
        {
            // Reset Jump State
            isOnGround = true;
            AnimatorEventManager.Instance.PlayerTouchGround(true);
            AnimatorEventManager.Instance.PlayerJump(false);
        }
    }

    private void Jumping()
    {
        var jumpPressed = Input.GetKey(KeyCode.Space);

        // Check if jumping
        if (jumpPressed && isOnGround)
        {
  
            AnimatorEventManager.Instance.PlayerJump(true);
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isOnGround = false;
            AnimatorEventManager.Instance.PlayerTouchGround(false);
        }
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

