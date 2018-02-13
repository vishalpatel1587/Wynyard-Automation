using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVE.Data;
using NUnit.Framework.Constraints;

namespace TestAutomation.Model.DataModel
{
    public class FileMetadataModel
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastAccessedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? FileTypeId { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }


        public bool Equals(FileMetadataList.FileMetadataRow row)
        {
            if (row.FileExtension==null)
            {
                row.FileExtension = "NULL";
            }
            return (FileName.Equals(row.FileName) &&
                    FilePath.Equals(row.FilePath) &&
                    CreatedDate.Equals(row.CreatedDate) &&
                    LastAccessedDate.Equals(row.LastAccessedDate) &&
                    LastModifiedDate.Equals(row.LastModifiedDate) &&
                    FileTypeId.Equals(row.FileTypeId) &&
                    FileExtension.Equals(row.FileExtension) &&
                    FileSize.Equals(row.FileSize));
        }

        public override string ToString()
        {
            return String.Format("[FileName: {0}\nFilePath: {1}\nCreatedDate: {2}\nLastAccessedDate: {3}\n" +
                                 "LastModifiedDate: {4}\nFileTypeId: {5}\nFileExtension: {6}\nFileSize: {7}]", 
                                 FileName, FilePath, CreatedDate, LastAccessedDate, LastModifiedDate,
                                 FileTypeId, FileExtension, FileSize);
        }
    }
}
