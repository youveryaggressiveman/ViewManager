using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.Files
{
    public interface IFileManager
    {
        public Task<string> FileReader(string pcName);
        public Task FileWriter(string text, string pcName);
    }
}
