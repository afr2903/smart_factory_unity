using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour{
    
    public SocketHandler socketHandler;
    public SmartController[] smartControllers;
    private Xarm6Controller[] xarmControllers;

    private int nRobots;

    void Start(){
        nRobots = smartControllers.Length;
        xarmControllers = new Xarm6Controller[ nRobots ];
        for(int i=0; i<nRobots; i++){
            xarmControllers[i] = smartControllers[i].transform.GetChild(0).GetComponent<Xarm6Controller>();
        }
    }

    // Update is called once per frame
    void Update(){
        if( socketHandler.InputReady() ){
            SendInput( socketHandler.GetInput() );
        }
    }

    void SendInput( float[] input ){
        int idx = 0;
        for(int i=0; i<nRobots; i++){
            Vector3 currentPose = new Vector3( input[idx], input[idx+1], input[idx+2] );
            idx += 3;
            float[] currentXarmAngles = { input[idx], input[idx+1], input[idx+2], input[idx+3], input[idx+4], 50 }; // 6th axis manual
            idx += 5;

            smartControllers[i].SetPose( currentPose );
            xarmControllers[i].SetAngles( currentXarmAngles );
        }
    }
}
