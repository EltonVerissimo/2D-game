using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float jumpForce = 2;

    private float _currentSpeed;

    [Header("Check ground setup")]

    public float groundCheckDistance = 1.0f;
    public LayerMask groundLayer;

    private bool isGrounded;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float landScaleY = .7f;
    public float landpScaleX = 1.5f;
    public float animationDuration = .3f;
    public Ease easeOutback = Ease.OutBack;

    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer))
            handleLandScale();

        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        _currentSpeed = (Input.GetKey(KeyCode.LeftControl)) ? speedRun : speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody.velocity = new Vector2(-_currentSpeed, playerRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody.velocity = new Vector2(_currentSpeed, playerRigidbody.velocity.y);
        }

        if (playerRigidbody.velocity.x > 0)
        {
            playerRigidbody.velocity -= friction;
        }
        else if (playerRigidbody.velocity.x < 0)
        {
            playerRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (isGrounded)
            {
                playerRigidbody.velocity = Vector2.up * jumpForce;
                playerRigidbody.transform.localScale = Vector2.one;
                DOTween.Kill(playerRigidbody.transform);
                handleScaleJump();
            }

        }
    }

    private void handleScaleJump()
    {
        playerRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
        playerRigidbody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
    }

    private void handleLandScale()
    {
        playerRigidbody.transform.DOScaleY(landScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
        playerRigidbody.transform.DOScaleX(landpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
    }
}
