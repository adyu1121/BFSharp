[![NuGet Badge](https://buildstats.info/nuget/BFshap)](https://www.nuget.org/packages/BFshap/)
# BFShap
It is a BF implementation made with C#.

>DotNet Framework : 6.0

## How to use it to your .Net project.
First, Using NameSpace BrainFuck.BFShap
----------
`using BrainFuck.BFShap`

Next, define twe function .
--------
ex :

```
static char input() {
    return Console.Read();
 }
 ```
 
```
static void output(char ASCII){
    Console.Write(ASCII)
}
```

Creat BF Cless.
-------------
`BF bf = new BF(input, output);`


Access the "bf.Code" property and enter the bf code.
--------------
`bf.Code = "++++++++++
\[>+++++++>++++++++++>+++>+<<<<-\]
\>++.\>+.+++++++..+++.>++++++++++++++.------------.<<+++++++++++++++.>.+++.------.--------.\>+.";`

Invoke "Step"
-----
`bf.Step();`

Run the code one step

`bf.Step(10);`

Run the code 10 step

When a parameter is received, it executes as many steps as the parameter.

Pass an argument "-1" to "Step"
---------
`bf.Step(-1);`

whe Pass an argument "-1" to "Step", it will run the bf code to the end

>To see more features, please read https://adyu1121.github.io/apiv1.1.html

To check all versions, read Docs

Docs link : https://adyu1121.github.io/

Since I am Korean, there may be grammatical errors.
Please let me know if there are any grammar mistakes.

>mail: adyu1121@gmail.com
