using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    void OnColliderEnter(Collider other)
    {
        if (other.tag.Equals("Player") && Time.timeScale > 0.2f)
        {
			StartCoroutine("ReloadLevel");
        }
    }
	
	IEnumerator ReloadLevel()
	{
		Time.timeScale = 1;
		AudioManager.instance.PlayDeath();
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(GameManager.instance.currentLevel);
	}

}
