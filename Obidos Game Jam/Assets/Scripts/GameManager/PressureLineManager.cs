using UnityEngine;

public class PressureLineManager : MonoBehaviour
{
    [SerializeField] float TimeIncrement = 30;
    [SerializeField] float MoveIncrement = 1;
    [SerializeField] PressureLine pressureLine;
    float currentTime;
    public bool countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown)
        {
            currentTime += Time.deltaTime;
            if (currentTime > TimeIncrement)
            {
                if (pressureLine != null)
                {
                    pressureLine.moveDistance += MoveIncrement;
                    currentTime = 0;
                }
            }
        }
    }
}
