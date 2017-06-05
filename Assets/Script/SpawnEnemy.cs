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

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] waypoints;
    //public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private UIManager uiManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

	// Use this for initialization
	void Start () { 

        //Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;

        lastSpawnTime = Time.time;
        uiManager =
            GameObject.Find("GameManager").GetComponent<UIManager>();

        //Debug.Log(testEnemyPrefab.GetComponent<MoveEnemy>().waypoints);
	
	}
	
	// Update is called once per frame
	void Update () {
        // 1
        int currentWave = uiManager.Wave;
        if (currentWave < waves.Length)
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
                uiManager.Wave++;
                uiManager.Gold = Mathf.RoundToInt(uiManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            // 5 
        }
        else
        {
            uiManager.gameOver = true;
            SceneManager.LoadScene("win");
            //GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            //gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }	
	
	}
}
