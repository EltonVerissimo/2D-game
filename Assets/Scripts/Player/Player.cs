using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float jumpForce = 2;

    private float _currentSpeed;

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        _currentSpeed = (Input.GetKey(KeyCode.LeftControl)) ? speedRun: speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody.velocity = new Vector2(-_currentSpeed, playerRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody.velocity = new Vector2(_currentSpeed, playerRigidbody.velocity.y);
        }

        if(playerRigidbody.velocity.x > 0)
        {
            playerRigidbody.velocity -= friction;
        }
        else if(playerRigidbody.velocity.x < 0)
        {
            playerRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            playerRigidbody.velocity = Vector2.up * jumpForce;
    }
}
