using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] float checkRadius = 0.5f;
    [SerializeField] GameObject powerUpPrefab;

    private Collider2D blockCollider;
    private string ballThatHit;
    
    void Start()
    {
        Collider2D[] blockCheck = Physics2D.OverlapCircleAll(transform.position, checkRadius, blockLayer);

        foreach (Collider2D block in blockCheck)
        {
            if (block.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }

        blockCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballThatHit = collision.gameObject.tag;
        StartCoroutine(DestroyBlock());
    }

    IEnumerator DestroyBlock()
    {
        blockCollider.enabled=false;
        yield return new WaitForSeconds(0.2f);
        if (powerUpPrefab != null)
        {
            GameObject powerup = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            powerup.GetComponent<PowerUpMovement>().SetDirection(ballThatHit);
        }
        Destroy(this.gameObject);
    }
}
