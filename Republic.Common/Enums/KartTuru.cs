using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Common.Enums
{
    public enum KartTuru : byte
    {        
        [Description("Rol Kartı")]
        Rol = 1,
        [Description("Yetki Kartı")]
        Yetki = 2,     
    }
}
