using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using app_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] notes value)
        {
            try
            {
                string fullPath = Directory.GetCurrentDirectory();
                DirectoryInfo info = new DirectoryInfo(fullPath);
                string parentDirectory = Environment.CurrentDirectory + @"\Data\data.json";
                // Read existing json data
                string jsonData = System.IO.File.ReadAllText(parentDirectory);
                // De-serialize to object or create new list
                List<notes> noteList = JsonConvert.DeserializeObject<List<notes>>(jsonData)
                                      ?? new List<notes>();
                notes existingNote = noteList.Where(x => x.id == value.id).FirstOrDefault();
                if(existingNote != null)
                {
                    existingNote.title = value.title;
                    existingNote.content = value.content;
                    existingNote.updatedby = value.updatedby;
                    existingNote.updateddate = DateTime.Now;
                }
                else
                {
                    noteList.Add(value);
                }                

                // Update json data string
                jsonData = JsonConvert.SerializeObject(noteList);
                System.IO.File.WriteAllText(parentDirectory, jsonData);

                return Ok();
            }
            catch (Exception _ex)
            {
                return BadRequest();
            }
        }

        // POST api/values
        [HttpDelete]
        public IActionResult Delete(List<int> value)
        {
            try
            {
                string fullPath = Directory.GetCurrentDirectory();
                DirectoryInfo info = new DirectoryInfo(fullPath);
                string parentDirectory = Environment.CurrentDirectory + @"\Data\data.json";
                // Read existing json data
                string jsonData = System.IO.File.ReadAllText(parentDirectory);
                // De-serialize to object or create new list
                List<notes> noteList = JsonConvert.DeserializeObject<List<notes>>(jsonData)
                                      ?? new List<notes>();

                foreach(int id in value)
                {
                    notes getNoteById = noteList.Where(x => x.id == id).FirstOrDefault();
                    if (getNoteById != null)
                    {
                        noteList.Remove(getNoteById);
                    }
                }

                // Update json data string
                jsonData = JsonConvert.SerializeObject(noteList);
                System.IO.File.WriteAllText(parentDirectory, jsonData);

                return Ok();
            }
            catch (Exception _ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public List<notes> Get()
        {
            try
            {
                string fullPath = Directory.GetCurrentDirectory();
                DirectoryInfo info = new DirectoryInfo(fullPath);
                string parentDirectory = Environment.CurrentDirectory + @"\Data\data.json";
                // Read existing json data
                string jsonData = System.IO.File.ReadAllText(parentDirectory);
                // De-serialize to object or create new list
                List<notes> noteList = JsonConvert.DeserializeObject<List<notes>>(jsonData)
                                      ?? new List<notes>();
                if (noteList.Any())
                {
                    return noteList;
                }
                return null;
            }
            catch (Exception _ex)
            {
                return null;
            }
        }
    }
}
