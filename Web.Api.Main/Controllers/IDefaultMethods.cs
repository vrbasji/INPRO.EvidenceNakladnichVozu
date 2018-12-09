using System.Collections.Generic;
using System.Web.Http;

namespace Web.Api.Main.Controllers
{
    public interface IDefaultMethods<T>
    {
        [HttpPost]
        int Add([FromBody]T data);

        [HttpPut]
        void Edit([FromBody]T data);

        [HttpGet]
        T Get(int id);

        [HttpDelete]
        void Delete(int id);

        [HttpGet]
        List<T> Get(int skip, int count);

        [HttpGet]
        List<T> Get(string query);
    }
}
