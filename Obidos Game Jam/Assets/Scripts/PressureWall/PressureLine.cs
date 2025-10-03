using TMPro;
using UnityEngine;
using System.Collections;

public class PressureLine : MonoBehaviour
{
    [SerializeField] public float moveDistance = 1;
    [SerializeField][Range(0.0f, 1.0f)] private float EaseOutValue;
    [SerializeField] private Canvas EndGameCanvas;
    [SerializeField] private GameObject slowPlayer1;
    [SerializeField] private GameObject slowPlayer2;
    [SerializeField] private float slowDuration = 5.0f;

    private TextMeshProUGUI endGameText;
    private Vector3 targetPosition, startPosition;
    private float alpha = 1.0f;
    private Coroutine resetP1Coroutine, resetP2Coroutine;

    private void Start()
    {
        endGameText = EndGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        EndGameCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (alpha < 1.0f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.Pow(alpha, EaseOutValue));
            alpha += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contactPoint in collision.contacts) 
        { 
            Vector2 hitNormal = contactPoint.normal;
            SetMovePositions(hitNormal.x);
            //transform.position += new Vector3 (hitNormal.x*moveDistance,0,0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DangerZone") 
        {
            EndGameCanvas.enabled = true;
            endGameText.text = "Player " + (transform.position.x > 0 ? "1" : "2") + " Wins!";
            BackWall[] backWalls = FindObjectsByType<BackWall>(FindObjectsSortMode.None);
            foreach (BackWall wall in backWalls)
            {
                Destroy(wall.gameObject);
            }
            Ball[] balls = FindObjectsByType<Ball>(FindObjectsSortMode.None);
            foreach(Ball ball in balls)
            {
                Destroy(ball.gameObject);
            }

        }
        
    }

    public void SetMovePositions(float xMovemnet)
    {
        startPosition = transform.position;
        targetPosition = transform.position + new Vector3(xMovemnet * moveDistance, 0, 0);
        alpha = 0.0f;
    }

    public void SetSlowPlayer1()
    {
        if (resetP1Coroutine != null) StopCoroutine(resetP1Coroutine);
        resetP1Coroutine = StartCoroutine("ResetSlowPlayer1");
        slowPlayer1.SetActive(true);
    }

    public void SetSlowPlayer2()
    {
        if (resetP2Coroutine != null) StopCoroutine(resetP2Coroutine);
        resetP2Coroutine = StartCoroutine("ResetSlowPlayer2");
        slowPlayer2.SetActive(true);
    }

    IEnumerator ResetSlowPlayer1()
    {
        yield return new WaitForSeconds(slowDuration);
        slowPlayer1.SetActive(false);
    }

    IEnumerator ResetSlowPlayer2()
    {
        yield return new WaitForSeconds(slowDuration);
        slowPlayer2.SetActive(false);
    }
}
