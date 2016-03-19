using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class BFS<T> : ISearcher<T>
    {
        public SearchResult Search(ISearchable<T> searchable)
        {
            SearchResult result = new SearchResult();

            HashSet<State<T>> visited = new HashSet<State<T>>();
            SortedSet<State<T>> pending = new SortedSet<State<T>>();

            

            return result;
        }
    }
}
