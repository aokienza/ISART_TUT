using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float velocity = 1;

    Transform _transform;

    bool _hided = false;
    public bool isHided
    {
        get { return _hided; }
    }

    // Use this for initialization
    void Start () {

        _transform = transform;

        InputHandler.OnTap += Movement;
        InputHandler.OnDoubleTap += Hide;

        _hided = false;
    }


	// Update is called once per frame
	void Update () {

	}

    void Movement()
    {
        foreach (Touch touch in InputHandler.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            Plane plane = new Plane(Vector3.up, _transform.position);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            float distance = 0;
            Vector3 direction = Vector3.zero;
            direction = (ray.GetPoint(distance) - _transform.position).normalized * velocity;

            if (isMovementPossible(direction))
            {
                transform.position = _transform.position + new Vector3(direction.x, 0, direction.z);
            }
        }
    }

    bool isMovementPossible(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, direction, out hit, velocity, 1 << 8))
        {
            if(hit.transform.CompareTag("Wall"))
            {
                return false;
            }
        }
        return true;
           
    }

    void Hide()
    {
        if (_hided)
        {
            transform.position = new Vector3(_transform.position.x, 0.5f, _transform.position.z);
            _hided = false;
        }
        else
        {
            transform.position = new Vector3(_transform.position.x, -100.5f, _transform.position.z);
            _hided = true;
        }
    }

    void Kill()
    {
        InputHandler.OnTap -= Movement;
        InputHandler.OnDoubleTap -= Hide;
    }
}
