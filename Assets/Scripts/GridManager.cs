using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class BoardRow
{
    [SerializeField]
    public List<TilePool.Tile> tiles;

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

    public enum BoardActions
    {    
        CountChains,
        AreThereUnchainedTiles,
        FindChainToAddTo, // add the new tile and any surorunding placed tiles to the chain
        CreateChain
    }

    int surroundingChainCount = 0;
    int chainToAddTo = 0;

    public ChainsData chainsData;
    public GameObject chainCreator;

    int chainToJoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < chainsData.hotelChains.Count; i++)
        {
            chainsData.hotelChains[i].size = 0;
            chainsData.hotelChains[i].active = false;
        }
        chainsData.chainsAvail = 7;

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
                grid[i].tiles.Add(new TilePool.Tile(j + 1, (char)(i + 65)));

                float posX = j * spacing;
                float posY = -i * spacing;

                var tileCopy = Instantiate(tile, new Vector2(transform.position.x + posX, transform.position.y + posY), transform.rotation, transform.GetChild(i));
                tileCopy.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    $"{j + 1}-{(char)(i + 65)}"; // +65 to start at ASCII char 'A'
            }
            
        }       
    }

    // checks the surrounding tiles for existing chain tiles, or for free tiles that can be cause chains, or add placed tiles to a chain
    public bool DoBoardAction(int _row, int _col, int action)
    {
        List<int> surroundingChains = new();

        bool TopTile()
        {
            switch (action)
            {
                case 0:
                    if (grid[_row - 1].tiles[_col].partOfChain && !surroundingChains.Contains((int)grid[_row - 1].tiles[_col].chain))
                    {
                        surroundingChains.Add((int)grid[_row - 1].tiles[_col].chain);
                        surroundingChainCount += 1;
                    }
                    return false;
                case 1:
                    return grid[_row - 1].tiles[_col].placed;
                case 2:
                    if (grid[_row - 1].tiles[_col].partOfChain)
                    {
                        chainToAddTo = (int)grid[_row - 1].tiles[_col].chain;
                        Debug.Log($"Chain to add to: {chainToAddTo}");
                    }
                    return false;
                case 3:
                    if (grid[_row - 1].tiles[_col].placed && !grid[_row - 1].tiles[_col].partOfChain)
                    {
                        // add the already placed top tile to a new chain being created
                        grid[_row - 1].tiles[_col].partOfChain = true;
                        grid[_row - 1].tiles[_col].chain = (TilePool.Tile.Chains)(chainToJoin + 1);
                        chainsData.hotelChains[chainToJoin].size += 1;
                    }
                    break;
            }
            return false;
        }
        bool BottomTile()
        {
            switch (action)
            {
                case 0:
                    if (grid[_row + 1].tiles[_col].partOfChain && !surroundingChains.Contains((int)grid[_row + 1].tiles[_col].chain))
                    {
                        surroundingChains.Add((int)grid[_row + 1].tiles[_col].chain);
                        surroundingChainCount += 1;
                    }
                    return false;
                case 1:
                    return grid[_row + 1].tiles[_col].placed;
                case 2:
                    if (grid[_row + 1].tiles[_col].partOfChain)
                    {
                        chainToAddTo = (int)grid[_row + 1].tiles[_col].chain;
                        Debug.Log($"Chain to add to: {chainToAddTo}");
                    }
                    return false;
                case 3:
                    if (grid[_row + 1].tiles[_col].placed && !grid[_row + 1].tiles[_col].partOfChain)
                    {
                        grid[_row + 1].tiles[_col].partOfChain = true;
                        grid[_row + 1].tiles[_col].chain = (TilePool.Tile.Chains)(chainToJoin + 1);
                        chainsData.hotelChains[chainToJoin].size += 1;
                    }
                    break;
            }
            return false;
        }
        bool LeftTile()
        {
            switch (action)
            {
                case 0:
                    if (grid[_row].tiles[_col - 1].partOfChain && !surroundingChains.Contains((int)grid[_row].tiles[_col - 1].chain))
                    {
                        surroundingChains.Add((int)grid[_row].tiles[_col - 1].chain);
                        surroundingChainCount += 1;
                    }
                    return false;
                case 1:
                    return grid[_row].tiles[_col - 1].placed;
                case 2:
                    if (grid[_row].tiles[_col - 1].partOfChain)
                    {
                        chainToAddTo = (int)grid[_row].tiles[_col - 1].chain;
                        Debug.Log($"Chain to add to: {chainToAddTo}");
                    }
                    return false;
                case 3:
                    if (grid[_row].tiles[_col - 1].placed && !grid[_row].tiles[_col - 1].partOfChain)
                    {
                        grid[_row].tiles[_col - 1].partOfChain = true;
                        grid[_row].tiles[_col - 1].chain = (TilePool.Tile.Chains)(chainToJoin + 1);
                        chainsData.hotelChains[chainToJoin].size += 1;
                    }
                    break;
            }
            return false;
        }
        bool RightTile()
        {
            switch (action)
            {
                case 0:
                    if (grid[_row].tiles[_col + 1].partOfChain && !surroundingChains.Contains((int)grid[_row].tiles[_col + 1].chain))
                    {
                        surroundingChains.Add((int)grid[_row].tiles[_col + 1].chain);
                        surroundingChainCount += 1;
                    }
                    return false;
                case 1:
                    return grid[_row].tiles[_col + 1].placed;
                case 2:
                    if (grid[_row].tiles[_col + 1].partOfChain)
                    {
                        chainToAddTo = (int)grid[_row].tiles[_col + 1].chain;
                        Debug.Log($"Chain to add to: {chainToAddTo}");
                    }
                    return false;
                case 3:
                    if (grid[_row].tiles[_col + 1].placed && !grid[_row].tiles[_col + 1].partOfChain)
                    {
                        grid[_row].tiles[_col + 1].partOfChain = true;
                        grid[_row].tiles[_col + 1].chain = (TilePool.Tile.Chains)(chainToJoin + 1);
                        chainsData.hotelChains[chainToJoin].size += 1;
                    }
                    break;
            }
            return false;
        }

        // top row
        if (_row == 0)
        {
            if (_col == 0)
            {
                return RightTile() || BottomTile();
            }
            else if (_col == 11)
            {
                return LeftTile() || BottomTile();
            }

            return LeftTile() || BottomTile() || RightTile();
        }
        // bottom row
        else if (_row == 8)
        {
            if (_col == 0)
            {
                return TopTile() || RightTile();
            }
            else if (_col == 11)
            {
                return TopTile() || LeftTile();
            }

            return LeftTile() || TopTile() || RightTile();
        }

        // left column
        else if (_col == 0)
        {
            return TopTile() || BottomTile() || RightTile();
        }
        // right column
        else if (_col == 11)
        {
            return TopTile() || BottomTile() || LeftTile();
        }
        else
        {
            return TopTile() || BottomTile() || LeftTile() || RightTile();
        }
    }

    public bool PlaceTile(Collider2D tileTarget, int targetRow, int targetCol)
    {
        surroundingChainCount = 0;
      
        // count the surrounding chains
        DoBoardAction(targetRow - 1, targetCol - 1, (int)BoardActions.CountChains);
        Debug.Log($"{surroundingChainCount} chains surround the last placed tile");

        if (surroundingChainCount == 1)
        {
            ShowTile();
            DoBoardAction(targetRow - 1, targetCol - 1, (int)BoardActions.FindChainToAddTo);
           
            // add the placed tile to the chain
            grid[targetRow - 1].tiles[targetCol - 1].chain = (TilePool.Tile.Chains)chainToAddTo;
            grid[targetRow - 1].tiles[targetCol - 1].partOfChain = true;
            chainsData.hotelChains[chainToAddTo - 1].size += 1;

            //add any surrounding non chain tiles to chain
            chainToJoin = chainToAddTo - 1;
            DoBoardAction(targetRow - 1, targetCol - 1, (int)BoardActions.CreateChain);
         
            Debug.Log($"The tile was added to {(TilePool.Tile.Chains)chainToAddTo}");
            return true;
        }
        // check for single surrounding tiles, which can create new chains
        else if (surroundingChainCount == 0 && 
            DoBoardAction(targetRow - 1, targetCol - 1, (int)BoardActions.AreThereUnchainedTiles) && 
            chainsData.chainsAvail > 0)
        {
            ShowTile();
            StartCoroutine(CreateChain(targetRow, targetCol, tileTarget));
            return true;
        }
        // single tile placement
        else if (surroundingChainCount == 0 && !DoBoardAction(targetRow - 1, targetCol - 1, (int)BoardActions.AreThereUnchainedTiles))
        {
            ShowTile();
            return true;
        }

        // shows the placed tile on the board
        void ShowTile()
        {
            tileTarget.GetComponent<SpriteRenderer>().color = Color.black;
            tileTarget.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            grid[targetRow - 1].tiles[targetCol - 1].placed = true;
        }

        // if tile is not placed
        return false;     
    }

    IEnumerator CreateChain(int targetRow, int targetCol, Collider2D tileTarget)
    {
        chainCreator.SetActive(true);
        int chainToAdd = 0;
        bool choseChain = false;

        while (!choseChain)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !chainsData.hotelChains[0].active)
            {
                chainToAdd = 1;               
                choseChain = true;            
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && !chainsData.hotelChains[1].active)
            {
                chainToAdd = 2;
                choseChain = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && !chainsData.hotelChains[2].active)
            {
                chainToAdd = 3;
                choseChain = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !chainsData.hotelChains[3].active)
            {
                chainToAdd = 4;
                choseChain = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && !chainsData.hotelChains[4].active)
            {
                chainToAdd = 5;
                choseChain = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) && !chainsData.hotelChains[5].active)
            {
                chainToAdd = 6;
                choseChain = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) && !chainsData.hotelChains[6].active)
            {
                chainToAdd = 7;
                choseChain = true;
            }
            yield return null;
        }

        chainsData.chainsAvail -= 1;

        chainCreator.SetActive(false);
        chainsData.hotelChains[chainToAdd - 1].active = true;

        // add last placed tile to the chain
        grid[targetRow - 1].tiles[targetCol - 1].chain = (TilePool.Tile.Chains)chainToAdd;
        grid[targetRow - 1].tiles[targetCol - 1].partOfChain = true;

        chainsData.hotelChains[chainToAdd - 1].size += 1;

        // add chain indicator
        tileTarget.GetComponent<SpriteRenderer>().color = chainsData.hotelChains[chainToAdd - 1].color;
        tileTarget.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = chainsData.hotelChains[chainToAdd - 1].chainName[0].ToString();

        chainToJoin = chainToAdd - 1;
        DoBoardAction(targetRow - 1, targetCol - 1, (int)BoardActions.CreateChain);

        Debug.Log($"NEW Chain Created! ({(TilePool.Tile.Chains)chainToAdd})"); // enum Chains starts with 'None'
    }
}
