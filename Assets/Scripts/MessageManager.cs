using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

public class MessageManager : MonoBehaviour {
    public Dictionary<string, System.Action> words = new Dictionary<string, System.Action>();
    public GameObject o3;
    public GameObject display;

    public bool nextMessage;
    public int humansWin;

    public int personality;
   
	public string fileName;
	public Text message;
	public Text option1;
	public Text option2; 
	public Text option3;
    public Text epilogue;
	public int lineNumber;

	//voice recognitor
	public static bool messageStop;

	private string[] MessageAndAnswers;
    private string[] epilogueOptions;

	private bool isOption3;

	// Use this for initialization
	void Start () 
	{
        humansWin = 0;
        o3.SetActive(false);
        nextMessage = false;
		personality = 0;
		messageStop = false; 
		lineNumber = 0;
		MessageAndAnswers = System.IO.File.ReadAllLines ("Assets/TextFiles/" + fileName);
        fileName = "epilogue.txt";
        epilogueOptions = System.IO.File.ReadAllLines("Assets/TextFiles/" + fileName);

        StartCoroutine(GetMessages());
    }

    private IEnumerator GetMessages()
	{
		while(lineNumber < MessageAndAnswers.Length)
		{
            if (MessageAndAnswers[lineNumber].IndexOf("P .") != -1)
            {
                o3.SetActive(true);
                message.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
                words.Add(MessageAndAnswers[lineNumber].Substring(3), () => { incPers1(); });
                option1.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
                words.Add(MessageAndAnswers[lineNumber].Substring(3), () => { incPers2(); });
                option2.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
                words.Add(MessageAndAnswers[lineNumber].Substring(3), () => { incPers3(); });
                option3.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
            }

            if(MessageAndAnswers[lineNumber].IndexOf("Q .") != -1)
            {
                o3.SetActive(false);
                option3.text = "";
                message.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
                words.Add(MessageAndAnswers[lineNumber].Substring(3), () => { choice1(); });
                option1.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
                words.Add(MessageAndAnswers[lineNumber].Substring(3), () => { choice2(); });
                option2.text = MessageAndAnswers[lineNumber].Substring(3);
                lineNumber++;
            }

            KeywordRecognizer recognizer = new KeywordRecognizer(words.Keys.ToArray());  //initialize recognizer, uses dictionary
            recognizer.OnPhraseRecognized += TextToAction;                               //when a phrase in dict is recognized, execute its function
            recognizer.Start();                                                          //activate recognizer
        
            yield return new WaitUntil(() => nextMessage);
                nextMessage = false;
                words.Clear();
                recognizer.Stop();
                lineNumber++;
            
            if (!o3.activeInHierarchy)
            {
                yield return new WaitForSeconds(5.0f);
                message.color = Color.white;
            }
        }
        display.SetActive(false);
        for (int i = 0; i < epilogueOptions.Length;)
        {
            if (humansWin >= 0)
            {
                while (i < 14)
                {
                    epilogue.text += epilogueOptions[i];
                    i++;
                }
                if(personality > 5 && personality < 11)
                {
                        i = 23;
                    while (i < 31)
                    {
                        epilogue.text += epilogueOptions[i];
                        i++;
                    }
                }
                else if(personality > 10 && personality < 15)
                {
                    
                    i = 47;
                    while (i < 51)
                    {
                        epilogue.text += epilogueOptions[i];
                        i++;
                    }
                }
                else
                {
                    i = 31;
                    while (i < 38)
                    {
                        epilogue.text += epilogueOptions[i];
                        i++;
                    }
                }
            }else if(humansWin < 0)
                {
                i = 14;
                while (i < 23)
                {
                    epilogue.text += epilogueOptions[i];
                    i++;
                }
                if (personality < 10)
                {
                    i = 47;
                    while (i < epilogueOptions.Length)
                    {
                        epilogue.text += epilogueOptions[i];
                        i++;
                    }
                }
                if(personality > 10)
                {
                    i = 51;
                    while (i < epilogueOptions.Length)
                    {
                        epilogue.text += epilogueOptions[i];
                        i++;
                    }
                }
          
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
        nextMessage = true;
    }
	private void incPers2()
	{
		personality += 2;
        nextMessage = true;
    }
	private void incPers3()
	{
		personality += 3;
        nextMessage = true;
    }
	private void choice1()
    {
        message.text = MessageAndAnswers[lineNumber];
        message.color = Color.green;
        nextMessage = true;
        lineNumber += 2;
        humansWin++;
    }				
    private void choice2()
    {
        lineNumber++;
        message.text = MessageAndAnswers[lineNumber];
        message.color = Color.red;
        nextMessage = true;
        lineNumber++;
        humansWin--;
    }
}
