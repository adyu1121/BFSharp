namespace BrainFuck.BFShap
{
    public enum ErrorrCode
    {
        Overflow = 1, Underflow,
        MemoryOver, MemoryUnder,
        LoopIsUnstart, LoopIsUnend,
        //InputFuncIsNull, OutputFuncIsNull,
        CodeEnd
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
