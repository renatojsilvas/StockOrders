﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrders.Domain
{
    public enum Status
    {
        New,
        Placed,
        Changed,
        Executed,
        ToBeRemoved,
    }
}
