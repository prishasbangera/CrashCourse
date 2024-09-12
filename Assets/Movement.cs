using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputAction moveAction;
    private Rigidbody rb;
    private bool canJump;

    private int numCollisions;

    [SerializeField]
    TextMeshPro scoreText;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput inputComponent = this.gameObject.GetComponent<PlayerInput>();
        moveAction = inputComponent.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 movement = moveAction.ReadValue<Vector2>();
        movement = movement.normalized;
        movement *= Time.deltaTime * speed;

        this.gameObject.transform.position += new Vector3(movement.x, 0, movement.y);
        */
    }

    private void FixedUpdate()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        movement = movement.normalized * speed;

        Vector3 oldVelocity = rb.velocity;
        float yVel = oldVelocity.y;

        Vector3 newVelocity = new Vector3(movement.x, yVel, movement.y);
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
        if (collision.gameObject.CompareTag("Platform"))
        {
            canJump = true;
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            numCollisions++;
            scoreText.text = "Collisions " + numCollisions;
        }


    }
}
