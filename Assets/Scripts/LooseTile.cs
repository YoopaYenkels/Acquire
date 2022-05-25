using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LooseTile : MonoBehaviour
{
    [Header("Position")]
    public int row;
    public int col;

    bool matchesSpace;

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
        if (Input.GetMouseButtonDown(0) && matchesSpace && gridManager.PlaceTile(tileTarget, targetRow, targetCol))
        {
            Destroy(gameObject);
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        targetCol= collision.transform.GetSiblingIndex() + 1;
        targetRow = collision.transform.parent.GetSiblingIndex() + 1;

        if (col == targetCol && row == targetRow)
        {
            matchesSpace = true;
            tileTarget = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BoardSpace")) 
            matchesSpace = false;
    }

}
