using Mapster;
using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;

namespace DailyJournaling.API.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<MoodState, MoodStateDTO>.NewConfig();
            TypeAdapterConfig<MoodStateDTO, MoodState>.NewConfig();
        }
    }
}
