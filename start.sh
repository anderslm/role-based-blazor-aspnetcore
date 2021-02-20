#!/bin/sh

dotnet run -p BankApi &
P1=$!
dotnet run -p Website & 
P2=$!

trap ctrl_c INT

function ctrl_c() {
   kill $P1
   kill $P2
}

wait $P1 $P2
