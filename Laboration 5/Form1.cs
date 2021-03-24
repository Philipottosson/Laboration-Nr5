using Htmlhelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboration_5
{
    public partial class Form1 : Form
    {
        List<string> imagesList = new List<string>();
        HtmlExtractor imageTagList = new HtmlExtractor();
        string input;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_URL_Click(object sender, EventArgs e)
        {
            Regex webb_url = new Regex(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})");
            input =  txt_URL.Text;
            if (webb_url.IsMatch(input))
            {

                imagesList = imageTagList.UrlLink(input).Result;
                if (imagesList.Count >= 1)
                {
                    foreach (var item in imagesList)
                    {
                        txt_multibox.Text += item +"\n";
                    }
                }
            }
            else MessageBox.Show("Wrong Input", "Warning", MessageBoxButtons.OK);
            //txt_multibox.Text = "";
            txt_URL.Text = "";
        
            
        }

        private void btn_saveAll_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            

            // Show the FolderBrowserDialog.
            DialogResult folder = folderBrowserDialog1.ShowDialog();
            if (folder == DialogResult.OK)
            {
                savePictures(folderBrowserDialog1.SelectedPath);
            }
        }
        public async void savePictures(string path)
        {
            int imageNr = 0;
            foreach (var item in imagesList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    imageNr++;
                    await DownloadPicture(path, item, imageNr);
                    label_count.Text = "Images saved: " + imageNr;
                }
            }
        }
        public async Task<bool> DownloadPicture(string path, string url, int imageNr)
        {
            
            HttpClient client = new HttpClient();
            
            string filename;
            //byte[] imageAsByte = await response.Content.ReadAsByteArrayAsync();
            Dictionary<Task<byte[]>, string> downloads = new Dictionary<Task<byte[]>, string>(); ;
            //var fileResponse = await client.GetAsync(url);
            //byte[] byteArray = await fileResponse.Content.ReadAsByteArrayAsync();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string fileExtension = Regex.Match(url, @"(?<=\.)(jpg|jpeg|png|gif|bmp)(?=(""|\?|$))").Value;
            downloads.Add(Task.Run(() => client.GetByteArrayAsync(url)), fileExtension); //nedladdningsjobb med filändelse

            
            while (downloads.Count > 0)
            {
                Task<byte[]> task = await Task.WhenAny(downloads.Keys);
                if (task.Exception == null)
                {
                    string fileEnding = downloads[task];
                    filename = $"\\image{imageNr,000}.{fileEnding}";

                    await SaveFile(path + filename, await task);
                }
                downloads.Remove(task);
            }
            return true;
            //return byteList;

        }
        public async Task SaveFile(string path, byte[] data)
        {
            try
            {
                var fs = new FileStream(path, FileMode.Create);
                await fs.WriteAsync(data, 0, data.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex.Message);
            }
        }

    }
}
