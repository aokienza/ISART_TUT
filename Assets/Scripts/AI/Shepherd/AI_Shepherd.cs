using UnityEngine;
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
    public float speed = 20f;
    public float maxRotSpeed = 200.0f;
    public float range;
    public float attackRange;

    int index;
    int layerMask = 1 << 8;
    float minTime = 0.1f;
    float _Velocity;
    Vector3 moveDirection;
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

        attackRange = 2f;//仮の数値

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

        isFind = !player.GetComponent<PlayerController>().isHided;

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
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);

        var newRotation = Quaternion.LookRotation(target.position - _transform.position).eulerAngles;
        var angles = _transform.rotation.eulerAngles;
        _transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle
                                            (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed), angles.z);
    }

    //NextIndexではindexを増大させて配列以外では0をセット

    #region movement function
    void Walk()
    {
        
    }

    void Attack()
    {
        if ((_transform.position - player.position).sqrMagnitude > range)
        {
            Move(player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().Die();
        }
    }
    #endregion
    #region animation functions
    IEnumerator RotateWait()
    {
        yield return new WaitForSeconds(/*animation["RotateWait"].length*/0.5f);
        del = true;
    }

    IEnumerator Wait()
    {
        //animation.CrossFade ("idle");
        yield return new WaitForSeconds(2.0f);
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


