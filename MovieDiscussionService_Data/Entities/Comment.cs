using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Entities
{
    public class Comment : TableEntity
    {
        public string Text { get; set; }          // sadrzaj komentara
        public string AuthorEmail { get; set; }   // Autor komentara
        public DateTime CreatedAt { get; set; }   // Kada je ostavljen komentar

        public Comment(string discussionId)
        {
            PartitionKey = discussionId; // grupisanje komentara po diskusiji
            RowKey = Guid.NewGuid().ToString();          // npr. GUID ili timestamp
        }

        public Comment() { } // default za Table Storage
    }
}
