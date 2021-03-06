using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cscript_point : MonoBehaviour {
	
	public int electricity = 2;
	public int steam = 3;
	
	public int captureRequirement = 100;
	public int captureProgress = 0;
	
	public Light controlPointLight;
	
	public cscript_player ownedPlayer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (captureProgress == captureRequirement)
		{
			ownedPlayer.AddElectricity(electricity);
			ownedPlayer.AddSteam(steam);
		}
		
		if (CheckForUnits() == true)
		{
			foreach (GameObject g in GetCollidingUnits())
			{
				if (captureProgress == 0)
				{
					ownedPlayer = g.GetComponent<cscript_unit>().GetOwnedPlayer ();
				}
				
				if (g.GetComponent<cscript_unit>().GetOwnedPlayer () == ownedPlayer)
				{
					RaiseCaptureProgress(1);
				}
				else
				{
					LowerCaptureProgress(1);
				}
			}
		}
		
		if (captureProgress > 0 && captureProgress < captureRequirement)
		{
			captureProgress -= 1;
			
			if (captureProgress < 0)
				captureProgress = 0;
		}
		
		if (captureProgress == 0)
			ownedPlayer = null;
		
		if (captureProgress != 0)
		{
			//GUI.Label(Rect(Camera.main.WorldToViewportPoint (transform.position)), "Capture Progress: " + captureProgress + " / " + captureRequirement);
		}
		
		if (ownedPlayer == null)
			controlPointLight.color = Color.white;
		else if (ownedPlayer != GameObject.FindGameObjectWithTag("Master").GetComponent<cscript_master>().GetPlayer()  && captureProgress == captureRequirement)
			controlPointLight.color = Color.red;
		else if (captureProgress == captureRequirement)
			controlPointLight.color = Color.green;
	}
	
	public void LowerCaptureProgress(int i)
	{
		captureProgress -= i;
		
		if (captureProgress < 0)
			captureProgress = 0;
	}
	
	public void RaiseCaptureProgress(int i)
	{
		captureProgress += i;
		
		if (captureProgress > captureRequirement)
			captureProgress = captureRequirement;
	}
	
	public void ChangeOwnedPlayer(cscript_player p)
	{
		ownedPlayer = p;
	}
	
	public cscript_player GetOwnedPlayer()
	{
		return ownedPlayer;
	}
	
	public bool CheckForUnits()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
		
		foreach (Collider c in hitColliders)
		{
			if (c.tag == "Unit")
			{
				return true;
			}
		}
		
		return false;
	}
	
	public List<GameObject> GetCollidingUnits()
	{
		List<GameObject> output = new List<GameObject>();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
		
		foreach (Collider c in hitColliders)
		{
			if (c.gameObject.tag == "Unit")
				output.Add(c.gameObject);
		}
		
		return output;
	}
}
