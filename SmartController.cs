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

    public SocketHandler socketHandler;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        

        targetPose = socketHandler.GetPose();

        if( transform.position.x != targetPose.x || transform.position.z != targetPose.y ){
            transform.position = Vector3.Lerp( 
                transform.position,
                new Vector3(targetPose.x/100, transform.position.y, targetPose.y/100),
                Mathf.PingPong(Time.time, speed/100)
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

    void SendPose(){
        Vector3 newPose = new Vector3(targetX, targetZ, targetTheta);
        string encodedPose = newPose.ToString();
    }
}
