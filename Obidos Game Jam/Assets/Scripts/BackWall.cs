using System.Collections;
using UnityEngine;

public class BackWall : MonoBehaviour
{
    private PressureLine pressureLine;
    [SerializeField] private bool moveLeft;
    [SerializeField] private Ball BallPrefab;
    [SerializeField] private Transform BallSpawn;
    [SerializeField] private float StartRespawnTime = 1.0f;
    [SerializeField] private float RespawnIncrease = 0.5f;

    private float currentRespawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressureLine = FindFirstObjectByType<PressureLine>();
        currentRespawnTime = StartRespawnTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            if (pressureLine != null && !ball.IsTripleBall()) 
            {
                //pressureLine.SetMovePositions(moveLeft ? -1 : 1);
                StartCoroutine(RespawnBall(ball.gameObject));
                currentRespawnTime += RespawnIncrease;
            }
            else if (pressureLine != null && ball.IsTripleBall())
            {
                ball.DestroyBall();
            }
        }
    }

    IEnumerator RespawnBall(GameObject ball)
    {
        Destroy(ball);
        yield return new WaitForSeconds(currentRespawnTime);
        Ball newBall = Instantiate(BallPrefab, BallSpawn);
        newBall.LaunchBall();
    }
}
