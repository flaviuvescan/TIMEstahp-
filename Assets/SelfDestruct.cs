using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float lifetime = 2f;

    void OnEnable()
    {
        StartCoroutine("KillSelf");
    }

    IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
