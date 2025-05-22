using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

namespace TestString
{
    public partial class Form1 : Form
    {
        public static int length = 2;
        public static String allChars = "ABCDEF0123456789";

        public Form1()
        {
            InitializeComponent();
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            convert();
        }

        private void convert() {
            TimeSpan time = new TimeSpan();
            DateTime time1 = DateTime.Now;

            richTextBox2.Text = fastConvertTo(richTextBox1.Text).Replace("-", "").Replace("\n", "");

            time = DateTime.Now - time1;
            Debug.WriteLine(time.TotalSeconds + " seconds");

            ////////

            time = new TimeSpan();
            time1 = DateTime.Now;

            richTextBox2.Text = fastConvertFrom(richTextBox2.Text);

            time = DateTime.Now - time1;
            Debug.WriteLine(time.TotalSeconds + " seconds");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public static String fastConvertTo(String text) {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            var result = BitConverter.ToString(bytes);

            return result;
        }

        public static String fastConvertFrom(String text)
        {
            text = text.Replace(" ", "").Replace("-", "").Replace("\n", "");
            byte[] bytes = null;

            bytes = Enumerable.Range(0, text.Length / 2).Select(x => System.Convert.ToByte(text.Substring(x * 2, 2), 16)).ToArray();

            return Encoding.Unicode.GetString(bytes);
        }

        public static String convertTo(String text) {
            String newtext = "";

            byte[] textBytes = Encoding.Unicode.GetBytes(text);
            for (int i = 0; i < textBytes.Length; i++) {
                //GET INDEX
                int remain = textBytes[i];
                for (int b = 0; b < length; b++) {
                    int divideBy = Int32.Parse(Math.Pow(allChars.Length, length - 1 - b).ToString());
                    newtext += allChars[remain / divideBy];
                    remain = remain % divideBy;
                }
            }

            return newtext;
        }

        public static String convertFrom(String text)
        {
            List<int> indexList = new List<int>();

            text = text.Replace(" ", "").Replace("\n", "");

            byte[] byteArray = new byte[text.Length / length];
            for (int i = 0; i < text.Length; i += 2)
            {
                String subString = text.Substring(i, 2);
                int index = 0;
                for (int b = 0; b < subString.Length; b++)
                {
                    int divideBy = Int32.Parse(Math.Pow(allChars.Length, length - 1 - b).ToString());
                    index += allChars.IndexOf(subString[b]) * divideBy;
                }
                byteArray[i / length] = (byte)index;
            }

            return Encoding.Unicode.GetString(byteArray);
        }
    }
}
