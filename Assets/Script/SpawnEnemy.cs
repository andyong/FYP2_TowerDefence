using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : Singleton<SpawnEnemy>{

    public GameObject[] waypoints;
    //public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    public bool startnewwave = false;

	// Use this for initialization
	void Start () { 

        //Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
        
        lastSpawnTime = Time.time;
        //LevelManager.Instance.CreateLevel();
        //Debug.Log(testEnemyPrefab.GetComponent<MoveEnemy>().waypoints);
	
	}
	
	// Update is called once per frame
	void Update () {
        // 1
        int currentWave = UIManager.Instance.Wave;
        if (currentWave < waves.Length)
        {
            if (startnewwave)
            {
                // 2
                float timeInterval = Time.time - lastSpawnTime;
                float spawnInterval = waves[currentWave].spawnInterval;
                if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                     timeInterval > spawnInterval) &&
                    enemiesSpawned < waves[currentWave].maxEnemies)
                {
                    // 3  
                    lastSpawnTime = Time.time;
                    GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
                    newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                    enemiesSpawned++;
                }
                // 4 
                if (enemiesSpawned == waves[currentWave].maxEnemies &&
                    GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    UIManager.Instance.Wave++;
                    startnewwave = false;
                    GameManager.Instance.waveBtn.SetActive(true);
                    if(UIManager.Instance.Wave == 3)
                    {
                        SceneManager.LoadScene("darkscene");
                    }
                    else if (UIManager.Instance.Wave == 6)
                    {
                        SceneManager.LoadScene("waterscene");
                    }
                    else if (UIManager.Instance.Wave == 9)
                    {
                        SceneManager.LoadScene("windscene");
                    }
                   
                    enemiesSpawned = 0;
                    lastSpawnTime = Time.time;
                }
            }
        }
        else
        {
            UIManager.Instance.gameOver = true;
            SceneManager.LoadScene("win");
        }	
	
	}
}
