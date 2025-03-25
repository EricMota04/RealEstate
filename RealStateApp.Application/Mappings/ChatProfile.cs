using AutoMapper;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Mappings
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Conversation, ConversationDto>();
            CreateMap<Message, MessageDto>();
        }
    }
}
