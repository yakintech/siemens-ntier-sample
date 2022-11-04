using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Dto.Models.WebUser.Response
{
    public class WebUserRegisterResponseDto : BaseDto
    {
        public string EMail { get; set; }
        public DateTime AddDate { get; set; }

        public object Token { get; set; }
    }
}
