using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboration_5
{
    public partial class Form1 : Form
    {
        List<string> imagesList = new List<string>();
        string input;
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// "Extract" button 
        /// Will check if the input is correct 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_URL_Click(object sender, EventArgs e)
        {
            Regex webb_url = new Regex(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})");
            input =  txt_URL.Text;
            if (webb_url.IsMatch(input))
            {

                imagesList = UrlLink(input).Result;
                if (imagesList.Count >= 1)
                {
                    foreach (var item in imagesList)
                    {
                        txt_multibox.Text += item +"\n";
                    }
                }
            }
            else MessageBox.Show("Wrong Input", "Warning", MessageBoxButtons.OK);
            txt_URL.Text = "";
        }
        /// <summary>
        /// "Save all" button
        /// Will ask the user to select a folder
        /// Send it to ImageURL method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_saveAll_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult folder = folderBrowserDialog1.ShowDialog();
            if (folder == DialogResult.OK)
            {
                ImageURL(folderBrowserDialog1.SelectedPath);
            }
        }
        /// <summary>
        /// Will check every url from the list with the imageURL:s
        /// and each one of them to "DownloadPicture" method together
        /// with the download path and the number for the image
        /// </summary>
        /// <param name="path"></param>
        public async void ImageURL(string path)
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
        /// <summary>
        /// Will tke the image-url and create a HttpClient and save the image as byte Array 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="url"></param>
        /// <param name="imageNr"></param>
        /// <returns></returns>
        public async Task<bool> DownloadPicture(string path, string url, int imageNr)
        {
            
            HttpClient client = new HttpClient();
            string filename;
            Dictionary<Task<byte[]>, string> downloads = new Dictionary<Task<byte[]>, string>(); ;
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string fileExtension = Regex.Match(url, @"(?<=\.)(jpg|jpeg|png|gif|bmp)(?=(""|\?|$))").Value;
            downloads.Add(Task.Run(() => client.GetByteArrayAsync(url)), fileExtension); //nedladdningsjobb med filändelse
            
            
            Task<byte[]> task = await Task.WhenAny(downloads.Keys);
            if (task.Exception == null)
            {
                string fileEnding = downloads[task];
                filename = $"\\image{imageNr,000}.{fileEnding}";

                await SaveFile(path + filename, await task);
            }
            downloads.Remove(task);
            
            return true;
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

        /// <summary>
        /// Will open the webbsite and find all the urls to images in this site
        /// </summary>
        /// <param name="url">Webb url</param>
        /// <returns>listValues</returns>
        public async Task<List<string>> UrlLink(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string html = await client.GetStringAsync(url);
            List<string> listValues = SeparateValues(html, url);
            return listValues;
        }

        /// <summary>
        /// Will take all the images and put them in a list!
        /// </summary>
        /// <param name="imageURL"></param>
        /// <param name="url">webb url</param>
        /// <returns>list</returns>
        public List<string> SeparateValues(string imageURL, string url)
        {
            List<string> list = new List<string>();
            string regexCheckimg = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection collection = Regex.Matches(imageURL, regexCheckimg, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match item in collection)
            {
                string temp = item.Groups[1].Value;
                if (temp.Contains("http"))
                {
                    list.Add(temp);
                }
                else list.Add(url + temp);
            }
            return list;
        }

    }
}
