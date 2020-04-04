using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegoLib.Abstract_Classes
{
    abstract class WAVFile {
        protected List<byte> _data;
        protected string _targetPath;

        protected WAVFile(string target) {
            _targetPath = target;
            _data = new List<byte>();
        }

        protected virtual byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
