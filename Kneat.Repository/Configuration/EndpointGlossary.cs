namespace Kneat.Service.Repository.Configuration
{
    /// <summary>
    /// Glossary of endpoints
    /// </summary>
    public abstract class EndpointGlossary
    {
        public string GetAllStarShipsUrlByPage
        {
            get
            {
                return "http://swapi.dev/api/starships/?page=";
            }
        }
    }
}
