using System.Linq.Expressions;

namespace Embarcaciones.AplicacionWeb.Models.Utils
{
    public class DataAttribute
    {
        public string Name { get; set; }

        public MemberExpression Property { get; set; }
    }
}
