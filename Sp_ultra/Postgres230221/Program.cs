using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres230221
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                PostgresAppConfig.Instance.Init(args[0]);
            }
            else
            {
                PostgresAppConfig.Instance.Init();
            }

            my_logger.Info("**************************** System started");

            string conn_string = PostgresAppConfig.Instance.ConnectionString; //"Host=localhost;Username=postgres;Password=admin;Database=postgres;";
            PostgresDAO dao = new PostgresDAO(conn_string);

            if (dao.TestDbConnection())
            {
                Console.WriteLine("Successfully connected to the DB...");

                var movies = dao.GetAllMovies();
                movies.ForEach(m => Console.WriteLine(m));

                int result = dao.Run_sp_GetMax(3, 7);

                var result_sp = dao.Run_sp("a_sp_max", new { x = 3, y = 7 });

                var sp_get_all_movies = dao.Run_sp("sp_get_all_movies", new { });

                List<Movie> the_movies = dao.Run_sp<Movie>("sp_get_all_movies", new { });

                Console.WriteLine("from sp generic!");
                movies.ForEach(m => Console.WriteLine(m));

                var a_sum_n_product = dao.Run_sp("a_sum_n_product", new { x = 4, y = 5 });

                // sp_get_all_movies
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Failed to connected to the DB...");
            }

            my_logger.Info("**************************** System shutdown");

            Console.ReadLine();
        }
    }
}
