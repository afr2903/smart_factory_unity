using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartController : MonoBehaviour{
    // Start is called before the first frame update
    public int robotID = 0;
    public float targetX = 0;
    public float targetZ = 0;
    public float targetTheta = 0;
    public float speed = 1f;

    private Vector3 targetPose;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

        if( transform.position.x != targetPose.x || transform.position.z != targetPose.y ){
            transform.position = Vector3.Lerp( 
                transform.position,
                new Vector3(targetPose.x/50, transform.position.y, targetPose.y/50),
                Mathf.PingPong(Time.time, speed/200)
            );
        }
        if( transform.rotation.y != targetPose.z){
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, targetPose.z, 0),
                Time.deltaTime * speed
            );
        }
    }

    public void SetPose(Vector3 pose){
        targetPose = pose;
    }

    public Vector3 GetPose(){
        return new Vector3( transform.position.x, transform.position.z, transform.rotation.y );
    }
}
