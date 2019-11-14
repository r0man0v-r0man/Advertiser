using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.FileModels
{
    public class FileModel
    {
        public Guid Uid { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public Links LinkProps { get; set; }

        public class Links
        {
            public string Download { get; set; }
        }
        public enum Response { Success, Failure }
    }


}
