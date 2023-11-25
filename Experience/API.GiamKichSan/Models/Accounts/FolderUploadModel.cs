namespace API.GiamKichSan.Models
{
    public class FolderUploadModel_Insert
    {
        public long IDParent { get; set; }
        public string Name { get; set; }
    }
    public class FolderUploadModel_Edit : FolderUploadModel_Insert
    {
        public long ID { get; set; }
    }
}
