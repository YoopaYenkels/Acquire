using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Tile
{
    public int row;
    public int col;

    bool placed;

    bool partOfChain;
    Chains chain;

    enum Chains
    {
        None,
        Tower,
        Luxor,
        Festival,
        American,
        Wordlewide,
        Cotinental,
        Imperial
    };

    Sprite sprite;

    public Tile(int _row, int _col)
    {
        row = _row;
        col = _col;
    }
}

public class GridManager : MonoBehaviour
{
    [SerializeField]
    int rows;
    [SerializeField]
    int cols;

    public List<Tile> grid = new();
    public GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float spacing = 1.2f;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Tile tileToAdd = new (i, j);
                grid.Add(tileToAdd);

                float posX = j * spacing;
                float posY = -i * spacing;
                
                var tileCopy = Instantiate(tile, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation, transform);
                int location = i * cols + j;
                tileCopy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                    $"{grid[location].col + 1}-{(char)(grid[location].row + 65)}"; // +65 to start at ASCII char 'A'
            }
            
        }       
    }
    
}
