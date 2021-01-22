using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameWeb_XL.Models
{
    public class TwoModel
    {
        private List<NguoiChoi> invite = new List<NguoiChoi>();
        public List<NguoiChoi> Invite { set; get; }

        private List<NguoiChoi> require = new List<NguoiChoi>();

        public List<NguoiChoi> Require { set; get; }

    }
}