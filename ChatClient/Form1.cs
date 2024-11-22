using ChatCoreLibrary.Models;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using MyChatClient = ChatCoreLibrary.Models.ChatClient;
using MyFileServerClient = ChatCoreLibrary.FileExchage.FileClient;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private List<ChatMessage> _chatMessages = [];
        private List<ChatUser> _chatUsers = [];

        private BindingList<ChatMessage> chatMessages = new();
        private BindingList<ChatUser> chatUsers = new();

        MyChatClient chatClient;
        MyFileServerClient fileServerClient;

        public Form1()
        {
            InitializeComponent();

            usersListBox.DataSource = chatUsers;

            dataGridView1.DataSource = chatMessages;
            //dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.Columns.Clear();
            //dataGridView1.Columns.Add(new DataGridViewColumn
            //{
            //    DataPropertyName = nameof(ChatMessage.Id),
            //    HeaderText = "Id",
            //    CellTemplate = new DataGridViewTextBoxCell()
            //});
            //dataGridView1.Columns.Add(new DataGridViewColumn
            //{
            //    DataPropertyName = nameof(ChatMessage.DateTime),
            //    HeaderText = "Time",
            //    CellTemplate = new DataGridViewTextBoxCell()
            //});
            //dataGridView1.Columns.Add(new DataGridViewColumn
            //{
            //    DataPropertyName = nameof(ChatMessage.Text),
            //    HeaderText = "Text",
            //    CellTemplate = new DataGridViewTextBoxCell()
            //});
            //dataGridView1.Columns.Add(new DataGridViewColumn
            //{
            //    DataPropertyName = "From.Login",
            //    HeaderText = "From",
            //    CellTemplate = new DataGridViewTextBoxCell()
            //});
            //dataGridView1.Columns.Add(new DataGridViewColumn
            //{
            //    DataPropertyName = "To.Login",
            //    //DataPropertyName = nameof(ChatMessage.To.Login),
            //    HeaderText = "To",
            //    CellTemplate = new DataGridViewTextBoxCell()
            //});


            chatClient = new MyChatClient();
            fileServerClient = new MyFileServerClient("127.0.0.1", 5001);

        }

        private void downloadButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                var item = (ChatMessage)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                if (item.FileData != null)
                {
                    saveFileDialog1.FileName = item.FileData.Filename;
                    saveFileDialog1.Filter = $"File (*{Path.GetExtension(item.FileData.Filename)})|*{Path.GetExtension(item.FileData.Filename)}";

                    var result = saveFileDialog1.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        return;
                    }

                    Task.Run(async () =>
                    {
                        item.FileData.Operation = FileOperation.Get;
                        await fileServerClient.DownloadFileAsync(item.FileData, saveFileDialog1.FileName);
                        MessageBox.Show($"Файл збережено в {saveFileDialog1.FileName}");
                    });
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedUser == null)
            {
                return;
            }

            var text = textBox1.Text;
            Task.Run(async () =>
            {
                await chatClient.SendMessageAsync(new ChatMessage
                {
                    To = SelectedUser,
                    Text = text,
                });
                textBox1.Invoke(() => { textBox1.Text = string.Empty; });
                await LoadMessagesAsync();
            });
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedUser == null)
            {
                return;
            }

            var text = textBox1.Text;
            var result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            Task.Run(async () =>
            {
                var fileData = await fileServerClient.UploadFileAsync(openFileDialog1.FileName);

                await chatClient.SendMessageAsync(new ChatMessage
                {
                    To = SelectedUser,
                    Text = $"Відправка файлу {fileData.Id} {fileData.Filename}",
                    FileData = fileData
                });

                await LoadMessagesAsync();
            });

        }


        async Task LoadUsersAsync()
        {
            var maxId = _chatUsers.Count > 0 ? _chatUsers.Max(x => x.Id) : 0;
            List<ChatUser> users = await chatClient.GetUsersAsync();
            var newUsers = users
                .Where(x => !_chatUsers.Select(x => x.Id).ToList().Contains(x.Id) && x.Id != chatClient.Me.Id);
            _chatUsers.AddRange(newUsers);
            _chatUsers.Sort((x, y) => { return x.Login.CompareTo(y.Login); });

            usersListBox.Invoke(() =>
            {
                chatUsers.Clear();
                _chatUsers.ForEach(x => chatUsers.Add(x));
                if (SelectedUser == null && _chatUsers.Count > 0)
                {
                    SelectedUser = chatUsers[0];
                    usersListBox.SelectedIndex = 0;
                }
                
            });

        }

        async Task LoadMessagesAsync()
        {
            var maxId = _chatMessages.Count > 0 ? _chatMessages.Max(x => x.Id) : 0;
            var messages = await chatClient.GetMessagesAsync(maxId);
            _chatMessages.AddRange(messages);

            UpdateMessagesList();

        }

        private List<ChatMessage> SelectedUserMessages
        {
            get
            {
                if (SelectedUser == null)
                {
                    return new();
                }

                return _chatMessages
                .Where(x =>
                (x.From.Id == chatClient.Me.Id && x.To.Id == SelectedUser.Id)
                ||
                (x.To.Id == chatClient.Me.Id && x.From.Id == SelectedUser.Id)
                ).ToList();
            }
        }

        void UpdateMessagesList()
        {
            dataGridView1.Invoke(() =>
            {
                chatMessages.Clear();
                SelectedUserMessages.ForEach(x => chatMessages.Add(x));
            });
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await LoadMessagesAsync();
                await LoadUsersAsync();
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var authForm = new AuthForm(chatClient);
            var res = authForm.ShowDialog();
            if (res != DialogResult.OK)
            {
                Close();
                return;
            }
            meLoginLabel.Text = chatClient.Me.Login;
            Task.Run(async () =>
            {
                await LoadMessagesAsync();
                await LoadUsersAsync();
            });
            timer1.Start();
        }

        private ChatUser SelectedUser { get; set; }

        private void usersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChatUser selected = usersListBox.SelectedItem as ChatUser;
            if (selected != null)
            {
                SelectedUser = selected;
                UpdateMessagesList();
            }
        }
    }
}
