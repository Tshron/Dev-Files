using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringUtils
{
    public static class ComparisonStatistics
    {
        public static int HowManyCharactersInRightPlace(string i_CompareToString, string i_StringToCheck)
        {
            int numOfRightPositions = 0;
            for(int i = 0; i < Math.Min(i_StringToCheck.Length, i_CompareToString.Length); i++)
            {
                if(i_CompareToString[i] == i_StringToCheck[i])
                {
                    numOfRightPositions++;
                }
            }

            return numOfRightPositions;
        }

        public static int HowManyRightCharacters(string i_CompareToString, string i_StringToCheck)
        {
            int numOfRightCharacters = 0;
            foreach(char character in i_CompareToString)
            {
                if(i_StringToCheck.Contains(character))
                {
                    numOfRightCharacters++;   
                }
            }

            return numOfRightCharacters;
        }
    }
}
