﻿using AutoMapper;
using SL.Sigesoft.Data;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile = AutoMapper.Profile;

namespace SL.Sigesoft.WebApi.Profiles
{
    public class SigesoftCoreProfile : Profile
    {
        public SigesoftCoreProfile()
        {
            this.CreateMap<Person, PersonDto>().ReverseMap();
            this.CreateMap<SystemUser, LoginModelDto>().ReverseMap();
          
            this.CreateMap<SystemUser, ListSystemUserDto>()
                .ForMember(u => u.Id, p => p.MapFrom(m => m.i_SystemUserId))
                .ForMember(u => u.UserName, p => p.MapFrom(m => m.v_UserName))
                .ForMember(u => u.FullName, p => p.MapFrom(m => string.Format("{0} {1}",
                        m.Person.v_FirstName, m.Person.v_FirstLastName)))
                .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
               .ReverseMap();

            this.CreateMap<Permission, PermissionDto>()
                .ForMember(u => u.RolId, p => p.MapFrom(m => m.i_RoleId))
                .ForMember(u => u.RolName, p => p.MapFrom(m => m.Roles.v_Description))
                .ReverseMap();

            this.CreateMap<Company, ListCompanyDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContacName, p => p.MapFrom(m => m.v_ContacName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ReverseMap();

            this.CreateMap<Company, CompanyRegisterDto>()
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContacName, p => p.MapFrom(m => m.v_ContacName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ReverseMap()
                .ForMember(u => u.CompanyHeadquarter, p => p.Ignore());

            this.CreateMap<Company, CompanyUpdateDataDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContacName, p => p.MapFrom(m => m.v_ContacName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ReverseMap()
                .ForMember(u => u.CompanyHeadquarter, p => p.Ignore());

            this.CreateMap<CompanyHeadquarter, ListCompanyHeadquarterDto>()
                .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ReverseMap();

            this.CreateMap<CompanyHeadquarter, CompanyHeadquarterRegisterDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ReverseMap();
            

            this.CreateMap<CompanyHeadquarter, CompanyHeadquarterDto>().ReverseMap();
        }
    }
}
