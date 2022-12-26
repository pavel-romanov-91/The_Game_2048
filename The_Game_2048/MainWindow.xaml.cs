using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace The_Game_2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int COUNT = 4;
        private string[][] map = new string[COUNT][];
        public MainWindow()
        {
            Init();
            InitializeComponent();
            lst.ItemsSource = map;
        }
        private void Init()
        {
            for (int i = 0; i < COUNT; i++)
            {
                map[i] = new string[COUNT];
                for (int j = 0; j < COUNT; j++)
                {
                    map[i][j] = "";
                }
            }
            genRandom();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Up":
                    map = Transpose(Transpose(map).Select(Merge).ToArray());
                    break;
                case "Right":
                    map = map.Select(p => Merge(p.Reverse().ToArray()).Reverse().ToArray()).ToArray();
                    break;
                case "Down":
                    map = Transpose(Transpose(map).Select(p => Merge(p.Reverse().ToArray()).Reverse().ToArray()).ToArray());
                    break;
                case "Left":
                    map = map.Select(Merge).ToArray();
                    break;
                default:
                    break;
            }
            lst.ItemsSource = map;
            genRandom();
        }
        private string[] Merge(string[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != "")
                {
                    int k = i;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (arr[j] != "")
                        {
                            k = j;
                            break;
                        }
                    }
                    if (k != i)
                    {
                        if (arr[i] == arr[k])
                        {
                            arr[k] = (int.Parse(arr[i]) * 2).ToString();
                            arr[i] = "";
                        }
                        else
                        {
                            if (i - k > 1)
                            {
                                arr[k + 1] = arr[i];
                                arr[i] = "";
                            }
                        }
                    }
                    else
                    {
                        arr[0] = arr[i];
                        arr[i] = "";
                    }
                }
            }
            return arr;
        }
        private string[][] Transpose(string[][] doubleArr)
        {
            int row = doubleArr.Length;
            int col = doubleArr[0].Length;
            string[][] result = new string[row][];
            for (int i = 0; i < row; i++)
            {
                result[i] = new string[col];
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    result[j][i] = doubleArr[i][j];
                }
            }
            return result;
        }
        private void genRandom()
        {
            List<int[]> available = new List<int[]>();

            for (int i = 0; i < COUNT; i++)
            {
                for (int j = 0; j < COUNT; j++)
                {
                    if (map[i][j] == "")
                    {
                        available.Add(new int[] { i, j });
                    }
                }
            }
            Random random = new Random();
            int[] position = available[random.Next(available.Count())];
            map[position[0]][position[1]] = random.Next(1) == 0 ? "2" : "4";
        }
    }
}

