using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Camera thisCam;
    public float cameraOffset = 2f;
    private bool zoomedOut = false;
    public float zoomFactor = 1.5f;
    public float switchTime = 0.1f;
    private float initialScale;
    public LeanTweenType easingIn = LeanTweenType.easeOutExpo;
    public LeanTweenType easingOut = LeanTweenType.easeInExpo;

    void Awake()
    {
        initialScale = thisCam.orthographicSize;
    }

    void Update()
    {
        Vector2 cameraPos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);

        Vector3 newCameraPosition = new Vector3(cameraPos.x / cameraOffset, cameraPos.y / cameraOffset, -300);

        transform.localPosition = newCameraPosition;

        if (Input.GetMouseButtonDown(1) && !zoomedOut)
        {
            zoomedOut = true;

            LeanTween.cancel(gameObject);

            LeanTween.value(gameObject, initialScale, initialScale * zoomFactor, switchTime)
            .setEase(easingIn)
            .setIgnoreTimeScale(true)
            .setOnUpdate((float x) =>
            {
                thisCam.orthographicSize = x;
            });
        }

        if (Input.GetMouseButtonUp(1) && zoomedOut)
        {
            zoomedOut = false;

            LeanTween.cancel(gameObject);

            LeanTween.value(gameObject, initialScale * zoomFactor, initialScale, switchTime)
            .setEase(easingOut)
            .setIgnoreTimeScale(true)
            .setOnUpdate((float x) =>
            {
                thisCam.orthographicSize = x;
            });
        }
    }   
}
