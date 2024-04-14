using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
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
    [SerializeField]    [Range(5.0f, 100.0f)]   private float speed;

    void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        float x = Input.GetAxis(lateralInputAxis);
        moveVector = new Vector2(x * speed, 0);
        playerRB.MovePosition(playerRB.position + moveVector * Time.fixedDeltaTime);

        if (moveVector != Vector2.zero)
        {
            Debug.Log("moving");
        }
    }
}
