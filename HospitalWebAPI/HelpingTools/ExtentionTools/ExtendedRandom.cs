using System;
using System.Collections.Generic;
using System.Linq;
using HelpingTools.Interfaces;

namespace HelpingTools.ExtentionTools
{
    public class ExtendedRandom : Random, IExtendedRandom
    {
        public bool GetRandomBool(int truePercent = 50)
        {
            var value = Next(100);
            return value < truePercent;
        }

        public T GetRandomValueFromList<T>(IEnumerable<T> list)
        {
            var copyList = list.ToList();
            var index = Next(copyList.Count());
            return copyList.ToList()[index];
        }

        protected virtual void Swap<T>(int firstIndex, int secondIndex, List<T> list)
        {
            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        public IEnumerable<T> GetShuffledList<T>(IEnumerable<T> list)
        {
            var copyList = list.ToList();
            var count = copyList.Count();

            for (var itteration = 0; itteration < count; itteration++)
            {
                var firstIndex = Next(count);
                var secondIndex = Next(count);
                Swap(firstIndex, secondIndex, copyList);
            }

            return copyList;
        }

        public IEnumerable<int> GenerateRandomList(int count, int minValue, int maxValue)
        {
            return Enumerable.Repeat(0, 0).Select(value => Next(minValue, maxValue)).ToList();
        }

        public IEnumerable<int> GenerateRandomList(int count, int maxValue)
        {
            return GenerateRandomList(count, 0, maxValue);
        }
    }
}
