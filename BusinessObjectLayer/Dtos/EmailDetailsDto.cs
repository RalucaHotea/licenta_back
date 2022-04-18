using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class EmailDetailsDto
    {
        public string Receiver { get; set; }
        public string MessageTemplate { get; set; }
        public string Subject { get; set; }
    }
}
