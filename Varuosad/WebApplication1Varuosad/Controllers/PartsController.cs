using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1Varuosad.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1Varuosad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        // api/parts?page=1   read 1-30
        // api/parts?page=2   read 31-60
        // api/parts?name=Polt
        // api/parts?sort=Price odavamad enne
        // api/parts?sort=-Price kallimad enne
        // GET: api/<PartsController>
        [HttpGet]
        public IEnumerable<Part> Get([FromQuery] QueryParams queryParams)
        {
            List<Part> parts = new List<Part>();
            var path = @"C:\Users\ADMIN\Desktop\LE.txt";
            // int i = 0;
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.SetDelimiters(new string[] { "\t" });
                csvParser.HasFieldsEnclosedInQuotes = true;

                while (!csvParser.EndOfData)
                {
                    // i++;
                    // if (i > queryParams.PageSize)
                    //     break;

                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    var part = new Part();
                    part.SerialNumber = fields[0];
                    part.Name = fields[1];
                    part.Price = Double.Parse(fields[8]);
                    parts.Add(part);
                }
            }
            var aShitQuery = parts.AsQueryable();

            if (queryParams.Sort.ToLower() == "price")
            {
                aShitQuery = aShitQuery.OrderBy(part => part.Price);
            }
            else if (queryParams.Sort.ToLower() == "-price")
            {
                aShitQuery = aShitQuery.OrderByDescending(part => part.Price);
            }
            else if (queryParams.Sort.ToLower() == "name")
            {
                aShitQuery = aShitQuery.OrderBy(part => part.Name);
            }
            else if (queryParams.Sort.ToLower() == "-name")
            {
                aShitQuery = aShitQuery.OrderByDescending(part => part.Name);
            }

            aShitQuery = aShitQuery.Skip((queryParams.Page - 1) * queryParams.PageSize).Take(queryParams.PageSize);
            return aShitQuery.ToList();
        }

        // GET api/<PartsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
