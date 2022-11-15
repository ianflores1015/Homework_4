/* Dictionary.cs
 * Author: Rod Howell
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NameLookup
{
    /// <summary>
    /// A generic dictionary in which keys must implement IComparable.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    [Serializable]
    public class Dictionary<TValue>
    {
        /// <summary>
        /// The max key length
        /// </summary>
        private const int _maxKeyLength = 65535;

        /// <summary>
        /// The Value of c + 1
        /// </summary>
        private const double _cPlus1 = 49.0 / 24.0;

        /// <summary>
        /// A hash function equal to zero
        /// </summary>
        private static IHashFunction _zeroHashFunction = new ZeroHashFunction();

        /// <summary>
        /// The primary hash function
        /// </summary>
        private IHashFunction _primaryHashFunction;

        /// <summary>
        /// The primary hash function
        /// </summary>
        private IHashFunction[] _primaryHashTable;

        /// <summary>
        /// The secondary hash table
        /// </summary>
        private KeyValuePair<string, TValue>[] _secondaryHashTables;

        /// <summary>
        /// Gives the offsets of the secondary hash table 
        /// </summary>
        private int[] offsets;

        /// <summary>
        /// The number of keys in the primary hash table
        /// </summary>
        public int numberOfKeys { get; }

        /// <summary>
        /// The number of keys in the secondary hash table 
        /// </summary>
        public int numberOfSecondaryKeys { get; }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="list">The keys and values to be stored</param>
        public Dictionary(IList<KeyValuePair<string, TValue>> list)
        {
            if(list == null)
            {
                throw new ArgumentNullException();
            }
            if(list.Count > Int32.MaxValue / _cPlus1)
            {
                throw new ArgumentException();
            }
            foreach(KeyValuePair<string, TValue> pair in list)
            {
                if(pair.Key == null)
                {
                    throw new ArgumentException();
                }
                if(pair.Value == null)
                {
                    _primaryHashFunction = new ZeroHashFunction();
                    _primaryHashTable = new IHashFunction[1];
                }
            }
        }

        private void Hash(IList<KeyValuePair<string, TValue>> list)
        {
            List<KeyValuePair<string, TValue>>[] holder = new List<KeyValuePair<string, TValue>>[list.Count];
            RandomHashFunction random = new RandomHashFunction(holder.Length, _maxKeyLength);
            int randPrimaryHashFunction;
            foreach (KeyValuePair<string, TValue> listPair in list)
            {
                holder[randPrimaryHashFunction] = new List<KeyValuePair<string, TValue>>();
                foreach (KeyValuePair<string, TValue> tablePair in holder[randPrimaryHashFunction])
                {
                    if (holder[randPrimaryHashFunction].Contains(tablePair))
                    {
                        throw new ArgumentException("The key " + tablePair.Key + " occurs more than once.");
                    }
                    else
                    {
                        holder[randPrimaryHashFunction].Add(tablePair);
                    }
                }
            }

            long sumOfSquares = 0;
            do
            {
               foreach(KeyValuePair<string, TValue> listPair in list)
               randPrimaryHashFunction = random.ComputeHashFunction(list[0].Key);
            } while (sumOfSquares > holder.Length * _cPlus1);


            ///Problems ^
        }
    }        
}
