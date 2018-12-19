using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlatformCollision : MonoBehaviour {
    
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float disableTime;
    [SerializeField]
    private float enableTime;


    private Animator anim;
    private Collider col;
    private MeshRenderer mesh;
    private bool destroyed;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MeshRenderer>().material.name != mesh.material.name)
        {
            if(!destroyed)
            {
                anim.Play("FadeOut");
                remove();
                destroyed = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(this.tag == "Final")
        {
            StartCoroutine(delayedfinish(2));
        }
        else if(collision.collider.GetComponent<MeshRenderer>().material.name != mesh.material.name)
        {
            if (!destroyed)
            {
                anim.Play("FadeOut");
                remove();
                destroyed = true;
            }
        }
    }

    private void remove()
    {
        StartCoroutine(delayedDisable(disableTime));
        StartCoroutine(delayedEnable(enableTime));
    }

    IEnumerator delayedDisable(float time)
    {
        yield return new WaitForSeconds(time);
        col.enabled = false;
        mesh.enabled = false;
        player.GetComponent<PlayerController>().isOnWall = false;
    }

    IEnumerator delayedEnable(float time)
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
        mesh.enabled = true;
        anim.Rebind();
        destroyed = false;
    }

    IEnumerator delayedfinish(int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(4);
    }

}