using UnityEngine;
using System.Collections;

public class TowerButton : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite sprite;

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
}
