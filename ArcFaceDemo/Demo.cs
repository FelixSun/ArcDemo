using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognization
{
    public partial class Demo : Form
    {
        List<ArcFace.Staff> staffs = ArcFace.ReadExcel.ReadyData();
        public Demo()
        {
            InitializeComponent();
        }

        private void Demo_Load(object sender, EventArgs e)
        {
            //无边框设置
            this.FormBorderStyle = FormBorderStyle.None;
            //全屏显示
            this.WindowState = FormWindowState.Maximized;
            InitView();


        }

        private void InitView()
        {
            if (string.IsNullOrEmpty(Main.Id))
            {
                return;
            }
            ArcFace.Staff staff = new ArcFace.Staff();
            staff = staffs.Find(s => s.StaffID == Main.Id);
            //Panel panel = new Panel();
            var panel = this.panel1;
            panel.AutoSize = true;
            panel.Dock = DockStyle.Fill;
            panel.BackgroundImage = new Bitmap("C:\\FeatureData\\readydata\\seat.png");
            panel.BackgroundImageLayout = ImageLayout.Center;
            panel.BackColor = Color.AliceBlue;
            panel.Location = new Point(0, 0);
            //头像控件
            PictureBox picbox = new PictureBox();
            picbox.Width = 200;
            picbox.Height = 200;
            picbox.Image = new Bitmap("D:\\FeatureData\\Image\\" + staff.StaffID + ".jpeg");
            picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            picbox.Location = new Point(285, 200);
            panel.Controls.Add(picbox);

            //姓名lable标签
            Label label = new Label();
            label.Text = staff.Name;
            label.BackColor = Color.Brown;
            label.Font = new Font(FontFamily.Families.First(), (float)40, FontStyle.Bold);
            label.ForeColor = Color.White;
            label.Width = 300;
            label.Height = 50;
            label.Location = new Point(235, 460);
            label.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(label);
            //座位号lable标签
            Label seat = new Label();
            seat.Text = staff.SeatNumber;
            seat.BackColor = Color.White;
            seat.Font = new Font("", (float)90, FontStyle.Bold);
            seat.Width = 400;
            seat.Height = 150;
            seat.Location = new Point(700, 250);
            seat.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(seat);
            
            this.Controls.Add(panel);

           
        }

        public virtual void ReflashDemo()
        {
            DS(this.panel1);
            Demo_Load(this, null);
            
        }
        private void DS(Control item)
        {
            Queue<Control> query = new Queue<Control>();
            for (var i = 0; i < item.Controls.Count; i++)
            {
                if (item.Controls[i].HasChildren)
                {
                    DS(item.Controls[i]);
                }else
                {
                    query.Enqueue(item.Controls[i]);
                }
            }
            while (query.Count != 0)
            {
                query.Dequeue().Dispose();
            }
        }

    }
}
