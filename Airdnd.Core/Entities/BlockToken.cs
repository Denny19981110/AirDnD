using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class BlockToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExprieTime { get; set; }
    }
}
