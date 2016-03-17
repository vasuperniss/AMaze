namespace Maze_Library.Algorithms
{
    interface ISearcher<T>
    {
        SearchResult Search(ISearchable<T> searchable);
    }
}
