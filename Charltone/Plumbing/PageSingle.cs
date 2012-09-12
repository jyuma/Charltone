using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Charltone.Plumbing
{
    public class PageSingle<T>
    {
        public PageSingle(IEnumerable<T> Item)
        {
        }

        public T Item { get; private set; }
    }
}