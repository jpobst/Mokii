Mokii - Toy implementation of MS's "Roslyn" APIs

Roslyn:
http://msdn.microsoft.com/en-us/roslyn

Status:

Right now there is a skeleton VB.Net implementation that basically 
supports the operations in the sample:

https://github.com/jpobst/Mokii/blob/master/MokiiSample/Program.cs


This involves parsing tokens out of a simple VB program:

https://github.com/jpobst/Mokii/blob/master/MokiiSample/input.txt


And then performing a few simple transforms on the token model:

- Replace string literal with another string literal
- Insert spaces in front of open parentheses
- Output new source code with naive keyword highlighting