using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingBasket
    {
        public Guid Id { get; set; }

        [Column("Товары")]
        public List<Product> Products { get; set; }

        [Column("Id пользователя")]
        public Guid UserId { get; set; }
    }
}
