using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	private Rigidbody body;
    public float bulletSpeed = 15f;
    public GameObject invisibleBullet;
	public GameObject invisibleBulletPrefab;
    public GameObject explosion;

    void OnEnable()
    {
		body = GetComponent<Rigidbody>();

        StartCoroutine("SelfDestructBullet");
    }

    void Update()
    {
		if(body != null)
		{
			body.MovePosition(body.position + transform.right * bulletSpeed * Time.deltaTime);
		}
			//body.velocity += directionVector * bulletSpeed * Time.deltaTime;
		//body.AddForce(directionVector * 1000 * bulletSpeed * Time.deltaTime);
    }

    IEnumerator SelfDestructBullet()
    {
        yield return new WaitForSeconds(10);
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        if (invisibleBullet != null)
        {
            Destroy(invisibleBullet);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
