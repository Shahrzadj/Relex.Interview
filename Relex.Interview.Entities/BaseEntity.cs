using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Entities
{
    public interface IEntity 
    {
        int Id { get; set; }
    }

    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
