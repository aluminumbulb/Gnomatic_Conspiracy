using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

	//int leftSideTouched = 0;
	//int rightSideTouched = 0;
	bool leftSideTouched = false;
	bool rightSideTouched = false;
	Rect leftSide,rightSide;

    private void Start()
    {
        player = GetComponent<Player>();

		leftSide = new  Rect(0, 0, Screen.width / 4.0f, Screen.height);
		rightSide = new Rect(((Screen.width/4f)*3),0,Screen.width / 4.0f,Screen.height);

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
		if (Input.touchCount > 0) {
			Vector2[] touchPlaces = new Vector2[Input.touches.Length];
			for (int i = 0; i < touchPlaces.Length; i++) {
				touchPlaces [i] = Input.touches [i].position;
			}

			for (int j = 0; j < touchPlaces.Length; j++) {
				if (leftSide.Contains (touchPlaces [j])) {
					leftSideTouched = true;
				}

				if (rightSide.Contains (touchPlaces [j])) {
					rightSideTouched = true;
				}
			}

			if (rightSideTouched && !leftSideTouched) {
				player.SetDirectionalInput (Vector2.right);
			}

			if (!rightSideTouched && leftSideTouched) {
				player.SetDirectionalInput (Vector2.left);
			}

			if (rightSideTouched && leftSideTouched) {
				player.OnJumpInputDown();
			}
		}
		leftSideTouched = false;
		rightSideTouched = false;
		
		}
		//player.OnJumpInputUp();
	}