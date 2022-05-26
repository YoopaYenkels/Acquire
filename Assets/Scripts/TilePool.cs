using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TilePool : ScriptableObject
{
    [System.Serializable]
    public class Tile
    {
        [Header("Position")]
        public int col;
        public char row;
       
        [Header("Tile Stats")]
        public bool placed;
        public bool partOfChain;

        [SerializeField]
        public Chains chain;

        public bool isChainIndicator;

        public enum Chains
        {
            None,
            Tower,
            Luxor,
            Festival,
            American,
            Worldwide,
            Continental,
            Imperial
        };

        public Tile(int _col, char _row)
        {
            col = _col;
            row = _row;       
        }
    }

    public List<Tile> tileCoords;
}