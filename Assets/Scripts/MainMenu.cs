using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    public void OpenLevel()
    {
        // Temporary
        SceneManager.LoadScene("Level01");
    }

    // Update is called once per frame
    void Update () {
	
	}
}
