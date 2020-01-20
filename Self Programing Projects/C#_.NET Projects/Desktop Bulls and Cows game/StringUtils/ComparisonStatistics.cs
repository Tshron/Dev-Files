using System;
using System.Collections.Generic;

namespace StringUtils
{
    public static class ComparisonStatistics
    {
        public static int HowManyCharactersInRightPlace(List<string> i_CompareToString, List<string> i_StringToCheck)
        {
            int numOfRightPositions = 0;
            for (int i = 0; i < Math.Min(i_StringToCheck.Count, i_CompareToString.Count); i++)
            {
                if (i_CompareToString[i].Equals(i_StringToCheck[i]))
                {
                    numOfRightPositions++;
                }
            }

            return numOfRightPositions;
        }

        public static int HowManyRightCharacters(List<string> i_CompareToString, List<string> i_StringToCheck)
        {
            int numOfRightCharacters = 0;
            foreach (string word in i_CompareToString)
            {
                if (i_StringToCheck.Contains(word))
                {
                    numOfRightCharacters++;
                }
            }

            return numOfRightCharacters;
        }
    }
}
