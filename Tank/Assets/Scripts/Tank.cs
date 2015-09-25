using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour 
{
	public GameObject Body;
	public GameObject Turret;

	public ControllableCharacter MachinegunTarget;
	public ControllableCharacter CannonTarget;

	void OnMouseEnter()
	{
		GameController.Instance.SetCursorHint("Tank");
	}

	void MakeDecision()
	{

	}

	void Rotate()
	{

	}

	void MachinegunAction()
	{

	}

	void CannonAction()
	{

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
