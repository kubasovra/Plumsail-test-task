using System.Text;
using json_api_test.DTO;
using Microsoft.AspNetCore.Mvc;

namespace json_api_test.Controllers {
   [ApiController]
   [Route("api/test/[action]")]
   public class TestController : ControllerBase {
      private readonly IStorage _storage;

      public TestController(IStorage storage) {
         _storage = storage;
      }

      [RequestSizeLimit(1_000_000_000)]
      [HttpPost]
      public async Task<IActionResult> UploadAsync([FromBody] UploadRequest request) {
         var name = string.IsNullOrWhiteSpace(request.Name) ? Guid.NewGuid().ToString() : request.Name;
         byte[] json;
         try {
            json = Encoding.UTF8.GetBytes(request.Json.ToString());
         }
         catch(EncoderFallbackException ex) {
            return BadRequest(ex.Message);
         }

         var uploadingTasks = new Task<string>[] {
            _storage.UploadAsync(Convert.FromBase64String(request.File), name),
            _storage.UploadAsync(json, $"{name}.json")
         };

         var tasksResults = await Task.WhenAll(uploadingTasks);
         var content = tasksResults[0];
         var data = tasksResults[1];

         return Ok(new {
            name,
            data,
            content
         });
      }
   }
}
