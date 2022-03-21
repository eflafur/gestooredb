using Ecosystem.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecosystem.RepoService
{
    public interface IInsertTable<T> where T:class
    {
        object getData(string procName,string data);
        string insertData(T value,string procName=null);
        userprofile IsValidUserInformationFDirect(userprofile model);
        string insertDataSp(string data);
        IEnumerable<Dictionary<string, object>> GetDataByParam(string query);
        public int CreateTable(string create);
    }
}
