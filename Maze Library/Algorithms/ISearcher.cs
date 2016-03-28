namespace Maze_Library.Algorithms
{
    interface ISearcher<T>
    {
        SearchPathResult<T> Search(ISearchable<T> searchable);
    }
}
