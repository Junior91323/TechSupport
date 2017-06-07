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
        public IHttpActionResult Get()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<RequestDTO, RequestVM>());
            var res = Mapper.Map<IEnumerable<RequestDTO>, IEnumerable<RequestVM>>(DB.RequestService.GetList());

            return Ok(res);
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<RequestDTO, RequestVM>());
            var res = Mapper.Map<RequestDTO, RequestVM>(DB.RequestService.Get(id));

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]RequestVM item)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Mapper.Initialize(cfg => cfg.CreateMap<RequestVM, RequestDTO>());
            RequestDTO request = Mapper.Map<RequestVM, RequestDTO>(item);

           // DB.RequestService.Create(request);

            return Ok();

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
