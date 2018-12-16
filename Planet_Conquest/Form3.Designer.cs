namespace Planet_Conquest
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxOffense = new System.Windows.Forms.TextBox();
            this.textBoxDefense = new System.Windows.Forms.TextBox();
            this.textBoxOffenseTitle = new System.Windows.Forms.TextBox();
            this.textBoxDefenseTitle = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonRetreat = new System.Windows.Forms.Button();
            this.textBoxAction = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxOffense
            // 
            this.textBoxOffense.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOffense.ForeColor = System.Drawing.Color.White;
            this.textBoxOffense.Location = new System.Drawing.Point(45, 127);
            this.textBoxOffense.Multiline = true;
            this.textBoxOffense.Name = "textBoxOffense";
            this.textBoxOffense.ReadOnly = true;
            this.textBoxOffense.Size = new System.Drawing.Size(180, 200);
            this.textBoxOffense.TabIndex = 0;
            // 
            // textBoxDefense
            // 
            this.textBoxDefense.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDefense.ForeColor = System.Drawing.Color.White;
            this.textBoxDefense.Location = new System.Drawing.Point(549, 127);
            this.textBoxDefense.Multiline = true;
            this.textBoxDefense.Name = "textBoxDefense";
            this.textBoxDefense.ReadOnly = true;
            this.textBoxDefense.Size = new System.Drawing.Size(180, 200);
            this.textBoxDefense.TabIndex = 1;
            // 
            // textBoxOffenseTitle
            // 
            this.textBoxOffenseTitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOffenseTitle.ForeColor = System.Drawing.Color.White;
            this.textBoxOffenseTitle.Location = new System.Drawing.Point(45, 71);
            this.textBoxOffenseTitle.Multiline = true;
            this.textBoxOffenseTitle.Name = "textBoxOffenseTitle";
            this.textBoxOffenseTitle.ReadOnly = true;
            this.textBoxOffenseTitle.Size = new System.Drawing.Size(180, 50);
            this.textBoxOffenseTitle.TabIndex = 2;
            // 
            // textBoxDefenseTitle
            // 
            this.textBoxDefenseTitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDefenseTitle.ForeColor = System.Drawing.Color.White;
            this.textBoxDefenseTitle.Location = new System.Drawing.Point(549, 71);
            this.textBoxDefenseTitle.Multiline = true;
            this.textBoxDefenseTitle.Name = "textBoxDefenseTitle";
            this.textBoxDefenseTitle.ReadOnly = true;
            this.textBoxDefenseTitle.Size = new System.Drawing.Size(180, 50);
            this.textBoxDefenseTitle.TabIndex = 3;
            // 
            // buttonContinue
            // 
            this.buttonContinue.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContinue.Location = new System.Drawing.Point(271, 362);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(86, 32);
            this.buttonContinue.TabIndex = 4;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonRetreat
            // 
            this.buttonRetreat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRetreat.Location = new System.Drawing.Point(417, 362);
            this.buttonRetreat.Name = "buttonRetreat";
            this.buttonRetreat.Size = new System.Drawing.Size(86, 32);
            this.buttonRetreat.TabIndex = 5;
            this.buttonRetreat.Text = "Retreat";
            this.buttonRetreat.UseVisualStyleBackColor = true;
            this.buttonRetreat.Click += new System.EventHandler(this.buttonRetreat_Click);
            // 
            // textBoxAction
            // 
            this.textBoxAction.BackColor = System.Drawing.Color.Black;
            this.textBoxAction.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAction.ForeColor = System.Drawing.Color.Red;
            this.textBoxAction.Location = new System.Drawing.Point(271, 102);
            this.textBoxAction.Multiline = true;
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.ReadOnly = true;
            this.textBoxAction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAction.Size = new System.Drawing.Size(232, 225);
            this.textBoxAction.TabIndex = 6;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Planet_Conquest.Properties.Resources.combatBackground;
            this.ClientSize = new System.Drawing.Size(784, 436);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxAction);
            this.Controls.Add(this.buttonRetreat);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.textBoxDefenseTitle);
            this.Controls.Add(this.textBoxOffenseTitle);
            this.Controls.Add(this.textBoxDefense);
            this.Controls.Add(this.textBoxOffense);
            this.Name = "Form3";
            this.Text = "Combat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOffense;
        private System.Windows.Forms.TextBox textBoxDefense;
        private System.Windows.Forms.TextBox textBoxOffenseTitle;
        private System.Windows.Forms.TextBox textBoxDefenseTitle;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonRetreat;
        private System.Windows.Forms.TextBox textBoxAction;
    }
}