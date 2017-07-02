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
        if(price <= GameManager.Instance.Currency)
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
}
