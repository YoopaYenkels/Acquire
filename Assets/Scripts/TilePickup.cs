using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TilePickup : MonoBehaviour
{   
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
                TilePool.Tile coordsToAdd = new TilePool.Tile(j, (char)(i - 1 + 65));
                tilePool.tileCoords.Add(coordsToAdd);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // move tile with mouse
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


    // draw a random tile from the pool
    private void OnMouseDown()
    {
        if (tilePool.tileCoords.Count > 0)
        {
            int tileIndex = Random.Range(0, tilePool.tileCoords.Count - 1);

            spawnedTile = Instantiate(tileCopy, transform.position, Quaternion.identity, transform.parent = null);

            spawnedTile.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                $"{tilePool.tileCoords[tileIndex].col}-{tilePool.tileCoords[tileIndex].row}";

            spawnedTile.GetComponent<LooseTile>().col = tilePool.tileCoords[tileIndex].col;
            spawnedTile.GetComponent<LooseTile>().row = tilePool.tileCoords[tileIndex].row - 64;

            tilePool.tileCoords.RemoveAt(tileIndex);
        }    
    }
}
