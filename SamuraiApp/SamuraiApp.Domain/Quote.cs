using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Domain
{
    public class Quote
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; }
        public Guid SamuraiId { get; set; }

    }
}
