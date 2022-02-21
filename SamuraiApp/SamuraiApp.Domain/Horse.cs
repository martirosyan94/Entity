using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Domain
{
    public class Horse
    {
        public Guid Id { get; set; }
        public Guid SamuraiId { get; set; }
        public string Name { get; set; }
    }
}
