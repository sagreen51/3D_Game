using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {

    [HideInInspector]
    public bool isDead;

    private Material currentMat;
    private MeshRenderer mesh;
    private PlayerController player;
    private Rigidbody rb;

    private Collider col;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        currentMat = mesh.material;
        if (isDead)
        { 
            gameOver();
            Physics.gravity *= 1.03f;
        }

        if(rb.transform.position.y < -10)
        {
            isDead = true;
        }
    }
    
    //Death ↓

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MeshRenderer>().material.name == currentMat.name)
        {
            col.enabled = false;
            player.enabled = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            isDead = true;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {   
        if (collision.collider.GetComponent<MeshRenderer>().material.name == currentMat.name)
        {
            col.enabled = false;
            player.enabled = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            isDead = true;
        }
    }

    private void gameOver()
    {
        StartCoroutine(delayedLoad());
    }

    IEnumerator delayedLoad()
    {

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
