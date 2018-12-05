using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private Material currentMat;
    private MeshRenderer mesh;
    private PlayerController player;

    private Collider col;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        player = GetComponent<PlayerController>();
    }
    void Update()
    {
        currentMat = mesh.material;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MeshRenderer>().material.name == currentMat.name)
        {
            col.enabled = false;
            player.enabled = false;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {   
        if (collision.collider.GetComponent<MeshRenderer>().material.name == currentMat.name)
        {
            col.enabled = false;
            player.enabled = false;
        }
    }
}
