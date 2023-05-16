using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aim_Tracker
{
    public partial class Form2 : Form
    {
        private List<Button> buttons;
        private List<Timer> timers;
        private List<int> rec;

        Timer gametimer = new Timer();
        Timer targettimer = new Timer();
        Timer rectimer;

        int gt;
        int gs;
        int recck;
        double totalrec;
        int cnt;
        int steady;

        Random r = new Random();

        public Form2()
        {
            InitializeComponent();
            timerset();
            gameset();

            buttons = new List<Button>();
            timers = new List<Timer>();
            rec = new List<int>();
        }

        private void gameset()
        {
            this.Width = 1000;
            this.Height = 600;
            gt = 2;
            gs = 0;
            totalrec = 0;
            cnt = 0;
            recck = 0;
            steady = 3;

            gametimer.Interval = 1000;
            targettimer.Interval = 2000;
            gametimer.Start();

            lblTime.Text = "시간 : " + gt;
            lblScore.Text = "점수 : " + gs;
            lblSteady.Text = steady.ToString();
            lblSteady.Visible = true;
        }

        private void timerset()
        {
            gametimer.Interval = 1000;
            gametimer.Tick += Gametimer_Tick;
            targettimer.Interval = 2000;
            targettimer.Tick += Targettimer_Tick;
        }

        private void Targettimer_Tick(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Text = "";
            btn.Width = 20;
            btn.Height = 20;
            btn.BackColor = Color.Red;
            btn.Location = new Point(r.Next(0 + btn.Width / 2, ClientSize.Width - btn.Width / 2),
                r.Next(0 + btn.Height / 2, ClientSize.Height - 46 - btn.Height / 2));
            btn.Click += Btn_Click;
            Controls.Add(btn);

            rectimer = new Timer();
            rectimer.Interval = 10;
            rectimer.Tick += Rectimer_Tick;
            rectimer.Start();
            timers.Add(rectimer);

            rec.Add(recck);

            buttons.Add(btn);
        }

        private void Rectimer_Tick(object sender, EventArgs e)
        {
            int index = timers.IndexOf(rectimer);
            rec[index]++;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            int index = buttons.IndexOf(btn);

            if (timers[index].Enabled)
            {
                timers[index].Stop();
            }

            btn.Visible = false;
            btn.Enabled = false;

            gs += 1;
            lblScore.Text = "점수 : " + gs;

            if (targettimer.Interval > 200)
                targettimer.Interval -= 100;

            foreach (var i in rec)
                Console.WriteLine(i);
        }

        private void Gametimer_Tick(object sender, EventArgs e)
        {
            if (steady > 1) 
            {
                steady -= 1;
                lblSteady.Text = steady.ToString();
            }

            else if (steady == 1)
            {
                steady -= 1;
                lblSteady.Text = "게임 시작!";
            }

            else if (steady <= 0)
            {
                lblSteady.Visible = false;
                targettimer.Start();
                gt -= 1;
                lblTime.Text = "시간 : " + gt;
                if (gt == 0)
                {
                    gametimer.Stop();
                    targettimer.Stop();
                    foreach (var i in timers)
                    {
                        int index = timers.IndexOf(i);
                        if (i.Enabled)
                        {
                            i.Stop();
                            rec[index] = 0;
                        }
                    }

                    foreach (var i in rec)
                    {
                        if (i != 0)
                        {
                            totalrec += i;
                            cnt += 1;
                        }
                    }

                    totalrec = (totalrec / cnt) * 0.01;
                    foreach (var i in buttons)
                        Controls.Remove(i);

                    DialogResult result = MessageBox.Show("점수 : " + gs +
                        "\n평균 반응속도 : " + totalrec + "초\n다시 하시겠습니까?", "게임 종료!", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        buttons.Clear();
                        timers.Clear();
                        rec.Clear();
                        gameset();
                    }
                    else
                        this.Close();
                }
            }
        }
    }
}