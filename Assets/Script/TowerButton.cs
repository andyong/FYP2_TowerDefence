using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerButton : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int price;
    [SerializeField]
    private Text priceText;


    private void Start()
    {
        priceText.text = "$" + price;

        GameManager.Instance.Changed += new CurrencyChanged(CheckPrice);
    }

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }
	public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    private void CheckPrice()
    {
        //enough money
        if (price <= GameManager.Instance.Currency)
        {
            GetComponent<Image>().color = Color.white;
            priceText.color = Color.white;
        }
        //too poor
        else
        {
            GetComponent<Image>().color = Color.grey;
            priceText.color = Color.grey;
        }
    }

    public void ShowInfo(string type)
    {
        string tooltip = string.Empty;

        switch(type)
        {
            case "Fire":
                FireTower fire = towerPrefab.GetComponentInChildren<FireTower>();
                tooltip = string.Format("<color=#ffa500ff><size=20><b>Fire</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec \nTick time: {3} sec \nTick damage: {4}\nCan apply a DOT to the target", fire.Damage, fire.Proc, fire.DebuffDuration, fire.TickTime, fire.TickDamage);
                break;
            case "Frost":
                FrostTower frost = towerPrefab.GetComponentInChildren<FrostTower>();
                tooltip = string.Format("<color=#00ffffff><size=20><b>Frost</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec\nSlowing factor: {3}%\nHas a chance to slow down the target", frost.Damage, frost.Proc, frost.DebuffDuration, frost.SlowingFactor);
                break;
            case "Poison":
               PoisonTower poison = towerPrefab.GetComponentInChildren<PoisonTower>();
                tooltip = string.Format("<color=#00ff00ff><size=20><b>Poison</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec \nTick time: {3} sec \nSplash damage: {4}\nCan apply dripping poison", poison.Damage, poison.Proc, poison.DebuffDuration, poison.TickTime, poison.SplashDamage);
                break;
            case "Lightning":
                LightningTower lightning = towerPrefab.GetComponentInChildren<LightningTower>();
                tooltip = string.Format("<color=#add8e6ff><size=20><b>Lightning</b></size></color>\nDamage: {0} \nProc: {1}%\nDebuff duration: {2}sec\n Has a chance to stunn the target", lightning.Damage, lightning.Proc, lightning.DebuffDuration);
                break;
        }
        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowTowerStats();
    }
}
