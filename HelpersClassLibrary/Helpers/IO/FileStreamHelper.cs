using System.IO;

namespace Lugia.Helpers.IO
{
    class FileStreamHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <param name="fileMode"></param>
        public void WriteLine(string file, string data, FileMode fileMode = FileMode.Create)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string ReadToEnd(string file)
        {
            StreamReader reader = new StreamReader(file);
            string data = reader.ReadToEnd();
            reader.Close();
            return data;
        }
    }
}
