using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class BallMovement : MonoBehaviour
{

    private Rigidbody2D ballRB;

    private Vector3 initialBallPosition;
    private Vector3 initialPaddlePosition;

    private GameObject paddle;

    [SerializeField] private float initialBallSpeed = 5.0f;
    [SerializeField] private bool ballLaunched = false;

    private void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
        initialBallPosition = transform.position;
        paddle = GameObject.FindGameObjectWithTag("Player");
        initialPaddlePosition = paddle.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ballLaunched)
        {
            StartGame();
            ballLaunched = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            // Calculate the direction of the bounce based on where the ball hit the paddle
            float hitPointX = collision.contacts[0].point.x;
            float paddleCenterX = collision.gameObject.transform.position.x;
            float relativeHitPoint = (hitPointX - paddleCenterX) / (collision.collider.bounds.size.x / 2);

            // Calculate the bounce direction based on the relative hit point
            Vector2 bounceDirection = new Vector2(relativeHitPoint, 1).normalized;

            // Apply the bounce direction to the ball's velocity
            ballRB.velocity = bounceDirection * ballRB.velocity.magnitude;

            // Rotate the bounce angle based on the paddleBounceAngle
            ballRB.velocity = Quaternion.Euler(0, 0, 45f * -relativeHitPoint) * ballRB.velocity;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            ballRB.velocity = new Vector2(ballRB.velocity.x, ballRB.velocity.y).normalized * initialBallSpeed;
        }

        if(collision.gameObject.CompareTag("Dead"))
        {
            transform.position = initialBallPosition;
            paddle.transform.position = initialPaddlePosition;
            ballRB.velocity = Vector2.zero;
            ballLaunched = false;
        }
    }

    private void StartGame()
    {
        ballRB.velocity = new Vector2 (Random.Range(-1f, 1f), 1).normalized * initialBallSpeed;
    }

}