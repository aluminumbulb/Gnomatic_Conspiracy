using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	private GameController _gameController;
	public static Player player;
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    public float moveSpeed = 8.0f;

	public float dashAmount = -14f;
	private bool hasDashed = false;
	public float dashCooldown = 0.9f;
	private float timeStamp = 0f;
	private int dashCounter = 0;


	public AudioClip jumpSound;
	public AudioClip wallClingSound;
	public AudioSource source;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    public float wallSlideSpeedMax = 0f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding;
    private int wallDirX;

	private Animator anim;
	public Canvas pauseMenu;
	private bool paused = false;

	public void Awake(){
		if (player == null) {
			player = this;
		} else if (controller != null) {
			Destroy (this);
		}
	}

	public void Start(){
		_gameController = GameObject.FindObjectOfType<GameController> ();
		_gameController.lastRoomEntered = SceneManager.GetActiveScene ().name;
		//if(SceneManager.GetActiveScene ().name == _gameController.lastRoomEntered){
		//	transform.position = _gameController.lastSavedLocation;
		//}

		_gameController.paintWorld ();

		controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

		anim = GetComponent<Animator> ();
		pauseMenu.GetComponent<Canvas> ().enabled = false;

		source = GetComponent<AudioSource> ();

	}

    private void Update()
    {
        CalculateVelocity();
        HandleWallSliding();

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }

		if(Input.GetKey(KeyCode.Q)){
			//Unpause();
			//Debug.Log(anim.GetBool("paused"));	
		}

		if (hasDashed) {
			velocity.y = 0f;
			dashCounter++;
			if (dashCounter > 8) {
				hasDashed = false;
				dashCounter = 0;
			}
		}
    }

	public void OnMouseDown(){
		if (paused == false) {
			StartCoroutine (pause ());
		}
	}


	IEnumerator pause(){
		anim.SetBool ("paused",true);
		paused = true;
		yield return new WaitForSeconds (1);
		pauseMenu.GetComponent<Canvas> ().enabled = true;
	}

	public void Unpause(){
		anim.SetBool ("paused", false);
		paused = false;
		pauseMenu.GetComponent<Canvas> ().enabled = false;

	}

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

	public void onDash()
	{
		if (Time.time <= timeStamp)
			return;
		if (timeStamp < Time.time)
			timeStamp = Time.time + dashCooldown;
		
		if (!controller.facingLeft) {
			velocity.x = dashAmount * minJumpVelocity;
		} else {
			velocity.x = -dashAmount * minJumpVelocity;
		}
		hasDashed = true;
	}

    public void OnJumpInputDown()
    {
		

        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
				jumpfx ();
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
				jumpfx ();
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
				jumpfx ();
            }
            isDoubleJumping = false;
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
			jumpfx ();
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = true;
			jumpfx ();
        }

    }

	public void jumpfx(){
		source.PlayOneShot (jumpSound, 0.9f);	
	}

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void HandleWallSliding()
    {
		
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
			wallSliding = true;

			//source.PlayOneShot (wallClingSound, 0.9f);	
			


            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
		anim.SetBool ("isClinging", wallSliding);

    }

    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
    }
}
