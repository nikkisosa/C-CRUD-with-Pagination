using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Pagination
{
    class config
    {

       private static string sqlCon = @"Data Source=(localdb)\V11.0;AttachDbFilename="+System.IO.Path.GetFullPath(@"..\..\DATA\CRUD.MDF")+";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
        public static SqlConnection sqlconnection = new SqlConnection(sqlCon);

        public static List<Entity.variables> records = new List<Entity.variables>();
        public static Entity.variables record = new Entity.variables();
    }
}
