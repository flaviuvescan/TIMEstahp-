using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	void OnTriggerStay(Collider other)
	{
		if(other.tag.Equals("Player") && Time.timeScale == 1)
		{
			GameManager.instance.GoToNextLevel();
		}
	}
}
