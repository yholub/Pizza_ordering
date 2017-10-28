using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Ordering.DataProvider.UnitOfWork;

namespace Pizza_Ordering.Services.BLs
{
    public class OrderBL_MOC : IOrderBL
    {
        static List<Order> Orders { get; set; }

        static OrderBL_MOC() {
            /*Orders = new List<Order>();
            Orders.Add(new Order
            {
                State = State.Pending,
                Name = "Папероні",
                Start = new DateTime(2017, 10, 24, 17, 0, 0),
                Price = 70,
                End = new DateTime(2017, 10, 24, 17, 20, 0)
            });

            Orders.Add(new Order
            {
                State = State.Pending,
                Name = "Наполетана",
                Start = new DateTime(2017, 10, 24, 17, 10, 0),
                Price = 80,
                End = new DateTime(2017, 10, 24, 17, 30, 0)
            });

            Orders.Add(new Order
            {
                State = State.Pending,
                Name = "Кальцоне",
                Start = new DateTime(2017, 10, 24, 18, 10, 0),
                Price = 40,
                End = new DateTime(2017, 10, 24, 18, 30, 0)
            });
            Orders.Add(new Order
            {
                State = State.Pending,
                Name = "Чезаре",
                Start = new DateTime(2017, 10, 24, 17, 40, 0),
                Price = 60,
                End = new DateTime(2017, 10, 24, 18, 00, 0)
            });

            Orders.Add(new Order
            {
                State = State.Pending,
                Name = "Наполетана",
                Start = new DateTime(2017, 10, 24, 19, 10, 0),
                Price = 80,
                End = new DateTime(2017, 10, 24, 19, 30, 0)
            });

            Orders.Add(new Order
            {
                State = State.Pending,
                Name = "Папероні",
                Start = new DateTime(2017, 10, 24, 19, 20, 0),
                Price = 70,
                End = new DateTime(2017, 10, 24, 19, 40, 0)
            });

            for(int i = 0; i < Orders.Count; ++i) {
                Orders[i].Id = i;
            }*/
        }

        public List<Order> GetOrdersSince(DateTime st)
        {
            return Orders.Where(o => o.End > st && o.State != State.Rejected).ToList();
        }

        public List<Order> GetPendingOrdersSince(DateTime st)
        {
            return Orders.Where(o => o.End > st && o.State == State.Pending).ToList();
        }

        public void Accept(int id)
        {
            Orders[id].State = State.Accept;
        }

        public void Reject(int id)
        {
            Orders[id].State = State.Rejected;
        }
    }
}
