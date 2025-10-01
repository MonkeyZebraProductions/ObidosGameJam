using Unity.VisualScripting;
using UnityEngine;

public class BackWall : MonoBehaviour
{
    private PressureLine pressureLine;
    [SerializeField] private bool moveLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressureLine = FindFirstObjectByType<PressureLine>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pressureLine != null) 
        {
            pressureLine.SetMovePositions(moveLeft ? -1 : 1);
        }
    }
}
