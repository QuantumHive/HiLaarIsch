using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiLaarIsch.Models
{
    public class ResetPasswordViewModel
    {
        [HiddenInput]
        public Guid UserId { get; set; }
        [HiddenInput]
        public string MailToken { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare(nameof(ResetPasswordViewModel.NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}