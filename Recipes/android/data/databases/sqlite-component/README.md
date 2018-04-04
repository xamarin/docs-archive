---
id: 26172BD6-0F3D-48B8-B396-2033F654D76E
title: "Create a Database with SQLite.NET"
brief: "This recipe will demonstrate how to create an SQLite database with the SQLite.NET PCL library."
dateupdated: 2018-01-19
---

# Recipe

The SQLite component allows for the creation of the database using both synchronous and asynchronous methods. The construction of the table class itself is the same for both. You will need to add the  [SQLite.NET PCL](https://www.nuget.org/packages/SQLite.Net-PCL/) library from NuGet. 


Within your application, create a file called **Person.cs**. This will contain the table for the database. The following snippet demonstrates how to set up the table. The overload for `ToString()` at the end is used to aid in debugging but depending on what is being displayed during the debug session, may take a while. It does not have an impact outside of debugging so can be safely included. `[PrimaryKey, AutoIncrement]` requires `using SQLite;` to be inserted at the top of the source file.
 
```csharp
public class Person
{
     [PrimaryKey, AutoIncrement]
     public int ID { get; set; }

     public string FirstName { get; set; }

     public string LastName { get; set; }

     public override string ToString()
     {
        return string.Format("[Person: ID={0}, FirstName={1}, LastName={2}]", ID, FirstName, LastName);
     }
}
```

The SQLite table class cannot contain a number of commonly used types (including arrays, all generic classes and GUIDs). If you wish to include these in your table classes, place `[Ignore]` before the property.

1. The first step in the creation of the database is to create a connection. The following snippet shows how to perform this with the async method `.CreateTableAsync<T>`: 

        private async Task<string> createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                {
                     await connection.CreateTableAsync<Person>();
                     return "Database created";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

2. The SQLite component has 4 keys methods for the insertion or updating of data; `InsertAsync`, `UpdateAsync`, `InsertAllAsync` and `UpdateAllAsync`. It is easy to determine which to use.  `InsertAsync` and `InsertAllSync` return **0** if the data doesn’t exist and the data was not inserted. If the value is non-zero, then the data exists and the `UpdateAsync` or `UpdateAllAsync` was used. The following snippet shows how to perform this operation.
 
        private async Task<string> insertUpdateData(Person data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAsync(data) != 0)
                    await db.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

3. To find the number of records in the database, `Count(*)` can be used as part of the SQLite query as shown in this code. Depending on the size and complexity of the database, using `Count(*)` as a parameterless query can be slow. 

        private async Task<int> findNumberRecords(string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                // this counts all records in the database, it can be slow depending on the size of the database
                var count = await db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Person");
        
                // for a non-parameterless query
                // var count = db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");
        
                return count;
              }
              catch (SQLiteException ex)
              {
                  return -1;
              }
          }

