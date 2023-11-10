using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace _106._2
{
    public static class GloVar
    {
        public static NpgsqlConnection SqlCONN  = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
    }
}
