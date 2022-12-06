using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetector : MonoBehaviour{

    public Transform person;
    public Transform rightHand;
    public Transform leftHand;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void FixedUpdate(){
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        //Right hand
        Vector3 rightHandPos = person.position + rightHand.position;
        Vector3 rightHandDir = rightHand.rotation.eulerAngles;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rightHandPos, transform.TransformDirection(rightHandDir), out hit, Mathf.Infinity, layerMask)){
            Debug.DrawRay(rightHandPos, transform.TransformDirection(rightHandDir) * hit.distance, Color.blue);
            Debug.Log("Did Hit");
        }
        else{
            Debug.DrawRay(rightHandPos, transform.TransformDirection(rightHandDir) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
