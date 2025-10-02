using UnityEngine;

public class TripleBall : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement paddle = other.GetComponent<PlayerMovement>();
        if (paddle != null)
        {
            GameObject ball1 = Instantiate(ballPrefab, other.transform.position + GetOffset(paddle.tag), Quaternion.identity);
            ball1.tag = paddle.tag;
            ball1.GetComponent<Ball>().SetSpeed(GetCorrectSpeed(paddle.tag));
            GameObject ball2 = Instantiate(ballPrefab, other.transform.position + GetOffset(paddle.tag), Quaternion.identity);
            ball2.tag = paddle.tag;
            ball2.GetComponent<Ball>().SetSpeed(GetCorrectSpeed(paddle.tag));
            Destroy(gameObject);
        }
    }

    Vector3 GetOffset (string playerTag)
    {
        Vector3 offset = Vector3.zero;
        if (playerTag == "Player 1")
        {
            offset = Vector3.right;
        }
        else if (playerTag == "Player 2")
        {
            offset = Vector3.left;
        }
        return offset;
    }

    float GetCorrectSpeed (string playerTag)
    {
        float speed = 0f;
        if (playerTag == "Player 1")
        {
            speed = FindFirstObjectByType<SpawnBalls>().GetCurrentSpeedP1();
        }
        else if (playerTag == "Player 2")
        {
            speed = FindFirstObjectByType<SpawnBalls>().GetCurrentSpeedP2();
        }
        return speed;
    }
}
