using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProductStore.Controllers
{
    public class TalentsController : ApiController
    {
        private static readonly TalentRepository repo = new TalentRepository();

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repo.GetAll();
        }

        [Route("api/talents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent talent = repo.Get(id);
            if (talent== null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return talent;
        }

        [HttpPost]
        [Route("api/talents")]
        public Talent AddTalent(Talent talent)
        {
            if (talent == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repo.Add(talent);
            return talent;
        }

        [HttpPut]
        [Route("api/talents/{id}")]
        public Talent EditTalent(int id, Talent talent)
        {
            if (talent == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            talent.Id = id;
            repo.Update(talent);
            return talent;
        }

        [HttpDelete]
        [Route("api/talents/{id}")]
        public string DeleteTalent(int id)
        {

            repo.Remove(id);
            return "Deleted " + id;
        }

    }
}
