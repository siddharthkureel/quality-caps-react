using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualityCaps.Data;
using QualityCaps.Models;

namespace QualityCaps.Controllers
{
    [Produces("application/json")]
    [Route("api/CapsApi")]
    public class CapsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CapsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CapsApi
        [HttpGet]
        public IEnumerable<Cap> GetCap()
        {
            return _context.Cap;
        }
        
    }
}