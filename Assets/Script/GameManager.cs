using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>{

    public event CurrencyChanged Changed;

    public TowerButton ClickedButton { get; private set; }

    //current selected tower
    private TowerRange selectedTower;
    
    public GameObject waveBtn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //HandleException();
	}

    public void PickTower(TowerButton towerButton)
    {
        if (UIManager.Instance.Gold >= towerButton.Price && SpawnEnemy.Instance.startnewwave == false)
        {
            this.ClickedButton = towerButton;
            HoverIcon.Instance.Activate(towerButton.Sprite);
        }
    }

    public void TowerBought()
    {
        if (UIManager.Instance.Gold >= ClickedButton.Price)
        {
            UIManager.Instance.Gold -= ClickedButton.Price;
            HoverIcon.Instance.Deactivate();
            ClickedButton = null;
        }
    }

    public void OnCurrencyChanged()
    {
        if(Changed != null)
        {
            Changed();
        }
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
    private void HandleException()
    {
        if (Input.touchCount > 0)
        {
            HoverIcon.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        //UIManager.Instance.Wave++;
        //StartCoroutine(SpawnWave());
        SpawnEnemy.Instance.startnewwave = true;
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(2.5f);
    }
}

