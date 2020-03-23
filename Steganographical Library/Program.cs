using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StegoLib
{
    class Program
    {
        [STAThread]
        static void Main(string[] args) {


            var wav = new WAVConstructor("testing.wav");
            wav.Construct(LoadDataFile());
            Console.WriteLine("Completed!");
            Console.ReadKey(true);
        }

        static byte[] LoadDataFile() {

            List<byte> data = new List<byte>();
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(ofd.FileName)) {
                        while (!reader.EndOfStream) {
                            data.Add(byte.Parse(reader.ReadLine()));
                        }
                    }
                }
            }

            return data.ToArray();

        }
    }
}
