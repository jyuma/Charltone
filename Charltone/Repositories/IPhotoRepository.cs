using System.Collections.Generic;
using Charltone.Domain;

namespace Charltone.Repositories
{
    public interface IPhotoRepository
    {
        IList<Photo> GetList(int id);
        //Photo GeSingle(int id);
        byte[] GetData(int id);
        int GetDefaultId(int id);
        void UpdateDefault(int id, int newid);
        void AddPhoto(int id, byte[] file);
        void Delete(int id);
        int Count (int id);
    }
}