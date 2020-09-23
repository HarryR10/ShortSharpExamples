using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqExtension
{
    public static class LinqExtension
    {
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> LeftJoinTo<T1, T2>(
            this IEnumerable<T1> left, IEnumerable<T2> right)
        {
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(left, right);
        }

        public static IEnumerable OnEquals<T1, T2>(
            this Tuple<IEnumerable<T1>, IEnumerable<T2>> tuple,
                                string leftColumn,
                                string rightColumn)
        {
            var ResultList =
                from left in tuple.Item1
                join right in tuple.Item2
                    on left.GetType().GetProperty(leftColumn).GetValue(left)
                    equals right.GetType().GetProperty(rightColumn).GetValue(right)
                    into outer
                from subRight in outer.DefaultIfEmpty()
                select new
                {
                    left,
                    subRight
                };

            return ResultList;
        }
    }
}
