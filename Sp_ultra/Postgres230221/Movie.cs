using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres230221
{
    public class Movie
    {
        public long Id { get; set; }
        
        public string Title { get; set; }

        //[MyFieldName(ColumnName = "release_date")]
        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }
        
        public double Price { get; set; }

        //[MyFieldName(ColumnName = "country_id")]
        [Column("country_id")]
        public long CountryId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
