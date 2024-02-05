using App.Logic.Interfaces;

namespace App.Infrastructure;

internal interface IRepositoryProvider
{
    ICategoryRepository CategoryRepository { get; }
    ISingerRepository SingerRepository { get; }
    IAlbumRepository AlbumRepository { get; }
    ISongRepository SongRepository { get; }
}