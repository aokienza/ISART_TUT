using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {


    public delegate void SheepEated(Transform value);
    public event SheepEated OnSheepEated;

    public delegate void PlayerDeath(Transform player);
    public event PlayerDeath OnPlayerDeath;

    public float velocity = 20;

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

        GameManager.instance.OnGameEnd += Uninstanciate;

        _hided = false;
    }


	// Update is called once per frame
	void Update () {

    }

    void Movement()
    {
        foreach (Touch touch in InputHandler.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            float distance = 0;
            Vector3 direction = Vector3.zero;
            direction = (ray.GetPoint(distance) - _transform.position).normalized * Time.deltaTime;

            if (isMovementPossible(direction))
            {
                transform.position = _transform.position + new Vector3(direction.x, 0, direction.z) * velocity * 50;
            }
        }
    }

    bool isMovementPossible(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, direction, out hit, velocity * 50, 1 << 8))
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
            _transform.position = new Vector3(_transform.position.x, 0.5f, _transform.position.z);
            _hided = false;
        }
        else
        {
            _transform.position = new Vector3(_transform.position.x, -100.5f, _transform.position.z);
            _hided = true;
        }
    }

    void Kill()
    {
        InputHandler.OnTap -= Movement;
        InputHandler.OnDoubleTap -= Hide;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sheep") && other.transform.GetComponent<AI_Sheep>().isReady)
        {
            if(OnSheepEated != null)
                OnSheepEated(other.transform);
            other.transform.GetComponent<AI_Sheep>().Death();
        }
    }

    public void Death()
    {
        OnPlayerDeath(_transform);
    }

    public void Uninstanciate()
    {
        GameManager.instance.OnGameEnd -= Uninstanciate;
    }
}
