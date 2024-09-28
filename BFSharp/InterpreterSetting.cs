namespace BrainFuck.BFSharp
{
    public struct BFSetting
    {
        public bool OverFlowIsError;
        public bool UnderFlowError;
        public bool MemoryUnderIsError;
        public bool MemoryOverIsError;
        public bool SignValueOutput;
        public char SignValue;

        public static readonly BFSetting DefaultBFSetting = new BFSetting()
        {
            OverFlowIsError = true,
            UnderFlowError = true,
            MemoryUnderIsError = true,
            MemoryOverIsError = true,
            SignValueOutput = false,
            SignValue = '?',
        };
    }
    
}
