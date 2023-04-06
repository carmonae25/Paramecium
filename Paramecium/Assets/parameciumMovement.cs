using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parameciumMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float angleBound = 90f;
    private Vector2 movementDirection;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = Vector2.right;
    }

    // Need to figure this out
    /*
    private void FixedUpdate() {
    }
    */
}
