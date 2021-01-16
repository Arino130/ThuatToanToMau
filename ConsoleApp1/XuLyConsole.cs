using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuatToanToMau
{
    public class XuLyConsole
    {
         
        public XuLyConsole() {
            Console.OutputEncoding = Encoding.UTF8;
        }
        // Read File
        // Array containing data
        private int[,] res;
        private int n; //dòng
        private int m; //cột
        private Dictionary<int, int> lstTopLevel;
        private List<KeyValuePair<int, int>> items;
        public void ReadFile()
        {
            String input = File.ReadAllText(@"data.txt");           
            // Assign values ​​of rows and columns
            var temp=input.Split('\n').ToList();
            n = temp.Count;//dòng
            temp = temp[0].Trim().Split(' ').ToList();
            m = temp.Count;//cột
            res = new int[n, m]; 
            int j = 0;
            foreach (var row in input.Split('\n'))
            {
                    temp = row.Trim().Split(' ').ToList();
                
                    for (int i=0;i<temp.Count;i++)
                    {
                        // Assign value of list TXT to variable
                        res[j, i] = Convert.ToInt32(temp[i]);
                    }
                    j++;
            }
            // ii = jj = 2 for omitting the first 2 values ​​(column and row values ​​in txt)
            Console.Write("\t");
            for ( int  ii  =  0 ; ii  <  n ; ii ++ )
            {
                for (int jj = 0; jj < m; jj++)
                {
                    Console.Write(res[ii, jj]);
                    Console.Write(jj < m - 1 ? "," : " ");
                }
                Console.Write("\n\t");
            }
            //Danh sách đỉnh bậc
            lstTopLevel = new Dictionary<int, int>();
            //Duyệt dòng
            temp = input.Split('\n').ToList();
            int count = 0;
            for (int i = 0; i <n; i++)
            {
                //lstTopLevel.Add(1, 1);
                var tempItem = temp[i].Trim().Split(' ').ToList();
                //Tính số lượng cột
                for (j = 0; j < m; j++)
                {
                    if (Convert.ToInt32(tempItem[j].ToString()) != 0)
                        count += 1;
                }
                //Thêm gtrị key=Đỉnh ,Value=Bậc vào mảng
                lstTopLevel.Add(i, count);
                count = 0;
            }
            //Sắp xếp theo bậc từ lớn -> bé
            items = (from pair in lstTopLevel
                        orderby pair.Value descending
                        select pair).ToList();
            //Show đỉnh,bậc sau khi sort
            Console.WriteLine("\nĐỉnh/Bậc sau khi sắp xếp:");
            foreach(KeyValuePair<int,int>item in items)
            {
                Console.WriteLine("\t"+(item.Key+1)+" "+item.Value);
            }     
        }
        private int Mau = 0;
        public bool CheckContainItems(int value) { 
            foreach(KeyValuePair<int,int>a in items)
            {
                if (a.Key == value)
                    return true;
            }
            return false;
        }
        private List<int> lstBlack;
        //Thêm những cạnh kề vào danh sách để so sánh
        public void addItemsKe(int k)
        {
            for (int i = 0; i < n; i++)
            {
                if (res[k, i] == 1 && !lstBlack.Contains(i))
                {
                    lstBlack.Add(i);
                }
            }
        }
        public void ToMau()
        {
            //Đệ quy đến khi ko còn phần tử trong mảng Đỉnh
            if (items.Count != 0)
            {
                Console.Write("\n\tMàu " + (Mau + 1) + ": " + (items[0].Key+1) + ","); 
                lstBlack = new List<int>();
                addItemsKe(items[0].Key);
                for (int i = 0; i < m; i++)
                {
                    if (res[items[0].Key, i] == 0 && items[0].Key != i && CheckContainItems(i))
                    {
                        if (!lstBlack.Contains(i) || lstBlack.Count == 0)
                        {
                            addItemsKe(i);
                            //Show vị trí có gtrị = 0
                            Console.Write((i + 1).ToString() + ",");
                            //Lấy ra đỉnh(item) tại vị trí có gtrị = 0 
                            KeyValuePair<int, int> item = items.Find((lItem) => lItem.Key.Equals(i));
                            //Xóa item tại vị trí đó
                            items.Remove(item);
                        }
                        else
                        {
                            lstBlack.Remove(i);
                        }
                    }
                }
                //Xóa vị trí item đã duyệt đầu tiên
                items.Remove(items[0]);
                Mau++;
                //Đệ quy
                ToMau();
            }
            else
                return;
        }
    }
}
