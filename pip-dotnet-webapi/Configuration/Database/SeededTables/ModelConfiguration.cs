
using Pip.Models;

namespace Pip.Configuration.Database
{
    public class InitialModelConfiguration : SeededTable<NewModel>
    {
        public override IEnumerable<NewModel> SeedData
        {
            get
            {
                return new[] {
                    new NewModel { Code = 1, Name = "First"  },
                    new NewModel { Code = 2, Name = "Second" },
                };
            }
        }
    }
}
