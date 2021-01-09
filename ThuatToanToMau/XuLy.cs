using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ThuatToanToMau
{
    public class XuLy
    {
         
        public void Xuly() {
        }
        private int[,] res;
        private int n; //dòng
        private Dictionary<int, int> lstTopLevel;
        private List<KeyValuePair<int, int>> items;
        public void ReadData(string FileName,RichTextBox rTxtSort,RichTextBox rTxtBegin)
        {
            String input="";
            if (rTxtBegin.Text.Trim() != string.Empty)
            {
                List<string> myList = rTxtBegin.Lines.ToList();
                myList.RemoveAt(myList.Count - 1);
                rTxtBegin.Lines = myList.ToArray();
                rTxtBegin.Refresh();
                input = rTxtBegin.Text;
            }
            else if (FileName != String.Empty)
            {
                input = File.ReadAllText(FileName);
            }
            else
                return;
            var temp=input.Split('\n').ToList();
            n = temp.Count;//dòng
            temp = temp[0].Trim().Split(' ').ToList();
            res = new int[n, n];
            int j = 0;
            foreach (var row in input.Split('\n'))
            {
                temp = row.Trim().Split(' ').ToList();

                for (int i = 0; i < temp.Count; i++)
                {
                    res[j, i] = Convert.ToInt32(temp[i]);
                }
                j++;
            }
            ShowDataBegin(rTxtBegin,res,n);
            //Danh sách đỉnh bậc
            lstTopLevel = new Dictionary<int, int>();
            //Duyệt dòng
            temp = input.Split('\n').ToList();
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                //lstTopLevel.Add(1, 1);
                var tempItem = temp[i].Trim().Split(' ').ToList();
                //Tính số lượng cột
                for (j = 0; j < n; j++)
                {
                    if (Convert.ToInt32(tempItem[j].ToString()) != 0)
                        count += 1;
                }
                //Thêm gtrị key=Đỉnh ,Value=Bậc vào mảng
                lstTopLevel.Add(i, count);
                count = 0;
            }
            items = new List<KeyValuePair<int, int>>();
            //Sắp xếp theo bậc từ lớn -> bé
            items = (from pair in lstTopLevel
                     orderby pair.Value descending
                     select pair).ToList();
            ShowDataSort(rTxtSort);
        }
        public void ShowDataSort(RichTextBox rTxtSort)
        {
            //Show đỉnh,bậc sau khi sort
            rTxtSort.Text = string.Empty;
            if (items!=null)
            {
                foreach (KeyValuePair<int, int> item in items)
                {
                    rTxtSort.Text = rTxtSort.Text + ("Đỉnh: " + (item.Key + 1) + " , Bậc:" + item.Value) + "\n";
                }
            }
        }
        //index đỉnh cần lấy
        public KeyValuePair<int, int> getItems(int index)
        {
            index--;
            if (CheckContainItems(index))
            {
                return items.Find((lItem) => lItem.Key.Equals(index));
            }                
            return new KeyValuePair<int, int>(-1,-1);
        }
        public void ShowDataBegin(RichTextBox rTxtBegin,int[,] Data,int size)
        {
            rTxtBegin.Text = String.Empty;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    rTxtBegin.Text = rTxtBegin.Text + " " + Data[i, j];
                }
                rTxtBegin.Text = rTxtBegin.Text + "\n";
            }
        }
        public bool CheckContainItems(int key) { 
            foreach(KeyValuePair<int,int>a in items)
            {
                if (a.Key == key)
                    return true;
            }
            return false;
        }
        public int Mau = 0;
        public void ToMau(RichTextBox rTxtResult)
        {
            //Đệ quy đến khi ko còn phần tử trong mảng Đỉnh
            if (items.Count != 0)
            {
                rTxtResult.Text= rTxtResult.Text+("\nMàu " + (Mau + 1) + ": " + (items[0].Key+1) + ",");
                for (int i = 0; i < n; i++)
                {
                    if (res[items[0].Key, i] == 0 && items[0].Key != i && CheckContainItems(i))
                    {
                        //Show vị trí có gtrị = 0
                        rTxtResult.Text = rTxtResult.Text + ((i+1).ToString() + ",");
                        //Lấy ra đỉnh(item) tại vị trí có gtrị = 0 
                        KeyValuePair<int, int> item = items.Find((lItem) => lItem.Key.Equals(i));
                        //Xóa item tại vị trí đó
                        items.Remove(item);
                    }
                }
                rTxtResult.Text = rTxtResult.Text + "\n";
                //Xóa vị trí item đã duyệt đầu tiên'
                items.Remove(items[0]);
                Mau++;
                //Đệ quy
                ToMau(rTxtResult);
            }
            else
                return;
        }
    }
}
