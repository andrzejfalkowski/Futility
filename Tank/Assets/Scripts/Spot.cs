using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Path
{
	public List<GameObject> PathPoints = new List<GameObject>();
}

public class Spot : MonoBehaviour 
{
	public List<Spot> Neighbors;
	public List<Path> Paths;
	public bool Blink = false;

	float blinkDirection = 1f;

	void Update()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(Blink)
		{
			Color color = sr.color;
			color.a = Mathf.Clamp01(color.a + blinkDirection * Time.deltaTime);
			sr.color = color;

			if(color.a <= 0f || color.a >= 1f )
				blinkDirection *= -1f;
		}
		else
		{
			Color color = sr.color;
			color.a = 0f;
			sr.color = color;
		}
	}

	void OnMouseEnter()
	{
		GameController.Instance.SetCursorHint("Go to");
	}

	void OnMouseDown()
	{
		//Debug.Log ("Clicked on " + this.gameObject.name);
		//GameController.Instance.SelectSpot(this);
	}

	void Start()
	{
		Init();
	}

	public void Init()
	{
		for(int i = 0; i < Neighbors.Count; i++)
		{
			if(Paths.Count < Neighbors.Count)
				Paths.Add (new Path());

			if(!Paths[i].PathPoints.Contains(Neighbors[i].gameObject))
				Paths[i].PathPoints.Add(Neighbors[i].gameObject);
		}
	}
}
