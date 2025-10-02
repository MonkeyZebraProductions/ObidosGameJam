using UnityEngine;

public class PressureLine : MonoBehaviour
{

    [SerializeField] public float moveDistance = 1;
    [SerializeField][Range(0.0f, 1.0f)] private float EaseOutValue;
    private Vector3 targetPosition, startPosition;
    private float alpha = 1.0f;

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (alpha < 1.0f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.Pow(alpha, EaseOutValue));
            alpha += Time.deltaTime;
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
        if (collision.tag == "DangerZone") Debug.Log("End Game");
    }

    public void SetMovePositions(float xMovemnet)
    {
        startPosition = transform.position;
        targetPosition = transform.position + new Vector3(xMovemnet * moveDistance, 0, 0);
        alpha = 0.0f;
    }
}
