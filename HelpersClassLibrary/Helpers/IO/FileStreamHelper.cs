using System.IO;

namespace Lugia.Helpers.IO
{
    class FileStreamHelper
    {
        public void FileStream(string file, string data, FileMode fileMode = FileMode.Create)
        {
            try
            {
                FileStream fs = new FileStream(file, fileMode);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(data);
                sw.Close();
                fs.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
