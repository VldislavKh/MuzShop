using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductType
    {
        public Guid Id { get; set; }

        [Column("Тип товара")]
        public string Type { get; set; }

        [Column("Описание")]
        public string Description { get; set; }
    }
}
