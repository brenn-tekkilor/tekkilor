#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Retail.Interfaces
{
    public class IHTMLImageEqualityComparer : EqualityComparer<IHTMLImage>
    {
        public override bool Equals([AllowNull] IHTMLImage x, [AllowNull] IHTMLImage y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Src == y.Src
                    && x.Alt == y.Alt)
                return true;
            else
                return false;
        }
        public override int GetHashCode([DisallowNull] IHTMLImage obj)
        {
            unchecked
            {
                if (obj != null)
                {
                    int result = (obj.Src != null
                        ? obj.Src.GetHashCode(
                        StringComparison.CurrentCultureIgnoreCase) : 0);
                    result = (result * 397) ^ (obj.Alt != null
                        ? obj.Alt.GetHashCode(
                        StringComparison.CurrentCultureIgnoreCase) : 0);
                    return result;
                }
                else
                    return 0;
            }
        }
    }
}
