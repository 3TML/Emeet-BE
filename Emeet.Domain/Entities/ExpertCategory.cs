using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class ExpertCategory
    {
        public Guid Id { get; set; }
        public Guid ExpertId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Expert Expert { get; set; }
        public virtual Category Category { get; set; }
    }
}
