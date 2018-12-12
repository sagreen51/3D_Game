using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasText : MonoBehaviour {

    [SerializeField]
    private Player player;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
    public void updateCanvas(string newText)
    {
        text.text = newText;
    }
}
