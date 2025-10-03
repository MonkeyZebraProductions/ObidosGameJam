using UnityEngine;

public class ActivateEffectDanger : MonoBehaviour
{
    [SerializeField] private GameObject dangerEffectPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PressureLine>() != null) dangerEffectPrefab.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PressureLine>() != null) dangerEffectPrefab.SetActive(false);
    }
}
