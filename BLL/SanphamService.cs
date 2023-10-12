using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
namespace BLL
{
    public class SanphamService
    {
        public List<Sanpham> GetAll()
        {
            HanghoaModel context = new HanghoaModel();
            return context.Sanphams.ToList();
        }
        public void Add(Sanpham s)
        {
            HanghoaModel context = new HanghoaModel();

            context.Sanphams.Add(s);
            context.SaveChanges();

        }
      
        public void delete(string masp)
        {
            HanghoaModel context = new HanghoaModel();
            Sanpham s = context.Sanphams.FirstOrDefault(p => p.MaSP == masp);   
            if(s != null)
            {
                context.Sanphams.Remove(s);
                context.SaveChanges();
            }

        }
        public void Update(Sanpham s)
        {   
            HanghoaModel context = new HanghoaModel();
            if (context.Sanphams.Any(p => p.MaSP == s.MaSP))
            {
                context.Entry(s).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}
