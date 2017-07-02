using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour {

	public void LoadScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string SceneName = currentScene.name;
        SceneManager.LoadScene(sceneName);
        //if(SceneName == "firescene")
        //{
        //    GetComponent<AudioSource>().Stop();
        //}
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
