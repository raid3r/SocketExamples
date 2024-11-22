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

        MyChatClient chatClient;
        MyFileServerClient fileServerClient;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = chatMessages;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewColumn
            {
                DataPropertyName = nameof(ChatMessage.Id),
                HeaderText = "Id",
                CellTemplate = new DataGridViewTextBoxCell()
            });
            dataGridView1.Columns.Add(new DataGridViewColumn
            {
                DataPropertyName = nameof(ChatMessage.DateTime),
                HeaderText = "Time",
                CellTemplate = new DataGridViewTextBoxCell()
            });
            dataGridView1.Columns.Add(new DataGridViewColumn
            {
                DataPropertyName = nameof(ChatMessage.Text),
                HeaderText = "Text",
                CellTemplate = new DataGridViewTextBoxCell()
            });


            chatClient = new MyChatClient();
            fileServerClient = new MyFileServerClient("127.0.0.1", 5001);
            
        }

        private void downloadButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
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
            var text = textBox1.Text;
            Task.Run(async () =>
            {
                await chatClient.SendMessageAsync(new ChatMessage
                {
                    //TODO TO
                   Text = text,
                });
                textBox1.Invoke(() => { textBox1.Text = string.Empty; });
                await LoadMessagesAsync();
            });
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            var result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            Task.Run(async () =>
            {
                var fileData = await fileServerClient.UploadFileAsync(openFileDialog1.FileName);

                //await chatClient.SendMessageAsync(new ChatMessage
                //{
                //    UserName = "test user",
                //    Text = $"Відправка файлу {fileData.Id} {fileData.Filename}",
                //    FileData = fileData
                //});

                await LoadMessagesAsync();
            });

        }


        async Task LoadUsersAsync()
        {
            var maxId = _chatUsers.Count > 0 ? _chatUsers.Max(x => x.Id) : 0;
            //TODO

            //List<ChatUser> users = await chatClient.GetUsersAsync();
            //var newUsers = ;
            //_chatUsers.AddRange(newUsers);
           

            //dataGridView1.Invoke(() =>
            //{
            //    messages.ForEach(x => chatMessages.Add(x));
            //});
        }

        async Task LoadMessagesAsync()
        {
            var maxId = _chatMessages.Count > 0 ? _chatMessages.Max(x => x.Id) : 0;
            var messages = await chatClient.GetMessagesAsync(maxId);
            _chatMessages.AddRange(messages);

            dataGridView1.Invoke(() =>
            {
                messages.ForEach(x => chatMessages.Add(x));
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await LoadMessagesAsync();

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
            timer1.Start();
        }
    }
}
