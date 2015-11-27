using System;

namespace RedCloudWork.Domian
{
    public class UploadInfo: BaseEntity
    {
        public DateTime UploadTime { get; set; }
        public string FileName { get; set; }
        public float FileSize { get; set; }
    }
}