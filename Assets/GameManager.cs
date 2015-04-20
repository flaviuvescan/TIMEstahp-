using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public int currentLevel;
	public bool lastLevel = false;

	void Awake()
	{
		instance = this;
	}

	public void GoToNextLevel()
	{
		if(!lastLevel)
		{
			AudioManager.instance.PlayNextlevel();
			Application.LoadLevel(currentLevel + 1);
		}
		else
		{
			Application.OpenURL("www.ludumdare.com");
			Application.Quit();
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
	   	{
			Application.Quit();
		}
	}
}
