using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Hear : MonoBehaviour {
    private KeywordRecognizer recognizer;
    private Dictionary<string, System.Action> words = new Dictionary<string, System.Action>();
    public AudioSource song;

    void Start()
    {
        song = GetComponent<AudioSource>();

        words.Add("testing", () => { testCall(); });               //map a phrase to a function
        recognizer = new KeywordRecognizer(words.Keys.ToArray());  //initialize recognizer, uses dictionary
        recognizer.OnPhraseRecognized += TextToAction;             //when a phrase in dict is recognized, execute its function
        recognizer.Start();                                        //activate recognizer
    }

    private void TextToAction(PhraseRecognizedEventArgs args)
    {
        Action action;
        if (words.TryGetValue(args.text, out action))
        {
            action.Invoke();
        }
    }

    private void testCall()
    {
        song.Play();
    }
}
