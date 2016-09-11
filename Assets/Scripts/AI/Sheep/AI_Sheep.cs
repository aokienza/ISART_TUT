using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]//キャラクターコントローラがアタッチされていることを保証

public class AI_Sheep : MonoBehaviour {
	#region variables
	CharacterController _controller;
	Transform _transform;

	GameObject _player;

	#region movement variables
	public float speed = 2f;
    public float Maxspeed = 18f;
    float Minspeed;
    public float range;
    public float maxRotSpeed = 200.0f;

    Vector3 moveDirection;
    float minTime = 0.1f;
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


	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController> ();
		_transform = GetComponent<Transform>();//_controllerでコントローラーを制御
        Minspeed = speed;
	}


    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Rottimer();

        if (!waiting)
        {
            if (!isRotate)
            {
                if (_player.GetComponent<PlayerController>().isHided) return;
                Move();
            }
            else
            {
                _Move();
            }
        }
        else
        {
            Waittimer();
        }

		}

	void Move(){
        if (speed < Maxspeed) speed = speed + (Time.deltaTime*(Maxspeed - Minspeed));
				moveDirection = _transform.forward;
				moveDirection *= speed;
				moveDirection.y += Physics.gravity.y * Time.deltaTime;
				_controller.Move (moveDirection * Time.deltaTime);

				var newRotation = Quaternion.LookRotation (_player.transform.position - _transform.position * rot).eulerAngles;
                newRotation.y = newRotation.y + Random.Range(90,270);
				var angles = _transform.rotation.eulerAngles;
				_transform.rotation = Quaternion.Euler (angles.x, Mathf.SmoothDampAngle 
		                               			 (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed) , angles.z);
		        }

    void _Move()
    {
        if (speed < Maxspeed) speed = speed + (Time.deltaTime * (Maxspeed - Minspeed));
        moveDirection = _transform.forward;
        moveDirection *= speed;
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);

        var newRotation = Quaternion.LookRotation( _transform.position * rot).eulerAngles;
        var angles = _transform.rotation.eulerAngles;
        _transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle
                                            (angles.y, newRotation.y + 135, ref _Velocity, minTime, maxRotSpeed), angles.z);
    }
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
            speed = Minspeed;
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // hit.gameObjectで衝突したオブジェクト情報が得られる
        if(hit.gameObject.tag == "Wall")
        { 
            isRotate = true;
        } 
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
