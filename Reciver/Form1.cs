using System;
using System.Windows.Forms;

namespace Reciver
{
    public partial class Form1 : Form
    {
        private const double R = 287;      
        private const double V = 1.0;           
        private const int MaxInputLength = 20; 

        private string _lastValidPressure;
        private string _lastValidTemp;

        public Form1()
        {
            InitializeComponent();

            _lastValidPressure = Pressure.Text;
            _lastValidTemp = Temperature.Text;

            Pressure.TextChanged += NumberField_TextChanged;
            Temperature.TextChanged += NumberField_TextChanged;
        }

        private void RezButton_Click(object sender, EventArgs e)
        {
            if (!TryReadDouble(Pressure.Text, out double p))
            {
                ShowError("Поле «Давление» пустое или содержит недопустимое значение.");
                return;
            }
            if (!TryReadDouble(Temperature.Text, out double T))
            {
                ShowError("Поле «Температура» пустое или содержит недопустимое значение.");
                return;
            }
            if (T <= -273.0)
            {
                ShowError("Температура не может быть равна –273 °C или ниже (абсолютный ноль).");
                return;
            }

            double m = (p + 1.0) * 98000.0 * V / (R * (T + 273.0));

            Massa.Text = m.ToString("F4") + " кг";
        }


        private void NumberField_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            string value = tb.Text;

            if (value.Length == 0)
            {
                RememberValid(tb, value);
                return;
            }

            if (!IsValidDecimal(value))
            {
                RevertToLastValid(tb);
                return;
            }

            if (value.Length > MaxInputLength)
            {
                tb.Text = value.Substring(0, MaxInputLength);
                tb.SelectionStart = MaxInputLength;
                RememberValid(tb, tb.Text);
                return;
            }

            RememberValid(tb, value);
        }

        private static bool IsValidDecimal(string s)
        {
            bool hasComma = false;
            foreach (char c in s)
            {
                if (c == ',')
                {
                    if (hasComma) return false; 
                    hasComma = true;
                }
                else if (!char.IsDigit(c))
                {
                    return false;           
                }
            }
            return true;
        }

        private void RevertToLastValid(TextBox tb)
        {
            string last = (tb == Pressure) ? _lastValidPressure : _lastValidTemp;

            int caret = Math.Min(Math.Max(tb.SelectionStart - 1, 0), last.Length);

            tb.Text = last;
            tb.SelectionStart = caret;
        }

        private void RememberValid(TextBox tb, string value)
        {
            if (tb == Pressure) _lastValidPressure = value;
            else _lastValidTemp = value;
        }

        private static bool TryReadDouble(string text, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(text) || text.Trim() == ",")
                return false;

            return double.TryParse(text, out value);
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка ввода",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                RezButton.PerformClick();
            }
        }
    }
}