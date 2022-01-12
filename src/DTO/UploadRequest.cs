using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace json_api_test.DTO {
   public class UploadRequest {
      [Required(AllowEmptyStrings = false)]
      public string Name {
         get; set;
      }

      [Required]
      public JToken Json {
         get; set;
      }

      [Required]
      public string File {
         get; set;
      }
   }
}
