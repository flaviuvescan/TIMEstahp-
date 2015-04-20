using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public static CameraShake instance;
    public float maxOffset = 2f;
    public float shakeTime = 0.3f;
    public LeanTweenType ease = LeanTweenType.easeShake;
    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.localPosition;
        instance = this;
    }

    public void ShakeCamera()
    {
        LeanTween.moveLocal(gameObject, new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset)), shakeTime)
            .setEase(ease)
				.setIgnoreTimeScale(true)
            .setOnComplete(ReturnToZero);
    }

    public void ReturnToZero()
    {
        transform.localPosition = initialPosition;
    }
}
