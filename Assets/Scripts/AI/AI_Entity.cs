using UnityEngine;
using System.Collections;

public class AI_Entity : MonoBehaviour, EventHandler
{

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

    Transform _warningZone;

    protected Animator _animator;
    protected CharacterController _controller;
    protected AudioSource _audioSource;
    protected Transform _transform;
    protected Transform _player;
    protected float minTime = 0.1f;

    #region capacities variables
    protected bool _isReady = false;
    public bool isReady
    {
        get { return _isReady; }
    }
    protected bool Destroying = false;
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
        _warningZone = transform.GetChild(0).GetComponent<Transform>();
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
        _animator = _transform.GetChild(1).GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _warningZone.transform.localScale = new Vector3(detectionRange*0.7f, detectionRange * 0.7f, 0)
;    }

    void Update()
    {
        if(!Destroying)
            onUpdate();
    }

    public virtual void onUpdate()
    {
        if (!_isReady)
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

        if(!Destroying)
         OnDeath(transform);
        Destroying = true;

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
            transform.LookAt(endPos, transform.up);
            transform.position = Vector3.Lerp(startPos, endPos, counter / timer);
            yield return null;
        }

        transform.GetComponent<SphereCollider>().enabled = true;
        _isReady = true;
    }


    protected float playerDistance()
    {
        return Vector3.Distance(_transform.position, _player.transform.position);
    }

    protected bool PlayerDetecteable()
    {
        return (playerDistance() < detectionRange && !_player.GetComponent<PlayerController>().isHided);
    }
    #endregion

    public void OnDestroy()
    {
        Unregister();
    }

    public virtual void Unregister()
    {

    }
}
