using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text dialogueBox;

	public string fileName;
	private string[] allLines;
	private int lineNumber;

	// Use this for initialization
	void Start () {
		lineNumber = 0;
		dialogueBox.text = "Testing";
		allLines = System.IO.File.ReadAllLines ("Assets/TextFiles/" + fileName);

		dialogueBox.text = allLines [0];

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space")) 
		{
			lineNumber++;
		}
		while (allLines [lineNumber] != " ") 
		{
			dialogueBox.text = allLines [lineNumber];

		}
	}
}
