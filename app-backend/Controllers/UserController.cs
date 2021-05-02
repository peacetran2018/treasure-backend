using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using app_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet("{username}/{password}")]
        public user Get(string username, string password)
        {
            try
            {
                string fullPath = Directory.GetCurrentDirectory();
                DirectoryInfo info = new DirectoryInfo(fullPath);
                string parentDirectory = Environment.CurrentDirectory + @"\Data\user.json";
                // Read existing json data
                string jsonData = System.IO.File.ReadAllText(parentDirectory);
                // De-serialize to object or create new list
                List<user> userList = JsonConvert.DeserializeObject<List<user>>(jsonData)
                                      ?? new List<user>();
                user existingUser = userList.Where(x => x.username.Trim() == username.Trim()).FirstOrDefault();
                if(existingUser != null)
                {
                    return existingUser;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }        

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]user value)
        {
            try
            {
                string fullPath = Directory.GetCurrentDirectory();
                DirectoryInfo info = new DirectoryInfo(fullPath);
                string parentDirectory = Environment.CurrentDirectory + @"\Data\user.json";
                // Read existing json data
                string jsonData = System.IO.File.ReadAllText(parentDirectory);
                // De-serialize to object or create new list
                List<user> userList = JsonConvert.DeserializeObject<List<user>>(jsonData)
                                      ?? new List<user>();
                user existingUser = userList.Where(x => x.username.Trim() == value.username.Trim()).FirstOrDefault();
                if (existingUser == null)
                {
                    if(userList.Count == 0)
                    {
                        value.id = 1;
                    }
                    else
                    {
                        value.id = userList.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
                    }
                    
                    userList.Add(value);
                }
                else
                {
                    return Conflict();
                }

                // Update json data string
                jsonData = JsonConvert.SerializeObject(userList);
                System.IO.File.WriteAllText(parentDirectory, jsonData);

                return Ok();
            }
            catch (Exception _ex)
            {
                return BadRequest();
            }
        }
    }
}
