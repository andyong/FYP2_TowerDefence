using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }


	// Use this for initialization
	void Start () {

        CreateLevel();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CreateLevel()
    {

        string[] mapData = new string[]
        {
            "0000" ,"1111" ,"2222" ,"3333" ,"4444" ,"5555"
        };

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(), x,y,worldStart);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStartPoint)
    {
        int tileIndex = int.Parse(tileType);

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);

        newTile.transform.position = new Vector3(worldStartPoint.x + (TileSize * x), worldStartPoint.y - (TileSize * y), 0);
    }


}
