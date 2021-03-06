﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace HelpingTools.Interfaces
{
    public interface IExtendedRandom
    {
        int Next();

        int Next(int minValue, int maxValue);

        int Next(int maxValue);

        bool GetRandomBool(int truePercent = 50);

        T GetRandomValueFromList<T>(IEnumerable<T> list);

        IEnumerable<T> GetShuffledList<T>(IEnumerable<T> list);

        IEnumerable<int> GenerateRandomList(int count, int minValue, int maxValue);

        IEnumerable<int> GenerateRandomList(int count, int maxValue);

        T GetRandomEnumValue<T>();
    }
}
