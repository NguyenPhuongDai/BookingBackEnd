namespace BookingTourWeb_WebAPI.Models.InputModels
{
    public class InputFilterGuestsChuyenBay
    {
        public string fromPlace { get; set; }
        public string toPlace { get; set; }
        public string startDate { get; set; }
        public string bookDate { get; set; }
        public string typeSeat { get; set; }
    }
}
