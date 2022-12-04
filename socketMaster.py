from pyModbusTCP.client import ModbusClient
import socket
import time


mHost, mPort = "10.22.233.34", 12345
c = ModbusClient(host=mHost, port=mPort)

host, port = "127.0.0.1", 25001;
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host, port))

xPos = 0
yPos = 0
angle = 0
targetPose = [0, 0, 0] #Vector3   x = 0, y = 0, θ = 0

while True:
    if c.open:
        bits = c.read_holding_registers(0, 12)
        xPos = bits[11]
        yPos = bits[12]
        angle = bits[4]
        
    else:
        print("unable to connect")

    targetPose = [xPos, yPos, 0]
    posString = ','.join(map(str, targetPose)) #Converting Vector3 to a string, example "0,0,0"
    print(posString)

    sock.sendall(posString.encode("UTF-8")) #Converting string to Byte, and sending it to C#
    receivedData = sock.recv(1024).decode("UTF-8") #receiveing data in Byte fron C#, and converting it to String
    print(receivedData)


    
    
    
    
"""}
    targetX = float(input("Target X position: "))
    targetZ = float(input("Target Z position: "))
    targetTheta = float(input("Target theta angle: "))

    targetPose = [targetX, targetZ, targetTheta]
    posString = ','.join(map(str, targetPose)) #Converting Vector3 to a string, example "0,0,0"
    print(posString)

    sock.sendall(posString.encode("UTF-8")) #Converting string to Byte, and sending it to C#
    receivedData = sock.recv(1024).decode("UTF-8") #receiveing data in Byte fron C#, and converting it to String
    print(receivedData)
"""