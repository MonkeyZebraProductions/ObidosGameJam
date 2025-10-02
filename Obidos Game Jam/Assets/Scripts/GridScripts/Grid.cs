using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Grid 
{

    private int width;
    private int height;
    private float cellSize;
    private GameObject block;
    private Vector2 originPos;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize, GameObject block, Vector2 originPos)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.block = block;
        this.originPos = originPos;

        gridArray = new int[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //GameObject.Instantiate(block, GetWorldPosition(x, y), Quaternion.identity);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector2 GetWorldPosition(int x, int y) { return new Vector2(x, y)*cellSize + originPos; }
}
