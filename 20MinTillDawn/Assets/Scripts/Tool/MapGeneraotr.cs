using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneraotr : MonoBehaviour
{
    public BoundsInt map;
    public BoundsInt map1;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("MapGenerator/Tilemap").GetComponent<Tilemap>().cellBounds;
        map1.SetMinMax(map.min*3,map.max*3);
        //map.ClampToBounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
