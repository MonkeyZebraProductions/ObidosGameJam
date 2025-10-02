using UnityEngine;

public class BackWall : MonoBehaviour
{
    private PressureLine pressureLine;
    [SerializeField] private bool moveLeft;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressureLine = FindFirstObjectByType<PressureLine>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            if (pressureLine != null && !ball.IsTripleBall()) 
            {
                pressureLine.SetMovePositions(moveLeft ? -1 : 1);
            }
            else if (pressureLine != null && ball.IsTripleBall())
            {
                ball.DestroyBall();
            }
        }
    }
}
