using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryReadWriter
{
    public partial class Form1 : Form
    {
        //定数
        public const string DEFALTDIRECTRY = @"C:\test\";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileName =
                (string[])e.Data.GetData(DataFormats.FileDrop, false);

            ReadFileData(fileName[0]);

        }

        private void ReadFileData( string aFileName )
        {
            DateTime now = DateTime.Now;
            //区切り文字を宣言
            char[] delimitChars = { '\t' };

            byte[] byteArray;
            string line = "";
            ArrayList al = new ArrayList();


            using (StreamReader sr = new StreamReader(
                aFileName, Encoding.GetEncoding("Shift_JIS")))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    //al.Add(line);
                    //タブ区切りで分割
                    string[] words = line.Split(delimitChars);

                    if (words[1] != null)
                    {
                        int intData = int.Parse(words[1]);
                        byteArray = BitConverter.GetBytes(intData);
                        using (FileStream stream = new FileStream(DEFALTDIRECTRY + now.ToString("yyyyMMddHHmmss")
                            + ".bin", FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                        {
                            using (BinaryWriter writerSync = new BinaryWriter(stream))
                            {
                                writerSync.Write(byteArray[0]);
                            }
                        }
                    }


                }
            }
        }
    }
}
