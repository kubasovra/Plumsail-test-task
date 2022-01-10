namespace json_api_test {
   public interface IStorage {
      public Task<string> UploadAsync(byte[] data, string name);
   }

   public class StorageService : IStorage {
      public async Task<string> UploadAsync(byte[] data, string name) {
         var file = Environment.CurrentDirectory + name;
         await File.WriteAllBytesAsync(file, data);

         return file;
      }
   }
}
