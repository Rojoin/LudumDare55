using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent (typeof (BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public LayerMask collisionLayer; // player collides with these: Obstacles, Level
    private Vector2 moveVector;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRB;
    private string lateralInputAxis = "Horizontal";
    [SerializeField][Range(5.0f, 100.0f)] private float speed;
    float x;
    [SerializeField] private Animator animator;
    public bool isLookingRight = true;
    [SerializeField] private Transform pivot;

    void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
        moveVector = new Vector2(x * speed * Time.deltaTime, 0);
        // playerRB.MovePosition(playerRB.position + moveVector * Time.fixedDeltaTime);
        playerRB.AddForce(moveVector, ForceMode2D.Impulse);

        if (moveVector != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            Debug.Log("moving");
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void SetTiredness(bool isTired)
    {
        animator.SetBool("isTired", isTired);
    }

    public void FlipHorizontal()
    {
        if (isLookingRight)
            isLookingRight = false;
        else
            isLookingRight = true;

        pivot.Rotate(0, 180, 0);
    }
}
