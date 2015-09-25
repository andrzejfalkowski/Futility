using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour 
{
	public Text CursorHint;

	public Text RoundCounter;
	public Text RoundIndicator;

	public Text SelectedCharacterText;
	public Text Equipment;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		Vector3 pos = Input.mousePosition;
		pos.y += 10f;
		CursorHint.rectTransform.position = pos;
	}
}
