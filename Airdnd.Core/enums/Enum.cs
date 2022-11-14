using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Core.enums
{
    public enum StatusType
    {
        Editing = 0,
        HasUpload = 1,
        HasTakeDown = 2
    }
    public enum GenderType
    {
        [Description("女")]
        Female = 0,
        [Description("男")]
        Male = 1,
        [Description("未指定")]
        Other = 2
    }
    public enum LoginType
    {
        Email = 0,
        Facebook = 1,
        Google = 2,
        Line = 3
    }
}
