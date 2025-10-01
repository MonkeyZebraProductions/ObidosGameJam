using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPositions;

    private bool p1KeyboardJoined;
    private bool p2KeyboardJoined;
    private bool[] gamePadjoined;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
