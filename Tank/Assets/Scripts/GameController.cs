using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EGameState
{
	Gameplay,
	GameOver,
	Outro
}

public class GameController : MonoBehaviour 
{
	public UI HUD;

	public EGameState CurrentGameState = EGameState.Gameplay;
	public int CurrentTurn = 0;

	public List<ControllableCharacter> Heroes;
	public Tank EnemyTank;

	public ControllableCharacter CurrentlySelectedHero;
	public Spot CurrentlySelectedSpot;

	public int CurrentRound = 0;
	public bool IsPlayerRound = false;

	static GameController _instance;
	static public GameController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameController)) as GameController;
			}
			return _instance;
		}
	}


	public void Init()
	{
		//SelectHero();
	}

	public void SelectHero(ControllableCharacter selectedHero)
	{
		CurrentlySelectedHero = selectedHero;

		HUD.SelectedCharacterText.text = "Selected character: " + selectedHero.Name;

		string equipmentString = "Equipment: ";
		if(CurrentlySelectedHero.Equipment.Count == 0)
			equipmentString += "none";
		else
		{
			for(int i = 0; i < CurrentlySelectedHero.Equipment.Count - 1; i++)
			{
				equipmentString += CurrentlySelectedHero.Equipment[i].ToString() + ", ";
			}
			equipmentString += CurrentlySelectedHero.Equipment[CurrentlySelectedHero.Equipment.Count - 1].ToString();
		}
		HUD.Equipment.text = equipmentString;
	}

	public void UnselectHero()
	{
		CurrentlySelectedHero = null;

		HUD.SelectedCharacterText.text = "Selected character: none";
		HUD.Equipment.text = "Equipment: none";
	}

	public void SelectSpot(Vector3 target)
	{
		if(CurrentlySelectedHero != null && CurrentlySelectedHero.CurrentCondition != ECharacterCondition.Dead)
		{
			CurrentlySelectedHero.GoToSpot(target);
		}
	}

	bool CheckForGameOver()
	{
		foreach(var hero in Heroes)
		{
			if(hero.CurrentCondition != ECharacterCondition.Dead)
				return false;
		}
		return true;
	}

	public void SetCursorHint(string hint)
	{
		HUD.CursorHint.text = hint;
	}

	public void CharacterActionEnd(ControllableCharacter hero)
	{
		hero.CurrentState = ECharacterState.Idle;
	}

	public void TankActionEnd()
	{
	}

	void Start () 
	{
		Init();
	}

	void Update () 
	{
	
	}
}
