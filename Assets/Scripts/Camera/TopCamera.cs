using UnityEngine;
using System.Collections;

public class TopCamera : MonoBehaviour, EventHandler
{

    LevelManager _level;
    Transform target;

    public float height = 20f;

    Vector3 offset;
	// Use this for initialization
	void Awake ()
    {
        _level = (LevelManager)FindObjectOfType(typeof(LevelManager));
        _level.OnPlayerSpawn += SetTarget;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(target != null)
            Recenter();
	}

    void SetTarget(Transform value)
    {
        target = value;
        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.LookAt(target);
    }

    void Recenter()
    {
        transform.position = Vector3.Lerp(new Vector3(target.position.x, target.position.y + height, target.position.z), 
                                            new Vector3(transform.position.x, target.position.y + height, transform.position.z), 
                                                Time.deltaTime);
    }


    public void Shake(float intensity, float time)
    {
        StartCoroutine(doShake(intensity, time));
    }

    IEnumerator doShake(float intensity, float time)
    {
        float timer = 0;
        Quaternion rot = transform.rotation;
        while (timer < time)
        {
            offset = Random.insideUnitSphere * intensity;
            transform.rotation = Quaternion.Euler(new Vector3(90 + offset.x, rot.y + offset.y, rot.z + offset.z));
            timer += Time.deltaTime;
            yield return null;
        }
        transform.rotation = rot;
        offset = Vector3.zero;
    }


    public void OnDestroy()
    {
        Unregister();
    }

    public void Unregister()
    {
        _level.OnPlayerSpawn -= SetTarget;
    }
}
