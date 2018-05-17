using AForge.Video.DirectShow;
using FaceRecognization.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognization
{
    public partial class Main : Form
    {
        public static string Id { get; set; }
        /// <summary>
        /// 虹软SDK的AppId
        /// </summary>
        const string AppId = "6sb92GV64BhTcrRCC3DcSFryRNerFrxpmS6q13aApBfZ";
        /// <summary>
        /// 虹软SDK人脸检测的Key
        /// </summary>
        const string DKey = "3H89ZtqkFaEieyvQMA6ov3FmGWr8ywQuyY2oSZBdW3hw";
        /// <summary>
        /// 虹软SDK人脸比对的Key
        /// </summary>
        const string RKey = "3H89ZtqkFaEieyvQMA6ov3GFv7trn4pYQt13Bqzf5ghq";
        /// <summary>
        /// 视频源
        /// </summary>
        VideoCaptureDevice _VideoSource = null;
        /// <summary>
        /// 摄像头获取的图片和现实的图片的宽度高度比率
        /// </summary>
        float _RateW = 1, _RateH = 1;
        Font _FontId;
        Pen _PenFace;
        /// <summary>
        /// 识别一次所需时间，单位毫秒
        /// </summary>
        long _TS = 0;

        System.Threading.CancellationTokenSource _CancellationTokenSource = new System.Threading.CancellationTokenSource();
        /// <summary>
        /// 准备注册的人脸的序号
        /// </summary>
        int _RegisterIndex = -1;
        /// <summary>
        /// 准备注册的人脸特征值
        /// </summary>
        byte[] _RegisterFeatureData = null;

        List<ArcFace.Staff> staffs = ArcFace.ReadExcel.ReadyData();

        //Dictionary<int, string> _MatchId = new Dictionary<int, string>();
        //Dictionary<int, Rectangle> _MatchRect = new Dictionary<int, Rectangle>();



        Task _MatchTask;
        Demo demo = new Demo();

        public Main()
        {
            InitializeComponent();
            //InitImgInfo();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (_VideoSource != null)
            {
                _RateH = 1.0F * this.VideoPlayer.Height / this._VideoSource.VideoResolution.FrameSize.Height;
                _RateW = 1.0F * this.VideoPlayer.Width / this._VideoSource.VideoResolution.FrameSize.Width;
                _FontId = new Font(this.Font.FontFamily, (int)(1.5 * this.Font.Size / System.Math.Max(_RateH, _RateW)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _VideoSource = Video.GetVideoSource();
            //获取摄像头参数
            if (null == _VideoSource)
            {
                MessageBox.Show("没有检测到摄像头");
                this.Close();
                return;
            }

            string msg;
            if (!ArcFace.Api.Init(out msg, AppId, DKey, RKey))
            {
                MessageBox.Show(msg);
                this.Close();
                return;
            }
            this.VideoPlayer.VideoSource = _VideoSource;
            this.VideoPlayer.Start();

            this.Resize += Main_Resize;
            this.WindowState = FormWindowState.Maximized;


            _PenFace = new Pen(Color.Red, 1);
            _PenFace.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            _PenFace.DashPattern = new float[] { 5, 5 };



            _MatchTask = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                while (!_CancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        var img = this.VideoPlayer.GetCurrentVideoFrame();
                        //var img = (Bitmap)Bitmap.FromFile("d:\\photo.bmp");

                        ArcFace.Api.FaceMatch(img);
                        img.Dispose();

                        sw.Stop();
                        _TS = sw.ElapsedMilliseconds;
                    }
                    catch (System.Exception ex)
                    {
                        string s = ex.Message;
                    }
                }
            }, _CancellationTokenSource.Token);

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_VideoSource != null)
            {
                _CancellationTokenSource.Cancel();
                for (int i = 0; i < 10; i++)
                {
                    if (_MatchTask.Status == TaskStatus.RanToCompletion)
                        break;
                    Thread.Sleep(1000);
                }
                this.VideoPlayer.Stop();

                ArcFace.Api.Close();
            }
        }

        private void VideoPlayer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(_RateW, _RateH);
            for (int i = 0; i < ArcFace.Api.CacheFaceResults.FaceNumber; i++)
            {
                if (string.IsNullOrEmpty(ArcFace.Api.CacheFaceResults[i].ID))
                {
                    e.Graphics.DrawRectangle(_PenFace, ArcFace.Api.CacheFaceResults[i].Rectangle);
                    //e.Graphics.DrawString(_TS + "," + ArcFace.Api.CacheFaceResults[i].Score + "," + ArcFace.Api.CacheFaceResults[i].ID, this._FontId, Brushes.White, ArcFace.Api.CacheFaceResults[i].Rectangle.Location);
                    e.Graphics.DrawString(_TS + "," + ArcFace.Api.CacheFaceResults[i].Score, this._FontId, Brushes.White, ArcFace.Api.CacheFaceResults[i].Rectangle.Location);
                }
                else
                {
                    var staff = staffs.Find(s => s.StaffID == ArcFace.Api.CacheFaceResults[i].ID);
                    //ShowImg(ArcFace.Api.CacheFaceResults[i].ID);
                    if (staff != null)
                    {
                        e.Graphics.DrawRectangle(Pens.Green, ArcFace.Api.CacheFaceResults[i].Rectangle);
                        e.Graphics.DrawString(_TS + "," + ArcFace.Api.CacheFaceResults[i].Score + "," + staff.Name, this._FontId, Brushes.White, ArcFace.Api.CacheFaceResults[i].Rectangle.Location);
                        if (Id != staff.StaffID)
                        {
                            System.Threading.Thread.Sleep(1000);
                            Id = staff.StaffID;
                            //先关闭在打开窗体                          
                            if (demo != null)
                                demo.Close();
                            demo = new Demo();
                            demo.Show();

                        }
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(Pens.Green, ArcFace.Api.CacheFaceResults[i].Rectangle);
                        e.Graphics.DrawString(_TS + "," + ArcFace.Api.CacheFaceResults[i].Score + "," + ArcFace.Api.CacheFaceResults[i].ID, this._FontId, Brushes.White, ArcFace.Api.CacheFaceResults[i].Rectangle.Location);

                    }//e.Graphics.DrawString(ArcFace.Api.CacheFaceResults[i].ID, this._FontId, Brushes.White, ArcFace.Api.CacheFaceResults[i].Rectangle.Location);
                    ShowImg(staff);
                }
            }

        }


        private void VideoPlayer_MouseMove(object sender, MouseEventArgs e)
        {

            if (ArcFace.Api.CacheFaceResults.FaceNumber == 1)
            {
                _RegisterIndex = 0;
                this.VideoPlayer.Cursor = Cursors.Hand;
                return;
            }
            var x = e.X / _RateW;
            var y = e.Y / _RateH;
            _RegisterIndex = ArcFace.Api.CacheFaceResults.Items.IndexOf(ArcFace.Api.CacheFaceResults.Items.Take(ArcFace.Api.CacheFaceResults.FaceNumber).FirstOrDefault(ii => x >= ii.Rectangle.Left && x <= ii.Rectangle.Right && y >= ii.Rectangle.Top && y <= ii.Rectangle.Bottom));


            this.VideoPlayer.Cursor = _RegisterIndex == -1 ? Cursors.Default : Cursors.Hand;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _MatchTask = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                while (!_CancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        var img = this.VideoPlayer.GetCurrentVideoFrame();
                        //var img = (Bitmap)Bitmap.FromFile("d:\\photo.bmp");

                        ArcFace.Api.FaceMatch(img);
                        img.Dispose();

                        sw.Stop();
                        _TS = sw.ElapsedMilliseconds;
                    }
                    catch (System.Exception ex)
                    {
                        string s = ex.Message;
                    }
                }
            }, _CancellationTokenSource.Token);
        }


        private void VideoPlayer_Click(object sender, EventArgs e)
        {
            if (_RegisterIndex == -1)
            {
                MessageBox.Show("请点击人脸位置");
                return;
            }

            this.TextBoxID.Text = ArcFace.Api.CacheFaceResults[_RegisterIndex].ID;
            if (!string.IsNullOrEmpty(this.TextBoxID.Text))
            {

                var staff = staffs.Find(s => s.StaffID == this.TextBoxID.Text);
                if (staff != null)
                {
                    this.txtName.Text = staff.Name;
                    this.txtTableNum.Text = staff.SeatNumber;
                }
                else
                {
                    this.txtName.Text = "";
                    this.txtTableNum.Text = "";
                }
            }
            else
            {
                this.txtName.Text = "";
                this.txtTableNum.Text = "";
            }
            this.groupBox1.Text = ArcFace.Api.CacheFaceResults[_RegisterIndex].Score.ToString();
            this._RegisterFeatureData = ArcFace.Api.CacheFaceResults[_RegisterIndex].GetFeatureData();
            var img = this.VideoPlayer.GetCurrentVideoFrame();
            var r = ArcFace.Api.CacheFaceResults[_RegisterIndex].Rectangle; r.Inflate((int)(r.Width * 0.5), (int)(r.Height * 0.5));
            if (r.X < 0)
            {
                r.Width += r.X; r.X = 0;
            }
            if (r.Y < 0)
            {
                r.Height += r.Y; r.Y = 0;
            }
            var nImg = new Bitmap(r.Width, r.Height);
            using (var g = Graphics.FromImage(nImg))
            {
                g.DrawImage(img, new Rectangle(0, 0, r.Width, r.Height), r, GraphicsUnit.Pixel);
                //g.DrawRectangle(_PenFace, r);            
            }
            this.pictureBox1.Image = nImg;
            img.Dispose();
        }


        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (_RegisterFeatureData == null)
            {
                MessageBox.Show("没有人脸数据，请面对摄像头并点击视频");
                return;
            }
            if (string.IsNullOrEmpty(this.TextBoxID.Text))
            {
                MessageBox.Show("请输入Id");
                this.TextBoxID.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(this.txtName.Text))
            //{
            //    MessageBox.Show("请输入Name");
            //    this.txtName.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.txtTableNum.Text))
            //{
            //    MessageBox.Show("请输入Table Number");
            //    this.txtTableNum.Focus();
            //    return;
            //}

            if (ArcFace.Api.CheckID(this.TextBoxID.Text))
            {
                if (MessageBox.Show(@"您要替换[{this.TextBoxID.Text}]的人脸数据吗？", "咨询", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;
            }

            ArcFace.Api.AddFace(this.TextBoxID.Text, _RegisterFeatureData, this.pictureBox1.Image, this.txtName.Text, this.txtTableNum.Text);
        }


        private void ShowImg(ArcFace.Staff staff)
        {
            //this._RegisterFeatureData = ArcFace.Api.CacheFaceResults[_RegisterIndex].GetFeatureData();
            //var img = this.VideoPlayer.GetCurrentVideoFrame();
            //var r = ArcFace.Api.CacheFaceResults[_RegisterIndex].Rectangle; r.Inflate((int)(r.Width * 0.5), (int)(r.Height * 0.5));
            //if (r.X < 0)
            //{
            //    r.Width += r.X; r.X = 0;
            //}
            //if (r.Y < 0)
            //{
            //    r.Height += r.Y; r.Y = 0;
            //}
            //var nImg = new Bitmap(r.Width, r.Height);
            //using (var g = Graphics.FromImage(nImg))
            //{
            //    g.DrawImage(img, new Rectangle(0, 0, r.Width, r.Height), r, GraphicsUnit.Pixel);
            //    //g.DrawRectangle(_PenFace, r);            
            //}
            //this.pictureBox1.Image = nImg;
            //img.Dispose();

            //staffId
            if (staff != null)
            {
                var nImg = new Bitmap("D:\\FeatureData\\Image\\" + staff.StaffID + ".jpeg");
                this.pictureBox1.Image = nImg;
                this.TextBoxID.Text = staff.StaffID;
                this.txtName.Text = staff.Name;
                this.txtTableNum.Text = staff.SeatNumber;
            }
            else
            {
                this.txtName.Text = "";
                this.txtTableNum.Text = "";
            }

        }

        /// <summary>
        /// 切换窗体闪烁，解决办法
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息

                return;

            base.WndProc(ref m);

        }
    }
}