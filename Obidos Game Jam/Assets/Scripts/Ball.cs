using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float BallSpeed = 10;
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
        Debug.Log(rb2D.linearVelocity.magnitude);
        if(rb2D.linearVelocity.magnitude<minBallSpeed)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * minBallSpeed;
        }

        if (rb2D.linearVelocity.magnitude > maxBallSpeed)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * maxBallSpeed;
        }
    }
}
