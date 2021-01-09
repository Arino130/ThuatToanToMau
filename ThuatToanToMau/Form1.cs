using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThuatToanToMau
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        private int[,] Data;
        private int n=0;
        private Random rand;
        private string fileName="";
        private OpenFileDialog openFileDialog;
        private XuLy xuly;
        public void RandomArray()
        {
            rand = new Random();
            n = Convert.ToInt32(txtSize.Text);
            Data = new int[n, n];
            rTxtBegin.Text = String.Empty;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        Data[i, j] = rand.Next(0, 2);
                        Data[j, i] = Data[i, j];
                    }
                    else
                    {
                        Data[i, j] = 0;
                        Data[j, i] = Data[i, j];
                    }
                }
            }   
        }
        //Random Size button
        private void button1_Click(object sender, EventArgs e)
        {
            Init();
            txtSize.Text = String.Join("", txtSize.Text.Trim().Split(' ').ToArray());
            if (txtSize.Text != "")
            {
                if (Convert.ToInt32(txtSize.Text.Trim()) < 2)
                    MessageBox.Show("Size không được nhỏ hơn 2 !");
                else
                {
                    RandomArray();
                    xuly.ShowDataBegin(rTxtBegin,Data,n); 
                    xuly.ReadData("", rTxtSort, rTxtBegin);
                }
                return;
            }
            MessageBox.Show("Bạn chưa nhập Size !");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Init();
        }
        //Lấy đường dẫn
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Init();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";

            if ((DialogResult)openFileDialog.ShowDialog()==DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                xuly.ReadData(fileName, rTxtSort, rTxtBegin);
            }
        }

        public void Init()
        {
            xuly = new XuLy();
            rTxtBegin.SelectionAlignment = HorizontalAlignment.Center;
            txtSearch.Text = String.Empty;
            rTxtResult.Text = String.Empty;
            rTxtBegin.Text = String.Empty;
            rTxtSort.Text = String.Empty; 
            fileName = string.Empty;
        }
        //Button Start
        private void button2_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;
            rTxtResult.Text = string.Empty;
            if (rTxtBegin.Text != String.Empty)
            {
                xuly.Mau = 0;
                xuly.ToMau(rTxtResult);
                //Chạy lại để tạo mảng (Đỉnh,Bậc) vì sau khi chạy xog array sẽ = null
                xuly.ReadData(fileName, rTxtSort, rTxtBegin);
            }
            else
            {
                MessageBox.Show("Dữ liệu rỗng !");
            }  
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtSearch.Text = String.Join("", txtSearch.Text.Trim().Split(' ').ToArray());
            if (txtSearch.Text != "" && rTxtBegin.Text!=String.Empty)
            {
                KeyValuePair<int,int> temp=xuly.getItems(Convert.ToInt32(txtSearch.Text));
                if(temp.Key!=-1 && temp.Value != -1)
                {
                    rTxtSort.Text = "Đỉnh: " + (temp.Key + 1) + " ,Bậc: " + temp.Value;
                }
                else
                {
                    rTxtSort.Text = "Không tìm thấy !";                 
                }
                return;
            }
            xuly.ShowDataSort(rTxtSort);
        }
    }
}
