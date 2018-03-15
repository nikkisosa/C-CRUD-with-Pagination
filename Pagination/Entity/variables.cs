using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Entity
{
    class variables
    {
        public static int sizePerPage = 10;

        public int id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string gender
        {
            get;
            set;
        }

        public int totalCount
        {
            get;
            set;
        }
    }
}
