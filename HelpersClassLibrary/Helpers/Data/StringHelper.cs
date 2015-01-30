using System;
using System.Text.RegularExpressions;

namespace Lugia.Helpers.Data
{
    /// <summary>
    /// string字符串的特殊转化
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 金额转换成大写中文
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string ToChinese(this decimal money)//扩展方法
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException("参数money不能为负值！");
            string s = money.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            s = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(s, ".", delegate(Match m) { return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
        }
    }
}
