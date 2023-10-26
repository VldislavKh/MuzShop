using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Column("Имя")]
        public string Name { get; set; }

        [Column("Пароль")]
        public string Password { get; set; }

        [Column("Id роли")]
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }

        [Column("Избранное")]
        public List<Product> WishList { get; set; } = new List<Product>();

        [Column("Корзина")]
        public List<Product> ShoppingBasket { get; set; } = new List<Product>();
    }
}
