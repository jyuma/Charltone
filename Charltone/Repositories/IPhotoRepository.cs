using System.Collections.Generic;
using Charltone.Domain;

namespace Charltone.Repositories
{
    public interface IPhotoRepository
    {
        IList<Photo> GetList(int id);
        IList<int> GetIdList(int id);
        byte[] GetData(int id);
        byte[] GetHomePageImageData(int id);
        IList<int> GetHomePageImageIdList();
        IList<HomePageImage> GetHomePageImageList();
        HomePageImage GetHomePageImage(int id);
        void AddHomePageImage(byte[] file);
        void UpdateHomePageImage(int id, byte[] file, int sortOrder);
        int GetDefaultId(int id);
        void UpdateDefault(int id, int newid);
        void AddPhoto(int id, byte[] file);
        void Delete(int id);
        void DeleteHomePageImage(int id);
        int Count (int id);
    }
}