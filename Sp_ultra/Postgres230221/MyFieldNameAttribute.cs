using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres230221
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MyFieldNameAttribute : Attribute
    {
        public string ColumnName { get; set; }
    }
}
