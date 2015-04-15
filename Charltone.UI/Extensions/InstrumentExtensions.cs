using Charltone.Domain.Entities;

namespace Charltone.UI.Extensions
{
    public static class InstrumentExtensions
    {
        public static string GetClassId(this Instrument instrument, int index)
        {
            var classId = "";

            switch (index % 3)
                {
                    case 0:
                        classId = "left";
                        break;
                    case 1:
                        classId = "middle";
                        break;
                    case 2:
                        classId = "right";
                        break;
                }
                return classId;
        }
    }
}