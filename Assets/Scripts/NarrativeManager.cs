using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour {

	public Text dialogueBox;

	public string fileName;
	private string[] allLines;
	private int lineNumber;
	public static bool stopNarrative;

	// Use this for initialization
	void Start () {
		
		stopNarrative = false;
		lineNumber = 0;
		allLines = System.IO.File.ReadAllLines ("Assets/TextFiles/" + fileName);

		dialogueBox.text = allLines [0];

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!stopNarrative) 
		{
			if (Input.GetKeyDown ("space")) 
			{
				lineNumber++;
			}
		}

		if (allLines[lineNumber].IndexOf(" ") == -1) 
		{
			dialogueBox.text = "INCOMING MESSAGE";
			stopNarrative = true;
		} 
		else 
		{
			
			dialogueBox.text = allLines [lineNumber];
		}

	}
}
