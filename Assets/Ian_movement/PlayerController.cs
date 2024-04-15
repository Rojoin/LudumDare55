using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public LayerMask collisionLayer; // player collides with these: Obstacles, Level
    private Vector2 moveVector;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRB;
    private string lateralInputAxis = "Horizontal";
    [SerializeField] [Range(5.0f, 100.0f)] private float speed;
    private float speedTired = 1.0f;
    [Range(0.0f, 1.0f)] public float tiredSpeedMultipliyer = 0.3f;
    [SerializeField] private SoundSO[] stepsSounds;
    float x;
    bool movesWithButtons = false;
    [SerializeField] private Animator animator;
    public bool isLookingRight = true;
    [SerializeField] private Transform pivot;
    [Range(0.0f, 3.0f)] public float timeBetweenSteps = 0.1f;
    private float timerSteps;

    void Awake()
    {

    }

    private void OnEnable()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        SetPlayerStateRB();
    }

    private void OnDisable()
    {
        SetPlayerStateRB(false);
    }

    void SetPlayerStateRB(bool state = true)
    {
        playerCollider.enabled = state;
        playerRB.bodyType = state ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (!movesWithButtons)
            x = Input.GetAxis(lateralInputAxis);


        if ((x < 0 && isLookingRight) || (x > 0 && !isLookingRight))
            FlipHorizontal();

        if (Input.GetKeyDown(KeyCode.B))
            SetTiredness(true);

        if (Input.GetKeyDown(KeyCode.V))
            SetTiredness(false);
    }

    private void FixedUpdate()
    {
        moveVector = new Vector2(x * speed * speedTired * Time.deltaTime, 0);
        // playerRB.MovePosition(playerRB.position + moveVector * Time.fixedDeltaTime);
        playerRB.AddForce(moveVector, ForceMode2D.Impulse);

        if (moveVector != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            Debug.Log("moving");
            timerSteps += Time.fixedDeltaTime;
            if (timerSteps >= timeBetweenSteps)
            {
                stepsSounds[Random.Range(0, stepsSounds.Length)].PlaySound();
                timerSteps -= timeBetweenSteps;
            }
        }
        else
        {
            timerSteps = timeBetweenSteps;
            animator.SetBool("isWalking", false);
        }
    }

    public void SetTiredness(bool isTired)
    {
        animator.SetBool("isTired", isTired);
        speedTired = isTired ? tiredSpeedMultipliyer : 1.0f;
    }

    public void FlipHorizontal()
    {
        if (isLookingRight)
            isLookingRight = false;
        else
            isLookingRight = true;

        pivot.Rotate(0, 180, 0);
    }

    public void ButtonInput(int axisValue)
    {
        movesWithButtons = true;
        x = axisValue;
    }
}