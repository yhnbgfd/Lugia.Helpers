using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Lugia.Helpers.Database
{
    static class SQLiteHelper
    {
        private static SQLiteConnection _connection = new SQLiteConnection();

        static SQLiteHelper()
        {
            Console.WriteLine(@"asd");
        }
    }
}
