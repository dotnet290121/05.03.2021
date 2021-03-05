using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres230221
{
    interface IDataAccess
    {
        bool TestDbConnection();

        List<Movie> GetAllMovies();

        int Run_sp_GetMax(int x, int y);

        List<Dictionary<string, object>> Run_sp(string sp_name, object dataHolder);
    }
}
