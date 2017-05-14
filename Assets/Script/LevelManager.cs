using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] tilePrefabs;

    public float Tile_size
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

    private void CreateLevel()
    {
        //string[] mapData = new string[]
        //{
        //    "0000", "1111", "2222", "3333", "4444", "5555"
        //};
        string[] mapData = ReadFromTextFile();

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

    private void PlaceTile(string tileType, int x, int y, Vector3 worldOrigin)
    {   //"1"

        int tileIndex = int.Parse(tileType);

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]); //initialize level with tiles
        
        newTile.transform.position = new Vector3(worldOrigin.x + (Tile_size * x), worldOrigin.y - (Tile_size * y), 0);
    }

    private string[] ReadFromTextFile()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace("\r\n", "\n");

        return data.Split('\n');
    }
}
