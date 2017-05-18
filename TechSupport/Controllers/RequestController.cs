using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechSupport.BLL.Interfaces;
using TechSupport.BLL.Services;
using TechSupport.Models;
using TechSupport.BLL.DTO;
using AutoMapper;

namespace TechSupport.Controllers
{
    public class RequestController : ApiController
    {
        IUnitOfService DB;
        public RequestController(IUnitOfService db)
        {
            this.DB = db;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]RequestVM item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Mapper.Initialize(cfg => cfg.CreateMap<RequestVM, RequestDTO>());
                RequestDTO request = Mapper.Map<RequestVM, RequestDTO>(item);

                DB.RequestService.Push(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
