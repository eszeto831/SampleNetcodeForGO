#!/bin/sh
# This is a comment!
echo Hello World Unity Netcode for GO        # This is a comment, too!
now=$(($(date +%s%N)/1000000))
echo "Current time : $now"
#../Builds/TicTacToe/MulticonnectWinServer/MirrorTest.exe -nographics -batchmode
#../Builds/Tank/Server/MirrorTest.exe -nographics -batchmode
../Builds/Server/SampleNetcode.exe -nographics -batchmode
echo Unity Starting