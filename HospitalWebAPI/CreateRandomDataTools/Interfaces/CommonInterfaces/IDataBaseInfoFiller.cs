using System;

namespace CreateRandomDataTools.Interfaces.CommonInterfaces
{
    public interface IDataBaseInfoFiller
    {
        void FillDataBase(Func<string, bool> showStatusFunction = null);
    }
}
