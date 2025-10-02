using System.Collections;
using UnityEngine;
using TMPro;

public class BackWall : MonoBehaviour
{
    private PressureLine pressureLine;
    [SerializeField] private Ball BallPrefab;
    [SerializeField] private Transform BallSpawn;
    [SerializeField] private string PlayerTag;
    [SerializeField] private float StartRespawnTime = 0.5f;
    [SerializeField] private float RespawnIncrease = 0.5f;
    [SerializeField] private float RespawnTimeCap = 5.0f;
    [SerializeField] private TextMeshProUGUI RespawnText;

    private float currentRespawnTime,displayRespawnTime;
    private bool isCountdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressureLine = FindFirstObjectByType<PressureLine>();
        currentRespawnTime = StartRespawnTime;
        RespawnText.enabled = false;
    }

    private void Update()
    {
        if (isCountdown)
        {
            RespawnText.text = ( displayRespawnTime<1.0f ? "0":"") + displayRespawnTime.ToString("#.00") + "s";
            displayRespawnTime-=Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            if (pressureLine != null && !ball.IsTripleBall()) 
            {
                currentRespawnTime += RespawnIncrease;
                StartCoroutine(RespawnBall(ball.gameObject));
                if (currentRespawnTime < RespawnTimeCap)
                {
                    currentRespawnTime += RespawnIncrease;
                }
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
        RespawnText.enabled = true;
        displayRespawnTime = currentRespawnTime;
        isCountdown = true;
        yield return new WaitForSeconds(currentRespawnTime);
        Ball newBall = Instantiate(BallPrefab, BallSpawn.position,Quaternion.identity);
        newBall.gameObject.tag = PlayerTag;
        if (PlayerTag == "Player 1") newBall.SetSpeed(FindFirstObjectByType<SpawnBalls>().GetCurrentSpeedP1());
        else if (PlayerTag == "Player 2") newBall.SetSpeed(FindFirstObjectByType<SpawnBalls>().GetCurrentSpeedP2());
        newBall.LaunchBall();
        RespawnText.enabled = false;
        isCountdown = false;
    }
}
