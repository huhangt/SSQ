using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] redNums = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10"
        ,"11", "12", "13", "14", "15", "16", "17", "18", "19", "20"
        ,"21", "22", "23", "24", "25", "26", "27", "28", "29", "30"
        ,"31", "32", "33"};

        string[] blueNums = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10"
        ,"11", "12", "13", "14", "15", "16"};

        private static readonly Object OSS_LOCK = new Object();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaskFactory taskFactory = new TaskFactory();

            isStop = false;

            foreach (var item in groupBox1.Controls)
            {
                if (item is Label)
                {
                    if ("lblBlue".Equals(((Label)item).Name))
                    {
                        taskFactory.StartNew(() =>
                        {
                            while (true)
                            {
                                int index = GetIndexNumber(0, blueNums.Length);

                                string text = blueNums[index];

                                if (!isStop)
                                {
                                    UpdateLabelText((Label)item, text);
                                }
                            }
                        });
                    }
                    else
                    {
                        taskFactory.StartNew(() =>
                        {
                            while (true)
                            {
                                int index = GetIndexNumber(0, redNums.Length);

                                string text = redNums[index];
                                if (!isStop)
                                {
                                    lock (OSS_LOCK)
                                    {
                                        if (isExsited(text))
                                        {
                                            continue;
                                        }

                                        UpdateLabelText((Label)item, text);
                                    }
                                }
                            }
                        });
                    }
                }
            }
        }

        private bool isStop = false;

        private void button2_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

        public bool isExsited(string text)
        {
            foreach (var item in groupBox1.Controls)
            {
                if (item is Label && ((Label)item).Text.Equals(text))
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateLabelText(Label label,string text)
        {
            if (label.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    if (!isStop)
                    {
                        label.Text = text;
                    }
                }));
            }
            else
            {
                label.Text = text;
            }
        }
        public int GetIndexNumber(int min,int max)
        {
            Thread.Sleep(1000);

            Guid guid = Guid.NewGuid();
            int seek = DateTime.Now.Millisecond;
            foreach (var item in guid.ToString())
            {
                switch (item)
                {
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                        seek += 1;
                        break;
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                        seek += 2;
                        break;
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                        seek += 3;
                        break;
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        seek += 4;
                        break;
                    default:
                        seek += 5;
                        break;
                }
            }

            Random random = new Random(seek);
            return random.Next(min, max);
        }

    }
}
