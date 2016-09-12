using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour,  EventHandler
{


    public delegate void SheepEated(Transform value);
    public event SheepEated OnSheepEated;

    public delegate void PlayerDeath(Transform player);
    public event PlayerDeath OnPlayerDeath;

    public float velocity = 20;

    Transform _transform;
    Animator _animator;
    AudioSource _audioSource;

    public AudioClip moveSound;

    bool _dead = false;
    bool _hided = false;
    public bool isHided
    {
        get { return _hided; }
    }

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
        _transform = transform;
        _animator = _transform.GetChild(0).GetComponent<Animator>();
        InputHandler.instance.OnTap += Movement;
        InputHandler.instance.OnDoubleTap += Hide;
        InputHandler.instance.StopTap += Idle;
        _hided = false;
    }

    void Movement()
    {
        foreach (Touch touch in InputHandler.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            float distance = 0;
            Vector3 direction = Vector3.zero;
            direction = (ray.GetPoint(distance) - _transform.position).normalized * Time.deltaTime;
            Debug.Log(direction.magnitude * velocity);
            _animator.SetFloat("speed", direction.magnitude * velocity);
            if (isMovementPossible(direction))
            {
                Vector3 targetPos = _transform.position + new Vector3(direction.x, 0, direction.z) * velocity * 50;

                transform.LookAt(targetPos);
                transform.position = _transform.position + new Vector3(direction.x, 0, direction.z) * velocity * 50;
            }
        }
    }

    public void Cought()
    {
        _dead = true;
        _animator.SetBool("isDie", true);
    }

    void Idle()
    {
        _animator.SetFloat("speed", 0);
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
        if (_hided && !_dead)
        {
            _transform.position = new Vector3(_transform.position.x, 0.5f, _transform.position.z);
            _animator.SetTrigger("isDigUp");
            _hided = false;
        }
        else if (!_hided && !_dead)
        {
            _transform.position = new Vector3(_transform.position.x, -100.5f, _transform.position.z);
            _animator.SetTrigger("isDigDown");
            _hided = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sheep") && other.transform.GetComponent<AI_Sheep>().isReady)
        {
            if(OnSheepEated != null)
                OnSheepEated(other.transform);

            _animator.SetBool("isEat", _hided);
            other.transform.GetComponent<AI_Sheep>().Death();
        }
    }


    public void OnDestroy()
    {
        Unregister();
    }

    public void Unregister()
    {
        InputHandler.instance.OnTap -= Movement;
        InputHandler.instance.OnDoubleTap -= Hide;
        InputHandler.instance.StopTap -= Idle;
        Destroy(this);
    }
}
