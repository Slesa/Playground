using System.ComponentModel;
using System.Windows;
using System;

namespace dotnetpro.WPF.TableReport
{
    public class ColumnDefinition
    {
        /// <summary>
        /// Name der Spalte.
        /// </summary>
        /// <remarks>Wird als Zuordnung zu den daten verwendet.</remarks>
        public string Name { get; set; }

        /// <summary>
        /// Titel der Spalte.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Typ der Spalte.
        /// </summary>
        /// <remarks>Definiert wie die Werte in der Splate formatiert werden.</remarks>
        public Type Type { get; set; }

        /// <summary>
        /// Breite der Spalte.
        /// </summary>
        public double Width { get; set; }

        public TextAlignment Alignment
        {
            get
            {
                return Type.Name == "String" ? TextAlignment.Left : TextAlignment.Right;
            }
        }

    }
}
