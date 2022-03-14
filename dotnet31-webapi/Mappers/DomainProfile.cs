using AutoMapper;
using cmes_webapi.Api.Dto;
using cmes_webapi.ServiceRepository.OMS;

namespace paas_inpo_calypsops_mocks.Dominio
{

    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            //CreateMap<TipoOperatoriaInput, TipoOperatoriaRequestDatosDto>()
            //    .ForMember(dest => dest.AgenteColocador, opt => opt.MapFrom(x => x.BrokerID))
            //    .ForMember(dest => dest.Canal, opt => opt.Ignore())
            //    .ForMember(dest => dest.Comitente, opt => opt.Ignore())
            //    .ForMember(dest => dest.HostId, opt => opt.Ignore());

            
            CreateMap<TipoOperatoriaResponseDatosDto, TipoOperatoriaOutput>()
                .ForMember(dest => dest.BrokerID, opt => opt.MapFrom(x => x.TipoOperatoria))
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.ClientID, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Internal, opt => opt.Ignore())
                .ForMember(dest => dest.IsAcdi, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedUser, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.ProductExceptions, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTypes, opt => opt.Ignore())
                .ForMember(dest => dest.SetupDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.SetupUser, opt => opt.Ignore())
                .ForMember(dest => dest.TradeBook, opt => opt.Ignore());
                
        }

    }
}