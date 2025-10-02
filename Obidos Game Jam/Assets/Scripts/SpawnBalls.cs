using UnityEngine;
using System.Collections;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] Ball BallPrefab;
    [SerializeField] Transform P1BallSpawn, P2BallSpawn;

    [SerializeField] private float normalBallSpeed = 10f;
    [SerializeField] private float slowBallSpeed = 5f;
    [SerializeField] private float slowDuration = 5.0f;

    private float currentSpeedP1, currentSpeedP2;
    private Coroutine resetP1Coroutine, resetP2Coroutine;

    public void BallSpawn()
    {
        currentSpeedP1 = normalBallSpeed;
        Ball p1Ball = Instantiate(BallPrefab, P1BallSpawn.position, Quaternion.identity);
        p1Ball.gameObject.tag = "Player 1";
        p1Ball.SetSpeed(normalBallSpeed);
        p1Ball.LaunchBall();

        currentSpeedP2 = normalBallSpeed;
        Ball p2Ball = Instantiate(BallPrefab, P2BallSpawn.position, Quaternion.identity);
        p2Ball.gameObject.tag = "Player 2";
        p2Ball.SetSpeed(normalBallSpeed);
        p2Ball.LaunchBall();
    }

    public float GetCurrentSpeedP1()
    {
        return currentSpeedP1;
    }

    public float GetCurrentSpeedP2()
    {
        return currentSpeedP2;
    }

    public void SlowBalls(string playerToAffect)
    {
        if (playerToAffect == "Player 1") currentSpeedP1 = slowBallSpeed;
        else if (playerToAffect == "Player 2") currentSpeedP2 = slowBallSpeed;

        foreach (Ball ball in FindObjectsByType<Ball>(FindObjectsSortMode.None))
        {
            if (ball.tag == playerToAffect)
            {
                ball.SetSpeed(slowBallSpeed);
            }
        }

        if (playerToAffect == "Player 1") 
        {
            if (resetP1Coroutine != null) StopCoroutine(resetP1Coroutine);
            resetP1Coroutine = StartCoroutine("ResetSpeedP1");
        }
        else if (playerToAffect == "Player 2")
        {
            if (resetP2Coroutine != null) StopCoroutine(resetP2Coroutine);
            resetP2Coroutine = StartCoroutine("ResetSpeedP2");
        }
    }

    IEnumerator ResetSpeedP1()
    {
        yield return new WaitForSeconds(slowDuration);

        currentSpeedP1 = normalBallSpeed;
        foreach (Ball ball in FindObjectsByType<Ball>(FindObjectsSortMode.None))
        {
            if (ball.tag == "Player 1")
            {
                ball.SetSpeed(normalBallSpeed);
            }
        }
    }

    IEnumerator ResetSpeedP2()
    {
        yield return new WaitForSeconds(slowDuration);

        currentSpeedP2 = normalBallSpeed;
        foreach (Ball ball in FindObjectsByType<Ball>(FindObjectsSortMode.None))
        {
            if (ball.tag == "Player 2")
            {
                ball.SetSpeed(normalBallSpeed);
            }
        }
    }
}
