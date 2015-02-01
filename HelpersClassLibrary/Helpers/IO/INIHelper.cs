using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Lugia.Helpers.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class INIHelper
    {
        private string _path = AppDomain.CurrentDomain.BaseDirectory + "Helpers.ini";

        /// <summary>
        /// Ini文件
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        //声明写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //声明读INI文件的API函数 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 
        /// </summary>
        public INIHelper() { }
        /// <summary>
        /// 类的构造函数，传递INI文件的路径和文件名
        /// </summary>
        /// <param name="iniFile"></param>
        public INIHelper(string iniFile)
        {
            _path = iniFile;
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        /// <summary>
        /// 读取INI文件 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Read(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, _path);
            return temp.ToString();
        }
    }
}
