using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcJson.Net.Models
{
    public class EnumerableWrapper<T>
    {
        public EnumerableWrapper(IEnumerable<T> items)
        {
            this.Items = items;
        }

        public IEnumerable<T> Items { get; set; }
    }
}