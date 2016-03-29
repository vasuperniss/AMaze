namespace Maze_Library.Algorithms
{
    interface ISearcher<T>
    {
        PathSearchResult<State<T>> Search(ISearchable<T> searchable);
    }
}
