using UnityEngine;
using UnityEngine.UIElements;

public class PressureLine : MonoBehaviour
{

    [SerializeField] private float moveDistance = 1;
    private Vector3 targetPosition,startPosition;
    private float alpha = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha<1.0f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, alpha);
            alpha += Time.deltaTime * 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contactPoint in collision.contacts) 
        { 
            Vector2 hitNormal = contactPoint.normal;
            SetMovePositions(hitNormal.x);
            //transform.position += new Vector3 (hitNormal.x*moveDistance,0,0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //End the Game
        Debug.Log("End Game");
    }

    public void SetMovePositions(float xMovemnet)
    {
        startPosition = transform.position;
        targetPosition = transform.position + new Vector3(xMovemnet * moveDistance, 0, 0);
        alpha = 0.0f;
    }
}
