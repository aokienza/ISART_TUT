using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour, EventHandler
{
    public static InputHandler instance = null;
    float ButtonCooler = 0.5f ;
    int ButtonCount = 0;
    bool tapped = false;


    public delegate void TapAction();
    public event TapAction OnTap;

    public delegate void UntapAction();
    public event UntapAction StopTap;

    public delegate void DoubleTapAction();
    public event DoubleTapAction OnDoubleTap;

    public static List<Touch> touches;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    public static bool isTouching()
    {
        List<Touch> touches = InputHandler.touches;
        return (touches.Count > 0);
    }

    public bool isDoubleTapping()
    {
        List<Touch> touches = InputHandler.touches;
        foreach (Touch touch in touches)
        {
            if(ButtonCooler < 0 && touch.tapCount >= 2)
            {
                ButtonCooler = 0.6f;
                return true;
            }
        }
        return false;
    }

    public static List<Touch> GetTouches()
    {
        return InputHelper.GetTouches();
    }

    void Update()
    {
        touches = InputHelper.GetTouches();

        if (isTouching())
        {
            tapped = true;
            if (OnTap != null)
                OnTap();
        }
        else if (tapped && !isTouching())
        {
            tapped = false;
            if (StopTap != null)
                StopTap();
        }

        if (isDoubleTapping() || Input.GetKeyDown(KeyCode.F))
            if (OnDoubleTap != null)
                OnDoubleTap();

        if (ButtonCooler > 0)
            ButtonCooler -= Time.deltaTime;
    }

    void onDestroy()
    {
        Unregister();
    }


    public void OnDestroy()
    {
        Unregister();
    }

    public virtual void Unregister()
    {
        OnDoubleTap = null;
        OnTap = null;
    }
}
