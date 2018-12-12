using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public Player player;

    private string starColor;
    private MeshRenderer mesh;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();
        starColor = mesh.material.name;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MeshRenderer>().material.name == starColor)
        {
            player.CollectStar(starColor);
            Destroy(this.gameObject);
        }
    }
}
