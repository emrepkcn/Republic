using Republic.BLL.Base;
using Republic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Republic.BLL.General
{
   public  class RolYetkileriBll : BaseContextBLL<RolYetkileri>
    {
        public RolYetkileriBll() : base(Common.Enums.KartTuru.Yetki) { }

    }
}
