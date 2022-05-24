using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LooseTile : MonoBehaviour
{
    [Header("Position")]
    public int row;
    public int col;

    bool canPlace;

    int targetCol;
    int targetRow;

    GridManager gridManager;
    Collider2D tileTarget;

    void Start()
    {
       gridManager = FindObjectOfType<GridManager>();        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            Destroy(gameObject);

            // show placed tile
            tileTarget.GetComponent<SpriteRenderer>().color = Color.black;
            tileTarget.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;

            Debug.Log($"Placed a tile at : Row - {targetRow} // Column - {targetCol}");

            // update grid data
            gridManager.grid[targetRow - 1].tiles[targetCol - 1].placed = true;
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        targetCol= collision.transform.GetSiblingIndex() + 1;
        targetRow = collision.transform.parent.GetSiblingIndex() + 1;

        if (col == targetCol && row == targetRow)
        {
            canPlace = true;

            tileTarget = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BoardSpace")) 
            canPlace = false;
    }
}
