using TMPro;
using UnityEngine;

public class PressureLine : MonoBehaviour
{

    [SerializeField] public float moveDistance = 1;
    [SerializeField][Range(0.0f, 1.0f)] private float EaseOutValue;
    [SerializeField] private Canvas EndGameCanvas;

    private TextMeshProUGUI endGameText;
    private Vector3 targetPosition, startPosition;
    private float alpha = 1.0f;

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
}
