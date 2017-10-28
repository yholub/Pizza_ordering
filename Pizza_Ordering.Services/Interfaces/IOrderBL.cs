using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface IOrderBL
    {
       List<Order> GetOrdersSince(DateTime st);

       List<Order> GetPendingOrdersSince(DateTime st);

       void Accept(int id);

       void Reject(int id);

    }
}
