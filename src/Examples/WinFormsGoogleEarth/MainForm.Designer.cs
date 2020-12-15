namespace EmsApi.Example.GoogleEarth
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_emsSystemLabel = new System.Windows.Forms.Label();
            this.m_flightIdBox = new System.Windows.Forms.TextBox();
            this.m_flightLabel = new System.Windows.Forms.Label();
            this.m_trajectoryLabel = new System.Windows.Forms.Label();
            this.m_openKmlButton = new System.Windows.Forms.Button();
            this.m_trajectoryTypeDropdown = new EmsApi.Example.GoogleEarth.TrajectoryTypeDropdownBox();
            this.m_emsSystemDropdown = new EmsApi.Example.GoogleEarth.EmsSystemDropdownBox();
            this.SuspendLayout();
            // 
            // m_emsSystemLabel
            // 
            this.m_emsSystemLabel.AutoSize = true;
            this.m_emsSystemLabel.Location = new System.Drawing.Point(46, 46);
            this.m_emsSystemLabel.Name = "m_emsSystemLabel";
            this.m_emsSystemLabel.Size = new System.Drawing.Size(141, 25);
            this.m_emsSystemLabel.TabIndex = 1;
            this.m_emsSystemLabel.Text = "EMS System:";
            // 
            // m_flightIdBox
            // 
            this.m_flightIdBox.Location = new System.Drawing.Point(193, 181);
            this.m_flightIdBox.Name = "m_flightIdBox";
            this.m_flightIdBox.Size = new System.Drawing.Size(347, 31);
            this.m_flightIdBox.TabIndex = 2;
            this.m_flightIdBox.TextChanged += new System.EventHandler(this.m_flightIdBox_TextChanged);
            // 
            // m_flightLabel
            // 
            this.m_flightLabel.AutoSize = true;
            this.m_flightLabel.Location = new System.Drawing.Point(93, 184);
            this.m_flightLabel.Name = "m_flightLabel";
            this.m_flightLabel.Size = new System.Drawing.Size(94, 25);
            this.m_flightLabel.TabIndex = 3;
            this.m_flightLabel.Text = "Flight Id:";
            // 
            // m_trajectoryLabel
            // 
            this.m_trajectoryLabel.AutoSize = true;
            this.m_trajectoryLabel.Location = new System.Drawing.Point(19, 115);
            this.m_trajectoryLabel.Name = "m_trajectoryLabel";
            this.m_trajectoryLabel.Size = new System.Drawing.Size(168, 25);
            this.m_trajectoryLabel.TabIndex = 5;
            this.m_trajectoryLabel.Text = "Trajectory Type:";
            // 
            // m_openKmlButton
            // 
            this.m_openKmlButton.Enabled = false;
            this.m_openKmlButton.Location = new System.Drawing.Point(217, 243);
            this.m_openKmlButton.Name = "m_openKmlButton";
            this.m_openKmlButton.Size = new System.Drawing.Size(134, 48);
            this.m_openKmlButton.TabIndex = 6;
            this.m_openKmlButton.Text = "Open KML";
            this.m_openKmlButton.UseVisualStyleBackColor = true;
            this.m_openKmlButton.Click += new System.EventHandler(this.m_openKmlButton_Click);
            // 
            // m_trajectoryTypeDropdown
            // 
            this.m_trajectoryTypeDropdown.DisplayMember = "TrajectoryId";
            this.m_trajectoryTypeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_trajectoryTypeDropdown.FormattingEnabled = true;
            this.m_trajectoryTypeDropdown.Location = new System.Drawing.Point(193, 112);
            this.m_trajectoryTypeDropdown.Name = "m_trajectoryTypeDropdown";
            this.m_trajectoryTypeDropdown.Size = new System.Drawing.Size(347, 33);
            this.m_trajectoryTypeDropdown.TabIndex = 4;
            this.m_trajectoryTypeDropdown.ValueMember = "TrajectoryId";
            this.m_trajectoryTypeDropdown.SelectedIndexChanged += new System.EventHandler(this.m_trajectoryTypeDropdown_SelectedIndexChanged);
            // 
            // m_emsSystemDropdown
            // 
            this.m_emsSystemDropdown.DisplayMember = "Name";
            this.m_emsSystemDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_emsSystemDropdown.FormattingEnabled = true;
            this.m_emsSystemDropdown.Location = new System.Drawing.Point(193, 43);
            this.m_emsSystemDropdown.Name = "m_emsSystemDropdown";
            this.m_emsSystemDropdown.Size = new System.Drawing.Size(347, 33);
            this.m_emsSystemDropdown.TabIndex = 0;
            this.m_emsSystemDropdown.ValueMember = "Id";
            this.m_emsSystemDropdown.SelectedIndexChanged += new System.EventHandler(this.m_emsSystemDropdown_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 326);
            this.Controls.Add(this.m_openKmlButton);
            this.Controls.Add(this.m_trajectoryLabel);
            this.Controls.Add(this.m_trajectoryTypeDropdown);
            this.Controls.Add(this.m_flightLabel);
            this.Controls.Add(this.m_flightIdBox);
            this.Controls.Add(this.m_emsSystemLabel);
            this.Controls.Add(this.m_emsSystemDropdown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Google Earth Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EmsSystemDropdownBox m_emsSystemDropdown;
        private System.Windows.Forms.Label m_emsSystemLabel;
        private System.Windows.Forms.TextBox m_flightIdBox;
        private System.Windows.Forms.Label m_flightLabel;
        private TrajectoryTypeDropdownBox m_trajectoryTypeDropdown;
        private System.Windows.Forms.Label m_trajectoryLabel;
        private System.Windows.Forms.Button m_openKmlButton;
    }
}

