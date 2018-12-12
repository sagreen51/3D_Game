using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [HideInInspector]
    public bool isDead;

    public float reloadTime;
    
    private PlayerController player;
    private Rigidbody rb;

    private string[] starsCollected = new string[5];
    private int starIndex = 0;

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (isDead)
        { 
            gameOver();
            Physics.gravity *= 1.03f;
        }
        
        if (rb.transform.position.y < -10)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            player.enabled = false;
            isDead = true;
        }
    }
    
    //Death ↓
    
    private void gameOver()
    {
        StartCoroutine(delayedLoad());
    }

    IEnumerator delayedLoad()
    {

        yield return new WaitForSeconds(reloadTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CollectStar(string starColor)
    {
        starsCollected[starIndex++] = starColor;
    }

    public string[] getStars()
    {
        return starsCollected;
    }
}
