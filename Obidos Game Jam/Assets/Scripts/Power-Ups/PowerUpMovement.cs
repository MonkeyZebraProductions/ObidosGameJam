using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject destroyEffectPrefab;

    private Vector3 direction = Vector3.zero;
    private AudioManager audioManager;
    [SerializeField] private string HitSoundName = "Powerup";

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

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

        if (Mathf.Abs(transform.position.x) > 15f)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyEffect()
    {
        Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        if (audioManager != null) 
        {
            if (!audioManager.IsPlaying(HitSoundName))
            {
                audioManager.Play(HitSoundName);
            }
            else if (!audioManager.IsPlaying(HitSoundName + "1"))
            {
                audioManager.Play(HitSoundName + "1");
            }
            else if (!audioManager.IsPlaying(HitSoundName + "2"))
            {
                audioManager.Play(HitSoundName + "2");
            }
        }
        Destroy(gameObject);
    }
}