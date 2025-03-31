using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SO_playerSetup SO_PlayerSetup;

    [SerializeField]
    private bool isGrounded;
    private bool leftFlip = false;


    public ItemManager itemManager;

    private float _currentSpeed;
    private Animator _currentPlayer;

    //public Animator animator;
    public float timeStamp = 0.8f;
    public float coolDownPeriodInSeconds = 0.8f;

    private void Awake()
    {
        _currentPlayer = Instantiate(SO_PlayerSetup.player, transform);

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

        _currentPlayer.SetTrigger(SO_PlayerSetup.triggerDeath);
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
        if (isGrounded)
        {
            DOTween.Kill(playerRigidbody.transform);
        }
        HandleMovement();
    }

    void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, SO_PlayerSetup.groundCheckDistance, SO_PlayerSetup.groundLayer) && !isGrounded)
        {
            if (isGrounded == false)
            {
                _currentPlayer.SetTrigger("Land");
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
            _currentSpeed = SO_PlayerSetup.speedRun;
            _currentPlayer.SetBool(SO_PlayerSetup.boolSprint, true);
        }
        else
        {
            _currentSpeed = SO_PlayerSetup.speed;
            _currentPlayer.SetBool(SO_PlayerSetup.boolSprint, false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftFlip = true;
            playerRigidbody.velocity = new Vector2(-_currentSpeed, playerRigidbody.velocity.y);
            if (playerRigidbody.transform.localScale.x != -1)
            {
                playerRigidbody.transform.DOScaleX(-1, .1f);
            }
            _currentPlayer.SetBool(SO_PlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            leftFlip = false;
            playerRigidbody.velocity = new Vector2(_currentSpeed, playerRigidbody.velocity.y);
            if (playerRigidbody.transform.localScale.x != 1)
            {
                playerRigidbody.transform.DOScaleX(1, .1f);
            }
            _currentPlayer.SetBool(SO_PlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(SO_PlayerSetup.boolRun, false);
        }

        if (playerRigidbody.velocity.x > 0)
        {
            playerRigidbody.velocity -= SO_PlayerSetup.friction;
        }
        else if (playerRigidbody.velocity.x < 0)
        {
            playerRigidbody.velocity += SO_PlayerSetup.friction;
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
            _currentPlayer.SetTrigger("Jump");
            isGrounded = false;
            timeStamp = Time.time + coolDownPeriodInSeconds;
            playerRigidbody.velocity = Vector2.up * SO_PlayerSetup.jumpForce;
            playerRigidbody.transform.localScale = new Vector2(leftFlip ? -1 : 1, 1);
            handleScaleJump();
        }
    }

    private void handleScaleJump()
    {
        if (leftFlip)
        {
            playerRigidbody.transform.DOScaleX(-SO_PlayerSetup.jumpScaleX, SO_PlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SO_PlayerSetup.easeOutback);
        }
        else
        {

            playerRigidbody.transform.DOScaleX(SO_PlayerSetup.jumpScaleX, SO_PlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SO_PlayerSetup.easeOutback);
        }
        playerRigidbody.transform.DOScaleY(SO_PlayerSetup.jumpScaleY, SO_PlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SO_PlayerSetup.easeOutback);
    }

    private void handleLandScale()
    {
        if (leftFlip)
        {
            playerRigidbody.transform.DOScaleX(-SO_PlayerSetup.landpScaleX, SO_PlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SO_PlayerSetup.easeOutback);
        }
        else
        {
            playerRigidbody.transform.DOScaleX(SO_PlayerSetup.landpScaleX, SO_PlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SO_PlayerSetup.easeOutback);
        }
        playerRigidbody.transform.DOScaleY(SO_PlayerSetup.landScaleY,SO_PlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SO_PlayerSetup.easeOutback);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
