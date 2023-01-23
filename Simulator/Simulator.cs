using BlApi;
using System.Data;

namespace Simulator;

public static class Simulator
{
    //static readonly BlApi.IBl bl = BlApi.Factory.Get();
    ////Func<> report;
    //private volatile bool Active;
    //static readonly Random random = new Random();



    //public static void Activate()
    //{
    //    new Thread(() =>
    //    {
    //        Active = true;
    //        while (Active)
    //        {
    //            int? idOrder = bl.Order.GetNextOrder();
    //            if (idOrder != null)
    //            {
    //                BO.Order order = bl.Order.GetOrderDetails(idOrder);
    //                int delay = random.Next(3, 11);
    //                DataTime time = DataTime.Now + new TimeSpan(delay * 1000);
    //                report(order, time);
    //                Thread.Sleep(delay * 1000);
    //                report("finish");
    //                bl.Order.UpdateStatuse(order);
    //            }
    //        }
    //        report("finished");
    //    }).Start();

    //}

    //public static void Register(Func<> fg)
    //{
    //    report += fg;
    //}

    //public static void UnRegister()
    //{

    //}

}