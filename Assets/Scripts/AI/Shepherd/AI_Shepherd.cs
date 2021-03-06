﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]//キャラクターコントローラがアタッチされていることを保証

public class AI_Shepherd : AI_Entity
{
    #region movement variables
    int layerMask = 1 << 8;

    float _Velocity;

    Vector3 moveDirection;
    Vector3 rescuePosition;
    Vector3 patrolPosition;
    #endregion

    #region delegate variable

    delegate void Action();
    Action action;

    #endregion
    public AudioClip OnSpotSound;

    #region AI movements
    void Move(Vector3 targetPos)
    {
        moveDirection = _transform.forward;
        moveDirection *= speed;
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);

        var newRotation = Quaternion.LookRotation(targetPos - _transform.position).eulerAngles;
        var angles = _transform.rotation.eulerAngles;
        _transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle
                                            (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed), angles.z);
    }

    void Detection()
    {        
        if (!_playerObject.GetComponent<PlayerController>()._dead)
        {
            _audioSource.clip = OnSpotSound;
            _audioSource.Play();
        }
        
    }

    void Walk()
    {
        _animator.SetBool("isMoving", true);
        if (Vector3.Distance(_transform.position, patrolPosition) > 5f)
            Move(patrolPosition);
        else
            patrolPosition = LevelManager.instance.getRandomPositionOnStage();
    }

    void Rescue()
    {
        _animator.SetBool("isMoving", true);
        if (Vector3.Distance(_transform.position , rescuePosition) > 5f)
            Move(rescuePosition); 
        else
            WalkAction();
    }

    void Attack()
    {
        if (Vector3.Distance(_transform.position , _player.position) > 0f)
            Move(_player.position);   
    }

    #endregion
    #region animation functions
    IEnumerator RotateWait()
    {
        yield return new WaitForSeconds(/*animation["RotateWait"].length*/0.5f);
    }

    IEnumerator Wait()
    {
        //animation.CrossFade ("idle");
        yield return new WaitForSeconds(2.0f);
    }
    #endregion

    #region AI behaviour

    public void RescueAction(Transform target)
    {
        rescuePosition = target.position;
        if(action != Attack)
            action = Rescue;
    }

    void AttackAction()
    {
        action = Attack;
    }

    void WalkAction()
    {
        action = Walk;
        patrolPosition = LevelManager.instance.getRandomPositionOnStage();
    }

    #endregion

    #region Class inherited
    public override void onStart()
    {
        base.onStart();

        action = Walk;

        OnPlayerDetectedStart += Detection;
        OnPlayerDetectedStay += AttackAction;
        OnPlayerDetectedEnd += WalkAction;

        layerMask = ~layerMask;
    }

    public override void onUpdate()
    {
        base.onUpdate();
        action();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && isReady)
        {
            _animator.SetTrigger("isAttaking");
            _animator.SetBool("isMoving", false);
            other.transform.GetComponent<PlayerController>().Cought();
            LevelManager.instance.PlayerCought();
            enabled = false;
        }
    }
    #endregion

    public override void Unregister()
    {
        base.Unregister();
        OnPlayerDetectedStart -= Detection;
        OnPlayerDetectedStay -= AttackAction;
        OnPlayerDetectedEnd -= WalkAction;
    }
}


