using System;
using SQLite;

namespace CreateDatabaseWithSqliteNet
{
    public class Person
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}

