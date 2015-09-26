using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour 
{
	public GameObject HeroHUDPrefab;

	public Text CursorHint;
	public Text ActivePauseIndicator;

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

	public void Init()
	{
		for(int i = 0; i < GameController.Instance.Heroes.Count; i++)
		{
			GameObject heroHUDObject = Instantiate(HeroHUDPrefab) as GameObject;

			heroHUDObject.GetComponent<HeroHUD>().Hero = GameController.Instance.Heroes[i];

			Image heroHUD = heroHUDObject.GetComponent<Image>();
			heroHUD.transform.SetParent(this.transform);
			heroHUD.transform.SetAsFirstSibling();
			Vector3 pos = heroHUD.rectTransform.position;
			pos.x = 70f * i + 10f;
			pos.y = 5f;
			heroHUD.rectTransform.position = pos;

		}
	}
}
