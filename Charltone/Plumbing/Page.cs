namespace Charltone.Plumbing
{
	using System.Collections.Generic;
	using System.Linq;

	public class Page<T>
    {
        public Page(IEnumerable<T> items, int pageNumber, int totalItemsCount, int pageSize, IList<PhotoLibrary> photos = null)
        {
            Items = items.ToArray();
            PageNumber = pageNumber;
            TotalItemsCount = totalItemsCount;
            PageSize = pageSize;
            Photos = photos;
        }

        public T[] Items { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalItemsCount { get; private set; }
        public int PageSize { get; private set; }
        public IList<PhotoLibrary> Photos { get; private set; }
    }

}