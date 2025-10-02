using UnityEngine;
using TMPro;
using System.Collections;

public class StartScreenCountdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    private PlayerInputManager playerInputManager;
    public void StartCountown(PlayerInputManager inPlayerInputManager)
    {
        playerInputManager = inPlayerInputManager;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.0f);
        playerInputManager.SpawnAssets();
        Destroy(gameObject);
    }
}
