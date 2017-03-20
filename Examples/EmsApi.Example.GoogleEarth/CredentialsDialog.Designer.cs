namespace EmsApi.Example.GoogleEarth
{
    partial class CredentialsDialog
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
            this.m_endpointBox = new System.Windows.Forms.TextBox();
            this.m_endpointLabel = new System.Windows.Forms.Label();
            this.m_userBox = new System.Windows.Forms.TextBox();
            this.m_userLabel = new System.Windows.Forms.Label();
            this.m_passwordBox = new System.Windows.Forms.TextBox();
            this.m_passLabel = new System.Windows.Forms.Label();
            this.m_loginButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_endpointBox
            // 
            this.m_endpointBox.Location = new System.Drawing.Point(164, 29);
            this.m_endpointBox.Name = "m_endpointBox";
            this.m_endpointBox.Size = new System.Drawing.Size(299, 31);
            this.m_endpointBox.TabIndex = 0;
            // 
            // m_endpointLabel
            // 
            this.m_endpointLabel.AutoSize = true;
            this.m_endpointLabel.Location = new System.Drawing.Point(14, 31);
            this.m_endpointLabel.Name = "m_endpointLabel";
            this.m_endpointLabel.Size = new System.Drawing.Size(142, 25);
            this.m_endpointLabel.TabIndex = 1;
            this.m_endpointLabel.Text = "API Endpoint:";
            // 
            // m_userBox
            // 
            this.m_userBox.Location = new System.Drawing.Point(164, 82);
            this.m_userBox.Name = "m_userBox";
            this.m_userBox.Size = new System.Drawing.Size(299, 31);
            this.m_userBox.TabIndex = 2;
            // 
            // m_userLabel
            // 
            this.m_userLabel.AutoSize = true;
            this.m_userLabel.Location = new System.Drawing.Point(34, 84);
            this.m_userLabel.Name = "m_userLabel";
            this.m_userLabel.Size = new System.Drawing.Size(116, 25);
            this.m_userLabel.TabIndex = 3;
            this.m_userLabel.Text = "Username:";
            // 
            // m_passwordBox
            // 
            this.m_passwordBox.Location = new System.Drawing.Point(164, 135);
            this.m_passwordBox.Name = "m_passwordBox";
            this.m_passwordBox.PasswordChar = '*';
            this.m_passwordBox.Size = new System.Drawing.Size(299, 31);
            this.m_passwordBox.TabIndex = 4;
            // 
            // m_passLabel
            // 
            this.m_passLabel.AutoSize = true;
            this.m_passLabel.Location = new System.Drawing.Point(34, 137);
            this.m_passLabel.Name = "m_passLabel";
            this.m_passLabel.Size = new System.Drawing.Size(112, 25);
            this.m_passLabel.TabIndex = 5;
            this.m_passLabel.Text = "Password:";
            // 
            // m_loginButton
            // 
            this.m_loginButton.Location = new System.Drawing.Point(165, 192);
            this.m_loginButton.Name = "m_loginButton";
            this.m_loginButton.Size = new System.Drawing.Size(94, 51);
            this.m_loginButton.TabIndex = 6;
            this.m_loginButton.Text = "Log In";
            this.m_loginButton.UseVisualStyleBackColor = true;
            this.m_loginButton.Click += new System.EventHandler(this.m_loginButton_Click);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(286, 192);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(95, 51);
            this.m_cancelButton.TabIndex = 7;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // CredentialsDialog
            // 
            this.AcceptButton = this.m_loginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(496, 282);
            this.ControlBox = false;
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_loginButton);
            this.Controls.Add(this.m_passLabel);
            this.Controls.Add(this.m_passwordBox);
            this.Controls.Add(this.m_userLabel);
            this.Controls.Add(this.m_userBox);
            this.Controls.Add(this.m_endpointLabel);
            this.Controls.Add(this.m_endpointBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CredentialsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMS API Log In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_endpointBox;
        private System.Windows.Forms.Label m_endpointLabel;
        private System.Windows.Forms.TextBox m_userBox;
        private System.Windows.Forms.Label m_userLabel;
        private System.Windows.Forms.TextBox m_passwordBox;
        private System.Windows.Forms.Label m_passLabel;
        private System.Windows.Forms.Button m_loginButton;
        private System.Windows.Forms.Button m_cancelButton;
    }
}