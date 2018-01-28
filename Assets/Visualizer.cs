using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour {
    public GameObject square;
    public int scale;
    private GameObject[] squares = new GameObject[8];

    void Start() {
        for (int i = 0; i < 8; i++)
        {
            GameObject single = Instantiate(square, transform.position, transform.rotation);
            squares[i] = single;
            transform.position += Vector3.right * 0.3f;
        }
	}
	
	void Update () {
		for(int i = 0; i < 8; i++)
        {
            squares[i].transform.localScale = new Vector3(0.25f, Listen.buffer[i] * scale, 1);
        }
	}
}
