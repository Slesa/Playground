using System;

namespace NetDLX.Core
{
    // http://cs.uns.edu.ar/~jechaiz/arquitectura/windlx/windlx.html
    public interface IAssemble
    {
        bool CanAssemble(string command);
        UInt32 Translate(string command, string[] args);
    }
}