using UnityEngine;

public class Slow : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement paddle = other.GetComponent<PlayerMovement>();
        if (paddle != null)
        {
            if (other.tag == "Player 1")
            {
                FindFirstObjectByType<SpawnBalls>().SlowBalls("Player 2");
                FindFirstObjectByType<PressureLine>().SetSlowPlayer2();
                foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Player 2"))
                {
                    if (thing.GetComponent<PlayerMovement>() != null) thing.GetComponent<PlayerMovement>().SlowPlayer();
                }
            }
            else if (other.tag == "Player 2")
            {
                FindFirstObjectByType<SpawnBalls>().SlowBalls("Player 1");
                FindFirstObjectByType<PressureLine>().SetSlowPlayer1();
                foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Player 1"))
                {
                    if (thing.GetComponent<PlayerMovement>() != null) thing.GetComponent<PlayerMovement>().SlowPlayer();
                }
            }
            Destroy(gameObject);
        }
    }
}
