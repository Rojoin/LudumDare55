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
    void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        x = Input.GetAxis(lateralInputAxis);
    }

    private void FixedUpdate()
    {
        moveVector = new Vector2(x * speed * Time.deltaTime, 0);
        // playerRB.MovePosition(playerRB.position + moveVector * Time.fixedDeltaTime);
        playerRB.AddForce(moveVector, ForceMode2D.Impulse);

        if (moveVector != Vector2.zero)
        {
            Debug.Log("moving");
        }
    }
}
