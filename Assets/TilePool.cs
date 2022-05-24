using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TilePool : ScriptableObject
{
    [System.Serializable]
    public class Coords
    {     
        public int col;
        public char row;

        public Coords(int _col, char _row)
        {
            col = _col;
            row = _row;
        }
    }

    public List<Coords> tileCoords;
}