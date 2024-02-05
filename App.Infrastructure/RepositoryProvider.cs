using App.Logic.Interfaces;

namespace App.Infrastructure;

internal class RepositoryProvider : IRepositoryProvider
{
    public RepositoryProvider(ICategoryRepository categoryRepository, 
        ISingerRepository singerRepository, 
        IAlbumRepository albumRepository, 
        ISongRepository songRepository)
    {
        CategoryRepository = categoryRepository;
        SingerRepository = singerRepository;
        AlbumRepository = albumRepository;
        SongRepository = songRepository;
    }

    public ICategoryRepository CategoryRepository { get; }
    public ISingerRepository SingerRepository { get; }
    public IAlbumRepository AlbumRepository { get; }
    public ISongRepository SongRepository { get; }
}