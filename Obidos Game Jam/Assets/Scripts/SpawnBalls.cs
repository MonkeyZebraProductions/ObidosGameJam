using UnityEngine;

public class SpawnBalls : MonoBehaviour
{

    [SerializeField] Ball BallPrefab;
    [SerializeField] Transform P1BallSpawn,P2BallSpawn;
    public void BallSpawn()
    {
        Ball p1Ball = Instantiate(BallPrefab,P1BallSpawn);
        p1Ball.LaunchBall();

        Ball p2Ball = Instantiate(BallPrefab, P2BallSpawn);
        p2Ball.LaunchBall();
    }
}
