using System.Collections.Generic;

namespace NetDLX.Code
{
    public class Program
    {
        public int CurrentAddress { get; set; }
        public string Name { get; set; }
        public List<uint> Code { get; private set; }
        public List<Label> Labels { get; private set; }

        public Program()
        {
            CurrentAddress = 0;
            Code = new List<uint>();
            Labels = new List<Label>();
        }

        public void AddLabel(Label label)
        {
            label.Address = CurrentAddress;
            CurrentAddress += label.Size;
            Labels.Add(label);
        }

        public void AddCode(uint opCode)
        {
            Code.Add(opCode);
            CurrentAddress += 1;
        }
    }
}