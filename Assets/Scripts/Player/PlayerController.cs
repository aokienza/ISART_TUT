using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float velocity = 1;

    private bool hide = false;
    // Use this for initialization
    void Start () {

        InputHandler.OnTap += Movement;
        InputHandler.OnDoubleTap += Hide;

        hide = false;
    }


	// Update is called once per frame
	void Update () {

	}

    void Movement()
    {
        foreach (Touch touch in InputHandler.touches)
        {
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                Plane plane = new Plane(Vector3.up, transform.position);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                float distance = 0;
                Vector3 direction = Vector3.zero;
                if (plane.Raycast(ray, out distance))
                {
                    direction = (ray.GetPoint(distance) - transform.position).normalized * velocity;
                }
                transform.position = transform.position + new Vector3(direction.x, 0, direction.z);
            }
        }
    }

    void Hide()
    {
        if (hide)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            hide = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -20.5f, transform.position.z);
            hide = true;
        }
    }

    void Kill()
    {
        InputHandler.OnTap -= Movement;
        InputHandler.OnDoubleTap -= Hide;
    }
}
