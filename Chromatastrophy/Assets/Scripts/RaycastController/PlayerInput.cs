using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

	private Collider2D playerCollider;
	Touch leftTouch, rightTouch, jumpTouch;
	Rect leftRect,rightRect, jumpRect;

    private void Start()
    {
        player = GetComponent<Player>();

		leftRect = new  Rect(0, 0, Screen.width / 8.0f, Screen.height);
		rightRect = new Rect((Screen.width/8f),0, Screen.width / 8.0f,Screen.height);
		jumpRect = 	new Rect((Screen.width*(3f/4f)),0, Screen.width / 4.0f,Screen.height);
		playerCollider = GetComponent<BoxCollider2D> ();

		leftTouch.phase = TouchPhase.Ended;
		rightTouch.phase = TouchPhase.Ended;
		jumpTouch.phase = TouchPhase.Ended;
    
	
	}

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

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
				leftTouch = touch;
			}

			if (rightRect.Contains (touch.position)) {
				rightTouch = touch;
			}

			if(jumpRect.Contains(touch.position)){
				jumpTouch = touch;
			}
		}

		if (jumpTouch.phase == TouchPhase.Began) {
			player.OnJumpInputDown ();
		}

		if (jumpTouch.phase == TouchPhase.Ended) {
			 player.OnJumpInputUp ();
		}

		if ((leftTouch.phase == TouchPhase.Stationary)) {
			player.SetDirectionalInput (Vector2.left);
		}
		if ((rightTouch.phase == TouchPhase.Stationary)) {
			player.SetDirectionalInput (Vector2.right);
		}
	}
}