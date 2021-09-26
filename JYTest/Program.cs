using JYORMCommon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JYTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var source = new List<SysUser>();

            source.Add(new SysUser { Id = 1 });
            source.Add(new SysUser { Id = 2 });
            source.Add(new SysUser { Id = 3 });
            source.Add(new SysUser { Id = 4 });
            source.Add(new SysUser { Id = 5 });
            source.Add(new SysUser { Id = 6 });

            var express = source.Where(x => x.Id > 5) ;
            var sql = express.ToString();


        }
    }
}
