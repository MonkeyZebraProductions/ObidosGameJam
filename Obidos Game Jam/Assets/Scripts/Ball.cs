using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float AxisRatio = 5;
    [SerializeField] private float AngleSarpness = 0.3f;
    [SerializeField] private bool tripleBall = false;
    [SerializeField] private string HitSoundName, ExplosionName;
    private float currentSpeed, minBallSpeed, maxBallSpeed;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
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

    public void LaunchBall()
    {
        rb2D.AddForce(Random.insideUnitCircle.normalized * currentSpeed, ForceMode2D.Impulse);
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

        if (Mathf.Abs(rb2D.linearVelocity.x) < AngleSarpness * currentSpeed)
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
                rb2D.linearVelocity = new Vector2(xVelocity, hitNormal.y*AxisRatio).normalized * currentSpeed;

            }
        }

        if (Mathf.Abs(rb2D.linearVelocity.y) < AngleSarpness * currentSpeed)
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
                rb2D.linearVelocity = new Vector2(hitNormal.x*AxisRatio , yVelocity).normalized * currentSpeed;
            }
        }
        if(audioManager != null)
        {
            if(!audioManager.IsPlaying(HitSoundName))
            {
                audioManager.Play(HitSoundName);
            }
            else if (!audioManager.IsPlaying(HitSoundName + "1"))
            {
                audioManager.Play(HitSoundName + "1");
            }
            else if (!audioManager.IsPlaying(HitSoundName + "2"))
            {
                audioManager.Play(HitSoundName + "2");
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
