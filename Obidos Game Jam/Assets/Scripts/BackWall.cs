using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackWall : MonoBehaviour
{
    private PressureLine pressureLine;
    [SerializeField] private Ball BallPrefab;
    [SerializeField] private Transform BallSpawn;
    [SerializeField] private string PlayerTag;
    [SerializeField] private float StartRespawnTime = 0.5f;
    [SerializeField] private float RespawnIncrease = 0.5f;
    [SerializeField] private float RespawnTimeCap = 5.0f;
    [SerializeField] private Image timerImage;
    [SerializeField] private GameObject directionIndicator;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject respawnEffect;
    [SerializeField] private string HitSoundName = "Explosion";

    private float currentRespawnTime,displayRespawnTime;
    private bool isCountdown;
    private Vector2 respawnDirection;
    private AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressureLine = FindFirstObjectByType<PressureLine>();
        currentRespawnTime = StartRespawnTime;
        audioManager=FindFirstObjectByType<AudioManager>();
    }

    private void Update()
    {
        if (isCountdown)
        {
            timerImage.fillAmount = 1 - displayRespawnTime/currentRespawnTime;
            displayRespawnTime-=Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
            if (audioManager != null)
            {
                if (!audioManager.IsPlaying(HitSoundName))
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
            if (pressureLine != null && !ball.IsTripleBall()) 
            {
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
        respawnDirection = Random.insideUnitCircle.normalized;
        directionIndicator.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(respawnDirection.y, respawnDirection.x) * Mathf.Rad2Deg);
        directionIndicator.SetActive(true);
        timerImage.fillAmount = 1;
        displayRespawnTime = currentRespawnTime;
        isCountdown = true;
        yield return new WaitForSeconds(currentRespawnTime - 0.45f);
        Instantiate(respawnEffect, BallSpawn);
        yield return new WaitForSeconds(0.45f);
        Ball newBall = Instantiate(BallPrefab, BallSpawn.position, Quaternion.identity);
        newBall.gameObject.tag = PlayerTag;
        if (PlayerTag == "Player 1") newBall.SetSpeed(FindFirstObjectByType<SpawnBalls>().GetCurrentSpeedP1());
        else if (PlayerTag == "Player 2") newBall.SetSpeed(FindFirstObjectByType<SpawnBalls>().GetCurrentSpeedP2());
        newBall.LaunchBall(respawnDirection);
        timerImage.fillAmount = 0;
        directionIndicator.SetActive(false);
        isCountdown = false;
    }
}
