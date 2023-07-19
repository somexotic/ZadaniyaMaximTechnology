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
        public Zadanie7Controller(ILogger<Zadanie7Controller> logger)
        {
            _logger = logger;
        }
        /// <param name="str">Строка</param>
        /// <param name="sort">Типо сортировки qs-QuickSort ts-TreeSort</param>
        /// <returns></returns>
        [HttpGet(Name = "workWithStr")]
        [SwaggerResponse(200, "Успешно выполнено")]
        [SwaggerResponse(400, "Ошибка")]
        public IActionResult Get(string str, string sort)
        {
            if(sort != "ts" && sort != "qs")
            {
                return BadRequest("В поле 'sort' можно вводить только qs или ts");
            }
            string letters = workStr.checkStr(str);
            if (letters == "")
            {
                string firstStr = workStr.firstTask(str);
                string sorStr = workStr.fourthTask(firstStr, sort);
                List<workStr> line = new List<workStr>();
                line.Add(new workStr() {
                    processedStr = workStr.firstTask(str),
                    countLetters = workStr.secondTask(firstStr),
                    subString = workStr.thirdTask(firstStr),
                    sortedString = sorStr,
                    cutSortedString = workStr.fifthTask(sorStr.Length, sorStr)
                });
                return Ok(line);
            }
            else
            {
                string error = "Использованы запрещенные символы: " + letters;
                return BadRequest(error);
            }
        }
            
    }
}
