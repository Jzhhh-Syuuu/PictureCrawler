using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using mshtml;
using System.Configuration;

namespace PictureCrawler
{
    public partial class PictureCrawler : Form
    {
        public PictureCrawler()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void Crawl_Click(object sender, EventArgs e)
        {
            this.outputText.Text = "";
            int imagenum = 0;

            List<string> urls = GetURL(URL.Text);

            string path = SavePath.Text;

            int i = 10;
            foreach (var url in urls)
            {
                if (i > 0)
                {
                    Task.Run(() =>
                    {
                        RealAsyncDownloadImages(url, path, ref imagenum);
                    });
                    i--;
                }
            }

            //SingleThreadDownloadImages(URL.Text, path);


        }

        /// <summary>
        /// 模拟 同步
        /// </summary>
        private void SingleThreadDownloadImages(string URL, string Path)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    //调用线程 空转等待。。。
                    var content = client.GetStringAsync(URL).Result;
                    var html = new HtmlAgilityPack.HtmlDocument();
                    html.LoadHtml(content);

                    var imgsrcList = html.DocumentNode.SelectNodes("//img").Select(m => m.Attributes["src"].Value)
                                                      .ToList();

                    this.outputText.AppendText($"{URL}准备下载:{imgsrcList.Count}个...");

                    for (int i = 0; i < imgsrcList.Count; i++)
                    {
                        HttpClient http = new HttpClient();
                         imgsrcList[i] = http.GetByteArrayAsync(imgsrcList[i]).ToString();

                        this.outputText.AppendText("{i}:开始下载\r\n");
                        if (imgsrcList[i].Contains(@"//"))
                        {
                            try
                            {
                                var stream = client.GetStreamAsync(imgsrcList[i]).Result;

                                Image.FromStream(stream).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                this.outputText.AppendText($"{i}:已完成\r\n");
                            }
                            catch(Exception ex) {
                                this.outputText.AppendText($"{i}:{imgsrcList[i]}  下载失败\r\n");
                            }
                        }
                        else
                        {
                            try
                            {
                                var stream = client.GetStreamAsync(URL + imgsrcList[i]).Result;

                                Image.FromStream(stream).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                this.outputText.AppendText( $"{i}:已完成\r\n");
                            }
                            catch (Exception ex)
                            {
                                this.outputText.AppendText($"{i}:{imgsrcList[i]}  下载失败\r\n");
                            }
                        }
                    }

                    this.outputText.AppendText($"SingleThreadDownloadImages {URL} 执行结束");
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// 模拟爬取 异步
        /// </summary>
        private async void AsyncDownloadImages(string URL)
        {

            using (var client = new HttpClient())
            {
                //调用线程 空转等待。。。
                var content = await client.GetStringAsync(URL);
                var html = new HtmlAgilityPack.HtmlDocument();
                html.LoadHtml(content);

                var imgsrcList = html.DocumentNode.SelectNodes("//img").Select(m => m.Attributes["src"].Value)
                                                  .ToList();

                this.outputText.AppendText($"准备下载:{imgsrcList.Count}个...");

                for (int i = 0; i < imgsrcList.Count; i++)
                {
                    this.outputText.AppendText(i.ToString());
                    if (imgsrcList[i].Contains(@"//"))
                    {
                        //调用线程 空转等待。。。
                        var stream = await client.GetStreamAsync(imgsrcList[i]);

                        Image.FromStream(stream).Save($@"D:\test\{i}.jpg");
                    }
                    else
                    {
                        //调用线程 空转等待。。。
                        var stream = await client.GetStreamAsync(URL + imgsrcList[i]);

                        Image.FromStream(stream).Save($@"D:\test\{i}.jpg");
                    }
                }

                this.outputText.AppendText("SingleThreadDownloadImages 执行结束");
            }
        }

        /// <summary>
        /// 模拟爬取 真`异步
        /// </summary>
        /// <param name="URL">目标地址</param>
        /// <param name="Path">保存路径</param>
        private void RealAsyncDownloadImages(string URL, string Path, ref int imagenum)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    List<Task> tasks = new List<Task>();

                    if (URL.Substring(0, 2) == @"//")
                    {
                        URL = "http:" + URL;
                    }

