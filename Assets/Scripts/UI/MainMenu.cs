using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    GameObject startMenu;


    // Use this for initialization
    void Start() {

    }

    public void OpenLevel()
    {
        // Temporary
        SceneManager.LoadScene("Level01");
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update () {
	
	}
}
