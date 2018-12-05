using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float blockTime;
    [SerializeField]
    private float unblockTime;

    private float cameraBlockedRemember;
    private float prevPosBlockedRemember;
    private Vector3 initialOffset;
    private Camera cam;
    private Vector3 prevPos;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        initialOffset = offset;
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
        transform.position = target.transform.position + offset;
        
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
