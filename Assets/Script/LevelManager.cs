using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;


public class LevelManager : Singleton<LevelManager>{

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

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
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "firescene")
        {
            CreateLevel();
        }
        else if (sceneName == "darkscene")
        {
            CreateLevel2();
        }
        else if (sceneName == "waterscene")
        {
            CreateLevel3();
        }
        else if (sceneName == "windscene")
        {
            CreateLevel4();
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void TestDictionary()
    {
        //Dictionary<string, int> testDictionary = new Dictionary<string, int>();
    }


    public void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        
        //load form txt file
        string[] mapData = ReadFromTextFile();

        //calculate map size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for(int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                maxTile =  PlaceTile(newTiles[x].ToString(), x, y, worldOrigin);
            }
        }

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize,maxTile.y - TileSize));
    }

    public void RemoveLevel1()
    {
        //Tiles = new Dictionary<Point, TileScript>();

        //load form txt file
        string[] mapData = ReadFromTextFile();

        //calculate map size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                RemoveTile(newTiles[x].ToString(), x, y, worldOrigin);
            }
        }
    }

    public void CreateLevel2()
    {
        Tiles = new Dictionary<Point, TileScript>();

        //load form txt file
        string[] mapData = ReadFromTextFile2();

        //calculate map size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                maxTile = PlaceTile(newTiles[x].ToString(), x, y, worldOrigin);
            }
        }

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

    }

    public void CreateLevel3()
    {
        Tiles = new Dictionary<Point, TileScript>();

        //load form txt file
        string[] mapData = ReadFromTextFile3();

        //calculate map size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                maxTile = PlaceTile(newTiles[x].ToString(), x, y, worldOrigin);
            }
        }

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    public void CreateLevel4()
    {
        Tiles = new Dictionary<Point, TileScript>();

        //load form txt file
        string[] mapData = ReadFromTextFile4();

        //calculate map size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                maxTile = PlaceTile(newTiles[x].ToString(), x, y, worldOrigin);
            }
        }

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }


    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStartPoint)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();


        newTile.Setup(new Point(x, y), new Vector3(worldStartPoint.x + (TileSize * x), worldStartPoint.y - (TileSize * y), 0), map);
        if (tileType == "1" || tileType == "3" || tileType == "5" || tileType == "6" || tileType == "8")
        {
            newTile.IsEmpty = false;
        }

        return newTile.transform.position;
        
    }

    private void RemoveTile(string tileType, int x, int y, Vector3 worldStartPoint)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();


        newTile.Remove(new Point(x, y), new Vector3(worldStartPoint.x + (TileSize * x), worldStartPoint.y - (TileSize * y), 0), map);
    }

    private string[] ReadFromTextFile()
    {
        TextAsset bindData = Resources.Load("Level1") as TextAsset;

        string data = bindData.text.Replace("\r\n", "\n");

        return data.Split('\n');
    }

    private string[] ReadFromTextFile2()
    {
        TextAsset bindData = Resources.Load("Level2") as TextAsset;

        string data = bindData.text.Replace("\r\n", "\n");

        return data.Split('\n');
    }

    private string[] ReadFromTextFile3()
    {
        TextAsset bindData = Resources.Load("Level3") as TextAsset;

        string data = bindData.text.Replace("\r\n", "\n");

        return data.Split('\n');
    }

    private string[] ReadFromTextFile4()
    {
        TextAsset bindData = Resources.Load("Level4") as TextAsset;

        string data = bindData.text.Replace("\r\n", "\n");

        return data.Split('\n');
    }
}
