using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Triggers : MonoBehaviour {

    [SerializeField]
    [TextArea]
    private string newText;

    [SerializeField]
    private Text text;

    [SerializeField]
    private float destroyTime;
    
    private void OnTriggerEnter(Collider other)
    {
            text.enabled = true;
            text.text = newText;
            remove();
    }

    private void remove()
    {
        StartCoroutine(delayedDestroy(destroyTime));
    }

    IEnumerator delayedDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        text.enabled = false;
        Destroy(this.gameObject);
    }
}
