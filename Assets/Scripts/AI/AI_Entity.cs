using UnityEngine;
using System.Collections;

public class AI_Entity : MonoBehaviour {

    protected CharacterController _controller;
    protected Transform _transform;
    protected GameObject _player;
    protected float minTime = 0.1f;

    protected bool isReady = false;
    #region movement variables
    public float speed = 2f;
    public float range;
    public float maxRotSpeed = 200.0f;

    #endregion

    void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator GetOnSpot(Vector3 endPos)
    {
        float timer = 2f;
        float counter = 0f;
        Vector3 startPos = transform.position;

        while (counter < timer)
        {
            counter += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, counter / timer);
            yield return null;
        }

        isReady = true;
    }

    // Update is called once per frame
    void Update () {
        onUpdate();
    }

    public virtual void onUpdate()
    {
        if (!isReady)
        {
            return;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
