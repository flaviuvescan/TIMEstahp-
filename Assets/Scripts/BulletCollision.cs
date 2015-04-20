using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

    public BulletBehaviour bulletBehaviour;

	void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
        {
			AudioManager.instance.PlayExplode();
            bulletBehaviour.DestroyBullet();
        }
		else
		{
			if (Time.timeScale > 0.2f)
			{
				StartCoroutine("ReloadLevel");
			}
		}
    }

	IEnumerator ReloadLevel()
	{		
		Time.timeScale = 1;
		AudioManager.instance.PlayDeath();	
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(GameManager.instance.currentLevel);
	}

	void OnTriggerStay(Collider other)
	{
		if (!other.tag.Equals("Player"))
		{
			bulletBehaviour.DestroyBullet();
		}
		else
		{
			if (Time.timeScale > 0.2f)
			{
				Time.timeScale = 1f;
				Application.LoadLevel(GameManager.instance.currentLevel);
			}
		}
	}
}
