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

        private void PictureCrawler_Load(object sender, EventArgs e)
        {
            URL.Text = @"http://acg17.com/tag/pixiv";
            TotalLabel.Text = "";
            SavePath.Text = @"D:\新建文件夹";
            //SetBackgroundImageTransparent();
            this.TransparencyKey = Color.AntiqueWhite;
            this.BackColor = Color.AntiqueWhite;

        }

        public static int imagenum = 0;
        public static int Total = 0;
        public static int success = 0;
        public static int fail = 0;

        private void Crawl_Click(object sender, EventArgs e)
        {
            try
            {
                imagenum = 0;
                Total = 0;
                success = 0;
                fail = 0;
                Crawl.Enabled = false;

                if (SavePath.Text == "")
                {
                    MessageBox.Show("请选择保存位置");
                    return;
                }
                if (URL.Text == "")
                {
                    MessageBox.Show("请输入目标地址");
                    return;
                }

                this.outputText.Text = "";

                List<string> urls = GetURL(URL.Text);

                string path = SavePath.Text;

                //List<Task> tasks = new List<Task>();
                //foreach (var url in urls)
                //{
                //    var task = Task.Run(() =>
                //    {
                //        RealAsyncDownloadImages(url, path);
                //    });
                //    //tasks.Add(task);
                //}
                //Task.WaitAll(tasks.ToArray());

                System.Threading.ThreadPool.QueueUserWorkItem(w =>
                {
                    try
                    {
                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        sw.Start();

                        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = int.MaxValue };
                        Parallel.ForEach(urls, options, url =>
                        {
                            RealAsyncDownloadImages(url, path);
                        });

                        this.TotalLabel.Text = $@"{success + fail}/{Total}(success:{success},fail:{fail};)";
                        this.Invoke(new MethodInvoker(() => MessageBox.Show($"任务完成\n总共{Total}个目标\n成功{success}个\n失败{fail}个\n")));
                        sw.Stop();
                        System.Diagnostics.Debug.WriteLine($"爬取完毕，共耗时：{sw.Elapsed.TotalSeconds}秒");
                        Crawl.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"异常：{ex.Message}");
                    }
                }, null);

                //MessageBox.Show("完成");


                //SingleThreadDownloadImages(URL.Text, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("fail" + ex.Message);
                Crawl.Enabled = true;
            }

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
                            catch (Exception ex)
                            {
                                this.outputText.AppendText($"{i}:{imgsrcList[i]}  下载失败\r\n");
                            }
                        }
                        else
                        {
                            try
                            {
                                var stream = client.GetStreamAsync(URL + imgsrcList[i]).Result;

                                Image.FromStream(stream).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                this.outputText.AppendText($"{i}:已完成\r\n");
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
        private void RealAsyncDownloadImages(string URL, string Path)
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

                    Interlocked.Add(ref Total, imgsrcList.Count);

                    //imgsrcList = imgsrcList.Select(i => { if (i.Substring(0, 2) == @"//") { return "http:" + i; } else { return i; } }).ToList();

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
                        this.TotalLabel.Text = $@"{success + fail}/{Total}(success:{success},fail:{fail};)";
                        this.outputText.AppendText($"{URL}准备下载:{imgsrcList.Count}个...\r\n");
                    }));


                    for (int i = 0; i < imgsrcList.Count; i++)
                    {
                        Interlocked.Increment(ref imagenum);
                        var thisuri = imgsrcList[i];
                        var num = imagenum;
                        this.outputText.AppendText($"{num}:开始下载\r\n");

                        try
                        {
                            if (imgsrcList[i].Contains(@"//"))
                            {
                                if (imgsrcList[i].Substring(0, 2) == @"//")
                                {
                                    imgsrcList[i] = "http:" + imgsrcList[i];
                                }

                                var stream = client.GetStreamAsync(imgsrcList[i]).ContinueWith(p =>
                                {
                                    try
                                    {
                                        var a = p.Result;
                                        Image.FromStream(a).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                        Interlocked.Increment(ref success);
                                        this.outputText.AppendText($"{num}:已完成\r\n");
                                    }
                                    catch (Exception ex)
                                    {
                                        Interlocked.Increment(ref fail);
                                        this.outputText.AppendText($"{num}:{thisuri}  下载失败 : {ex.Message}\r\n");
                                    }
                                });
                                tasks.Add(stream);
                            }
                            else
                            {
                                var HostUrl = URL.Split('/')[2];
                                var stream = client.GetStreamAsync(@"http://" + HostUrl + imgsrcList[i]).ContinueWith(p =>
                                 {
                                     try
                                     {
                                         var a = p.Result;
                                         Image.FromStream(a).Save(Path + $@"\{Guid.NewGuid().ToString()}.jpg");
                                         Interlocked.Increment(ref success);
                                         this.outputText.AppendText($"{num}:已完成\r\n");
                                     }
                                     catch (Exception ex)
                                     {
                                         Interlocked.Increment(ref fail);
                                         this.outputText.AppendText($"{num}:{URL}  :  { thisuri}  下载失败 : {ex.Message}\r\n");
                                     }
                                 });
                                tasks.Add(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            Interlocked.Increment(ref fail);
                            this.outputText.AppendText($"{num}:{thisuri}  下载失败 : {ex.Message}\r\n");
                        }

                    }

                    Task.WaitAll(tasks.ToArray());
                    Invoke((Action)(() =>
                    {
                        this.outputText.AppendText($"{URL}: 执行结束\r\n");
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
                List<string> URLList = new List<string>();
                try
                {
                    var content = client.GetStringAsync(URL).ConfigureAwait(false).GetAwaiter().GetResult();
                    var html = new HtmlAgilityPack.HtmlDocument();
                    html.LoadHtml(content);

                    URLList = html.DocumentNode.SelectNodes("//a")?.Select(m => m.Attributes["href"].Value)?.ToList();
                    URLList.Insert(0, URL);
                    URLList = URLList.Where(u => u.Contains("#") != true).Distinct().ToList();
                    this.outputText.AppendText($"获取URL:{URLList.Count}个...\r\n");


                    return URLList;
                }
                catch (Exception ex)
                {
                    URLList.Add(URL);
                    return URLList;
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


        #region Form 背景透明化
        private Color tr_color = Color.Transparent;
        private bool b_start = false;
        bool[] b_visible = null;
        private void SetBackgroundImageTransparent()
        {
            Point pt = this.PointToScreen(new Point(0, 0));
            Bitmap b = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.CopyFromScreen(pt, new Point(), new Size(this.Width, this.Height));
            }

            this.BackgroundImage = b;
        }

        private void BeginSet()
        {
            tr_color = this.TransparencyKey;
            b_start = true;
        }
        private void Setting()
        {
            if (b_start)
            {
                b_visible = new bool[Controls.Count];
                for (int i = 0; i < Controls.Count; i++)
                {
                    b_visible[i] = Controls[i].Visible;
                    Controls[i].Visible = false;
                }
                BackgroundImage = null;
                BackColor = Color.White;
                b_start = false;
                this.TransparencyKey = Color.White;
            }
        }
        private void EndSet()
        {
            SetBackgroundImageTransparent();
            this.TransparencyKey = tr_color;
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Visible = b_visible[i];
            }
            b_start = false;
        }
        private void PictureCrawler_Move(object sender, EventArgs e)
        {
            //Setting();
        }
        private void PictureCrawler_ResizeBegin(object sender, EventArgs e)
        {
            //BeginSet();
        }

        private void PictureCrawler_ResizeEnd(object sender, EventArgs e)
        {
            //EndSet();
        }

        private void PictureCrawler_Resize(object sender, EventArgs e)
        {
            //Setting();
        }


        #endregion

        #region 无边框窗体移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private const int VM_NCLBUTTONDOWN = 0XA1;//定义鼠标左键按下
        private const int HTCAPTION = 2;
        private void PictureCrawler_MouseDown(object sender, MouseEventArgs e)
        {
            //为当前应用程序释放鼠标捕获
            ReleaseCapture();
            //发送消息 让系统误以为在标题栏上按下鼠标
            SendMessage((IntPtr)this.Handle, VM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        #endregion

        private void CloseImg_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("亲亲要退出嘛", "拜拜~", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void CloseImg_MouseDown(object sender, MouseEventArgs e)
        {
            CloseImg.BorderStyle = BorderStyle.Fixed3D;
        }

        private void CloseImg_MouseUp(object sender, MouseEventArgs e)
        {
            CloseImg.BorderStyle = BorderStyle.None;
        }

        private void CloseImg_MouseLeave(object sender, EventArgs e)
        {
            CloseImg.BorderStyle = BorderStyle.None;
        }
    }
}
