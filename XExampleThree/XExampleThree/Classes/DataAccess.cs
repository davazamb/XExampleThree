﻿using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XExampleThree.Classes
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Platform,
                System.IO.Path.Combine(config.DirectoryDB, "Employees.db3"));
            connection.CreateTable<Employee>();
        }

        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public T Find<T>(int id) where T : class
        {
            return connection.Table<T>().FirstOrDefault(model => model.GetHashCode() == id);
        }

        public T First<T>() where T : class
        {
            return connection.Table<T>().FirstOrDefault();
        }

        public List<T> GetList<T>() where T : class
        {
            return connection.Table<T>().ToList();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }

}
