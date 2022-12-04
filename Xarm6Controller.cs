using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xarm6Controller : MonoBehaviour{
    // Start is called before the first frame update
    public float[] manualAngle;
    public float speed = 1f;

    private Transform[] axis;
    private int[] axisRot = {1, 0, 0, 1, 0, 1};

    private float[] targetAngle;
    

    void Start(){
        targetAngle = new float[6];
        axis = new Transform[6];
        axis[0] = transform.GetChild(0).GetChild(0);
        for (int i=1; i<6; i++){
            axis[i] = axis[i-1].GetChild(0);
            //Debug.Log(axis[i].localRotation.eulerAngles);
        }
    }

    // Update is called once per frame
    void Update(){
        for(int i=0; i<6; i++){
            axis[i].localRotation = Quaternion.Lerp(
                axis[i].localRotation,
                Quaternion.Euler( axisRot[i]==0? targetAngle[i]:axis[i].localRotation.eulerAngles.x, axisRot[i]==1? targetAngle[i]:axis[i].localRotation.eulerAngles.y, axis[i].localRotation.eulerAngles.z),
                Time.deltaTime * speed
            );
        }
    }

    public void SetAngles(float[] angles){
        targetAngle = angles;
    }
}
