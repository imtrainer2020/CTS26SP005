namespace APILayer.Common
{
    public class CommonFuncs
    {
        public static MemoryStream UploadFile(IFormFile file)
        {
            using MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            return ms;
        }
    }
}
