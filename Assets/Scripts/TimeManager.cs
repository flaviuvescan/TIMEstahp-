using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance;

    public float switchingTime = 0.2f;

    public bool inBulletTime = false;

	public GameObject nextLevelGate;
	private Vector3 initialGateSize;

    void OnEnable()
    {
        instance = this;

		nextLevelGate = GameObject.Find("FinishGate");
		initialGateSize = nextLevelGate.transform.localScale;

    }

    void Update()
    {
		if(nextLevelGate == null)
		{
			nextLevelGate = GameObject.Find("FinishGate");
			initialGateSize = nextLevelGate.transform.localScale;
		}

        if (Input.GetMouseButtonDown(1) && !inBulletTime)
        {
            inBulletTime = true;

            LeanTween.cancel(gameObject);

            LeanTween.value(gameObject, 1, 0.01f, switchingTime)
            .setOnUpdate((float x) =>
            {
                Time.timeScale = x;
				nextLevelGate.transform.localScale = initialGateSize * x;
                Time.fixedDeltaTime = Time.timeScale * 0.002f;
            });
        }

        if (Input.GetMouseButtonUp(1) && inBulletTime)
        {
            inBulletTime = false;

            LeanTween.cancel(gameObject);

            LeanTween.value(gameObject, 0.01f, 1, switchingTime)
            .setOnUpdate((float x) =>
            {
                Time.timeScale = x;
				nextLevelGate.transform.localScale = initialGateSize * x;
				Time.fixedDeltaTime = Time.timeScale * 0.002f;
            });
        }
    }

}
