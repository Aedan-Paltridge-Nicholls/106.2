using CoreHtmlToImage;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _106._2.MainProgram
{
    class Imagegen
    {
        public void stuff(string directoryName)
        {
            List<string> files = new List<string>();

            foreach (string fileName in Directory.GetFiles(directoryName))
            {

                files.Add(fileName);
                // Do something with the file content
                // }
            }
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            conn.Open();
            int i =0;
            foreach (var item in files)
            {
                i++;
                string comm = $"UPDATE Book SET image = '{item}' WHERE bookid = {i}";
                NpgsqlCommand cmd = new NpgsqlCommand(comm, conn);
                cmd.ExecuteNonQuery();


            }
        }
    }
}
