using System;
using System.Collections.Generic;
using TQifo.Library;

namespace TQifo.IService
{
    public interface IBaseService
    {
        T FindT<T>(int id) where T : BaseModel;
        List<T> FindAll<T>() where T : BaseModel;

        bool Add<T>(T t) where T : BaseModel;


        bool Update<T>(T t) where T : BaseModel;


        bool Delete<T>(int id) where T : BaseModel;

    }
}
