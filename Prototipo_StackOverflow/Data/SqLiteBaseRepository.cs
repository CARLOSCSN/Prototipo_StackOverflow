using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_StackOverflow.Data
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Directory.GetParent(Environment.CurrentDirectory).Parent.FullName
                    .Replace(@"\Prototipo_StackOverflow\Prototipo_StackOverflowTests\bin", "") 
                    + "\\Prototipo_StackOverflow\\Prototipo_StackOverflow\\SimpleDb.sqlite"; }
        }

        public static SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
