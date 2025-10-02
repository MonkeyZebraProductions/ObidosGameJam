using UnityEngine;

public class Slow : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            if (other.tag == "Player 1")
            {
                foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Player 2"))
                {
                    if (thing.GetComponent<Ball>() != null) thing.GetComponent<Ball>().SlowBall();
                    if (thing.GetComponent<PlayerMovement>() != null) thing.GetComponent<PlayerMovement>().SlowPlayer();
                }
            }
            else if (other.tag == "Player 2")
            {
                foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Player 1"))
                {
                    if (thing.GetComponent<Ball>() != null) thing.GetComponent<Ball>().SlowBall();
                    if (thing.GetComponent<PlayerMovement>() != null) thing.GetComponent<PlayerMovement>().SlowPlayer();
                }
            }
            Destroy(gameObject);
        }
    }
}
