using UnityEngine;
using System.Collections;

public class AI_Entity : MonoBehaviour {

    public delegate void DeathAction(Transform transform);
    public event DeathAction OnDeath;

    public delegate void PlayerDetectionStartAction();
    public event PlayerDetectionStartAction OnPlayerDetectedStart;

    public delegate void PlayerDetectionStayAction();
    public event PlayerDetectionStayAction OnPlayerDetectedStay;

    public delegate void PlayerDetectionEndAction();
    public event PlayerDetectionEndAction OnPlayerDetectedEnd;

    #region capacities variables
    public float detectionRange = 20f;
    protected bool playerDetected = false;
    #endregion

    protected CharacterController _controller;
    protected Transform _transform;
    protected Transform _player;
    protected float minTime = 0.1f;

    #region capacities variables
    protected bool isReady = false;
    #endregion

    #region movement variables
    public float speed = 2f;
    public float maxRotSpeed = 200.0f;

    #endregion

    #region Class inherited
    void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        onUpdate();
    }

    public virtual void onUpdate()
    {
        if (!isReady)
            return;

        PlayerDetectionDispatcher();
    }

    private void PlayerDetectionDispatcher()
    {
        if (PlayerDetecteable())
        {
            if (playerDetected)
            {
                if (OnPlayerDetectedStay != null)
                    OnPlayerDetectedStay();
            }
            else
            {
                playerDetected = true;
                Debug.Log("Ici");
                if (OnPlayerDetectedStart != null)
                    OnPlayerDetectedStart();
            }
        }
        else if (!PlayerDetecteable() && playerDetected)
        {
            if(OnPlayerDetectedEnd != null)
                OnPlayerDetectedEnd();
            playerDetected = false;
        }
    }

    public virtual void Death()
    {
        OnDeath(transform);
        Destroy(gameObject);
    }
    #endregion

    #region AI Functions
    public IEnumerator GetOnSpot(Vector3 endPos)
    {
        transform.GetComponent<SphereCollider>().enabled = false;
        float timer = 2f;
        float counter = 0f;
        Vector3 startPos = transform.position;

        while (counter < timer)
        {
            counter += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, counter / timer);
            yield return null;
        }

        transform.GetComponent<SphereCollider>().enabled = true;
        isReady = true;
    }


    protected float playerDistance()
    {
        return Vector3.Distance(_transform.position, _player.transform. position);
    }

    protected bool PlayerDetecteable()
    {
        return (playerDistance() < detectionRange && !_player.GetComponent<PlayerController>().isHided);
    }

    #endregion
}
