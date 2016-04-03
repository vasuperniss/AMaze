namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Searcher Interface
    /// </summary>
    /// <typeparam name="T">the type of the states to use</typeparam>
    interface ISearcher<T>
    {
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable to search.</param>
        /// <returns>the search Path resulted by the search</returns>
        PathSearchResult<T> Search(ISearchable<T> searchable);
    }
}
