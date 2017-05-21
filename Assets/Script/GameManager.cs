using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>{

    //private TowerButton clickedTower;

    public TowerButton ClickedButton { get; private set; }

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

    //can't build towers etc
    //private void HandleException()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        HoverIcon.Instance.Deactivate();
    //    }
    //}
}
