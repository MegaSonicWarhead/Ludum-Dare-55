using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    public LayerMask interactableMask; // Layer mask for interactable objects
    public float interactDistance = 1f; // Maximum distance for interacting with objects

    private Rigidbody2D rb; // Player's rigidbody
    private Vector2 movement; // Movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement vector
        movement = movement.normalized;

        // Interact with objects
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Interact()
    {
        // Raycast to detect interactable objects
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, interactDistance, interactableMask);

        // Check if an object was hit
        if (hit.collider != null)
        {
            // Get the interactable object
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            // Check if the object is interactable
            if (interactable != null)
            {
                // Interact with the object
                interactable.Interact();
            }
        }
    }
}
