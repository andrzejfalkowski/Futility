using UnityEngine;
using System.Collections;

public class BG : MonoBehaviour 
{
	void OnMouseEnter()
	{
		GameController.Instance.SetCursorHint("");
	}
}
