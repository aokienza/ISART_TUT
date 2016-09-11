using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{

    float ButtonCooler = 0.5f ;
    int ButtonCount = 0;
    bool tapped = false;


    public delegate void TapAction();
    public static event TapAction OnTap;

    public delegate void DoubleTapAction();
    public static event DoubleTapAction OnDoubleTap;

    public static List<Touch> touches;

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
            if(ButtonCooler < 0 && touch.tapCount == 2)
            {
                ButtonCooler = 0.5f;
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
            OnTap();

        if (isDoubleTapping() || Input.GetKeyDown(KeyCode.F))
            OnDoubleTap();

        if (ButtonCooler > 0)
            ButtonCooler -= Time.deltaTime;
    }
}
