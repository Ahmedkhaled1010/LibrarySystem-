namespace LibraryManagmentSystem.Application.IClients
{
    public interface ISupabaseClient
    {
        Supabase.Client Client { get; }
        Task<string> UploadFileAsync(string bucketName, string fileName, Stream fileStream);

        Task Initialize();
    }
}
