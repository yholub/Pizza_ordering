using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PizzaType
    {
        [EnumMember(Value = "fix")]
        Fix,

        [EnumMember(Value = "modified")]
        Modified,

        [EnumMember(Value = "saved")]
        Saved
    }
}
