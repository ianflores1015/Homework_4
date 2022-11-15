/*ZeroHashFunction.cs
 * Author: Ian Flores
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NameLookup
{
    [Serializable]
    internal class ZeroHashFunction : IHashFunction
    {
        public int ComputeHashFunction(string s)
        {
            return 0;
        }
    }
}
