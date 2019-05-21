using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

	private Collider2D playerCollider;
	Touch leftTouch, rightTouch, jumpTouch;
	Vector2 startTouchJump;//startTouchLeft,startTouchRight;
	Rect leftRect,rightRect, jumpRect;
	public int dashSensitivity = 4;
    
	private void Start()
    {
        player = GetComponent<Player>();

		leftRect = new  Rect(0, 0, Screen.width / 5.0f, Screen.height);
		rightRect = new Rect((Screen.width/5f),0, Screen.width / 5.0f,Screen.height);
		jumpRect = 	new Rect((Screen.width*(3f/5f)),0, Screen.width*3 / 5.0f,Screen.height);
		playerCollider = GetComponent<BoxCollider2D> ();


		leftTouch.phase = TouchPhase.Ended;
		rightTouch.phase = TouchPhase.Ended;
		jumpTouch.phase = TouchPhase.Ended;
	}

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);


		if (Input.GetKeyDown("i") ) {
			player.onDash ();
		}

        if (Input.GetButtonDown("Vertical"))
        {
            player.OnJumpInputDown();
        }

        if (Input.GetButtonUp("Vertical"))
        {
            player.OnJumpInputUp();
        }
		if (Input.touches.Length > 0) {
			mobileControlls (Input.touches);
		}
    }

	private void mobileControlls(Touch[] touches){
		foreach (Touch touch in touches) {
			if (leftRect.Contains (touch.position)) {
				player.SetDirectionalInput (Vector2.left);
			}

			if (rightRect.Contains (touch.position)) {
				player.SetDirectionalInput (Vector2.right);
			}

			if(jumpRect.Contains(touch.position)){
				jumpTouch = touch;
			}
		}

		if (jumpTouch.phase == TouchPhase.Began) {
			startTouchJump = jumpTouch.position;
			player.OnJumpInputDown ();
		}

		if (jumpTouch.phase == TouchPhase.Moved) {
			if(Mathf.Abs(startTouchJump.x - jumpTouch.position.x)>dashSensitivity){
				player.onDash();
			}
		}
	}
}