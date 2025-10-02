using Array2DEditor;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] int GridWidth = 1;
    [SerializeField] int GridHeight = 1;
    [SerializeField] GameObject block;
    [SerializeField] Transform StartGidPosition;
    [SerializeField] Array2DBool InitialBlockPlacements;

    private Grid grid;
    private bool[,] startingCells;
    private float cellSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 originPos = new Vector2(StartGidPosition.transform.position.x, StartGidPosition.transform.position.y);
        cellSize = block.transform.localScale.x;
        grid = new Grid(GridWidth, GridHeight, cellSize, block, originPos);

        startingCells = InitialBlockPlacements.GetCells();
        //InitialBlockSpawn();
    }

    public void InitialBlockSpawn()
    {
        for (var y = 0; y < InitialBlockPlacements.GridSize.y; y++)
        {
            for (var x = 0; x < InitialBlockPlacements.GridSize.x; x++)
            {
                if (startingCells[y, x])
                {
                    Vector2 spawnPos = grid.GetWorldPosition(x, y) + new Vector2(cellSize / 2, cellSize / 2);
                    var prefabGO = Instantiate(block, spawnPos, Quaternion.identity);
                    prefabGO.name = $"({x}, {y})";

                    Vector2 mirrorSpawnPos = grid.GetWorldPosition(GridWidth - 1 - x, GridHeight - 1 - y) + new Vector2(cellSize / 2, cellSize / 2);
                    var mirrorPrefabGO = Instantiate(block, mirrorSpawnPos, Quaternion.identity);
                    mirrorPrefabGO.name = $"({x}, {y})";
                }
            }
        }
    }
}



