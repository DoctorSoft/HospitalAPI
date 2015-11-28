using DataBaseFiller.Dependencies;

namespace DataBaseFiller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new CreatorsFactory();
            var filler = factory.GetFiller();

            filler.FillDataBase();
        }
    }
}
