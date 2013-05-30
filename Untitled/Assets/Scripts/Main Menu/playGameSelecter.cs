using UnityEngine;
using System.Collections;

public class playGameSelecter : MonoBehaviour {
	
	public GameObject targetLight;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver()
	{
		targetLight.light.intensity = 0.8f;
	}
	
	void OnMouseExit()
	{
		targetLight.light.intensity = 0f;
	}
	
	void OnMouseDown()
	{
		Application.LoadLevel("Default");
	}
}