using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Collider2D blockCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        StartCoroutine(DestroyBlock());
    }

    IEnumerator DestroyBlock()
    {
        blockCollider.enabled=false;
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
