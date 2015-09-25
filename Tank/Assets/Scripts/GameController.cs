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
		CurrentRound = 0;
		NextPlayerRound();
	}

	public void NextPlayerRound()
	{
		IsPlayerRound = true;
		CurrentRound++;
		HUD.RoundCounter.text = "Round " + CurrentRound;
		HUD.RoundIndicator.text = "Player's turn";

		foreach(var hero in Heroes)
		{
			hero.ActionMade = false;
		}

		SelectHero(GetNextCharacter());
	}

	public void NextEnemyRound()
	{
		IsPlayerRound = false;
		HUD.RoundIndicator.text = "Enemy's turn";
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

		if(CurrentlySelectedSpot != null)
		{
			foreach(var neighbor in CurrentlySelectedSpot.Neighbors)
			{
				neighbor.Blink = false;
			}
		}
		CurrentlySelectedSpot = CurrentlySelectedHero.CurrentSpot;
		foreach(var neighbor in CurrentlySelectedSpot.Neighbors)
		{
			neighbor.Blink = true;
		}
	}

	public void UnselectHero()
	{
		CurrentlySelectedHero = null;

		HUD.SelectedCharacterText.text = "Selected character: none";
		HUD.Equipment.text = "Equipment: none";
	}

	public void SelectSpot(Spot spot)
	{
		if(CurrentlySelectedSpot != null && CurrentlySelectedSpot.Neighbors.Contains(spot))
		{
			if(CurrentlySelectedHero != null && CurrentlySelectedHero.CurrentState == ECharacterState.Idle)
			{
				CurrentlySelectedHero.GoToSpot(spot, CurrentlySelectedSpot.Neighbors.IndexOf(spot));

				foreach(var neighbor in CurrentlySelectedSpot.Neighbors)
				{
					neighbor.Blink = false;
				}
				
				//UnselectHero();
			}
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

	bool CheckForPlayerTurnEnd()
	{
		foreach(var hero in Heroes)
		{
			if(!hero.ActionMade && hero.CurrentCondition != ECharacterCondition.Dead)
				return false;
		}
		return true;
	}

	ControllableCharacter GetNextCharacter()
	{
		foreach(var hero in Heroes)
		{
			if(!hero.ActionMade && hero.CurrentCondition != ECharacterCondition.Dead)
				return hero;
		}
		return null;
	}

	public void SetCursorHint(string hint)
	{
		HUD.CursorHint.text = hint;
	}

	public void CurrentCharacterActionEnd()
	{
		if(CheckForPlayerTurnEnd())
			NextEnemyRound();
		else
			SelectHero(GetNextCharacter());
	}

	public void TankActionEnd()
	{
		NextPlayerRound();
	}

	void Start () 
	{
		Init();
	}

	void Update () 
	{
	
	}
}
