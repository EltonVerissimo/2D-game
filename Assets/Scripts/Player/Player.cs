using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public HealthBase healthBase;
    public ItemManager itemManager;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float jumpForce = 2;

    private float _currentSpeed;

    [Header("Check ground setup")]

    public float groundCheckDistance = .05f;
    public LayerMask groundLayer;
    [SerializeField]

    private bool isGrounded;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float landScaleY = .7f;
    public float landpScaleX = 1.5f;
    public float animationDuration = .1f;
    public Ease easeOutback = Ease.OutBack;
    [SerializeField]
    private bool leftFlip = false;

    [Header("Animation player")]
    public string boolRun = "Run";
    public string boolSprint = "Sprint";
    public string triggerDeath = "Death";
    public Animator animator;
    public float timeStamp = 0.8f;
    public float coolDownPeriodInSeconds = 0.8f;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.onKill += OnPlayerKill;
        }

        if (itemManager != null)
        {
            itemManager.onAddCoins += OnAddCoins;
        }
    }

    private void OnAddCoins()
    {
        itemManager.onAddCoins -= OnAddCoins;
    }

    private void OnPlayerKill()
    {
        healthBase.onKill -= OnPlayerKill;

        animator.SetTrigger(triggerDeath);
    }

    void Update()
    {
        // if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer) && !isGrounded)
        // {
        //     if (isGrounded == false)
        //         animator.SetTrigger("Land");
        //     handleLandScale();
        //     isGrounded = true;
        // }

        if (timeStamp <= Time.time)
        {
            HandleJump();
        }
        HandleMovement();
    }

    void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer) && !isGrounded)
        {
            if (isGrounded == false)
            {
                DOTween.Kill(playerRigidbody.transform);
                animator.SetTrigger("Land");
                handleLandScale();
                isGrounded = true;
            }
        }
    }

    private void HandleMovement()
    {
        // _currentSpeed = (Input.GetKey(KeyCode.LeftControl)) ? speedRun : speed;

        if ((Input.GetKey(KeyCode.LeftControl)))
        {
            _currentSpeed = speedRun;
            animator.SetBool(boolSprint, true);
        }
        else
        {
            _currentSpeed = speed;
            animator.SetBool(boolSprint, false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftFlip = true;
            playerRigidbody.velocity = new Vector2(-_currentSpeed, playerRigidbody.velocity.y);
            if (playerRigidbody.transform.localScale.x != -1)
            {
                playerRigidbody.transform.DOScaleX(-1, .1f);
            }
            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            leftFlip = false;
            playerRigidbody.velocity = new Vector2(_currentSpeed, playerRigidbody.velocity.y);
            if (playerRigidbody.transform.localScale.x != 1)
            {
                playerRigidbody.transform.DOScaleX(1, .1f);
            }
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
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
            // if (isGrounded)
            // {
            //     animator.SetTrigger("Jump");
            //     isGrounded = false;
            //     timeStamp = Time.time + coolDownPeriodInSeconds;
            //     playerRigidbody.velocity = Vector2.up * jumpForce;
            //     playerRigidbody.transform.localScale = new Vector2(leftFlip ? -1 : 1, 1);
            //     DOTween.Kill(playerRigidbody.transform);
            //     handleScaleJump();
            // }
            animator.SetTrigger("Jump");
            isGrounded = false;
            timeStamp = Time.time + coolDownPeriodInSeconds;
            playerRigidbody.velocity = Vector2.up * jumpForce;
            playerRigidbody.transform.localScale = new Vector2(leftFlip ? -1 : 1, 1);
            DOTween.Kill(playerRigidbody.transform);
            handleScaleJump();
        }
    }

    private void handleScaleJump()
    {
        if (leftFlip)
        {
            playerRigidbody.transform.DOScaleX(-jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
        }
        else
        {

            playerRigidbody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
        }
        playerRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
    }

    private void handleLandScale()
    {
        if (leftFlip)
        {
            playerRigidbody.transform.DOScaleX(-landpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
        }
        else
        {
            playerRigidbody.transform.DOScaleX(landpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
        }
        playerRigidbody.transform.DOScaleY(landScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(easeOutback);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
