using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lexikar_2.Models.Templates
{
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
