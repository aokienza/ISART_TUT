using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{

    public Text loading;

    public bool gameLoaded;

    void Update()
    {
        if (gameLoaded) this.gameObject.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(TextLoading(1));
    }

    private IEnumerator TextLoading(int prout)
    {   switch(prout)
        {
            case 1:
                loading.text = ("loading.");
                break;
            case 2:
                loading.text = ("loading..");
                break;
            case 3:
                loading.text = ("loading...");
                break;
        }
        yield return new WaitForSeconds(.2f);
        switch (prout)
        {
            case 1:
                StartCoroutine(TextLoading(2));
                break;
            case 2:
                StartCoroutine(TextLoading(3));
                break;
            case 3:
                StartCoroutine(TextLoading(1));
                break;
        }
    }

}
