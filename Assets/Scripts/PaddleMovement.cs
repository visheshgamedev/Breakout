using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    [SerializeField] private float paddleVelocity = 5.0f;

    private Rigidbody2D paddleRB;

    void Start()
    {
        paddleRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalForce = Input.GetAxis("Horizontal");
        float paddleXMovement = horizontalForce * paddleVelocity;
        paddleRB.velocity = new Vector2(paddleXMovement, 0f);
    }

}