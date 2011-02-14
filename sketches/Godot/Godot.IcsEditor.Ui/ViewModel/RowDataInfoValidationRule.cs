using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

// http://www.codeproject.com/KB/WPF/WPFDataGridExamples.aspx#details
namespace Godot.IcsEditor.Ui.ViewModel
{
    public class RowDataInfoValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var group = (BindingGroup)value;

            StringBuilder error = null;
            foreach (var item in group.Items)
            {
                // aggregate errors
                var info = item as IDataErrorInfo;
                if (info == null) continue;
                if (!string.IsNullOrEmpty(info.Error))
                {
                    if (error == null)
                        error = new StringBuilder();
                    error.Append((error.Length != 0 ? ", " : "") + info.Error);
                }
            }

            return error != null ? new ValidationResult(false, error.ToString()) : ValidationResult.ValidResult;
        }
    }
}