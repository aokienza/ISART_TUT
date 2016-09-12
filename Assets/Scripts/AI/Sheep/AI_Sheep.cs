﻿using UnityEngine;
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
    float waitTimer = 2f;
    float dist;

    bool isRotate = false;
    bool waiting = false;
    #endregion
    #endregion

    #region AI behaviour
    void CallHelp()
    {
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

    void Move()
    {
		moveDirection = _transform.forward;
		moveDirection *= speed;
		moveDirection.y += Physics.gravity.y * Time.deltaTime;
		_controller.Move (moveDirection * Time.deltaTime);

        if (isRotate)
        {
            var newRotation = Quaternion.LookRotation(_player.position - _transform.position * rot).eulerAngles;
            newRotation.y = newRotation.y + Random.Range(90, 270);
            var angles = _transform.rotation.eulerAngles;
            _transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle
                                                   (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed), angles.z);
        }
        else
        {
            var newRotation = Quaternion.LookRotation(_transform.position * rot).eulerAngles;
            var angles = _transform.rotation.eulerAngles;
            _transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle
                                                (angles.y, newRotation.y + 135, ref _Velocity, minTime, maxRotSpeed), angles.z);
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

    }

    public override void onUpdate()
    {
        base.onUpdate();

        Rottimer();

        if (!waiting)
        {
            Move();
        }
        else
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


    public override void Unregister()
    {
        base.Unregister();
        OnPlayerDetectedStart -= CallHelp;
    }
}
