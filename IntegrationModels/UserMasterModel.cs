using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
    public class UserMasterModel
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserCode { get; set; }
        public string RegistrationNo { get; set; }
        public string ApplicantName { get; set; }
        public string Address { get; set; }
        public int? UserTypeId { get; set; }
        public string UserType { get; set; }
        public int? RoleId { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string PinCode { get; set; }
        public int? IdentityProofId { get; set; }
        public string IdentityProofName { get; set; }
        public string Doc { get; set; }
        public string DocPath { get; set; }
        public string MobileNo { get; set; }
        public string EMailId { get; set; }
        public int? SQuestionId { get; set; }
        public string SQuestion { get; set; }
        public string QAnswer { get; set; }
        public string captcha { get; set; }

        public string Remark { get; set; }
        public int? IsApproved { get; set; }
        public int? IsReject { get; set; }
        public int? IsMailed { get; set; }
        public DateTime? IsMailedDateTime { get; set; }
        public int? IsSMS { get; set; }
        public DateTime? IsSMSDate { get; set; }
        public int? IsLogIn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string EMAIL_SENT { get; set; }
    }
}
