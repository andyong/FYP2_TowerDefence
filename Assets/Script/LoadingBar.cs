using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {

    AsyncOperation ao;
    public Slider progBar;

    public float fakeIncrement = 0f;
    public float fakeTiming = 0f;

    public bool isFakeLoadingBar;

    [SerializeField]
    private Text loadingtext;

    [SerializeField]
    private string loadingscene;


	// Use this for initialization
	void Start () {
        RandomLoadingText();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isFakeLoadingBar)
            StartCoroutine(LoadLevelWithRealProgress());
        else
            StartCoroutine(LoadLevelWithFakeProgress());
	
	}

    IEnumerator LoadLevelWithRealProgress()
    {
        yield return new WaitForSeconds(1);

        ao = SceneManager.LoadSceneAsync(8);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            progBar.value = ao.progress;

            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;

            }

            Debug.Log(ao.progress);
            yield return null;
        }
    }

    IEnumerator LoadLevelWithFakeProgress()
    {
        yield return new WaitForSeconds(1);

        while (progBar.value != 1f)
        {
            progBar.value += fakeIncrement;
            yield return new WaitForSeconds(fakeTiming);
        }

        while (progBar.value == 1f)
        {
            SceneManager.LoadScene(loadingscene);
            yield return null;
        }

    }

    void RandomLoadingText()
    {
        int random = Random.Range(0,8);

        switch (random)
        {
            case 0:
                {
                    loadingtext.text = "Enemies have extra 50%\n health in <color=#BD0101FF>Fire Level</color>";
                }
                break;
            case 1:
                {
                    loadingtext.text = "Tower have 1.5 seconds increased\n cooldown in <color=#1119B9FF>Frost Level</color>";
                }
                break;
            case 2:
                {
                    loadingtext.text = "Tower have 30% decreased\n damage in <color=#000000FF>Dark Level</color>";
                }
                break;
            case 3:
                {
                    loadingtext.text = "Enemies have 30% increased\n movement speed in <color=#A58315FF>Wind Level</color>";
                }
                break;
            case 4:
                {
                    loadingtext.text = "Enemies will increase\n <color=#BD0101FF>20% health</color> every wave!";
                }
                break;
            case 5:
                {
                    loadingtext.text = "Green slimes have <color=#A58315FF>fastest speed</color>\n but <color=#BD0101FF>lowest health</color>";
                }
                break;
            case 6:
                {
                    loadingtext.text = "Cyclops have <color=#BD0101FF>highest health!</color>";
                }
                break;
            case 7:
                {
                    loadingtext.text = "Complete each wave and gain <color=#D7EA25FF>extra Coin!</color>";
                }
                break;
            case 8:
                {
                    loadingtext.text = "Build <color=#1119B9FF>Frost Tower</color> to counter green slimes!";
                }
                break;
            default:
                break;
        }
    }
}
