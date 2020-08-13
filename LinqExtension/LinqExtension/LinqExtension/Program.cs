using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            var Left = new List<AAA>()
            {
                new AAA(1, "01"),
                new AAA(2, "02"),
                new AAA(3, "03")
            };

            var Right = new List<BBB>()
            {
                new BBB(1, 2001),
                new BBB(2, 2002)
            };

            var list = Left.LeftJoinTo(Right)
                .OnEquals("First", "BBBFirst");

            //var a =
            //    from i in list
            //    select new BBB()

            //list.SelectAs(() =>
            //        {
            //            foreach (var el in list)
            //            {
            //                var fromLeft = el.GetType().GetProperty("left").GetValue("left");
            //                var fromRight = el.GetType().GetProperty("subRight").GetValue("subRight");

            //                yield return new BBB
            //            (
            //                (int)fromLeft.GetType().GetProperty("First").GetValue("First"),
            //                (int)fromRight.GetType().GetProperty("BBBSecond").GetValue("BBBSecond")
            //            );
            //            }
            //        });


            //(doIt) =>
            //{
            //    doIt = new BBB
            //    (
            //        (int)fromLeft.GetType().GetProperty("First").GetValue("First"),
            //        (int)fromRight.GetType().GetProperty("BBBSecond").GetValue("BBBSecond")
            //    );
            //}
            //);
            //{fromLeft => (int)fromLeft.GetType().GetProperty("First").GetValue("First")}

        }
    }

    public class AAA
    {
        public int First { get; set; }
        public string Second { get; set; }
        public AAA(int first, string second)
        {
            First = first;
            Second = second;
        }
    }

    public class BBB
    {
        public int BBBFirst { get; set; }
        public int BBBSecond { get; set; }
        public BBB(int first, int second)
        {
            BBBFirst = first;
            BBBSecond = second;
        }
    }
}
