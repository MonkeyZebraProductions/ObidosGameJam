using UnityEngine;

public class BlockWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            FindFirstObjectByType<BlockSpawner>().SpawnBlockPower(other.tag == "Player 2");
            Destroy(gameObject);
        }
    }
}
