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
        public bool active;
    }

    public int chainsAvail;
    public List<HotelChain> hotelChains;
}
