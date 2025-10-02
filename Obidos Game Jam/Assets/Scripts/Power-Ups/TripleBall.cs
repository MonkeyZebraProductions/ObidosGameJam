using UnityEngine;

public class TripleBall : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null && !ball.IsTripleBall())
        {
            GameObject ball1 = Instantiate(ballPrefab, other.transform.position, Quaternion.identity);
            ball1.tag = ball.tag;
            ball1.GetComponent<Ball>().SetSpeed(ball.GetCurrentSpeed());
            GameObject ball2 = Instantiate(ballPrefab, other.transform.position, Quaternion.identity);
            ball2.tag = ball.tag;
            ball2.GetComponent<Ball>().SetSpeed(ball.GetCurrentSpeed());
            Destroy(gameObject);
        }
    }
}
