using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NameLookup
{
    internal class RandomHashFunction : IHashFunction
    {
        private static Random _random = new Random();
        
        private int _tableSize;
        
        private int _addedValue; //a0
        
        private int _lengthMultiplier; //a1

        private int[] _multipliers; //b0-bX

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="tableSize">The number of elements in the hash table</param>
        /// <param name="maxLengthKey">The longest key in the hash table</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public RandomHashFunction(int tableSize, int maxLengthKey) //where to use maxLengthKey?
        {
            if(tableSize < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                _tableSize = tableSize; // m
                int p = Int32.MaxValue - 1;
                int l = maxLengthKey;
                _addedValue = _random.Next(p);
                _lengthMultiplier = _random.Next(p);
                foreach(int i in _multipliers)
                {
                    _multipliers[i] = _random.Next(p);
                }

            }
        }

        /// <summary>
        /// Computes a hash function for a given key
        /// </summary>
        /// <param name="s">the key</param>
        /// <returns></returns>
        public int ComputeHashFunction(string s)
        {
            int n = s.Length;
            int m = _tableSize;
            int p = Int32.MaxValue - 1;
            long sum = 0;

            foreach (int i in _multipliers)
            {
                sum += _multipliers[i] * (long)s[i];
            }

            if(s.Length > _multipliers.Length)
            {
                return 0;
            }
            else
            {
                return (int)((_addedValue + _lengthMultiplier * n + sum) % p % m);
            }
        }
    }
}
