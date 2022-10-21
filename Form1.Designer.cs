namespace Hosts_changer
{
    partial class mainWindow
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
            this.mainLabel = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.cleanBtn = new System.Windows.Forms.Button();
            this.resetDefault = new System.Windows.Forms.Button();
            this.hostDisplay = new System.Windows.Forms.Button();
            this.cbotDc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainLabel.Location = new System.Drawing.Point(42, 32);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(729, 29);
            this.mainLabel.TabIndex = 0;
            this.mainLabel.Text = "Choose an option how you would like to modify your hosts file";
            // 
            // addBtn
            // 
            this.addBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.Location = new System.Drawing.Point(47, 96);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(187, 55);
            this.addBtn.TabIndex = 1;
            this.addBtn.Text = "Add a custom IP address";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeBtn.Location = new System.Drawing.Point(277, 96);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(187, 55);
            this.removeBtn.TabIndex = 2;
            this.removeBtn.Text = "Remove a specific IP address";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // cleanBtn
            // 
            this.cleanBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cleanBtn.Location = new System.Drawing.Point(508, 96);
            this.cleanBtn.Name = "cleanBtn";
            this.cleanBtn.Size = new System.Drawing.Size(187, 55);
            this.cleanBtn.TabIndex = 3;
            this.cleanBtn.Text = "Clean the whole hosts file";
            this.cleanBtn.UseVisualStyleBackColor = true;
            this.cleanBtn.Click += new System.EventHandler(this.cleanBtn_Click);
            // 
            // resetDefault
            // 
            this.resetDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetDefault.Location = new System.Drawing.Point(52, 201);
            this.resetDefault.Name = "resetDefault";
            this.resetDefault.Size = new System.Drawing.Size(182, 80);
            this.resetDefault.TabIndex = 4;
            this.resetDefault.Text = "Reset hosts file to windows default";
            this.resetDefault.UseVisualStyleBackColor = true;
            this.resetDefault.Click += new System.EventHandler(this.resetDefault_Click);
            // 
            // hostDisplay
            // 
            this.hostDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hostDisplay.Location = new System.Drawing.Point(282, 201);
            this.hostDisplay.Name = "hostDisplay";
            this.hostDisplay.Size = new System.Drawing.Size(182, 80);
            this.hostDisplay.TabIndex = 5;
            this.hostDisplay.Text = "Display current hosts file";
            this.hostDisplay.UseVisualStyleBackColor = true;
            this.hostDisplay.Click += new System.EventHandler(this.hostDisplay_Click);
            // 
            // cbotDc
            // 
            this.cbotDc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbotDc.Location = new System.Drawing.Point(508, 201);
            this.cbotDc.Name = "cbotDc";
            this.cbotDc.Size = new System.Drawing.Size(182, 80);
            this.cbotDc.TabIndex = 6;
            this.cbotDc.Text = "Discord server of our other project";
            this.cbotDc.UseVisualStyleBackColor = true;
            this.cbotDc.Click += new System.EventHandler(this.cbotDc_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 353);
            this.Controls.Add(this.cbotDc);
            this.Controls.Add(this.hostDisplay);
            this.Controls.Add(this.resetDefault);
            this.Controls.Add(this.cleanBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.mainLabel);
            this.Name = "mainWindow";
            this.Text = "Hosts modifier by JammuMies327#5283";
            this.Load += new System.EventHandler(this.mainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button cleanBtn;
        private System.Windows.Forms.Button resetDefault;
        private System.Windows.Forms.Button hostDisplay;
        private System.Windows.Forms.Button cbotDc;
    }
}

