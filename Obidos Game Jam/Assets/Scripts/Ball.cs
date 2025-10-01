using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float BallSpeed = 10;
    [SerializeField] private float AxisRatio = 5;
    private float minBallSpeed,maxBallSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(Random.insideUnitCircle.normalized * BallSpeed,ForceMode2D.Impulse);
        minBallSpeed = BallSpeed - 1.0f;
        maxBallSpeed = BallSpeed + 2.0f;
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
                rb2D.linearVelocity = new Vector2(hitNormal.x *AxisRatio , yVelocity).normalized * BallSpeed;
            }
        }
    }
}
