using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPositions;

    private bool p1KeyboardJoined;
    private bool p2KeyboardJoined;
    private bool[] gamePadjoined = {false, false};
    private string[] playerTags = {"Player 1", "Player 2"};

    private int index;
    private int gamePadIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current == null || index == 2)
        {
            return;
        }

        if (!p1KeyboardJoined && Keyboard.current.leftShiftKey.wasPressedThisFrame && index < 2) 
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player 1 Keyboard", pairWithDevice: Keyboard.current);

            player.transform.position = spawnPositions[0].position;
            p1KeyboardJoined = true;
            gamePadjoined[index] = true;
            player.tag = playerTags[0];
            index++;
        }

        if (!p2KeyboardJoined && Keyboard.current.rightShiftKey.wasPressedThisFrame && index < 2)
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player 2 Keyboard", pairWithDevice: Keyboard.current);

            player.transform.position = spawnPositions[1].position;
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
                gamePadjoined[index] = true;
                index++;
                player.tag = playerTags[index];
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