                    var content = client.GetStringAsync(URL).ConfigureAwait(false).GetAwaiter().GetResult();
                    var html = new HtmlAgilityPack.HtmlDocument();
                    html.LoadHtml(content);

                    var imgsrcList = html.DocumentNode.SelectNodes("//img").Select(m => m.Attributes["src"].Value).Distinct()
                                                    .ToList();


                    //var html = new HtmlAgilityPack.HtmlDocument();
                    //List<string> imgsrcList = new List<string>();
                    //var content = client.GetStringAsync(URL).ContinueWith(c =>
                    //{
                    //    try
                    //    {
                    //        string contentstring = c.ConfigureAwait(false).GetAwaiter().GetResult();
                    //        html.LoadHtml(contentstring);
                    //        imgsrcList = html.DocumentNode.SelectNodes("//img").Select(m => m.Attributes["src"].Value).Distinct()
                    //                               .ToList();
                    //        Invoke((Action)(() =>
                    //        {
                    //            this.outputText.AppendText($"{URL}准备下载:{imgsrcList.Count}个...\r\n");
                    //        }));
                    //    }
                    //    catch { }
                    //});
                    //tasks.Add(content);

                    Invoke((Action)(() =>
                    {
                        this.outputText.AppendText($"{URL}准备下载:{imgsrcList.Count}个...\r\n");
                    }));


                    for (int i = 0; i < imgsrcList.Count; i++)
                    {
                        Interlocked.Increment(ref imagenum);
                        var thisuri = imgsrcList[i];
                        var num = imagenum;
                        this.outputText.AppendText($"{num}:开始下载\r\n");
                        if (imgsrcList[i].Contains(@"//"))
                        {
                            var stream = client.GetStreamAsync(imgsrcList[i]).ContinueWith(p =>
                            {
                                try
                                {
                                    var a = p.Result;
                                    Image.FromStream(a).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                    this.outputText.AppendText( $"{num}:已完成\r\n");
                                }
                                catch (Exception ex)
                                {
                                    this.outputText.AppendText($"{num}:{thisuri}  下载失败 : {ex.Message}\r\n");
                                }
                            });
                            tasks.Add(stream);
                        }
                        else
                        {
                            var stream = client.GetStreamAsync(URL + imgsrcList[i]).ContinueWith(p =>
                            {
                                try
                                {
                                    var a = p.Result;
                                    Image.FromStream(a).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                    this.outputText.AppendText($"{num}:已完成\r\n");
                                }
                                catch (Exception ex)
                                {
                                    this.outputText.AppendText($"{num}:{URL}  :  { thisuri}  下载失败 : {ex.Message}\r\n");
                                }
                            });
                            tasks.Add(stream);
                        }

                    }
                    Task.WaitAll(tasks.ToArray());
                    Invoke((Action)(() =>
                    {
                        this.outputText.AppendText( $"{URL}: 执行结束\r\n");
                    }));
                }
                catch (Exception ex)
                {
                    //this.outputText.AppendText(ex.Message + "\r\n");
                }

            }
        }

        /// <summary>
        /// 爬取 某页面下的URL链接
        /// </summary>
        private List<string> GetURL(string URL)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = client.GetStringAsync(URL).ConfigureAwait(false).GetAwaiter().GetResult();
                    var html = new HtmlAgilityPack.HtmlDocument();
                    html.LoadHtml(content);

                    var URLList = html.DocumentNode.SelectNodes("//a").Select(m => m.Attributes["href"].Value)
                                                    ?.ToList();
                    URLList.Insert(0, URL);
                    URLList = URLList.Where(u => u.Contains("#") != true).Distinct().ToList();
                    this.outputText.AppendText($"获取URL:{URLList.Count}个...\r\n");


                    return URLList;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        private void OpenFilePath_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["FolderSelectedPath"] != "")
            {
                folderBrowserDialog.SelectedPath = ConfigurationManager.AppSettings["FolderSelectedPath"];
            }

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SavePath.Text = folderBrowserDialog.SelectedPath;
                //窗体关闭时，获取文件夹对话框的路径写入配置文件中
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["FolderSelectedPath"].Value = SavePath.Text;
                //一定要记得保存，写不带参数的config.Save()也可以
                config.Save(ConfigurationSaveMode.Modified);
                //刷新，否则程序读取的还是之前的值（可能已装入内存）
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }
        }
    }
}
