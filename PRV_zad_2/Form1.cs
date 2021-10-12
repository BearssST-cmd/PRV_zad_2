using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRV_zad_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int matrSize = 4;
        int Col;
        int Str;
        int Col1;
        int Str1;
        TextBox[] t;

        TextBox[,] MatrixNodes;
        TextBox[,] MatrixNodes1;
        Label[,] MatrixNodes2;
        int[,] mass1;
        int[,] mass2;
        int[,] mass3;
        TextBox tb;
        TextBox tb1;
        Label tb2;
        int counter = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button1.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            //int counter = 0;
            Col = Convert.ToInt32(textBox1.Text);
            Str = Convert.ToInt32(textBox2.Text);
            Col1 = Convert.ToInt32(textBox3.Text);
            Str1 = Convert.ToInt32(textBox4.Text);
            //t = new TextBox[Col * Str];
            //for (int i = 0; i < Col; i+=100)
            //{

            //    for (int j = 0; j < Str; j++)
            //    {


            //    }
            //}


            MatrixNodes = new TextBox[Col, Str];
            MatrixNodes1 = new TextBox[Col1, Str1];

            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Str; j++)
                {
                    tb = new TextBox();

                    MatrixNodes[i, j] = tb;

                    tb.Name = "TextBox" + MatrixNodes[i, j];

                    tb.Location = new Point(50 + (j * 150), 32 + (i * 50));

                    tb.Visible = true;

                    this.Controls.Add(tb);

                    counter++;
                }

                counter = 0;
            }
            for (int i = 0; i < Col1; i++)
            {
                for (int j = 0; j < Str1; j++)
                {

                    tb1 = new TextBox();

                    MatrixNodes1[i, j] = tb1;

                    tb1.Name = "TextBox1" + MatrixNodes1[i, j];

                    tb1.Location = new Point(700 + (j * 150), 32 + (i * 50));

                    tb1.Visible = true;

                    this.Controls.Add(tb1);
                    counter++;
                }

                counter = 0;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            counter = 0;
            mass1 = new int[Col, Str];
            mass2 = new int[Col1, Str1];
            mass3 = new int[Col, Str1];
            MatrixNodes2 = new Label[Col, Str];
            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Str; j++)
                {
                    //mass2[i, j] = Convert.ToInt32(tb.Controls["TextBox" + j.ToString()] as TextBox);

                    //mass1[i, j] = tb.Controls.;
                    //mass2[i, j] = Convert.ToInt32(tb.Controls.Find("TextBox1" + j.ToString(), true)[0]);
                    //mass2[i, j] = Convert.ToInt32(tb.Controls["TextBox" + i.ToString()] as TextBox);
                    mass1[i, j] = Convert.ToInt32(MatrixNodes[i, j].Text);



                }
            }
            for (int i = 0; i < Col1; i++)
            {
                for (int j = 0; j < Str1; j++)
                {
                    //mass2[i, j] = Convert.ToInt32(tb.Controls["TextBox" + j.ToString()] as TextBox);

                    //mass1[i, j] = tb.Controls.;
                    //mass2[i, j] = Convert.ToInt32(tb.Controls.Find("TextBox1" + j.ToString(), true)[0]);
                    //mass2[i, j] = Convert.ToInt32(tb.Controls["TextBox" + i.ToString()] as TextBox);

                    mass2[i, j] = Convert.ToInt32(MatrixNodes1[i, j].Text);


                }
            }

            Task<int[,]> parent = Task.Run(() =>
            {
                var res = new int[Col, Str1];

                TaskFactory factory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);

                for (int i = 0; i < Col; i++)
                {
                    for (int j = 0; j < Str1; j++)
                    {
                        res[i, j] = 0;

                        for (int k = 0; k < Str; k++)
                        {
                            int a = i, b = j, c = k;
                            factory.StartNew(() => {
                                res[a, b] += mass1[a, c] * mass2[c, b];
                            });
                        }

                    }
                }

                return res;
            });



            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Str; j++)
                {

                    tb2 = new Label();

                    MatrixNodes2[i, j] = tb2;

                    tb2.Name = "TextBox1" + MatrixNodes2[i, j];

                    tb2.Location = new Point(50 + (j * 150), 150 + (i * 50));

                    tb2.Visible = true;

                    this.Controls.Add(tb2);

                    counter++;


                }
            }

            var finalTask = await parent.ContinueWith(
                parentTask => parentTask.Result);

            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Str1; j++)
                {
                    MatrixNodes2[i, j].Text = finalTask[i, j].ToString();
                }
            }



        }
    }
}