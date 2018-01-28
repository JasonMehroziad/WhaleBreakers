using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;

public class MessageManager : MonoBehaviour {


	public int personality;

	public string fileName;
	public Text message;
	public Text option1;
	public Text option2; 
	public Text option3;
	private int lineNumber;

	//voice recognition
	private KeywordRecognizer recognizer;
	private Dictionary<string, System.Action> words = new Dictionary<string, System.Action>();

	public static bool messageStop;

	private string[] MessageAndAnswers;  

	private bool isOption3;

	// Use this for initialization
	void Start () 
	{
		personality = 0;
		messageStop = false; 
		lineNumber = 0;
		isOption3 = false; 
		MessageAndAnswers = System.IO.File.ReadAllLines ("Assets/TextFiles/" + fileName);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!messageStop) 
		{
			if (MessageAndAnswers [lineNumber].IndexOf ("P .") != -1) 
			{
				
				message.text = MessageAndAnswers [lineNumber].Substring(3);
				lineNumber++;
				words.Add (MessageAndAnswers [lineNumber].Substring (3), () => { incPers1 (); });
				option1.text = MessageAndAnswers [lineNumber].Substring (3);
				lineNumber++;
				words.Add (MessageAndAnswers [lineNumber].Substring (3), () => { incPers2 (); });
				option2.text = MessageAndAnswers [lineNumber].Substring (3);
				lineNumber++;
				words.Add (MessageAndAnswers [lineNumber].Substring (3), () => { incPers3 (); });
				option3.text = MessageAndAnswers [lineNumber].Substring (3);
				lineNumber++;
			}
		}
	}

	private void TextToAction(PhraseRecognizedEventArgs args)
	{
		Action action;
		if(words.TryGetValue(args.text, out action))
			{
				action.Invoke();
			}
	}

	private void incPers1()
	{
		personality++;
	}
	private void incPers2()
	{
		personality += 2;
	}
	private void incPers3()
	{
		personality += 3;
	}
						

	


}
