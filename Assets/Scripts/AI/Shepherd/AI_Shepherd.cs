﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]//キャラクターコントローラがアタッチされていることを保証

public class AI_Shepherd : MonoBehaviour
{
    #region variables
    CharacterController _controller;
    Transform _transform;
    [SerializeField]
    Transform player;

    public bool isFind = false;

    [SerializeField]
    public Transform[] _waypoint = new Transform[4];

    #region movement variables
    float speed = 8f;
    float gravity = 20f;
    Vector3 moveDirection;
    float maxRotSpeed = 200.0f;
    float minTime = 0.1f;
    float _Velocity;

    float range;
    float attackRange;
    int index;
    float angle = 90f;
    int layerMask = 1 << 8;


    bool isCorouting;
    #endregion
    #region delegate variable
    delegate void DelFunc();
    delegate IEnumerator DelEnum();
    DelFunc _delFunc;
    DelEnum _delEnum;
    bool del;


    #endregion
    #endregion


    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();//_controllerでコントローラーを制御

        index = 0;//0で始める

        range = 5f;
        attackRange = 2f;//仮の数値

        //animation ["RotateWait"].wrapMode = WrapMode.Once;
        _delFunc = this.Walk;
        _delEnum = null;
        del = true;
        isCorouting = false;

        layerMask = ~layerMask;
    }


    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (!player.GetComponent<PlayerController>().getHide())
        {
            isFind = true;
        }
        else
        {
            isFind = false;
        }

        if (AIFunction() && isCorouting)
        {
            StopAllCoroutines();
            del = true;
        }
        if (del)
        {
            _delFunc();
        }
        else if (!isCorouting)
        {
            isCorouting = true;
            StartCoroutine(_delEnum());
        }
    }

    void Move(Transform target)
    {
        moveDirection = _transform.forward;
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);

        var newRotation = Quaternion.LookRotation(target.position - _transform.position).eulerAngles;
        var angles = _transform.rotation.eulerAngles;
        _transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle
                                            (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed), angles.z);
    }

    //NextIndexではindexを増大させて配列以外では0をセット
    void NextIndex()
    {
        if (++index == _waypoint.Length)
        {
            index = 0;
        }
    }
    #region movement function
    void Walk()
    {
        if ((_transform.position - _waypoint[index].position).sqrMagnitude > range)
        {
            Move(_waypoint[index]);
            //animation.CrossFade("walk");
        }
        else
        {
            switch (index)
            {
                case 0:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 1:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 2:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 3:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 4:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 5:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 6:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                case 7:
                    del = false;
                    isCorouting = false;
                    _delEnum = this.RotateWait;
                    break;
                default:
                    NextIndex();
                    break;
            }
        }
    }

    void Attack()
    {
        if ((_transform.position - player.position).sqrMagnitude > range)
        {
            Move(player);
            //animation.CrossFade("walk");
        }
        else
        {

        }
    }
    #endregion
    #region animation functions
    IEnumerator RotateWait()
    {
        yield return new WaitForSeconds(/*animation["RotateWait"].length*/0.5f);

        NextIndex();
        del = true;
    }

    IEnumerator Wait()
    {
        //animation.CrossFade ("idle");
        yield return new WaitForSeconds(2.0f);
        NextIndex();
        del = true;
    }
    #endregion
    #region AI function
    bool AIFunction()
    {
        if(isFind)            
            {
                _delFunc = this.Attack;
                return true;

            }
            else
            {
                _delFunc = this.Walk;
                return false;
            }

    } 
}
#endregion

