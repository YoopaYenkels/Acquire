using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChainsData : ScriptableObject
{
    [System.Serializable]
    public class HotelChain
    {
        public string chainName;
        public Color color;

        public int size;
        public bool isActive;
        public bool isSafe;

        public int stocks;

        public List<TilePool.Tile> tilesInChain;
    }

    public int chainsAvail;
    public List<HotelChain> hotelChains;
}
