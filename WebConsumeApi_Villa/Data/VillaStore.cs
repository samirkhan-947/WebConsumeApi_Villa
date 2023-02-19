using WebConsumeApi_Villa.Models.DTO;

namespace WebConsumeApi_Villa.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
                new VillaDTO {Id=1,Name="salamu"},
                new VillaDTO {Id=2,Name="saif"},
                new VillaDTO {Id=3,Name="shahrukh"},
        };
    }
}
