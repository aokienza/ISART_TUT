using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    GameObject startMenu;
    [SerializeField]
    GameObject gameMenu;
    [SerializeField]
    AudioSource sound;

    public bool rightHanded;
    [SerializeField]
    Button DigButton;
    [SerializeField]
    GameObject recapsScore;
    [SerializeField]
    GameObject PointGauche;
    [SerializeField]
    GameObject PointDroit;
    public InputHandler INPUTS;

    PlayerController playerControls;


    public static MainMenu instance = null;

    public GameObject LoadingScreen;
    public GameObject GameOverButton;
    public GameObject leftHand, rightHand;

    // Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.gameObject);
    }
    private void Start()
    {
        IsRightHanded();
    }

    public void UnPauseLevel()
    {
        GameManager.instance.Pause(false);
    }

    public void PauseLevel()
    {
        GameManager.instance.Pause(true);
    }

    public void ValidateButton()
    {
        sound.Play();
    }

    public void ActivateButtonTap()
    {
        if (playerControls == null)
        {
            playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            ActivateButtonTap();
        }
        else if (playerControls != null)
        {
            playerControls.Hide();
        }

        Debug.Log("Has button taped");
    }
    public void OpenLevel(string LevelName)
    {
        GameManager.instance.LoadScene(LevelName);
        
        if ("MainMenu" == LevelName)
        {
            startMenu.SetActive(true);
            gameMenu.SetActive(false);
        }
    }

    public void IsRightHanded()
    {
        if (!rightHanded)
        {
            rightHanded = false;
            leftHand.SetActive(true);
            rightHand.SetActive(false);
            DigButton.GetComponent<Transform>().position = PointGauche.transform.position;
            recapsScore.GetComponent<Transform>().position = PointDroit.transform.position;
            
        }
        else if (rightHanded)
        {
            rightHanded = true;
            rightHand.SetActive(true);
            leftHand.SetActive(false);
            DigButton.GetComponent<Transform>().position = PointDroit.transform.position;
            recapsScore.GetComponent<Transform>().position = PointGauche.transform.position;
        }
    }
}

