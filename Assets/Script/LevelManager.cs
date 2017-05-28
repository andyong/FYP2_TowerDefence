using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class LevelManager : Singleton<LevelManager>{

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private Transform map;

    public Dictionary<Point, TileScript> Tiles { get; set; }
    public float TileSize

    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

	// Use this for initialization
	void Start () 
    {
       
        CreateLevel();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void TestDictionary()
    {
        Dictionary<string, int> testDictionary = new Dictionary<string, int>();
    
        
    }


    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        
        //load form txt file
        string[] mapData = ReadFromTextFile();

        //calculate map size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for(int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldOrigin);
            }
        }
        
    }


    private void PlaceTile(string tileType, int x, int y, Vector3 worldStartPoint)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();


        newTile.Setup(new Point(x, y), new Vector3(worldStartPoint.x + (TileSize * x), worldStartPoint.y - (TileSize * y), 0), map);
        if(tileType == "1" || tileType == "6")
        {
            newTile.IsEmpty = false;
        }
        
    }

    private string[] ReadFromTextFile()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace("\r\n", "\n");

        return data.Split('\n');
    }
}
