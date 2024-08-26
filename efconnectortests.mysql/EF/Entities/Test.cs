using System;
using System.Collections.Generic;

namespace efconnectortests.mysql.EF.Entities;

public partial class Test
{
    public long Id { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }
}
