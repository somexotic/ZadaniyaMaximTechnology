using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Zadanie7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class Zadanie7Controller : ControllerBase
    {
        private readonly ILogger<Zadanie7Controller> _logger;
        public static List<Task> tasks = new List<Task>();
        public Zadanie7Controller(ILogger<Zadanie7Controller> logger)
        {
            _logger = logger;
        }
        /// <param name="str">Строка</param>
        /// <param name="sort">Типо сортировки qs-QuickSort ts-TreeSort</param>
        /// <response code = "200">Успешно выполнено</response>
        /// <response code = "400">Ошибка</response>
        /// <response code = "503">Service Unavailable</response>
        /// <returns></returns>
        [HttpGet(Name = "workWithStr")]
        [SwaggerResponse(200, "Успешно выполнено")]
        [SwaggerResponse(400, "Ошибка")]
        [SwaggerResponse(503, "Service Unavailable")]
        public async Task<IActionResult> GetAsync(string str, string sort)
        {
            FromJson js = ReadJson.Read();
            List<workStr> line = new List<workStr>();
            tasks.RemoveAll(x => x.IsCompleted);

            if (sort != "ts" && sort != "qs")
            {
                return BadRequest("В поле 'sort' можно вводить только qs или ts");
            }
            string letters = workStr.checkStr(str);
            if (letters == "")
            {
                if(tasks.Count < Convert.ToInt32(js.Settings["ParalleLimit"][0]))
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        string firstStr = workStr.firstTask(str);
                        string sorStr = workStr.fourthTask(firstStr, sort);
                        line.Add(new workStr()
                        {
                            processedStr = workStr.firstTask(str),
                            countLetters = workStr.secondTask(firstStr),
                            subString = workStr.thirdTask(firstStr),
                            sortedString = sorStr,
                            cutSortedString = workStr.fifthTask(sorStr.Length, sorStr)
                        });
                    }));
                    await Task.WhenAll(tasks);
                    return Ok(line);
                }
                else
                {
                    return StatusCode(503, "Serivce Unavailable");
                }
            }
            else if (letters == "BlackList")
            {
                return BadRequest("Строка находится в черном списке слов");
            }
            else
            {
                string error = "Использованы запрещенные символы: " + letters;
                return BadRequest(error);
            }
        }
    }
}
