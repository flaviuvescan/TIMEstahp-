using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public AudioSource source;
	public AudioClip jump;
	public AudioClip shoot;
	public AudioClip explode;
	public AudioClip nextlevel;
	public AudioClip death;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayDeath()
	{
		source.PlayOneShot(death);
	}

	public void PlayJump()
	{
		source.PlayOneShot(jump);
	}

	public void PlayShoot()
	{
		source.PlayOneShot(shoot);
	}

	public void PlayNextlevel()
	{
		source.PlayOneShot(nextlevel);
	}

	public void PlayExplode()
	{
		source.PlayOneShot(explode);
	}
	
}
