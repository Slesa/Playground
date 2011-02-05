using MediaOwl.NetflixServiceReference;

namespace MediaOwl.Model.Netflix
{
    /// <summary>
    /// Represents a Genre delivered from Netflix with addidional <see cref="Amount"/>,
    /// which represents eg. the occurrencies of a genre in an actor's movie-portfolio.
    /// </summary>
    public class GenreExtended
    {
        public Genre GenreBase { get; set; }
        public string Name { get { return GenreBase.Name; } }
        public int Amount { get; set; }
    }
}