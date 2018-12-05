﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    private PlayerCollision col;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float blockTime;
    [SerializeField]
    private float unblockTime;

    private float time = 3f;
    private float cameraBlockedRemember;
    private float prevPosBlockedRemember;
    private Vector3 initialOffset;
    private Camera cam;
    private Vector3 prevPos;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        initialOffset = offset;
        col = player.GetComponent<PlayerCollision>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        /*
        cameraBlockedRemember -= Time.deltaTime;
        if(isBlocked(transform.position))
        {
            cameraBlockedRemember = blockTime;
        }

        prevPosBlockedRemember -= Time.deltaTime;
        if(isBlocked(prevPos))
        {
            prevPosBlockedRemember = blockTime;
        }


        if((cameraBlockedRemember > 0))       //if camera gets blocked
        {
            prevPos = transform.position;
            offset = offsetChange(offset);
        }
        else if ((prevPosBlockedRemember > 0))
        {
            prevPos = target.transform.position + initialOffset;
            offset = offsetChange(offset);
        }
        else
        {
            offset = initialOffset;
        }
        */
        
        if (!col.isDead)
        {
            transform.position = player.transform.position + offset;
        }
        else if(time > 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        else {
            transform.LookAt(player.transform);
        }



    }

    Vector3 offsetChange(Vector3 vec)
    {
        vec.y = 1.5f;

        return vec;
    }

    bool isBlocked(Vector3 initialPos)
    {
        return Physics.Raycast(initialPos, -offset, offset.magnitude - 1.5f);
        
    }

}
