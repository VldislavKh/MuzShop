using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        [Column("Роль")]
        public string Name { get; set; }
    }
}
