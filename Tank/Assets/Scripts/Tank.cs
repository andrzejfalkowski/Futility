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

	void RotateBody()
	{
		Vector3 pos = this.transform.position;
		Vector3 moveDirection = pos - MachinegunTarget.transform.position;
		
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 180f;
		float currentAngle = Body.transform.eulerAngles.z;

		float difference = targetAngle - currentAngle;
		if(difference > 180f)
			difference = currentAngle - targetAngle;

		if(Mathf.Abs(difference) > 1f)
			targetAngle = currentAngle + Mathf.Sign(difference) * 9f * Time.deltaTime;

		Body.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
	}

	void RotateTurret()
	{
		Vector3 pos = Turret.transform.position;
		Vector3 moveDirection = pos - CannonTarget.transform.position;
		
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 180f;
		float currentAngle = Turret.transform.eulerAngles.z;
		
		float difference = targetAngle - currentAngle;
		if(difference > 180f)
			difference = currentAngle - targetAngle;
		
		if(Mathf.Abs(difference) > 1f)
			targetAngle = currentAngle + Mathf.Sign(difference) * 30f * Time.deltaTime;
		
		Turret.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
	}

	void MachinegunAction()
	{

	}

	void CannonAction()
	{

	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(MachinegunTarget != null)
			RotateBody();
		if(CannonTarget != null)
			RotateTurret();
	}
}
