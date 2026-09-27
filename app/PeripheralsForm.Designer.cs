using GHelper.Properties;
using GHelper.UI;

namespace GHelper
{
    partial class PeripheralsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelTitle = new Label();
            panelPeripherals = new Panel();
            tableLayoutPeripherals = new TableLayoutPanel();
            buttonPeripheral1 = new RButton();
            buttonPeripheral2 = new RButton();
            buttonPeripheral3 = new RButton();
            buttonQuit = new RButton();
            panelPeripherals.SuspendLayout();
            tableLayoutPeripherals.SuspendLayout();
            SuspendLayout();
            //
            // labelTitle
            //
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitle.Location = new Point(16, 14);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(200, 32);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "GHelper4Peripherals";
            //
            // panelPeripherals
            //
            panelPeripherals.AutoSize = true;
            panelPeripherals.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelPeripherals.Controls.Add(tableLayoutPeripherals);
            panelPeripherals.Dock = DockStyle.Top;
            panelPeripherals.Location = new Point(12, 52);
            panelPeripherals.Margin = new Padding(0);
            panelPeripherals.Name = "panelPeripherals";
            panelPeripherals.Padding = new Padding(0, 4, 0, 0);
            panelPeripherals.Size = new Size(336, 150);
            panelPeripherals.TabIndex = 1;
            panelPeripherals.Visible = false;
            //
            // tableLayoutPeripherals
            //
            tableLayoutPeripherals.AutoSize = true;
            tableLayoutPeripherals.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPeripherals.ColumnCount = 1;
            tableLayoutPeripherals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPeripherals.Controls.Add(buttonPeripheral1, 0, 0);
            tableLayoutPeripherals.Controls.Add(buttonPeripheral2, 0, 1);
            tableLayoutPeripherals.Controls.Add(buttonPeripheral3, 0, 2);
            tableLayoutPeripherals.Dock = DockStyle.Top;
            tableLayoutPeripherals.Location = new Point(0, 4);
            tableLayoutPeripherals.Margin = new Padding(0);
            tableLayoutPeripherals.Name = "tableLayoutPeripherals";
            tableLayoutPeripherals.RowCount = 3;
            tableLayoutPeripherals.RowStyles.Add(new RowStyle());
            tableLayoutPeripherals.RowStyles.Add(new RowStyle());
            tableLayoutPeripherals.RowStyles.Add(new RowStyle());
            tableLayoutPeripherals.Size = new Size(336, 150);
            tableLayoutPeripherals.TabIndex = 0;
            //
            // buttonPeripheral1
            //
            buttonPeripheral1.Activated = false;
            buttonPeripheral1.BackColor = SystemColors.ControlLightLight;
            buttonPeripheral1.BorderColor = Color.Transparent;
            buttonPeripheral1.BorderRadius = 5;
            buttonPeripheral1.Dock = DockStyle.Fill;
            buttonPeripheral1.FlatAppearance.BorderSize = 0;
            buttonPeripheral1.FlatStyle = FlatStyle.Flat;
            buttonPeripheral1.Font = new Font("Segoe UI", 9F);
            buttonPeripheral1.ForeColor = SystemColors.ControlText;
            buttonPeripheral1.Image = Resources.icons8_maus_48;
            buttonPeripheral1.ImageAlign = ContentAlignment.MiddleLeft;
            buttonPeripheral1.Location = new Point(4, 4);
            buttonPeripheral1.Margin = new Padding(4);
            buttonPeripheral1.Name = "buttonPeripheral1";
            buttonPeripheral1.Secondary = true;
            buttonPeripheral1.Size = new Size(328, 42);
            buttonPeripheral1.TabIndex = 0;
            buttonPeripheral1.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonPeripheral1.UseVisualStyleBackColor = false;
            //
            // buttonPeripheral2
            //
            buttonPeripheral2.Activated = false;
            buttonPeripheral2.BackColor = SystemColors.ControlLightLight;
            buttonPeripheral2.BorderColor = Color.Transparent;
            buttonPeripheral2.BorderRadius = 5;
            buttonPeripheral2.Dock = DockStyle.Fill;
            buttonPeripheral2.FlatAppearance.BorderSize = 0;
            buttonPeripheral2.FlatStyle = FlatStyle.Flat;
            buttonPeripheral2.Font = new Font("Segoe UI", 9F);
            buttonPeripheral2.ForeColor = SystemColors.ControlText;
            buttonPeripheral2.Image = Resources.icons8_keyboard_48;
            buttonPeripheral2.ImageAlign = ContentAlignment.MiddleLeft;
            buttonPeripheral2.Location = new Point(4, 54);
            buttonPeripheral2.Margin = new Padding(4);
            buttonPeripheral2.Name = "buttonPeripheral2";
            buttonPeripheral2.Secondary = true;
            buttonPeripheral2.Size = new Size(328, 42);
            buttonPeripheral2.TabIndex = 1;
            buttonPeripheral2.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonPeripheral2.UseVisualStyleBackColor = false;
            //
            // buttonPeripheral3
            //
            buttonPeripheral3.Activated = false;
            buttonPeripheral3.BackColor = SystemColors.ControlLightLight;
            buttonPeripheral3.BorderColor = Color.Transparent;
            buttonPeripheral3.BorderRadius = 5;
            buttonPeripheral3.Dock = DockStyle.Fill;
            buttonPeripheral3.FlatAppearance.BorderSize = 0;
            buttonPeripheral3.FlatStyle = FlatStyle.Flat;
            buttonPeripheral3.Font = new Font("Segoe UI", 9F);
            buttonPeripheral3.ForeColor = SystemColors.ControlText;
            buttonPeripheral3.Image = Resources.icons8_headphones_48;
            buttonPeripheral3.ImageAlign = ContentAlignment.MiddleLeft;
            buttonPeripheral3.Location = new Point(4, 104);
            buttonPeripheral3.Margin = new Padding(4);
            buttonPeripheral3.Name = "buttonPeripheral3";
            buttonPeripheral3.Secondary = true;
            buttonPeripheral3.Size = new Size(328, 42);
            buttonPeripheral3.TabIndex = 2;
            buttonPeripheral3.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonPeripheral3.UseVisualStyleBackColor = false;
            //
            // buttonQuit
            //
            buttonQuit.Activated = false;
            buttonQuit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonQuit.BackColor = SystemColors.ControlLightLight;
            buttonQuit.BorderColor = Color.Transparent;
            buttonQuit.BorderRadius = 5;
            buttonQuit.FlatAppearance.BorderSize = 0;
            buttonQuit.FlatStyle = FlatStyle.Flat;
            buttonQuit.Font = new Font("Segoe UI", 9F);
            buttonQuit.ForeColor = SystemColors.ControlText;
            buttonQuit.Location = new Point(16, 214);
            buttonQuit.Name = "buttonQuit";
            buttonQuit.Secondary = true;
            buttonQuit.Size = new Size(328, 40);
            buttonQuit.TabIndex = 3;
            buttonQuit.Text = "Quit";
            buttonQuit.UseVisualStyleBackColor = false;
            //
            // PeripheralsForm
            //
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(360, 268);
            Controls.Add(buttonQuit);
            Controls.Add(panelPeripherals);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PeripheralsForm";
            Padding = new Padding(12, 12, 12, 12);
            ShowInTaskbar = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GHelper4Peripherals";
            panelPeripherals.ResumeLayout(false);
            panelPeripherals.PerformLayout();
            tableLayoutPeripherals.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitle;
        private Panel panelPeripherals;
        private TableLayoutPanel tableLayoutPeripherals;
        private RButton buttonPeripheral1;
        private RButton buttonPeripheral2;
        private RButton buttonPeripheral3;
        private RButton buttonQuit;
    }
}
