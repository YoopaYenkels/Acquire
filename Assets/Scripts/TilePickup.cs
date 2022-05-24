using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TilePickup : MonoBehaviour
{
    
    //List<KeyValuePair<char, int>> tiles = new();

    public GameObject tileCopy;


    GameObject spawnedTile;
    Vector3 mousePos;

    public TilePool tilePool;

    // Start is called before the first frame update
    void Start()
    {
        tilePool.tileCoords = new();

        for (int i = 1; i <= 9; i++)
        {          
            for (int j = 1; j <= 12; j++)
            {
                //tiles.Add(new KeyValuePair<char, int>((char)(i - 1 + 65), j));
                TilePool.Coords coordsToAdd = new TilePool.Coords(j, (char)(i - 1 + 65));
                tilePool.tileCoords.Add(coordsToAdd);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedTile)
        {
            mousePos = Input.mousePosition;
            spawnedTile.transform.position = Vector2.Lerp(spawnedTile.transform.position, Camera.main.ScreenToWorldPoint(mousePos), 10);
        }    
        
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(spawnedTile);
        }
    }

    private void OnMouseDown()
    {
        int tileIndex = Random.Range(0, tilePool.tileCoords.Count - 1);

        spawnedTile = Instantiate(tileCopy, transform.position, Quaternion.identity, transform.parent = null);
       
        spawnedTile.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = 
            $"{tilePool.tileCoords[tileIndex].col}-{tilePool.tileCoords[tileIndex].row}";
        spawnedTile.GetComponent<LooseTile>().col = tilePool.tileCoords[tileIndex].col;
        spawnedTile.GetComponent<LooseTile>().row = tilePool.tileCoords[tileIndex].row - 64;

        //Debug.Log($"You received tile {tiles[tileIndex - 1].Value}-{tiles[tileIndex - 1].Key} | Index: {tileIndex}");
        Debug.Log($"{tilePool.tileCoords.Count} tiles remaining");
        tilePool.tileCoords.RemoveAt(tileIndex);
    }
}
