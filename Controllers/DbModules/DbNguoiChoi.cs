using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameWeb_XL.Models;

namespace GameWeb_XL.Controllers.DbModules
{
    public class DbNguoiChoi
    {
        public static GameSQLEntities database = new GameSQLEntities();
        public static bool isExist(string username)
        {
            var sl = from a in database.NguoiChois
                     where a.Ten == username
                     select a;
            var count = sl.Count();
            if (count == 0) return false;
            return true;
                        
        }

        //public static bool isExist(string username);

        public static void add(NguoiChoi newnc)
        {
            try
            {
                NguoiChoi a = new NguoiChoi();
                database.NguoiChois.Add(newnc);
                database.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
           
        }

        public static void updateImage(string name, string image)
        {
            var id = get(name);
            var upd = database.NguoiChois.Where(x => x.Ten == name).FirstOrDefault();
            upd.HinhAnh = image;
            database.SaveChanges();
        }
        public static NguoiChoi get(string username)
        {
            var users = database.NguoiChois.Find(username);
            return users;
        }
    }
}