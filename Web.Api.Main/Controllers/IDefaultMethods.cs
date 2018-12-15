using System.Collections.Generic;
using System.Web.Http;

namespace Web.Api.Main.Controllers
{
    public interface IDefaultMethods<T>
    {
        [Route]
        [HttpPost]
        int Add([FromBody]T data);

        [Route]
        [HttpPut]
        void Edit(int id, T data);

        [Route("{id}")]
        [HttpGet]
        T Get(int id);

        [Route("{id}")]
        [HttpDelete]
        void Delete(int id);

        [Route("{skip}/{count}")]
        [HttpGet]
        List<T> Get(int skip, int count);

        [Route("find/{query}")]
        [HttpGet]
        List<T> Get(string query);
    }
}
