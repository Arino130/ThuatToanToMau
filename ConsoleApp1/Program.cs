using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuatToanToMau;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            XuLyConsole xuly = new XuLyConsole(); 
            Console.Write("++++Thuật Toán Tô Màu Đồ Thị++++\n\nDữ Liệu Ban Đầu:\n");
            xuly.ReadFile();
            //Tô màu
            Console.Write("\nTô màu:");
            xuly.ToMau();
            Console.ReadKey();
        }
    }
}
