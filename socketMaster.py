from pyModbusTCP.client import ModbusClient
import socket
import time


mHost, mPort = "10.22.240.51", 12345
c = ModbusClient(host=mHost, port=mPort)

host, port = "127.0.0.1", 25001;
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host, port))

nRobots = 2
xPos = []
yPos = []
angle = []
targetPose = [] #Vector3   x = 0, y = 0, θ = 0

xarm_angles = []
input_str = ""

while True:
    input_str = ''
    xPos = []
    yPos = []
    angle = []
    targetPose = []
    xarm_angles = []

    if c.open:
        bits = c.read_holding_registers(0, 37)

        for i in range(0, nRobots):
            # Smart position
            xPos.append( bits[11 + 19*i] )
            yPos.append( bits[12 + 19*i] )
            angle.append( bits[3 + 19*i] )
            targetPose.append( [xPos[i], yPos[i], angle[i]] )

            # Xarm angles
            """xarm_angles.append([])
            for j in range(0, 5):
                temp = str(bits[15+j + 18*i])
                sign = 1

                # Get int sign 10 = -, 11 = +
                if len( temp )>2:
                    if temp[0:1] == "11":
                        sign *= -1
                    xarm_angles[i].append( sign * int(temp[2:]) )
                else:
                    xarm_angles[i].append(0)

                xarm_angles[i].append( bits[15+j + 18*i] )
                """

            #Converting Vector3 to a string, example "0,0,0"
            if i != 0:
                input_str += ','
            input_str += ','.join( map(str, targetPose[i]) )
            input_str += ',0,0,0,0,0'
            #input_str += ','.join( map(str, xarm_angles[i]) )
        
    else:
        print("unable to connect")

    print(":D")
    print(input_str)
    print(":o")
    #test_str = "350,200,50,80,70,-60,30,10,400,100,110,100,-80,30,90,35"

    sock.sendall(input_str.encode("UTF-8")) #Converting string to Byte, and sending it to C#
    input_str = ''
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