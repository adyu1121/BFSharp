namespace BrainFuck.BFSharp
{
    public enum ErrorrCode : sbyte
    {
        Overflow = 1, Underflow,
        MemoryOver, MemoryUnder,
        LoopIsUnstart, LoopIsUnend,
        EOFInput, SignOutput,
        //InputFuncIsNull, OutputFuncIsNull,
        CodeEnd = 0
    }
    public struct Error
    {
        public ErrorrCode error;
        public long index;
        public Error(ErrorrCode error, long index)
        {
            this.error = error;
            this.index = index;
        }
    }
}
