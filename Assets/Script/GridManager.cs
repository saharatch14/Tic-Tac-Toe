using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int cols = 3;

    [SerializeField] public int rows = 3;

    [SerializeField] private float tileSize = 1;

    [SerializeField] private Transform _cam;

    [SerializeField] private Tile _tilePrefab;

    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    /*private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("backgroudfield"));

        for(int row=0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.name = $"Tile {row} {col}";
                tile.transform.position = new Vector2(posX, posY);

            }
        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        _cam.transform.position = new Vector3((float)rows / 2 -0.5f, (float)cols / 2 - 0.5f, -10);
    }*/


    void GenerateGrid()
    {
        //GameObject referenceTile = (GameObject)Instantiate(Resources.Load("backgroudfield"));
        _tiles = new Dictionary<Vector2, Tile>();
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {

                float posX = col * tileSize;
                float posY = row * -tileSize;

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(posX, posY), Quaternion.identity);
                spawnedTile.name = $"{row} {col}";

                var isOffset = (row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(row, col)] = spawnedTile;
            }
        }

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        _cam.transform.position = new Vector3((float)rows / 2 - gridW, (float)cols / 2 - gridH, -10);

    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
