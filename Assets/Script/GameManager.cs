using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WaveClass
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}


public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>{

    public event CurrencyChanged Changed;

    public GameObject[] waypoints;
    //public GameObject testEnemyPrefab;

    public WaveClass[] waves;
    public int timeBetweenWaves = 5;

    private int enemiesSpawned = 0;

    public TowerButton ClickedButton { get; private set; }

    //current selected tower
    private TowerRange selectedTower;
    
    public GameObject waveBtn;

    // currency
    private int currency;

    public int Currency
    {
        get { return currency; }
        set
        {
            this.currency = value;
            this.currencyTxt.text = value.ToString() + " <color=lime>$</color>";
        }
    }

    [SerializeField]
    private Text currencyTxt;
    // currecy

    // wave
    public Text waveLabel;

    private int wave;

    public int Wave
    {
        get { return wave; }
        set
        {
            this.wave = value;
            this.waveLabel.text = "WAVE: " + (wave + 1);
        }
    }
    private int currentWave;
    // wave

    // player health
    public Text healthLabel;

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthLabel.text = "HEALTH: " + health;
            if (health <= 0)
            {
                SceneManager.LoadScene("lose");

            }
        }
    }


    // objectpool
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

	// Use this for initialization
	void Start () {
        Currency = 300;
        Wave = 0;
        Health = 5;
	}
	
	// Update is called once per frame
	void Update () {
        //HandleException();
        CheckBtnActive();
        CheckVictory();
	}

    private void CheckVictory()
    {
        if (Wave >= waves.Length)
        {
            SceneManager.LoadScene("win");
        }
    }


    public void PickTower(TowerButton towerButton)
    {
        if (Currency >= towerButton.Price)
        {
            // stores the clicked button
            this.ClickedButton = towerButton;

            // Activates the hover icon
            HoverIcon.Instance.Activate(towerButton.Sprite);
        }
       
    }

    public void TowerBought()
    {
        if (Currency >= ClickedButton.Price)
        {
            Currency -= ClickedButton.Price;
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

    //public void StartWave()
    //{
    //    //UIManager.Instance.Wave++;
    //    //StartCoroutine(SpawnWave());
    //    SpawnEnemy.Instance.startnewwave = true;
    //    waveBtn.SetActive(false);
    //}

    private void CheckBtnActive()
    {
        if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Wave++;
            Currency = Mathf.RoundToInt(Currency * 1.5f);
            enemiesSpawned = 0;
            waveBtn.SetActive(true);
        }
    }


    public void StartWave()
    {
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {

        currentWave = Wave;

        if (currentWave < waves.Length)
        {

            float spawnInterval = waves[currentWave].spawnInterval;

            for (int i = 0; i < waves[currentWave].maxEnemies; i++)
            {
                string type = string.Empty;

                int monsterIndex = Random.Range(0, 4);

                switch (monsterIndex)
                {
                    case 0:
                        type = "Enemy 1";
                        break;
                    case 1:
                        type = "Enemy 2";
                        break;
                    case 2:
                        type = "Enemy 3";
                        break;
                    case 3:
                        type = "Enemy 4";
                        break;

                }

                GameObject monster = Pool.GetObject(type);

                waves[currentWave].enemyPrefab = monster;

                GameObject newEnemy = waves[currentWave].enemyPrefab;

                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;

                enemiesSpawned++;

                //MoveEnemy monster = Pool.GetObject(type).GetComponent<MoveEnemy>();
                //monster.waypoints = waypoints;
                //monster.Spawn();
                yield return new WaitForSeconds(spawnInterval);

            }

        }

    }
}

