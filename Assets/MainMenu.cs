using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string startLevel;

	// Use this for initialization
	public void StartGame () {
        SceneManager.LoadScene(startLevel);
	}
	
	// Update is called once per frame
	public void QuitGame () {
        Application.Quit();
	}
}
