using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;

public class SocketHandler : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    float[] receivedInput;
    string currentPose = "";

    bool running;

    private bool inputReady = false;


    private void Start(){
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }

    void GetInfo(){
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running){
            SendAndReceiveData();
        }
        listener.Stop();
    }

    void SendAndReceiveData(){
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

        if (dataReceived != null)
        {
            //---Using received data---
            receivedInput = StringToArr(dataReceived); //<-- assigning receivedPos value from Python
            inputReady = true;
            print("Data received");

            //---Sending Data to Host----
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(":)"); //Converting string to byte data
            nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
        }
    }

    public static float[] StringToArr(string sVector){

        // split the items
        string[] sArray = sVector.Split(',');

        float[] result = new float[sArray.Length];
        for(int i = 0; i<sArray.Length; i++){
            result[i] = float.Parse(sArray[i]);
        }

        return result;
    }

    public bool InputReady(){
        return inputReady;
    }

    public float[] GetInput(){
        inputReady = false;
        return receivedInput;
    }

    /*
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    */
}