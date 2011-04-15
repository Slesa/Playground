using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace dotnetpro.WPF.TableReport
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {

        private bool InEditMode = false;
        private Int16 LastValue;

        public NumericUpDown()
        {
           InitializeComponent();
        }

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new PropertyMetadata(new PropertyChangedCallback(MaximumChanged)));

        private void Up(object sender, RoutedEventArgs e)
        {
            InEditMode = true;
            TextControl.Text = ValidateText(Convert.ToString(Convert.ToInt16(TextControl.Text) + 1), TextControl.Text);
            InEditMode = false;
        }

        private void Down(object sender, RoutedEventArgs e)
        {
            InEditMode = true;
            TextControl.Text = ValidateText(Convert.ToString(Convert.ToInt16(TextControl.Text) - 1), TextControl.Text);

            InEditMode = false;
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(NumericUpDown), new PropertyMetadata(new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            nud.InEditMode = true;
            String oldValue;
            if (e.OldValue == null)
                oldValue = "";
            else
                oldValue = e.OldValue.ToString();
            nud.TextControl.Text = nud.ValidateText(e.NewValue.ToString(), oldValue);
            nud.InEditMode = false;
        }

        private static void MaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            nud.InEditMode = true;
            if (nud.Maximum < Convert.ToInt32(nud.Text))
                nud.TextControl.Text = nud.ValidateText(nud.Maximum.ToString(), nud.Maximum.ToString());
            nud.InEditMode = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int16 NewOK = 0;
            Int16.TryParse(TextControl.Text, out NewOK);
            if (NewOK > 0) LastValue = NewOK;
            if (Text != TextControl.Text) Text = TextControl.Text;
            if (InEditMode) return;
            TextControl.Text = ValidateText(TextControl.Text, Convert.ToString(LastValue));
        }

        private String ValidateText(String ChangedText, String OldText)
        {
            int newValue = 0;
            int.TryParse(ChangedText, out newValue);
            if (newValue == 0)
                return OldText;

            if (newValue < Minimum)
                return Convert.ToString(Minimum);

            if (newValue > Maximum & Maximum > 0)
                return Convert.ToString(Maximum);

            return ChangedText;
        }

        internal void OnTextBoxKeyDownInternal(Key key)
        {
            switch (key)
            {
                case Key.Prior:
                    Down(null, null);
                    return;
                case Key.Next:
                    Up(null, null);
                    return;
                case Key.Up:
                    Up(null, null);
                    return;
                case Key.Down:
                    Down(null, null);
                    return;
                default:
                    return;
            }
        }

        private void TextControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyUp(e);
            this.OnTextBoxKeyDownInternal(e.Key);
        }

        private void Border_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }
    }
}

