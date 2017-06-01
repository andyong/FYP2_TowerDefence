using UnityEngine;
using System.Collections;


public class GameManager : Singleton<GameManager>{

    //private TowerButton clickedTower;

    public TowerButton ClickedButton { get; private set; }

    //current selected tower
    private TowerRange selectedTower;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //HandleException();
	}

    public void PickTower(TowerButton towerButton)
    {
        this.ClickedButton = towerButton;
        HoverIcon.Instance.Activate(towerButton.Sprite);
    }

    public void TowerBought()
    {
        HoverIcon.Instance.Deactivate();
        ClickedButton = null;
    }

    public void SelectTower(TowerRange tower)
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
    }
    //can't build towers etc
    //private void HandleException()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        HoverIcon.Instance.Deactivate();
    //    }
    //}
}
