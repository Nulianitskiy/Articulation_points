using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace InTask_5
{
    public partial class InTask_Form : Form
    {
        public InTask_Form()
        {
            InitializeComponent();
        }

        private void MatrixPrint(int n, char[,] array)
        {
            RTB_Matrix.Text = "";

            for (int i = 1; i <= n; i++)
            {
                label1.Text += i + " ";
                label2.Text += i + "\n";
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    RTB_Matrix.Text += array[i, j] + " ";
                RTB_Matrix.Text += "\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader srN = new StreamReader("In5.txt");
            string line = srN.ReadLine();
            srN.Close();

            int n = 0;
            foreach (char l in line)
                if (l != ' ') n++;

            StreamReader sr = new StreamReader("In5.txt");

            char[,] matrix = new char[n, n];
            char d;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if ((char)sr.Peek() == ' ') { d = (char)sr.Read(); matrix[i, j] = (char)sr.Read(); }
                    else if ((char)sr.Peek() == '\r') { d = (char)sr.Read(); d = (char)sr.Read(); matrix[i, j] = (char)sr.Read(); }
                    else matrix[i, j] = (char)sr.Read();
                }
            sr.Close();

            MatrixPrint(n, matrix);

            int C_Orig = Component(matrix, n);
            label4.Text = "Компонент связности: " + C_Orig;

            for (int i = 0; i < n; i++)
                if (Artic_point(matrix, n, i, C_Orig)) label5.Text += i + 1 + " ";
        }

        bool Artic_point(char[,] array, int n, int del, int C_Orig)
        {
            char[,] newArr = new char[n - 1, n - 1];
            for (int i = 0; i < n - 1; i++)
            {
                if (i < del)
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (j < del) newArr[i, j] = array[i, j];
                        if (j >= del) newArr[i, j] = array[i, j + 1];
                    }
                if (i >= del)
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (j < del) newArr[i, j] = array[i + 1, j];
                        if (j >= del) newArr[i, j] = array[i + 1, j + 1];
                    }
            }
            if (Component(newArr, n - 1) > C_Orig) return true;
            else return false;
        }

        int Component(char[,] array, int n)
        {
            int[] used = new int[n];
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                if (used[i] == 0)
                {
                    dfs(i, used, array, n);
                    cnt++;
                }
            }
            return cnt;
        }
        void dfs(int s, int[] used, char[,] array, int n)
        {
            used[s] = 1;
            for (int i = 0; i < n; i++)
            {
                if (used[i] == 0 && array[s, i] == '1')
                    dfs(i, used, array, n);
            }
        }
    }
}
