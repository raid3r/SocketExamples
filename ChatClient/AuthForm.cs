using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyChatClient = ChatCoreLibrary.Models.ChatClient;

namespace ChatClient;

public partial class AuthForm : Form
{
    private MyChatClient chatClient { get; set; }

    public AuthForm(MyChatClient client)
    {
        this.chatClient = client;
        InitializeComponent();
    }

    private void authButton_Click(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            var result = await chatClient
            .RegisterAsync(new ChatCoreLibrary.Models.AuthData
            {
                Login = loginTextBox.Text,
                Password = passwordTextBox.Text,
            });

            if (result.User != null)
            {
                chatClient.Me = result.User;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                loginTextBox.Invoke(() => loginTextBox.Text = string.Empty);
                passwordTextBox.Invoke(() => passwordTextBox.Text = string.Empty);
            }
        });
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            var result = await chatClient
            .LoginAsync(new ChatCoreLibrary.Models.AuthData
            {
                Login = loginTextBox.Text,
                Password = passwordTextBox.Text,
            });

            if (result.User != null)
            {
                chatClient.Me = result.User;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                loginTextBox.Invoke(() => loginTextBox.Text = string.Empty);
                passwordTextBox.Invoke(() => passwordTextBox.Text = string.Empty);
            }
        });
    }
}
