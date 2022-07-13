using AutoMapper;
using ClientWebApi.Models;
using ClientWebApi.Services.Deal;
using Models;

namespace ClientWebApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostBestDealRequest, Input1>()
                .ForMember(d => d.ContactAddress, o => o.MapFrom(s => s.SourceAddress))
                .ForMember(d => d.WarehouseAddress, o => o.MapFrom(s => s.DestinationAddress))
                .ForMember(d => d.PackageDimensions, o => o.MapFrom(s => s.CartonDimensions))
                .ReverseMap();

            CreateMap<PostBestDealRequest, Input2>()
                .ForMember(d => d.Consignee, o => o.MapFrom(s => s.SourceAddress))
                .ForMember(d => d.Consignor, o => o.MapFrom(s => s.DestinationAddress))
                .ForMember(d => d.Cartons, o => o.MapFrom(s => s.CartonDimensions))
                .ReverseMap();

            CreateMap<PostBestDealRequest, Input3>()
                .ForMember(d => d.Source, o => o.MapFrom(s => s.SourceAddress))
                .ForMember(d => d.Destination, o => o.MapFrom(s => s.DestinationAddress))
                .ReverseMap();

            CreateMap<PostBestDealResponse, Output1>()
                .ForMember(d => d.Total, o => o.MapFrom(s => s.Total))
                .ReverseMap();

            CreateMap<PostBestDealResponse, Output2>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Total))
                .ReverseMap();

            CreateMap<PostBestDealResponse, Output3>()
                .ForMember(d => d.Quote, o => o.MapFrom(s => s.Total))
                .ReverseMap();
        }
    }
}
