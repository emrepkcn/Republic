using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Model.Entities
{
    public class Rol : BaseEntityDurum
    {

        [Required, StringLength(50)]
        public string RolAdi { get; set; }
        [StringLength(50)]
        public string Açıklama { get; set; }
        [InverseProperty("Rol")]//kademeli silmek için gerekli olan attribute
        public ICollection<RolYetkileri> RolYetkileri { get; set; }
    }
}
