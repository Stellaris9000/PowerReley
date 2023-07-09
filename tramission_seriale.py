import serial
import time
import os

boardDict = {
            "BOARD_1" : "1",
            "BOARD_2" : "2",
            "BOARD_3" : "3"
            } #TODO -> Add boards when needs accordingly with C code on Nucleo Board


def print_COM_settings(ser):
    set_dict = ser.get_settings()
    print("\n- PORT DATA -\nSending and receiving port: ", ser.port)
    for key, value in set_dict.items():
        print("\t"f'{key:<4} = {value}')


def ser_init(ser):
    # Parametri di init della comunicazione seriale
    ser.port = "COM4"
    ser.baudrate = 115200
    ser.bytesize = 8
    ser.parity = 'N'
    ser.stopbits = 1
    ser.timeout = 10


os.system('cls')
print("Board to restart:")
for k, v in boardDict.items():
    print("\t\t"f'{k:<4} print -> {v}')

serialcmd = input("\nINSERT BOARD TO RESTART: ")
while serialcmd not in str(boardDict.keys()):
    print("Selected board doesn't exist!")
    serialcmd = input("INSERT BOARD TO RESTART: ")

serialPort = serial.Serial()
ser_init(serialPort)
serialPort.open()

if serialPort.is_open:
    print_COM_settings(serialPort)
    serialPort.write(serialcmd.encode())
    timeout = 10
    start_time = time.time()
    while (time.time() - start_time) < timeout:
        bs = serialPort.read(32)
        print(bs.decode())
    serialPort.close()
    time.sleep(3)
    exit
else:
    print("COM PORT UNREACHABLE!")
    time.sleep(3)
    exit
