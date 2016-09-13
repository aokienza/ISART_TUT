using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]//キャラクターコントローラがアタッチされていることを保証

public class AI_Sheep : AI_Entity, EventHandler
{
    #region variables
    #region movement variables

    Vector3 moveDirection;
	float _Velocity;
    int index;
    int rot = 1;
    float rotTimer = 5f;
    float waitTimer = 1f;
    float dist;
    float fleeSpeed = 0;
    float maxFleedSpeed = 1f;
    bool isRotate = false;
    bool waiting = false;

    float gravity = -9.8f;

    public bool isJumping = false;
    float Jumppower = 100;

    public GameObject FXdeath;
    ScoreManager score;

    public AudioClip[] SheepSound;
    #endregion
    #endregion

    void Start()
    {
        onStart();
    }

    #region AI behaviour
    void CallHelp()
    {

        fleeSpeed = 0;
        GameObject[] shepherds = GameObject.FindGameObjectsWithTag("Shepherd");
        float shorterDistance = -1;
        GameObject shorterShepherds = null;
        for (var i = 0; i < shepherds.Length; i++)
        {
            float dist = Vector3.Distance(shepherds[i].transform.position, transform.position);
            if (shorterDistance == -1 || dist < shorterDistance)
            {
                shorterShepherds = shepherds[i];
            }
        }

        if(shorterShepherds != null)
        {
            shorterShepherds.GetComponent<AI_Shepherd>().RescueAction(transform);
        }
    }

    void Flee()
    {
        if (fleeSpeed < maxFleedSpeed)
            fleeSpeed += 0.075f;

        float y = _transform.position.y;
        Vector3 direction = _transform.position - _player.transform.position;
        Vector3 targetPos = _transform.position + new Vector3(direction.x, 0, direction.z) * fleeSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
        _controller.Move(direction *  Time.deltaTime * fleeSpeed);
        _transform.position = new Vector3(_transform.position.x, y, _transform.position.z);
    }

    void Move()
    {
        _animator.SetBool("isMoving", true);
        moveDirection = _transform.forward;
		moveDirection *= speed;
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().isHided || waiting)
        {
            _animator.SetBool("isMoving", false);
            moveDirection.x = 0;
            moveDirection.z = 0;
        }
        moveDirection.y += gravity * 70 * Time.deltaTime;

        _controller.Move (moveDirection * Time.deltaTime);

        if (isRotate)
        {
            var newRotation = Quaternion.LookRotation(_player.position - _transform.position * rot).eulerAngles;
            newRotation.y = newRotation.y + Random.Range(90, 270);
            var angles = _transform.rotation.eulerAngles;
            _transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle
                                                   (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed), 0);
        }
        else
        {
            var newRotation = Quaternion.LookRotation(_transform.position * rot).eulerAngles;
            var angles = _transform.rotation.eulerAngles;
            _transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle
                                                (angles.y, newRotation.y + 135, ref _Velocity, minTime, maxRotSpeed), 0);
        }

 
    }
   

    #endregion

    #region AI movement
    void Rottimer()
    {
        rotTimer -= Time.deltaTime;
        if (rotTimer < 0)
        {
            rot *= -1;
            rotTimer = Random.Range(1, 4);
            if (isRotate) isRotate = false;
            waiting = true;
        }
    }

    void Waittimer()
    {
        _animator.SetBool("isMoving", false);
        waitTimer -= Time.deltaTime;
        if (waitTimer < 0)
        {
            waitTimer = 1.0f;
            waiting = false;
        }
    }
    #endregion
    #region Class inherited
    public override void onStart()
    {
        base.onStart();
        OnPlayerDetectedStart += CallHelp;
        OnPlayerDetectedStay += Flee;
        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void trySheepSound()
    {
        if(Random.Range(0f,2048f) <= 2)
        {
            doSheepSound();
        }
    }

    void doSheepSound()
    {
        AudioClip sound = SheepSound[Random.Range(0, SheepSound.Length)];
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    public override void onUpdate()
    {
        base.onUpdate();
        trySheepSound();
        Rottimer();
        Move();

        if (waiting)
        {
            Waittimer();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Wall")
        {
            isRotate = true;
        }
    }
    #endregion

    public override void Death()
    {
        _animator.SetBool("isDead", true);
        Instantiate(FXdeath, _transform.position, Quaternion.identity);
        score.AddScore();
        base.Death();
    }

    public override void Unregister()
    {
        base.Unregister();
        OnPlayerDetectedStart -= CallHelp;
        OnPlayerDetectedStay -= Flee;
    }
}
