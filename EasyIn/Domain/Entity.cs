using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Domain
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public bool Removed { get; private set; }

        public Entity()
        {
            CreatedAt = DateTime.Now;
            Removed = false;
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
