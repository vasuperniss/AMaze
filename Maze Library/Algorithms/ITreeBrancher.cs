namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Tree Brancher interface
    /// </summary>
    /// <typeparam name="T">the type of States to be used</typeparam>
    interface ITreeBrancher<T>
    {
        /// <summary>
        /// Branches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>a Search States Tree</returns>
        TreeSearchResult<T> Branch(ISearchable<T> searchable);
    }
}