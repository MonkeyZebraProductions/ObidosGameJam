using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject destroyEffectPrefab;

    private Vector3 direction = Vector3.zero;

    public void SetDirection(string ballTag)
    {
        if (ballTag == "Player 1")
        {
            direction = Vector3.left;
        }
        else if (ballTag == "Player 2")
        {
            direction = Vector3.right;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnDestroy()
    {
        Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
    }
}