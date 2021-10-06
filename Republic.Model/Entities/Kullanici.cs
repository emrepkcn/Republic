using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Model.Entities
{
    public class Kullanici : BaseEntity
    {


        [Required, StringLength(30),]
        public string Adı { get; set; }
        [Required, StringLength(30),]
        public string Soyadı { get; set; }

        [Required, StringLength(50),]
        public string Email { get; set; }

        [Required, StringLength(32)]
        public string Şifre { get; set; }

        [StringLength(500)]
        public string Açıklama { get; set; }

        public long? RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
