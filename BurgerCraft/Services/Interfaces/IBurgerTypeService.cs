using BurgerCraft.Models;

namespace BurgerCraft.Services.Interfaces
{
    public interface IBurgerTypeService
    {
        IEnumerable<BurgerType> GetAll();
        BurgerType GetById(int id);
        void Add(BurgerType entity);
        void Delete(int id);
    }
}
