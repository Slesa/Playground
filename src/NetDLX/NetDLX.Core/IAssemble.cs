using System;

namespace NetDLX.Core
{
    public interface IAssemble
    {
        bool CanAssemble(string command);
        UInt32 Translate(string command, string[] args);
    }
}