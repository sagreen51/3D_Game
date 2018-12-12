using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour {
    
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float desTime;
    
    private Animator anim;
    private Collider col;
    private Material mat;
    private bool destroyed;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider>();
        mat = GetComponent<MeshRenderer>().material;
        anim = GetComponent<Animator>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MeshRenderer>().material.name != mat.name)
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
        if(collision.collider.GetComponent<MeshRenderer>().material.name != mat.name)
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
        StartCoroutine(delayedDestroy(desTime));
    }

    IEnumerator delayedDestroy(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
