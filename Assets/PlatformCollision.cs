using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour {
    
    [SerializeField]
    private GameObject player;

    private Collider col;
    private Material mat;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider>();
        mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
