using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganographical_Library.Abstracts
{
    public static class Helpers
    {
        public static byte[] LoadDataFile(params string[] filter)
        {

            List<byte> data = new List<byte>();
            const int CHUNK_SIZE = 16;

            using (OpenFileDialog ofd = new OpenFileDialog()) {

                string currentFilter = "";
                for (var i = 0; i < filter.Length; i++) {
                    if (filter[i][0] == '.') filter[i] = filter[i].Substring(1);        //Remove unnecessary '.' prefix
                    currentFilter += $"{filter[i]} Files|*.{filter[i]}|";
                }

                currentFilter += "All Files|*.*";
                ofd.Filter = currentFilter;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (BinaryReader reader = new BinaryReader(ofd.OpenFile())) {
                        byte[] chunk = new byte[CHUNK_SIZE];
                        chunk = reader.ReadBytes(CHUNK_SIZE);
                        while (chunk.Length > 0) {
                            //Dump bytes
                            for (int i = 0; i < chunk.Length; i++) data.Add(chunk[i]);

                            chunk = reader.ReadBytes(CHUNK_SIZE);
                        }
                    }

                }
            }
            return data.ToArray();
        }

        public static string[] ReadAllWords(this StreamReader reader) {
            throw new NotImplementedException("Fix!"); //TODO:FIX
/*
            List<string> words = new List<string>();
            string currentWord = "";
            while (!reader.EndOfStream) {
                char c = reader.Read();
                if (char.IsSeparator(c)) {                                              //Double If-check to prevent from empty words
                    if(currentWord != string.Empty) words.Add(currentWord);
                }
                else { currentWord += c; }
            }
            return words.ToArray();
*/
        }

        public static IEnumerable<byte> ExtractWAVDataBytes(byte[] source) {

            uint startPoint = 0;

            for (uint i = 0; i < source.Length - 8; i++) {
                if (source[i] == 100 && source[i + 1] == 97 && source[i + 2] == 116 && source[i + 3] == 97) {
                    startPoint = i + 8;
                    break;
                }
            }

            for (; startPoint < source.Length; startPoint++) { yield return source[startPoint]; }

        }
    }
}
