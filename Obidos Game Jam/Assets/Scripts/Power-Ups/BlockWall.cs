using UnityEngine;

public class BlockWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement paddle = other.GetComponent<PlayerMovement>();
        if (paddle != null)
        {
            FindFirstObjectByType<AudioManager>().Play("block");
            FindFirstObjectByType<BlockSpawner>().SpawnBlockPower(other.tag == "Player 1");
            gameObject.GetComponent<PowerUpMovement>().DestroyEffect();
        }
    }
}
