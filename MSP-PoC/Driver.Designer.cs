

namespace MSP_PoC
{
    partial class Driver
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {   
            this.Button = new System.Windows.Forms.Button();
            this.topLabel = new System.Windows.Forms.Label();
            this.choicesList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Button
            // 
            this.Button.Location = new System.Drawing.Point(137, 294);
            this.Button.Margin = new System.Windows.Forms.Padding(2);
            this.Button.Name = "Button";
            this.Button.Size = new System.Drawing.Size(220, 33);
            this.Button.TabIndex = 0;
            this.Button.Text = "Rozpocznij kupno biletów";
            this.Button.UseVisualStyleBackColor = true;
            this.Button.Click += new System.EventHandler(this.Button1_Click);
            // 
            // topLabel
            // 
            this.topLabel.AutoSize = true;
            this.topLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.topLabel.Location = new System.Drawing.Point(89, 9);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(306, 37);
            this.topLabel.TabIndex = 1;
            this.topLabel.Text = "KINO CINEMA CITY";
            this.topLabel.Click += new System.EventHandler(this.topLabel_Click);
            // 
            // choicesList
            // 
            this.choicesList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.choicesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.choicesList.FullRowSelect = true;
            this.choicesList.LabelWrap = false;
            this.choicesList.Location = new System.Drawing.Point(75, 55);
            this.choicesList.Name = "choicesList";
            this.choicesList.Size = new System.Drawing.Size(341, 225);
            this.choicesList.TabIndex = 2;
            this.choicesList.UseCompatibleStateImageBehavior = false;
            this.choicesList.View = System.Windows.Forms.View.Details;
            this.choicesList.SelectedIndexChanged += new System.EventHandler(this.choicesList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Opcja";
            this.columnHeader1.Width = 300;
            // 
            // Driver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 338);
            this.Controls.Add(this.choicesList);
            this.Controls.Add(this.topLabel);
            this.Controls.Add(this.Button);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Driver";
            this.Text = "Driver";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.ListView choicesList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

