﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NajotEdu.Application.Models
{
    public class UpdateGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeacherId { get; set; }
    }
}
