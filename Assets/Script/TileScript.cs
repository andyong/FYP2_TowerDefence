using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    //tile grid pos
    public Point GridPosition { get; private set; }

    private Color32 fullColor = Color.red;
    private Color32 emptyColor = Color.green;

    private SpriteRenderer spriteRenderer;

    //check if tile is available
    public bool IsEmpty { get; set; }

    private Tower myTower;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null)
        {
            if(IsEmpty)
            {
                SetColorTile(emptyColor);
            }
            if(!IsEmpty)
            {
                SetColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
        else if(!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton == null && Input.GetMouseButtonDown(0))
        {
            if(myTower != null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
    }

    private void OnMouseExit()
    {
        SetColorTile(Color.white);
    }

    //private void OnMouseOver()
    //{
    //    for (int i = 0; i < Input.touchCount; ++i)
    //    {
    //        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null)
    //        {
    //            if (Input.GetTouch(i).phase == TouchPhase.Began)
    //            {
    //                PlaceTower();
    //            }
    //        }
    //    }
    //}

    private void PlaceTower()
    {
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedButton.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

        IsEmpty = false;

        SetColorTile(Color.white);

        GameManager.Instance.TowerBought();

        Debug.Log("placing a tower");
    }

    private void SetColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
