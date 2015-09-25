using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EEquipmentType
{
	Pistol,
	Rifle,
	Molotov,
	Grenade,
	Panzerfaust,
	FirstAidKit,
}

public enum ECharacterCondition
{
	Alive,
	Dead
}

public enum ECharacterState
{
	Idle,
	Moving
}

public class ControllableCharacter : MonoBehaviour 
{
	public string Name = "Name";
	public List<EEquipmentType> Equipment;

	public ECharacterCondition CurrentCondition = ECharacterCondition.Alive;
	public ECharacterState CurrentState = ECharacterState.Idle;

	public Spot CurrentSpot;
	public Spot TargetSpot;
	public int TargetIndex;
	public int CurrentPathPointIndex;
	public GameObject TargetPathPoint;

	public bool ActionMade = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(CurrentState == ECharacterState.Moving)
		{
			Vector3 pos = this.transform.localPosition;
			if(Mathf.Abs(TargetPathPoint.transform.localPosition.x - pos.x) < 0.1f && 
			   Mathf.Abs(TargetPathPoint.transform.localPosition.y - pos.y) < 0.1f)
			{
				CurrentPathPointIndex++;
				if(CurrentPathPointIndex >= CurrentSpot.Paths[TargetIndex].PathPoints.Count)
				{
					this.transform.localPosition = TargetPathPoint.transform.localPosition;
					CurrentState = ECharacterState.Idle;
					CurrentSpot = TargetSpot;
					TargetSpot = null;

					GameController.Instance.CurrentCharacterActionEnd();
				}
				else
				{
					TargetPathPoint = CurrentSpot.Paths[TargetIndex].PathPoints[CurrentPathPointIndex];
				}
			}
			else
			{
				Vector3 moveDirection = pos - TargetPathPoint.transform.localPosition;
				moveDirection.Scale(new Vector3(10f, 10f, 1f));
				moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

				float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90f;
				this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				pos.x = pos.x - moveDirection.x * Time.deltaTime;
				pos.y = pos.y - moveDirection.y * Time.deltaTime;
				this.transform.localPosition = pos;
			}
		}
	}
	

	void OnMouseEnter()
	{
		if(ActionMade)
			GameController.Instance.SetCursorHint(Name + ": action used");
		else
			GameController.Instance.SetCursorHint("Select: " + Name);
	}

	void OnMouseDown()
	{
		//Debug.Log ("Clicked on " + this.gameObject.name);
		if(CurrentState == ECharacterState.Idle && !ActionMade)
			GameController.Instance.SelectHero(this);
	}

	public void GoToSpot(Spot targetSpot, int index)
	{
		TargetSpot = targetSpot;
		CurrentPathPointIndex = 0;
		TargetIndex = index;
		TargetPathPoint = CurrentSpot.Paths[TargetIndex].PathPoints[CurrentPathPointIndex];

		CurrentState = ECharacterState.Moving;

		ActionMade = true;
	}
}
