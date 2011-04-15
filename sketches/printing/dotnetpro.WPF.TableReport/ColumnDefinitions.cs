using System.Collections.ObjectModel;

namespace dotnetpro.WPF.TableReport
{
    public class ColumnDefinitions : KeyedCollection<string, ColumnDefinition>
    {
        protected override string GetKeyForItem(ColumnDefinition item)
        {
            return item.Name;
        }
    }
}
