using System;
using System.Collections.Generic;
using System.IO;

namespace BrainFuck.BFShap
{
    public class BFSharp
    {
        #region Type def
        private enum symbolList
        {
            Plus = '+',
            Minus = '-',
            PointUp = '>',
            PointDown = '<',
            Input = ',',
            OutPut = '.',
            LoopStart = '[',
            LoopEnd = ']'
        }
        /// <summary>
        /// This is the prototype of the function that will be executed when "," is executed.
        /// </summary>
        /// <returns></returns>
        public delegate char Input();
        /// <summary>
        /// This is the prototype of the function that will be executed when "." is executed.
        /// </summary>
        /// <param name="ASCII"></param>
        public delegate void Output(char ASCII);

        private const ushort MemoryLength = 32768;
        #endregion

        #region Property
        /// <summary>
        /// You can access values in memory
        /// </summary>
        /// <param name="i">Index of memory to access</param>
        /// <returns></returns>
        public long this[int i]
        {
            get { return Memory[i]; }
            set { Memory[i] = value; }
        }
        /// <summary>
        /// You can access BF's code
        /// </summary>
        public string Code
        {
            get
            {
                string result = string.Empty;
                foreach (symbolList symbol in BFCode)
                {
                    result += (char)symbol;
                }
                return result;
            }
            set
            {
                BFCode.Clear();
                Init();
                foreach (char ch in value)
                {
                    if(Enum.IsDefined(typeof(symbolList), (int)ch))
                        BFCode.Add((symbolList)ch);
                }
            }
        }
        /// <summary>
        /// This is the index of the bfcode currently in progress.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// This is the memory address of the current
        /// </summary>
        public int Pointer { get; private set; }
        #endregion

        #region Private valuable
        private long[] Memory = new long[MemoryLength];
        private Stack<int> LoopStack = new Stack<int>();
        private Error LastError = new Error(0,-1);

        private List<symbolList> BFCode = new List<symbolList>();
        private Input InputFunc;
        private Output OutputFunc;
        #endregion

        public BFSharp(Input input, Output output) : this(string.Empty, input, output) { }
        public BFSharp(string code, Input input, Output output)
        {
            Init();
            Code = code;
            SetIn(input);
            SetOut(output);
        }

        /// <summary>
        /// Reset the progress of code
        /// </summary>
        public void Init()
        {
            Index = 0;
            Pointer = 0;
            Array.Fill(Memory, 0);
            LoopStack.Clear();
            LastError = new Error(0, -1);
        }
        /// <summary>
        /// Change the function to run when run ","
        /// </summary>
        /// <param name="input">function that will be executed when "," is executed.</param>
        public void SetIn(Input input)
        {
            InputFunc = input;
        }
        /// <summary>
        /// Change the function to run when run ","
        /// </summary>
        /// <param name="input">function that will be executed when "," is executed.</param>
        public void SetIn(TextReader input)
        {
            InputFunc = () => (char)input.Read();
        }
        /// <summary>
        /// Change the function to run when run "."
        /// </summary>
        /// <param name="output">function that will be executed when "." is executed.</param>
        public void SetOut(Output output)
        {
            OutputFunc = output;
        }
        /// <summary>
        /// Change the function to run when run "."
        /// </summary>
        /// <param name="output">function that will be executed when "." is executed.</param>
        public void SetOut(TextWriter output)
        {
            OutputFunc = x => output.Write(x);
        }
        /// <summary>
        /// Returns the error of the last execution
        /// </summary>
        /// <returns>Last called error</returns>
        public Error GetLastError()
        {
            return LastError;
        }
        /// <summary>
        /// Executes steps as many times as ¡°loop¡±. -1 means it runs until the end.
        /// </summary>
        /// <param name="loop">Number of steps to execute</param>
        /// <returns>Returns true if the step proceeds without error.</returns>
        public bool Stap(int loop)
        {
            for (int i = loop; i != 0; i--)
            {
                bool result = Stap();
                if (!result) return false;
            }
            return true;
        }
        /// <summary>
        /// Run the code one step
        /// </summary>
        /// <returns>Returns true if the step proceeds without error.</returns>
        public bool Stap()
        {
            if (Index > BFCode.Count - 1)
                if (LoopStack.Count == 0) 
                    return RunError(ErrorrCode.CodeEnd);
                else return RunError(ErrorrCode.LoopIsUnend);
            switch (BFCode[Index])
            {
                case symbolList.Plus:
                    if (Memory[Pointer] == long.MaxValue) return RunError(ErrorrCode.Overflow);
                    Memory[Pointer]++;
                    break;
                case symbolList.Minus:
                    if (Memory[Pointer] == long.MinValue) return RunError(ErrorrCode.Underflow);
                    Memory[Pointer]--;
                    break;
                case symbolList.PointUp:
                    if (Pointer == MemoryLength - 1) return RunError(ErrorrCode.MemoryOver);
                    Pointer++;
                    break;
                case symbolList.PointDown:
                    if (Pointer == 0) return RunError(ErrorrCode.MemoryUnder);
                    Pointer--;
                    break;
                case symbolList.Input:
                    Memory[Pointer] = InputFunc();
                    break;
                case symbolList.OutPut:
                    OutputFunc((char)Memory[Pointer]);
                    break;
                case symbolList.LoopStart:
                    if (Memory[Pointer] != 0)
                    {
                        LoopStack.Push(Index);
                    }
                    else
                    {
                        int nestedLoopCount = 0;
                        while (true)
                        {
                            Index++;
                            if (Index > BFCode.Count - 1) return RunError(ErrorrCode.LoopIsUnend);
                            if (BFCode[Index] == symbolList.LoopStart)
                            {
                                nestedLoopCount++;
                            }
                            if (BFCode[Index] == symbolList.LoopEnd)
                            {
                                if (nestedLoopCount != 0)
                                {
                                    nestedLoopCount--;
                                }
                                else break;
                            }
                        }
                    }
                    break;
                case symbolList.LoopEnd:
                    if (LoopStack.Count == 0) return RunError(ErrorrCode.LoopIsUnstart);
                    Index = LoopStack.Pop() - 1;
                    break;
            }
            Index++;
            return true;
        }
        public override string ToString() 
        {
            return Code;
        }
        private bool RunError(ErrorrCode error)
        {
            LastError = new Error(error, Index);
            return false;
        }
    }
}