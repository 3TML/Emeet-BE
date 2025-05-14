using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class StaticFile
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Guid ExpertId { get; set; }

        public virtual Expert Expert { get; set; }
    }
}
