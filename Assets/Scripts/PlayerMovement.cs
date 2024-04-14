using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    public Animator animator;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);

        if (moveHorizontal == 0)
        {
            animator.SetInteger("horizontalInt", 0);
        }
        if (moveHorizontal < 0)
        {
            animator.SetInteger("horizontalInt", -1);
        }
        if (moveHorizontal > 0)
        {
            animator.SetInteger("horizontalInt", 1);
        }

        if (moveVertical == 0)
        {
            animator.SetInteger("verticalInt", 0);
        }
        if (moveVertical < 0)
        {
            animator.SetInteger("verticalInt", -1);
        }
        if (moveVertical > 0)
        {
            animator.SetInteger("verticalInt", 1);
        }
    }
}
