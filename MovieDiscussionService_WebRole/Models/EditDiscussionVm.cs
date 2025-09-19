using MovieDiscussionService_Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieDiscussionService_WebRole.Models
{
    public class EditDiscussionVm
    {
        public Discussion Discussion { get; set; }
        public Movie Movie { get; set; }
    }
}