using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_playerSetup : ScriptableObject
{
    public Animator player;
    public SOString playerName;

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
    public float timeStamp = 0.8f;
    public float coolDownPeriodInSeconds = 0.8f;
}
