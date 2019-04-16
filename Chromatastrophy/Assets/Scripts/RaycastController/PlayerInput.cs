using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

	Touch leftTouch, rightTouch, jumpTouch;
	Rect leftRect,rightRect, jumpRect;

    private void Start()
    {
        player = GetComponent<Player>();

		leftRect = new  Rect(0, 0, Screen.width / 8.0f, Screen.height);
		rightRect = new Rect((Screen.width/8f),0, Screen.width / 4.0f,Screen.height);
		jumpRect = 	new Rect((Screen.width*(3f/4f)),0, Screen.width / 4.0f,Screen.height);
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

		mobileControlls ();
    }

	private void mobileControlls(){
		foreach (Touch touch in Input.touches) {
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