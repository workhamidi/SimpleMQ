using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto.RabbitMQHttp
{
    public class StringQueryPaginationDto
    {
        
        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of elements for page (default value: 100)

        /// </summary>
        public int PageSize { get; set; } = 100;

        /// <summary>
        /// Filter by name, for example queue name, exchange name etc..
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Enables regular expression for the param name
        /// </summary>
        public bool UseRegex { get; set; } = false;
    }
}
