using AutoMapper;

namespace ProductosBFF.Mappings
{
    /// <summary>
    /// Interface generica la cual permite mapear un objeto
    /// </summary>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Método para mapear dos objetos
        /// </summary>
        /// <param name="profile">Perfil a mapear</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}