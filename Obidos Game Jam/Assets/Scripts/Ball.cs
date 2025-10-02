using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float BallSpeed = 10;
    [SerializeField] private float SlowBallSpeed = 5;
    [SerializeField] private float slowDuration = 5.0f;
    [SerializeField] private float AxisRatio = 5;
    [SerializeField] private bool tripleBall = false;
    private float currentSpeed, minBallSpeed, maxBallSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D.AddForce(Random.insideUnitCircle.normalized * BallSpeed, ForceMode2D.Impulse);
        SetSpeed(BallSpeed);
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
        minBallSpeed = currentSpeed - 1.0f;
        maxBallSpeed = currentSpeed + 2.0f;
        rb2D.linearVelocity = rb2D.linearVelocity.normalized * currentSpeed;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void SlowBall()
    {
        StopAllCoroutines();
        SetSpeed(SlowBallSpeed);
        StartCoroutine(ResetBallSpeed());
    }

    IEnumerator ResetBallSpeed()
    {
        yield return new WaitForSeconds(slowDuration);
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        SetSpeed(BallSpeed);

        if (!tripleBall)
        {
            foreach(Ball ball in FindObjectsByType<Ball>(FindObjectsSortMode.None))
            {
                if (ball != this && ball.tripleBall && ball.tag == this.tag)
                {
                    ball.ResetSpeed();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb2D.linearVelocity.magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb2D.linearVelocity.magnitude < minBallSpeed)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * minBallSpeed;
        }

        if (rb2D.linearVelocity.magnitude > maxBallSpeed)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * maxBallSpeed;
        }

        if (Mathf.Abs(rb2D.linearVelocity.x) < 0.1f * BallSpeed)
        {
            float xVelocity;
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                Vector2 hitNormal = contactPoint.normal;
                Debug.DrawLine(contactPoint.point, contactPoint.point + hitNormal * 2, Color.blue, 1.0f);

                if (rb2D.linearVelocity.x < 0.0f)
                {
                    xVelocity = -1.0f;
                }
                else
                {
                    xVelocity = 1.0f;
                }
                rb2D.linearVelocity = new Vector2(xVelocity, hitNormal.y*AxisRatio).normalized * BallSpeed;

            }
        }

        if (Mathf.Abs(rb2D.linearVelocity.y) < 0.1f * BallSpeed)
        {
            float yVelocity;
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                Vector2 hitNormal = contactPoint.normal;
                Debug.DrawLine(contactPoint.point, contactPoint.point + hitNormal * 2, Color.red, 1.0f);
                if(rb2D.linearVelocity.y<0.0f)
                {
                    yVelocity = -1.0f;
                }
                else
                {
                    yVelocity = 1.0f;
                }
                rb2D.linearVelocity = new Vector2(hitNormal.x*AxisRatio , yVelocity).normalized * BallSpeed;
            }
        }
    }

    public bool IsTripleBall()
    {
        return tripleBall;
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }
}
