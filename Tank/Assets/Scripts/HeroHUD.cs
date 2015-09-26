using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroHUD : MonoBehaviour 
{
	public ControllableCharacter Hero;
	public Button HeroButton;

	public void Click()
	{
		GameController.Instance.SelectHero(Hero);
	}

	public void Enter()
	{
		GameController.Instance.SetCursorHint("Select: " + Hero.Name);
	}

	void Update()
	{
		var colors = HeroButton.colors;
		if(GameController.Instance.CurrentlySelectedHero == Hero)
			colors.normalColor = Color.white;
		else
			colors.normalColor = Color.gray;

		HeroButton.colors = colors;
	}
}
