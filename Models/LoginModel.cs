using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameWeb_XL.Models
{
    public class LoginModel
    {
        [Required]
        public string Ten { set; get; }
        public string MatKhau { set; get; }
        public string MatKhauConfirm { set; get; }
        public string MatKhaucu { set; get; }

        public static GameSQLEntities database = new GameSQLEntities();
        public static bool login(string username, string pass)
        {
            var check = from a in database.NguoiChois
                        where (a.Ten == username) && (a.MatKhau == pass)
                        select a;
            var dem = check.Count();
            if (dem == 1) return true;
            else return false;
        }
    }
}