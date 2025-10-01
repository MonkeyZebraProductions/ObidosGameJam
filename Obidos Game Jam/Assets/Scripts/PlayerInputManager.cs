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

    private int index;
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

        if (!p1KeyboardJoined && Keyboard.current.qKey.wasPressedThisFrame) 
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player 1 Keyboard", pairWithDevice: Keyboard.current);

            player.transform.position = spawnPositions[index].position;
            p1KeyboardJoined = true;
            gamePadjoined[index] = true;
            index++;
        }

        if (!p2KeyboardJoined && Keyboard.current.eKey.wasPressedThisFrame)
        {
            var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player 2 Keyboard", pairWithDevice: Keyboard.current);

            player.transform.position = spawnPositions[index].position;
            p2KeyboardJoined = true;
            gamePadjoined[index] = true;
            index++;
        }

        if(index<Gamepad.all.Count)
        {
            if (!gamePadjoined[index] && Gamepad.all[index].buttonSouth.wasPressedThisFrame)
            {
                var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[index]);
                player.transform.position = spawnPositions[index].position;
                gamePadjoined[index] = true;
                index++;
            }
        }
     
        //foreach (var gamePad in Gamepad.all)
        //{

        //    if (!gamePadjoined[index] && gamePad.buttonSouth.wasPressedThisFrame )
        //    {
        //        var player = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: gamePad);
        //        player.transform.position = spawnPositions[index].position;
        //        gamePadjoined[index] = true;
        //        index++;
        //    }

        //}
    }
}
