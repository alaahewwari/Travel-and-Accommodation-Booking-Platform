namespace TABP.Domain.Enums
{
    /// <summary>
    /// Represents the available room types in the system.
    /// </summary>
    public enum RoomType : byte
    {
        /// <summary>
        /// A basic, low-cost room with minimal amenities.
        /// </summary>
        Budget = 1,
        /// <summary>
        /// A standard room with essential comforts, suitable for solo travelers or couples.
        /// </summary>
        Standard = 2,
        /// <summary>
        /// A deluxe room with enhanced furnishings and better amenities.
        /// </summary>
        Deluxe = 3,

        /// <summary>
        /// A luxurious room offering premium features and superior comfort.
        /// </summary>
        Luxury = 4,

        /// <summary>
        /// A large, high-end room with separate living space, often used for long stays or executive use.
        /// </summary>
        Suite = 5,

        /// <summary>
        /// A room designed to accommodate families, typically with extra beds and space.
        /// </summary>
        Family = 6
    }
}