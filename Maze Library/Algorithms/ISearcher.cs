namespace Maze_Library.Algorithms
{
    interface ISearcher<T>
    {
        PathSearchResult<T> Search(ISearchable<T> searchable);
    }
}
