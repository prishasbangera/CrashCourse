using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private Rigidbody rb;
    private bool canJump;


    [SerializeField]
    float jumpForce;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput inputComponent = this.gameObject.GetComponent<PlayerInput>();
        moveAction = inputComponent.actions.FindAction("Move");
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 movement = moveAction.ReadValue<Vector2>();
        movement = movement.normalized;
        float horizontalMove = Time.deltaTime * speed * movement.x;
        float verticalMove = Time.deltaTime * speed * movement.y;

        Vector3 oldPos = this.gameObject.transform.position;
        Vector3 newPos = oldPos + new Vector3(horizontalMove, 0, verticalMove);
        this.gameObject.transform.position = newPos;*/
    }

    private void FixedUpdate()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        movement = movement.normalized;
        float horizontalMove = speed * movement.x;
        float verticalMove = speed * movement.y;

        Vector3 oldVelocity = rb.velocity;
        float yVel = oldVelocity.y;

        Vector3 newVelocity = new Vector3(horizontalMove, yVel, verticalMove);
        rb.velocity = newVelocity;
    }

    void OnJump()
    {
        if (canJump)
        {
            Vector3 force = new Vector3(0, jumpForce, 0);
            rb.AddForce(force, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            canJump = true;
        }
    }
}
