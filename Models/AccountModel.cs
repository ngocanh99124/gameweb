using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GameWeb_XL.Models
{
    public class AccountModel
    {
        private GameSQLEntities context = new GameSQLEntities();
        public AccountModel()
        {
            context = new GameSQLEntities();
        }
        public bool Login(string Ten, string MatKhau)
        {
            bool res;
            object[] sqlparam = new SqlParameter[]
            {
                new SqlParameter("@Ten", Ten),
                new SqlParameter("@MatKhau", MatKhau)
            };
            if (Ten == null || MatKhau == null)
                res = false;
            else res = context.Database.SqlQuery<bool>("Account_Login @Ten, @MatKhau", sqlparam).SingleOrDefault();
            return res;
        }
        public bool changePass(string Ten, string MatKhau, string MatKhauConfirm, string MatKhaucu)
        {
            bool res;
            object[] sqlparam = new SqlParameter[]
           {
                new SqlParameter("@Ten", Ten),
                new SqlParameter("@MatKhaucu", MatKhaucu),
                new SqlParameter("@MatKhau", MatKhau),
                new SqlParameter("@MatKhauConfirm", MatKhauConfirm)
           };
            if (Ten == null || MatKhau == null || MatKhaucu == null || MatKhauConfirm == null)
                res = false;
            else
                res = context.Database.SqlQuery<bool>("ChangePass @Ten ,@MatKhaucu, @MatKhau ,@MatKhauConfirm", sqlparam).SingleOrDefault();
            return res;
        }

        public int HuyKB(string Ten1, string Ten2)
        {
            int a;
            object[] sqlparam = new SqlParameter[]
           {
                new SqlParameter("@Ten1", Ten1),
                new SqlParameter("@Ten2", Ten2)

           };
            if (Ten1 == null || Ten2 == null)
                a = 0;
            else
                a = context.Database.ExecuteSqlCommand("HuyKB @Ten1, @Ten2", sqlparam);
            return a;
        }
        public int AddFriend(string Ten1, string Ten2)
        {
            int a;
            object[] sqlparam = new SqlParameter[]
           {
                new SqlParameter("@Ten1", Ten1),
                new SqlParameter("@Ten2", Ten2)

           };
            if (Ten1 == null || Ten2 == null)
                a = 0;
            else
                a = context.Database.ExecuteSqlCommand("addFriend @Ten1, @Ten2", sqlparam);
            return a;
        }
        public int AcceptFriend(string Ten1, string Ten2)
        {
            int a;
            object[] sqlparam = new SqlParameter[]
           {
                new SqlParameter("@Ten1", Ten1),
                new SqlParameter("@Ten2", Ten2)

           };
            if (Ten1 == null || Ten2 == null)
                a = 0;
            else
                a = context.Database.ExecuteSqlCommand("AcceptFriend @Ten1, @Ten2", sqlparam);
            return a;
        }

        public int HuyYeuCau(string Ten1, string Ten2)
        {
            int a;
            object[] sqlparam = new SqlParameter[]
           {
                new SqlParameter("@Ten1", Ten1),
                new SqlParameter("@Ten2", Ten2)

           };
            if (Ten1 == null || Ten2 == null)
                a = 0;
            else
                a = context.Database.ExecuteSqlCommand("HuyYeuCau @Ten1, @Ten2", sqlparam);
            return a;
        }

        public bool LoginFB(string Ten)
        {
            object[] sqlparam = new SqlParameter[]
            {
                new SqlParameter("@Ten", Ten)
            };
            var res = context.Database.SqlQuery<bool>("LoginFB @Ten", sqlparam).SingleOrDefault();
            return res;
        }
        public string InsertForFacebook(NguoiChoi entity)
        {
            var user = context.NguoiChois.SingleOrDefault(x => x.Ten == entity.Ten);
            if (user == null)
            {
                context.NguoiChois.Add(entity);
                context.SaveChanges();
                return entity.Ten;
            }
            else
            {
                return entity.Ten;
            }
        }

    }
}