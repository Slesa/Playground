using System;

namespace DataGridDragDrop.Data
{
    public enum Selectable
    {
        First,
        Second,
        Third,
        Fourth
    }

    [Serializable]
    public class GridItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public bool Checked { get; set; }
        public int IntValue { get; set; }
        public string TextValue { get; set; }
        public Selectable Selection { get; set; }
    }
}