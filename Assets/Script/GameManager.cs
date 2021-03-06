﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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

    private HealthBar[] enemyHealth;

    public List<GameObject> enemyList;

    public GameObject[] waypoints;
    //public GameObject testEnemyPrefab;

    public WaveClass[] waves;
    public int timeBetweenWaves = 5;

    [SerializeField]
    private GameObject startTile;

    [SerializeField]
    private GameObject endTile;

    [SerializeField]
    private GameObject startWaypoint;

    [SerializeField]
    private GameObject endWaypoint;

    private bool isScale = false;

    public int enemiesSpawned
    {
        get;
        set;
    }

    public TowerButton ClickedButton { get; private set; }

    //current selected tower
    private Tower selectedTower;
    
    public GameObject waveBtn;

    // currency
    [SerializeField]
    private int currency;

    [SerializeField]
    private Text currencyTxt;
    // currency

    [SerializeField]
    private GameObject statsPanel;

    [SerializeField]
    private Text statText;

    [SerializeField]
    private GameObject effectsPanel; //lvl effect

    [SerializeField]
    private Text effectText;

    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private Text sellText;

    [SerializeField]
    private Text upgradePrice;

    [SerializeField]
    private Image pausedImage;
    // wave
    public Text waveLabel;

    int index_ = 0;

    // player health
    public Text healthLabel;

    public int Currency
    {
        get { return currency; }
        set
        {
            this.currency = value;
            this.currencyTxt.text =value.ToString();
            OnCurrencyChanged();
        }
    }

    private int wave;

    public int Wave
    {
        get { return wave; }
        set
        {
            this.wave = value;
            this.waveLabel.text =(wave) + "/" + waves.Length;
        }
    }
    private int currentWave;
    // wave

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthLabel.text = " " + health;
            if (health <= 0)
            {
                SceneManager.LoadScene("lose");

            }
        }
    }

    private bool paused;

    public bool Paused
    {
        get { return paused; }
    }


    // objectpool
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

	// Use this for initialization
	void Start () {
        //Currency = 500;
        Wave = 0;
        Health = 15;
        enemyList = new List<GameObject>();

        startTile.transform.position = startWaypoint.transform.position;
        endTile.transform.position = endWaypoint.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        //HandleException();
        CheckBtnActive();
        CheckVictory();
        waveBtnScale();
       
	}

    private void CheckVictory()
    {
        if (Wave == waves.Length && GameObject.FindGameObjectWithTag("Enemy")==null)
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

    public void SelectTower(Tower tower)
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();

        sellText.text = "+ $" + (selectedTower.Price / 2).ToString();
        upgradePanel.SetActive(true);
    }

    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = null;
        upgradePanel.SetActive(false);
    }
    ////can't build towers etc
    //private void HandleException()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        HoverIcon.Instance.Deactivate();
    //    }
    //}

    public void ShowLvlEffect()
    {
        effectsPanel.SetActive(!effectsPanel.activeSelf);
    }

    public void ShowLvlInfo(string lvleffect)
    {
        string tooltip = string.Empty;

        switch(lvleffect)
        {
            case "Fire":
                tooltip = string.Format("<color=#E0FFFF><size=12><b>Fire:</b> Enemies have extra 50% health</size></color>");
                break;
            case "Water":
                tooltip = string.Format("<color=#E0FFFF><size=12><b>Water:</b> Towers have 1.5 secs increased cooldown</size></color>");
                break;
            case "Dark":
                tooltip = string.Format("<color=#E0FFFF><size=12><b>Dark:</b> Towers have 30% decreased damage</size></color>");
                break;
            case "Wind":
                tooltip = string.Format("<color=#E0FFFF><size=12><b>Wind:</b> Enemies have 30% increased \nmovement speed</size></color>");
                break;

        }
        SetEffectTooltipText(tooltip);
        ShowLvlEffect();
    }

    public void ShowTowerStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void ShowSelectedTowerStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);  
        UpdateUpgradeTip();
    }

    public void SetTooltipText(string text)
    {
        statText.text = text;
    }

    public void SetEffectTooltipText(string text)
    {
        effectText.text = text;
    }

    public void UpdateUpgradeTip()
    {
        if (selectedTower != null)
        {
            sellText.text = "+ $" + (selectedTower.Price / 2).ToString();
            SetTooltipText(selectedTower.GetStats());

            if(selectedTower.NextUpgrade != null)
            {
                upgradePrice.text = "- $" + selectedTower.NextUpgrade.Price.ToString();
            }
            else
            {
                upgradePrice.text = string.Empty;
            }
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
        if (enemiesSpawned == waves[currentWave].maxEnemies)
        {
            enemiesSpawned = 0;
            waveBtn.SetActive(true);
        }
    }

    private void waveBtnScale()
    {
        if(!isScale)
        {
            if (waveBtn.transform.localScale.x <= 0.4 && waveBtn.transform.localScale.y <= 0.8 && waveBtn.transform.localScale.z <= 0.4)
            {
                isScale = true;
            }
            else
                waveBtn.transform.localScale = waveBtn.transform.localScale - new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

        }
       
        if(isScale)
        {
            if (waveBtn.transform.localScale.x >=0.5 && waveBtn.transform.localScale.y >= 1.0 && waveBtn.transform.localScale.z >= 0.5)
            {
                isScale = false;
            }
            else
                waveBtn.transform.localScale = waveBtn.transform.localScale + new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
        }

    }


    public void StartWave()
    {
        StartCoroutine(SpawnWave());
        SpawnEnemy.Instance.startnewwave = true;
        waveBtn.SetActive(false);
        int _random = (int)Random.Range(80, 150);
        Wave++;
        if (Wave != 1)
        {
            Currency += _random;


        }
            
    }

    private IEnumerator SpawnWave()
    {

        currentWave = Wave;
        Debug.Log(currentWave);

        if (currentWave < waves.Length - 1)
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

                newEnemy.GetComponent<MoveEnemy>().Spawn();

                enemiesSpawned++;

                enemyList.Add(newEnemy);

                // increase enemy health when wave increased
                if (currentWave + 1 > 1)
                {
                    Transform temp = newEnemy.transform.FindChild("HealthBar");
                    HealthBar health_bar = temp.gameObject.GetComponent<HealthBar>();
                    health_bar.maxHealth += health_bar.maxHealth * (currentWave + 1) * 0.2f;
                    health_bar.currentHealth += health_bar.currentHealth * (currentWave + 1) * 0.2f;
                }

                //index_ = enemyList.IndexOf(newEnemy);

                levelEffect(i);

                //GameObject healthBar = waves[currentWave].enemyPrefab;
                //enemyHealth[i] = healthBar.gameObject.GetComponent<HealthBar>();
                //enemyHealth[i].maxHealth += 50;
                //enemyHealth[i].currentHealth += 50;


                //MoveEnemy monster = Pool.GetObject(type).GetComponent<MoveEnemy>();
                //monster.waypoints = waypoints;
                //monster.Spawn();
                yield return new WaitForSeconds(spawnInterval);

            }

            enemyList.Clear();
        }

        else if(currentWave == waves.Length-1)
        {
            GameObject boss = Instantiate(waves[currentWave].enemyPrefab);
            boss.GetComponent<MoveEnemy>().waypoints = waypoints;
            boss.GetComponent<MoveEnemy>().Spawn();
            SoundManager.Instance.PlaySFX("boss");

        }

    }

    public void SellTower()
    {
        if(selectedTower != null)
        {
            Currency += selectedTower.Price / 2;

            selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;

            Destroy(selectedTower.transform.parent.gameObject);
            
            DeselectTower();
        }
        //else
        //{
            
        //}
    }

    public void UpgradeTower()
    {
        if(selectedTower !=null)
        {
            if(selectedTower.Level <= selectedTower.Upgrades.Length && Currency >= selectedTower.NextUpgrade.Price)
            {
                selectedTower.Upgrade();
            }
        }
    }

    private void levelEffect(int i)
    {

            if (LevelManager.Instance.firescene)
            {
                Debug.Log("+enemyHP");
                GameObject o = enemyList[i];
                Transform temp = o.transform.FindChild("HealthBar");
                HealthBar health_bar = temp.gameObject.GetComponent<HealthBar>();
                health_bar.maxHealth += health_bar.maxHealth * 0.5f;
                health_bar.currentHealth += health_bar.currentHealth * 0.5f;

            }

            else if (LevelManager.Instance.windscene)
            {
                Debug.Log("+enemySpeed");
                Debug.Log(i);
                GameObject o = enemyList[i];
                MoveEnemy enemies = o.GetComponent<MoveEnemy>();
                enemies.Speed += enemies.Speed * 0.3f;
            }

    }

    public void PauseGame()
    {
        paused = !paused;

        if(paused)
        {
            Time.timeScale = 0;
            pausedImage.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausedImage.gameObject.SetActive(false);
        }
    }
}

