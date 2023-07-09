#!/bin/bash

boardDict=(["BOARD_1"]="1" ["BOARD_2"]="2" ["BOARD_3"]="3") #TODO -> Add boards when needs accordingly with C code on Nucleo Board

function print_COM_settings() {
    set_dict=$(stty -F $1 -a)
    echo -e "\n- PORT DATA -\nSending and receiving port: $1"
    echo "$set_dict"
}

function ser_init() {
    # Parametri di init della comunicazione seriale
    stty -F $1 115200 cs8 -cstopb -parity -icanon min 0 time 10
}

clear
echo "Board to restart:"
for k in "${!boardDict[@]}"; do
    printf "\t\t%s print -> %s\n" "$k" "${boardDict[$k]}"
done

read -p $'\nINSERT BOARD TO RESTART: ' serialcmd
while [ ! ${boardDict[$serialcmd]+_} ]; do
    echo "Selected board doesn't exist!"
    read -p "INSERT BOARD TO RESTART: " serialcmd
done

serialPort="/dev/ttyS0"
ser_init $serialPort
echo "$(print_COM_settings $serialPort)"
echo -e "$serialcmd" > $serialPort
timeout=10
start_time=$(date +%s)
while [[ $(( $(date +%s) - start_time )) -lt $timeout ]]; do
    bs=$(cat $serialPort)
    echo -e "$bs"
done
sleep 3
exit
