using System.Collections.Generic;

namespace NetDLX.Code
{
    public class Program
    {
        public int CurrentAddress { get; set; }
        public string Name { get; set; }
        public List<byte> Code { get; private set; }
        public List<Label> Labels { get; private set; }

        public Program()
        {
            CurrentAddress = 0;
            Code = new List<byte>();
            Labels = new List<Label>();
        }

        public void AddLabel(Label label)
        {
            label.Address = CurrentAddress;
            CurrentAddress += label.Size;
            Labels.Add(label);
        }
    }
}