using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public enum TableStatus
    {
        OCCUPIED,
        CLEAN,
        DIRTY
    }
    public enum OrderDetailStatus
    {
        WAITING,
        COMPLETED,
        COOKING
    }
    public enum OrderStatus
    {
        PENDING,
        COMPLETED,
        REQUESTPAYMENT
    }
}
