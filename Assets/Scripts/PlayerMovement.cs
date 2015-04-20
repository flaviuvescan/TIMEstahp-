using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

    public LeanTweenType jumpEase = LeanTweenType.easeOutCirc;
    public float playerSpeed = 30f;
    public float jumpForce = 1f;
    public float gravity = -2f;
	private float gravityAdder = 0f;
    public Rigidbody body;

    private float xDirection = 0f;
    private float yDirection = 0f;
    private float jump = 0f;
    private Vector3 movementVector;
	private Vector3 rayDirection = new Vector3(0,-1,0);
	public float rayCastDist = 10f;
	private bool reloading = false;
    public enum PlayerState { onGround, falling, jumping };
    public PlayerState state = PlayerState.onGround;

    void Start()
    {
        instance = this;
    }

    public void SetFalling()
    {
        state = PlayerState.falling;
    }

    void OnCollisionEnter()
    {
        body.velocity = Vector3.zero;
    }

	void OnTriggerStay(Collider other)
	{
		if(other.tag.Equals("DeathCube"))
		{
			GameManager.instance.GoToNextLevel();
		}
	}

    void Update()
    {
		if(Vector3.Distance(Vector3.zero, transform.position) > 50)
		{
			Application.LoadLevel(GameManager.instance.currentLevel);
		}

        xDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && state == PlayerState.onGround)
        {
            state = PlayerState.jumping;
			AudioManager.instance.PlayJump();
            LeanTween.moveLocalY(gameObject, transform.localPosition.y + jumpForce, 0.3f).setEase(jumpEase).setIgnoreTimeScale(true).setOnComplete(SetFalling);
        }

        if (Mathf.Abs(xDirection) < 0.01f)
        {
            xDirection = 0f;
        }

        if (state == PlayerState.falling)
        {
			yDirection = gravity - gravityAdder;
			gravityAdder += 1f;
        }
        else
        {
			gravityAdder = 0f;
            yDirection = 0f;
        }

		RaycastHit hitRight;
		RaycastHit hitLeft;
		RaycastHit hitCenter;

		Debug.DrawRay(transform.position, rayDirection * rayCastDist, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.5f, 0, 0), rayDirection * rayCastDist, Color.red);
        Debug.DrawRay(transform.position - new Vector3(0.5f, 0, 0), rayDirection * rayCastDist, Color.red);

        if (Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), rayDirection, out hitRight, rayCastDist)
            || Physics.Raycast(transform.position - new Vector3(0.5f, 0, 0), rayDirection, out hitLeft, rayCastDist)
            || Physics.Raycast(transform.position, rayDirection, out hitCenter, rayCastDist))
        {
			if(hitLeft.collider != null && hitLeft.collider.tag.Equals("DeathCube")
			   || hitRight.collider != null && hitRight.collider.tag.Equals("DeathCube")
			   || hitCenter.collider != null && hitCenter.collider.tag.Equals("DeathCube"))
			{
				if(!reloading)
				StartCoroutine("ReloadLevel");
			}

			if(hitLeft.collider != null && hitLeft.collider.tag.Equals("Gate")
			   || hitRight.collider != null && hitRight.collider.tag.Equals("Gate")
			   || hitCenter.collider != null && hitCenter.collider.tag.Equals("Gate"))
			{
				GameManager.instance.GoToNextLevel();
			}


            if (state != PlayerState.onGround)
            {
                CameraShake.instance.ShakeCamera();
                state = PlayerState.onGround;
            }
        }
        else
        {
            if (state != PlayerState.jumping)
                state = PlayerState.falling;
        }

        movementVector = new Vector3(xDirection * playerSpeed, yDirection, 0) * Time.deltaTime / Time.timeScale;

        transform.Translate(movementVector);
    }

	IEnumerator ReloadLevel()
	{				
		reloading = true;
		AudioManager.instance.PlayDeath();
		Time.timeScale = 1;
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(GameManager.instance.currentLevel);
	}
}
