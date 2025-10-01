using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public float BallSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(Random.insideUnitCircle.normalized * BallSpeed,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
