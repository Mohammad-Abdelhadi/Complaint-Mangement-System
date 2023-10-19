﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace last_try_api.Models
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Complaint text is required.")]
        public string ComplaintText { get; set; }

        public List<Demand> Demands { get; set; } = new List<Demand>();

        public string AttachmentPath { get; set; } // Store the file path for the attached PDF

        [Required(ErrorMessage = "Language is required.")]
        public string Language { get; set; } // Should be either "Arabic" or "English"

        [Required(ErrorMessage = "User basic information is required.")]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsApproved { get; set; } // Indicates whether the complaint is approved by the administrator
    }
}