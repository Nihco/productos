using ProductosBFF.Domain.Causales;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Causales
{
	/// <summary>
	/// Clase Dto Motivos Cancelacion
	/// </summary>
	public class MotivosCancelacionDto:IMapFrom<MotivosCancelacion>
	{
		/// <summary>
		/// Codigo Motivo
		/// </summary>
		public string Codigo { get; set; }

		/// <summary>
		/// Descripcion Motivo
		/// </summary>
		public string Descripcion { get; set; }
	}
}
