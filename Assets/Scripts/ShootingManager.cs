using UnityEngine;
using System.Collections;

public class ShootingManager : MonoBehaviour {

    public GameObject bullet;
    public GameObject invisibleBullet;
	public int nrOfBullets = 1;
	public float delayBeforeStarting = 0f;
	public float delayBetweenShootings = 1f;
	private int current = 0;
	public bool isCannon = false;

	void OnEnable()
	{
		if(isCannon)
		{
			StartCoroutine("ApplyStartDelay");
		}
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCannon)
        {
			Shoot();
        }
    }

	IEnumerator ApplyStartDelay()
	{
		yield return new WaitForSeconds(delayBeforeStarting);
		StartCoroutine("DelayBetweenShootings");
	}

	IEnumerator DelayBetweenShootings()
	{
		if(current < nrOfBullets)
		{
			Shoot();
			current++;
			yield return new WaitForSeconds(delayBetweenShootings);
			StartCoroutine("DelayBetweenShootings");
		}	
	}

	void Shoot()
	{
		AudioManager.instance.PlayShoot();

		GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		bulletClone.transform.localRotation = transform.parent.localRotation;
		
		GameObject invisibleBulletClone = Instantiate(invisibleBullet, transform.position, Quaternion.identity) as GameObject;
		invisibleBulletClone.transform.localRotation = transform.parent.localRotation;
		
		bulletClone.GetComponent<BulletBehaviour>().invisibleBullet = invisibleBulletClone;

		invisibleBulletClone.transform.parent = bulletClone.transform;
	}
}
