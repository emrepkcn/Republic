using Republic.Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.BLL.Functions
{
    public static class Convert
    {
        public static TTarget EntityConvert<TTarget>(this IBaseEntity source)
        {
            if (source == null) return default(TTarget);

            var hedef = (TTarget)Activator.CreateInstance(typeof(TTarget));

            var hedeprop = typeof(TTarget).GetProperties().Where(x => x.CanWrite);
            var kaynakprop = source.GetType().GetProperties().Where(x => x.PropertyType.Namespace != "System.Drawing");

            foreach (var kp in kaynakprop)
            {
                var val = kp.GetValue(source);
                var hp = hedeprop.FirstOrDefault(x => x.Name == kp.Name);

                //if (hp.PropertyType != kp.PropertyType) continue;
                if (hp != null && hp.PropertyType == kp.PropertyType)
                    hp.SetValue(hedef, ReferenceEquals(val, "") ? null : val);



            }


            return hedef;

        }
        public static IEnumerable<TTarget> EntityListConvert<TTarget>(this IEnumerable<IBaseEntity> source)
        {
            return source?.Select(x => x.EntityConvert<TTarget>()).ToList();
        }
    }
}
