using System.Collections.Generic;
namespace EntityFrameWork
{
    public class System : Entity
    {
        protected List<Component> _interests = new List<Component>();

        public void AddInterest(Component c)
        {
            _intersts.Add(c);
        }

        public void RemoveInterst(Component c)
        {
            _intersts.Remove(c);
        }
    }
}