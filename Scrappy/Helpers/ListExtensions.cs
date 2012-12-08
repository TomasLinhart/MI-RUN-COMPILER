using System;
using System.Collections.Generic;

namespace Scrappy.Helpers
{
	public static class ListExtensions
	{
        public static T Firstest<T>(this List<T> list)
        {
            return list[0];
        }

		public static T Latest<T>(this List<T> list)
		{
			return list[list.Count - 1];
		}
	}
}

