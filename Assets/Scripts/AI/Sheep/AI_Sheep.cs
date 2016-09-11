using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]//キャラクターコントローラがアタッチされていることを保証

public class AI_Sheep : MonoBehaviour {
	#region variables
	CharacterController _controller;
	Transform _transform;

	GameObject _player;

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

    int rot = 1;
    float rotTimer = 5f;
    float idleTimer = 4f;
    float dist;

    bool isRotate = false;
	#endregion
	#endregion


	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController> ();
		_transform = GetComponent<Transform>();//_controllerでコントローラーを制御
	}


	// Update is called once per frame
	void Update () {
            _player = GameObject.FindGameObjectWithTag("Player");

        if (!isRotate)
        {
            Move();
        }
        else
        {
            _Move();
        }
            Rottimer();

		}

	void Move(){
				moveDirection = _transform.forward;
				moveDirection *= speed;
				moveDirection.y -= gravity * Time.deltaTime;
				_controller.Move (moveDirection * Time.deltaTime);

				var newRotation = Quaternion.LookRotation (_player.transform.position - _transform.position * rot).eulerAngles;
                newRotation.y = newRotation.y + Random.Range(90,270);
				var angles = _transform.rotation.eulerAngles;
				_transform.rotation = Quaternion.Euler (angles.x, Mathf.SmoothDampAngle 
		                               			 (angles.y, newRotation.y, ref _Velocity, minTime, maxRotSpeed) , angles.z);
		        }

    void _Move()
    {
        Debug.Log("_Move");
        moveDirection = _transform.forward;
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;
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
            rotTimer = Random.Range(2, 4);
            if (isRotate) isRotate = false;
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
}
