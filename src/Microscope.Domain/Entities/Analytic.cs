using Microscope.BuildingBlocks.SharedKernel;

namespace Microscope.Domain.Entities
{
    public class Analytic : Entity, IAggregateRoot
    {
        #region ctor
        protected Analytic()
        {

        }

        protected Analytic(Guid id, string key, string dimension) : this()
        {
            this.Id = id;
            this.Key = key;
            this.Dimension = dimension;
        }

        public static Analytic NewAnalytic(Guid id, string key, string dimension)
        {
            return new Analytic(id, key, dimension);
        }

        #endregion

        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Dimension { get; private set; }

        public void Update(string key, string dimension)
        {
            this.Key = key;
            this.Dimension = dimension;
        }
    }
}
