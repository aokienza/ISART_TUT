using UnityEngine;
using System.Collections;

public class FireAndDie : MonoBehaviour {

    Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = transform.GetComponent<Animator>();
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        float length = state.length;
        StartCoroutine(Destroy(length));
    }
	
	// Update is called once per frame
	IEnumerator Destroy(float length) {
        float timer = 0;
        while (timer < length)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
	}
}
