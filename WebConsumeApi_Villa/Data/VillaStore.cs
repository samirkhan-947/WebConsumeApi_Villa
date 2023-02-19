using WebConsumeApi_Villa.Models.DTO;

namespace WebConsumeApi_Villa.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
                new VillaDTO {Id=1,Name="Dravid",Occupancy=5,Sqrt=500},
                new VillaDTO {Id=2,Name="sachin",Occupancy=4,Sqrt=400},
                new VillaDTO {Id=3,Name="kohli",Occupancy=3,Sqrt=200},
        };
    }
}
