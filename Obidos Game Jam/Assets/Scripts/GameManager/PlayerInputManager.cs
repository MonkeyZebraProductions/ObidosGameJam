using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Sprite[] playerSprites;

    private bool p1KeyboardJoined;
    private bool p2KeyboardJoined;
    private bool[] gamePadjoined = {false, false};
    private string[] playerTags = {"Player 1", "Player 2"};

    private int index;
    private int gamePadIndex;
    private bool gameStart;

    private SpawnBalls spawnBalls;
    private BlockSpawner blockSpawner;
    private StartScreenCountdown startScreenCountdown;
    private AudioManager audioManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBalls = GetComponentInParent<SpawnBalls>();
        blockSpawner = GetComponentInParent<BlockSpawner>();
        audioManager = GetComponentInParent<AudioManager>();
        startScreenCountdown = FindFirstObjectByType<StartScreenCountdown>();
        startScreenCountdown.gameObject.SetActive(false);
        if (audioManager != null)
        {
            audioManager.Play("Music");
        }
    }

    public void SpawnAssets()
    {
        if (spawnBalls != null)
        {
            spawnBalls.BallSpawn();
        }
        else
        {
            Debug.LogError("Spall Balls Not Valid");
        }

        if (blockSpawner != null)
        {
            blockSpawner.InitialBlockSpawn();
        }
        else
        {
            Debug.LogError("Block Spawner Not Valid");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.FindKeyOnCurrentKeyboardLayout("r").wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Keyboard.current == null || index == 2)
        {
            if(!gameStart)
            {
                startScreenCountdown.gameObject.SetActive(true);
                startScreenCountdown.StartCountown(this);
                gameStart = true;
                FindFirstObjectByType<BlockSpawner>().StartPowerUpsSpawn();
            }
            return;
        }

        if (!p1KeyboardJoined && Keyboard.current.leftShiftKey.wasPressedThisFrame && index < 2) 
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player 1 Keyboard", pairWithDevice: Keyboard.current);

            player.transform.position = spawnPositions[0].position;
            player.GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[0];
            p1KeyboardJoined = true;
            gamePadjoined[index] = true;
            player.tag = playerTags[0];
            index++;
        }

        if (!p2KeyboardJoined && Keyboard.current.rightShiftKey.wasPressedThisFrame && index < 2)
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player 2 Keyboard", pairWithDevice: Keyboard.current);

            player.transform.position = spawnPositions[1].position;
            player.GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[1];
            p2KeyboardJoined = true;
            gamePadjoined[index] = true;
            player.tag = playerTags[1];
            index++;
        }

        if(gamePadIndex < Gamepad.all.Count && index < 2)
        {
            if (!gamePadjoined[index] && Gamepad.all[gamePadIndex].buttonSouth.wasPressedThisFrame)
            {
                var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[gamePadIndex]);
                player.transform.position = spawnPositions[index].position;
                player.GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[index];
                gamePadjoined[index] = true;
                player.tag = playerTags[index];
                index++;
                gamePadIndex++;
            }
        }
     
        //foreach (var gamePad in Gamepad.all)
        //{

        //    if (!gamePadjoined[index] && gamePad.buttonSouth.wasPressedThisFrame)
        //    {
        //        var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: gamePad);
        //        player.transform.position = spawnPositions[index].position;
        //        gamePadjoined[index] = true;
        //        index++;
        //    }
        //}
    }
}
