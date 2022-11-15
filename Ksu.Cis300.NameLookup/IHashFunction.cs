/* IHashFunction.cs
 * Author: Ian Flores
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NameLookup
{
    internal interface IHashFunction
    {
        int ComputeHashFunction(String s);
    }
}
