using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.BasicDTOs
{
    public class BaseOrderByDto
    {
        public OrderByDirection Direction { get; set; }
    }

    public enum OrderByDirection
    {
        Ascending = 1,
        Descending = 2
    }
}
