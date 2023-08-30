using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ProductType? Type { get; set; }

        [Column("Id типа товара")]
        public Guid? TypeId { get; set; }

        [Column("Наименование")]
        public string Name { get; set; }

        [Column("Производитель")]
        public string Vendor { get; set; }

        [Column("Модель")]
        public string Model { get; set; }

        [Column("Описание")]
        public string Description { get; set; }

        [Column("Цена, руб.")]
        public decimal Price { get; set; }

        [Column("Наличие, шт.")]
        public int Amount { get; set; }
    }
}
