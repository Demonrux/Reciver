using System.Drawing;
using System.Windows.Forms;

namespace Reciver
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private static readonly Color ColorHeaderBg = Color.FromArgb(44, 62, 80);
        private static readonly Color ColorHeaderSubtle = Color.FromArgb(176, 190, 197);
        private static readonly Color ColorFormBg = Color.FromArgb(244, 246, 247);
        private static readonly Color ColorCardBg = Color.White;
        private static readonly Color ColorResultBg = Color.FromArgb(232, 248, 245);
        private static readonly Color ColorTextPrimary = Color.FromArgb(55, 71, 79);
        private static readonly Color ColorAccent = Color.FromArgb(26, 188, 156);
        private static readonly Color ColorAccentHover = Color.FromArgb(22, 160, 133);
        private static readonly Color ColorAccentPress = Color.FromArgb(20, 143, 119);

        private static readonly Font FontTitle = new Font("Segoe UI", 20F, FontStyle.Bold);
        private static readonly Font FontSubtitle = new Font("Segoe UI", 9F);
        private static readonly Font FontCaption = new Font("Segoe UI", 10F);
        private static readonly Font FontInput = new Font("Segoe UI", 12F);
        private static readonly Font FontResult = new Font("Segoe UI", 24F, FontStyle.Bold);
        private static readonly Font FontButton = new Font("Segoe UI", 12F, FontStyle.Bold);
        private static readonly Font FontDefault = new Font("Segoe UI", 9F);

        private const int FormWidth = 500;
        private const int FormHeight = 546;
        private const int Margin = 24;
        private const int HeaderHeight = 90;
        private const int CardHeight = 210;
        private const int ResultHeight = 110;
        private const int ButtonHeight = 52;
        private const int FieldLeft = 20;
        private const int FieldWidth = 412;
        private const int FieldHeight = 34;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;

        private Panel inputCard;
        private Label lblPressureCaption;
        private TextBox Pressure;
        private Label lblTempCaption;
        private TextBox Temperature;

        private Panel resultPanel;
        private Label lblMassCaption;
        private Label Massa;

        private Button RezButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            CreateHeaderPanel();
            CreateInputCard();
            CreateResultPanel();
            CreateActionButton();
            ConfigureForm();

            this.ResumeLayout(false);
        }

        private void CreateHeaderPanel()
        {
            headerPanel = new Panel
            {
                BackColor = ColorHeaderBg,
                Dock = DockStyle.Top,
                Size = new Size(FormWidth, HeaderHeight),
                TabIndex = 0
            };

            lblTitle = new Label
            {
                AutoSize = true,
                Font = FontTitle,
                ForeColor = Color.White,
                Location = new Point(Margin, 16),
                Text = "Ресивер"
            };

            lblSubtitle = new Label
            {
                AutoSize = true,
                Font = FontSubtitle,
                ForeColor = ColorHeaderSubtle,
                Location = new Point(Margin, 55),
                Text = "Расчёт массы газа по уравнению состояния"
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
        }

        private void CreateInputCard()
        {
            inputCard = new Panel
            {
                BackColor = ColorCardBg,
                Location = new Point(Margin, 116),
                Size = new Size(FormWidth - 2 * Margin, CardHeight),
                TabIndex = 1
            };

            lblPressureCaption = CreateCaption("Давление, ати (избыточное)", FieldLeft, 16);
            Pressure = CreateNumberField("0", FieldLeft, 40, 1);

            lblTempCaption = CreateCaption("Температура, °C", FieldLeft, 92);
            Temperature = CreateNumberField("0", FieldLeft, 116, 2);

            inputCard.Controls.Add(lblPressureCaption);
            inputCard.Controls.Add(Pressure);
            inputCard.Controls.Add(lblTempCaption);
            inputCard.Controls.Add(Temperature);
        }

        private void CreateResultPanel()
        {
            resultPanel = new Panel
            {
                BackColor = ColorResultBg,
                Location = new Point(Margin, 340),
                Size = new Size(FormWidth - 2 * Margin, ResultHeight),
                TabIndex = 2
            };

            lblMassCaption = new Label
            {
                AutoSize = true,
                Font = FontCaption,
                ForeColor = ColorAccentHover,
                Location = new Point(FieldLeft, 14),
                Text = "Масса газа, кг"
            };

            Massa = new Label
            {
                AutoSize = false,
                Font = FontResult,
                ForeColor = ColorAccentHover,
                Location = new Point(FieldLeft, 40),
                Size = new Size(FieldWidth, 55),
                Text = "—",
                TextAlign = ContentAlignment.MiddleLeft
            };

            resultPanel.Controls.Add(lblMassCaption);
            resultPanel.Controls.Add(Massa);
        }

        private void CreateActionButton()
        {
            RezButton = new Button
            {
                BackColor = ColorAccent,
                FlatStyle = FlatStyle.Flat,
                Font = FontButton,
                ForeColor = Color.White,
                Location = new Point(Margin, 470),
                Size = new Size(FormWidth - 2 * Margin, ButtonHeight),
                TabIndex = 3,
                Text = "ВЫЧИСЛИТЬ МАССУ",
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Hand
            };

            RezButton.FlatAppearance.BorderSize = 0;
            RezButton.FlatAppearance.MouseOverBackColor = ColorAccentHover;
            RezButton.FlatAppearance.MouseDownBackColor = ColorAccentPress;
            RezButton.Click += RezButton_Click;
        }

        private void ConfigureForm()
        {
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = ColorFormBg;
            this.ClientSize = new Size(FormWidth, FormHeight);
            this.Font = FontDefault;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Ресивер";

            this.Controls.Add(RezButton);
            this.Controls.Add(resultPanel);
            this.Controls.Add(inputCard);
            this.Controls.Add(headerPanel);
        }

        private Label CreateCaption(string text, int x, int y)
        {
            return new Label
            {
                AutoSize = true,
                Font = FontCaption,
                ForeColor = ColorTextPrimary,
                Location = new Point(x, y),
                Text = text
            };
        }

        private TextBox CreateNumberField(string initialText, int x, int y, int tabIndex)
        {
            var tb = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = FontInput,
                Location = new Point(x, y),
                Size = new Size(FieldWidth, FieldHeight),
                MaxLength = MaxInputLength,             
                TabIndex = tabIndex,
                Text = initialText
            };

            tb.KeyDown += Input_KeyDown;       
            return tb;
        }
    }
}