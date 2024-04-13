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
    private Transform playerTransform;
    [SerializeField]    [Range(5.0f, 100.0f)]   private float speed;

    void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerTransform = GetComponent<Transform>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveVector = new Vector2(Input.GetAxisRaw(lateralInputAxis), 0);
        playerTransform.Translate(moveVector * speed * Time.deltaTime, Space.World);
        if (moveVector != Vector2.zero)
        {
            Debug.Log("moving");
        }        
    }
}
