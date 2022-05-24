using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Tile
{
    [Header("Position")]
    public int row;
    public int col;

    [Header("Tile Stats")]
    [SerializeField]
    public bool placed;

    [SerializeField]
    bool partOfChain;

    [SerializeField]
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

    public Tile(int _row, int _col)
    {
        row = _row;
        col = _col;
    }
}

[System.Serializable]
public class BoardRow
{
    [SerializeField]
    public List<Tile> tiles;

    public BoardRow()
    {
        tiles = new();
    }
}

public class GridManager : MonoBehaviour
{
    [SerializeField]
    int rows;
    [SerializeField]
    int cols;

    [SerializeField]
    public List<BoardRow> grid = new();
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
            grid.Add(new BoardRow());
            for (int j = 0; j < cols; j++)
            {
                grid[i].tiles.Add(new Tile(i, j));

                float posX = j * spacing;
                float posY = -i * spacing;

                var tileCopy = Instantiate(tile, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation, transform.GetChild(i));
                tileCopy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    $"{j + 1}-{(char)(i + 65)}"; // +65 to start at ASCII char 'A'
            }
            
        }       
    }
    
}
