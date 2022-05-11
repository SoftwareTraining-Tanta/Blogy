namespace BlogyBackend.Shared;
public static class ImageHandler
{
    public static string imagesFolder = Path.Combine(Environment.CurrentDirectory, "Images");
    public static byte[] ToBytes(this string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        return bytes;
    }
    public static string ToBase64(this byte[] bytes){
        string base64 = Convert.ToBase64String(bytes);
        return base64;
    }
}