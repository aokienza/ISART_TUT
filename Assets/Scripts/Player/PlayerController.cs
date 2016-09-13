using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour,  EventHandler
{


    public delegate void SheepEated(Transform value);
    public event SheepEated OnSheepEated;

    public float velocity = 20;

    Image UIHideMask;
    Transform _transform;
    Animator _animator;
    AudioSource _audioSource;

    public AudioClip moveSound;

    bool _dead = false;
    bool _hided = false;

    public AudioClip onDeath;
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

        UIHideMask = GameObject.Find("UIHideMask").GetComponent<Image>();

        _hided = false;
    }

    void Movement()
    {
        if (_dead)
            return;
        foreach (Touch touch in InputHandler.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            float distance = 0;
            Vector3 direction = Vector3.zero;
            direction = (ray.GetPoint(distance) - _transform.position).normalized;

            _animator.SetFloat("speed", direction.magnitude * velocity);

            if (isMovementPossible(direction))
            {
                Vector3 movement = new Vector3(direction.x, 0, direction.z) * velocity * 50;
                movement = movement.normalized * velocity * 50 * Time.deltaTime;

                Vector3 targetPos = _transform.position + new Vector3(movement.x, 0, movement.z) * Time.deltaTime;
                transform.LookAt(targetPos);
                transform.position = targetPos;
            }
        }
    }

    public void Cought()
    {
        _dead = true;
        _audioSource.clip = onDeath;
        _audioSource.Play();
        _animator.SetBool("isDie", true);
    }

    void Idle()
    {
        _animator.SetFloat("speed", 0);
    }

    bool isMovementPossible(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, direction, out hit, velocity * 50 * Time.deltaTime, 1 << 8))
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
            UIHideMask.color = new Color(255,255,255,0);
            _hided = false;
        }
        else if (!_hided && !_dead)
        {
            _transform.position = new Vector3(_transform.position.x, -100.5f, _transform.position.z);
            _animator.SetTrigger("isDigDown");
            UIHideMask.color = new Color(255, 255, 255, 255);
            _hided = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sheep") && other.transform.GetComponent<AI_Sheep>().isReady)
        {
            if(OnSheepEated != null)
                OnSheepEated(other.transform);

            _animator.SetTrigger("isEating");
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
