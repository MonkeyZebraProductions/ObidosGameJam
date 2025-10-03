using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] float checkRadius = 0.5f;
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] GameObject destroyEffectPrefab;

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
        DestroyBlock();
    }

    void DestroyBlock()
    {
        blockCollider.enabled = false;
        if (powerUpPrefab != null)
        {
            GameObject powerup = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            powerup.GetComponent<PowerUpMovement>().SetDirection(ballThatHit);
        }
        Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
