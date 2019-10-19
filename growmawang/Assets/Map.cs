using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	public static Map map;
	[SerializeField] public Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
		map = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
