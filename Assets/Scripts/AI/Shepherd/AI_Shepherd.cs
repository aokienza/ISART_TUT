using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]//キャラクターコントローラがアタッチされていることを保証

public class AI_Shepherd : AI_Entity
{
    #region movement variables
    int layerMask = 1 << 8;

    float _Velocity;

    Vector3 moveDirection;
    Vector3 rescuePosition;
    #endregion

    #region delegate variable

    delegate void Action();
    Action action;

    #endregion

    //NextIndexではindexを増大させて配列以外では0をセット

    #region AI movements
    void Move(Vector3 targetPos)
    {
        moveDirection = _transform.forward;
        moveDirection *= speed;
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);

        var newRotation = Quaternion.LookRotation(targetPos - _transform.position).eulerAngles;
        var angles = _transform.rotation.eulerAngles;
        _transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle
                                            (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed), angles.z);
    }

    void Walk()
    {

    }

    void Rescue()
    {
        if (Vector3.Distance(_transform.position , rescuePosition) > 5f)
        {
            Move(rescuePosition);
        }
        else
        {
            WalkAction();
        }
    }

    void Attack()
    {
        if (Vector3.Distance(_transform.position , _player.position) > 0f)
        {
            Move(_player.position);
        }
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
    }

    #endregion

    #region Class inherited
    public override void onStart()
    {
        base.onStart();

        action = Walk;

        OnPlayerDetectedStart += Attack;
        OnPlayerDetectedStay += Attack;
        OnPlayerDetectedEnd += Walk;

        layerMask = ~layerMask;
    }

    public override void onUpdate()
    {
        base.onUpdate();
        Debug.Log(action != Attack);
        action();
    }

    public override void Death()
    {
        base.Death();
        OnPlayerDetectedStart -= Attack;
        OnPlayerDetectedStay -= Attack;
        OnPlayerDetectedEnd -= Walk;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().Death();
        }
    }
    #endregion
}


