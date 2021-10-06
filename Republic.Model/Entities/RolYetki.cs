
using Republic.Common.Enums;
using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Model.Entities
{
    public class RolYetki : BaseEntity
    {
        public KartTuru KartTuru { get; set; }
    }
}
