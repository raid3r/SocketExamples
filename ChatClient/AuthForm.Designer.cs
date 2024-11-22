namespace ChatClient
{
    partial class AuthForm
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
            authButton = new Button();
            loginTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            SuspendLayout();
            // 
            // authButton
            // 
            authButton.Location = new Point(59, 193);
            authButton.Name = "authButton";
            authButton.Size = new Size(75, 23);
            authButton.TabIndex = 0;
            authButton.Text = "Register";
            authButton.UseVisualStyleBackColor = true;
            authButton.Click += authButton_Click;
            // 
            // loginTextBox
            // 
            loginTextBox.Location = new Point(59, 93);
            loginTextBox.Name = "loginTextBox";
            loginTextBox.Size = new Size(151, 23);
            loginTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(59, 133);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(151, 23);
            passwordTextBox.TabIndex = 2;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(147, 195);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(75, 23);
            loginButton.TabIndex = 3;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 450);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(loginTextBox);
            Controls.Add(authButton);
            Name = "AuthForm";
            Text = "AuthForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button authButton;
        private TextBox loginTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
    }
}