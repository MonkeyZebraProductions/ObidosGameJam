using UnityEngine;
using UnityEngine.UIElements;

public class PressureLine : MonoBehaviour
{

    [SerializeField] private float moveDistance = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contactPoint in collision.contacts) 
        { 
            Vector2 hitNormal = contactPoint.normal;
            transform.position += new Vector3 (hitNormal.x*moveDistance,0,0);
        }

    }
}
