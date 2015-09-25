using UnityEngine;
using System.Collections;

public class BG : MonoBehaviour 
{
	void OnMouseEnter()
	{
		if(GameController.Instance.CurrentlySelectedHero.CurrentCondition != ECharacterCondition.Dead &&
		   GameController.Instance.CurrentlySelectedHero.CurrentState != ECharacterState.Idle)
			GameController.Instance.SetCursorHint("Go to");
	}

	void OnMouseDown()
	{
		//Debug.Log ("Clicked on " + this.gameObject.name);
		float mouseX = (Input.mousePosition.x);
		float mouseY = (Input.mousePosition.y);
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0));
		GameController.Instance.SelectSpot(mousePosition);
	}
}
