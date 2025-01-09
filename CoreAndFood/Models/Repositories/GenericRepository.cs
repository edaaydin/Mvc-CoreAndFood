using CoreAndFood.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoreAndFood.Repositories
{
    public class GenericRepository<T> where T : class, new()   // T mutlaka bir Class olmalı diyoruz bu kod blogunda.
    {
        // crud islemlerine ait metotları olustururuz.

        private readonly CoreAndFoodDbContext _context;

        public GenericRepository(CoreAndFoodDbContext context)
        {
            _context = context;
        }

        public List<T> TList()
        {
            // sınıflarımı cagırdıgım yerlerde T den gelen deger olacak.
            return _context.Set<T>().ToList(); // T den istedigim sınıfını listele.
        }
        public List<T> TList(string p)
        {
            return _context.Set<T>().Include(p).ToList();
        }

        public void TAdd(T entity)
        {
            _context.Set<T>().Add(entity); // T den gelenleri db'ye ekle.
            _context.SaveChanges();
        }

        public void TDelete(T entity)
        {
            _context.Set<T>().Remove(entity); // T den gelen nesneyi db'de sil.
            _context.SaveChanges();
        }

        public void TUpdate(T entity)
        {
            _context.Set<T>().Update(entity); // T den gelen nesneleri db'de güncelle
            _context.SaveChanges();
        }

        public T TGet(int id)
        {
            return _context.Set<T>().Find(id); // id'den gondermis oldugum degerleri bul.
        }

        public List<T> List(Expression<Func<T, bool>> filter) // istedigim sutunda arama yapabilirim.
        {
            return _context.Set<T>().Where(filter).ToList(); // filter'dan gelen degeri bana listele.
        }
    }
}