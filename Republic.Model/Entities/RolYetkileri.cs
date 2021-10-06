
using Republic.Common.Enums;
using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Model.Entities
{
    public class RolYetkileri : BaseEntity
    {
        public long RolId { get; set; }

        public KartTuru KartTuru { get; set; }
        public byte Görebilir { get; set; }
        public byte Ekleyebilir { get; set; }
        public byte Değiştirebilir { get; set; }
        public byte Silebilir { get; set; }
        public Rol Rol { get; set; }
    }
}
