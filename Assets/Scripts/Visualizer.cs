using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour {
    public GameObject square;
    public int scale;
    private GameObject[] squares = new GameObject[64];


    // Use this for initialization
    void Start() {
        for (int i = 0; i < 64; i++)
        {
            GameObject single = Instantiate(square, transform.position, transform.rotation);
            squares[i] = single;
            transform.position += Vector3.right;
        }
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 64; i++)
        {
            squares[i].transform.localScale = new Vector3(1, Listen.spectrum[i] * scale, 1);
        }
	}
}
