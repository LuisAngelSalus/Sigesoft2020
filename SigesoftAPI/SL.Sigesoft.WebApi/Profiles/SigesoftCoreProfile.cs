using AutoMapper;
using SL.Sigesoft.Data;
using SL.Sigesoft.Dtos;
using SL.Sigesoft.Dtos.Win;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Win;
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
            this.CreateMap<Person, PersonDto>()
                .ForMember(u => u.PersonId, p => p.MapFrom(m => m.i_PersonId))
                .ForMember(u => u.FirstName, p => p.MapFrom(m => m.v_FirstName))
                .ForMember(u => u.FirstLastName, p => p.MapFrom(m => m.v_FirstLastName))
                .ForMember(u => u.SecondLastName, p => p.MapFrom(m => m.v_SecondLastName))
                .ReverseMap();

            this.CreateMap<Person, PersonRegistertDto>()                
                .ForMember(u => u.FirstName, p => p.MapFrom(m => m.v_FirstName))
                .ForMember(u => u.FirstLastName, p => p.MapFrom(m => m.v_FirstLastName))
                .ForMember(u => u.SecondLastName, p => p.MapFrom(m => m.v_SecondLastName))
                .ReverseMap();

            this.CreateMap<Person, PersonUpdateDto>()
                .ForMember(u => u.PersonId, p => p.MapFrom(m => m.i_PersonId))
                .ForMember(u => u.FirstName, p => p.MapFrom(m => m.v_FirstName))
                .ForMember(u => u.FirstLastName, p => p.MapFrom(m => m.v_FirstLastName))
                .ForMember(u => u.SecondLastName, p => p.MapFrom(m => m.v_SecondLastName))
                .ReverseMap();

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

            this.CreateMap<SystemUser, GetSystemUserDto>()
                .ForMember(u => u.Id, p => p.MapFrom(m => m.i_SystemUserId))
                .ForMember(u => u.PersonId, p => p.MapFrom(m => m.i_PersonId))
                .ForMember(u => u.UserName, p => p.MapFrom(m => m.v_UserName))
                .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
                .ForMember(u => u.Phone, p => p.MapFrom(m => m.v_Phone))
                .ReverseMap();

            this.CreateMap<SystemUser, SystemUserRegisterDto>()
                .ForMember(u => u.PersonId, p => p.MapFrom(m => m.i_PersonId))
                .ForMember(u => u.UserName, p => p.MapFrom(m => m.v_UserName))
                .ForMember(u => u.Password, p => p.MapFrom(m => m.v_Password))
                .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
                .ForMember(u => u.Phone, p => p.MapFrom(m => m.v_Phone))
            .ReverseMap();

            this.CreateMap<SystemUser, SystemUserUpdateDataDto>()
                .ForMember(u => u.SystemUserId, p => p.MapFrom(m => m.i_SystemUserId))
                .ForMember(u => u.UserName, p => p.MapFrom(m => m.v_UserName))
                .ForMember(u => u.Password, p => p.MapFrom(m => m.v_Password))
                .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
                .ForMember(u => u.Phone, p => p.MapFrom(m => m.v_Phone))
            .ReverseMap();

            this.CreateMap<Company, ListCompanyDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContactName, p => p.MapFrom(m => m.v_ContactName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ReverseMap();

            this.CreateMap<Company, CompanyRegisterDto>()
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContactName, p => p.MapFrom(m => m.v_ContactName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ForMember(u => u.District, p => p.MapFrom(m => m.v_District))
                .ForMember(u => u.PhoneCompany, p => p.MapFrom(m => m.v_PhoneCompany))
                .ReverseMap();                

            this.CreateMap<Company, CompanyUpdateDataDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContactName, p => p.MapFrom(m => m.v_ContactName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ForMember(u => u.District, p => p.MapFrom(m => m.v_District))
                .ForMember(u => u.PhoneCompany, p => p.MapFrom(m => m.v_PhoneCompany))
                .ReverseMap();                

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

            this.CreateMap<CompanyHeadquarter, CompanyHeadquarterUpdateDataDto>()
                .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ReverseMap();

            this.CreateMap<Company, CompanyDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.IdentificationNumber, p => p.MapFrom(m => m.v_IdentificationNumber))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.ContactName, p => p.MapFrom(m => m.v_ContactName))
                .ForMember(u => u.Mail, p => p.MapFrom(m => m.v_Mail))
                .ForMember(u => u.District, p => p.MapFrom(m => m.v_District))
                .ForMember(u => u.PhoneCompany, p => p.MapFrom(m => m.v_PhoneCompany))
                .ReverseMap();

            this.CreateMap<CompanyHeadquarter, CompanyHeadquarterDto>()                
                .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.Address, p => p.MapFrom(m => m.v_Address))
                .ForMember(u => u.PhoneNumber, p => p.MapFrom(m => m.v_PhoneNumber))
                .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
                .ReverseMap();

            this.CreateMap<CompanyContact, ListCompanyContactDto>()
             .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
             .ForMember(u => u.CompanyHeadquarterName, p => p.MapFrom(m => m.v_CompanyHeadquarterName))
             .ForMember(u => u.FullName, p => p.MapFrom(m => m.v_FullName))
             .ForMember(u => u.TypeUs, p => p.MapFrom(m => m.v_TypeUs))
             .ForMember(u => u.Dni, p => p.MapFrom(m => m.v_Dni))
             .ForMember(u => u.CM, p => p.MapFrom(m => m.v_CM))
             .ForMember(u => u.Phone, p => p.MapFrom(m => m.v_Phone))
             .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
             .ReverseMap();

            this.CreateMap<Info, InfoDto>()
                .ForMember(u => u.Ruc, p => p.MapFrom(m => m.Ruc))
                .ForMember(u => u.RazonSocial, p => p.MapFrom(m => m.RazonSocial))
                .ForMember(u => u.TipoVia, p => p.MapFrom(m => m.TipoVia))
                .ForMember(u => u.NombreVia, p => p.MapFrom(m => m.NombreVia))
                .ForMember(u => u.CodigoZona, p => p.MapFrom(m => m.CodigoZona))
                .ForMember(u => u.TipoZona, p => p.MapFrom(m => m.TipoZona))
                .ForMember(u => u.Numero, p => p.MapFrom(m => m.Numero))
                .ForMember(u => u.Interior, p => p.MapFrom(m => m.Interior))
                .ForMember(u => u.Lote, p => p.MapFrom(m => m.Lote))
                .ForMember(u => u.Departamento, p => p.MapFrom(m => m.Departamento))
                .ForMember(u => u.Manzana, p => p.MapFrom(m => m.Manzana))
                .ForMember(u => u.Distrito, p => p.MapFrom(m => m.Distrito))
                .ReverseMap();

            this.CreateMap<Detail, DetailDto>()
                .ForMember(u => u.TipoVia, p => p.MapFrom(m => m.TipoVia))
                .ForMember(u => u.NombreVia, p => p.MapFrom(m => m.NombreVia))
                .ForMember(u => u.CodigoZona, p => p.MapFrom(m => m.CodigoZona))
                .ForMember(u => u.TipoZona, p => p.MapFrom(m => m.TipoZona))
                .ForMember(u => u.Numero, p => p.MapFrom(m => m.Numero))
                .ForMember(u => u.Interior, p => p.MapFrom(m => m.Interior))
                .ForMember(u => u.Lote, p => p.MapFrom(m => m.Lote))
                .ForMember(u => u.Departamento, p => p.MapFrom(m => m.Departamento))
                .ForMember(u => u.Manzana, p => p.MapFrom(m => m.Manzana))
                .ReverseMap();


            this.CreateMap<Component, ListComponentDto>()
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.CostPrice, p => p.MapFrom(m => m.r_CostPrice))
                .ForMember(u => u.BasePrice, p => p.MapFrom(m => m.r_BasePrice))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                .ReverseMap();


            this.CreateMap<QuotationModel, QuotationDto>()
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.QuotationId))
                .ForMember(u => u.Code, p => p.MapFrom(m => m.Code))
                .ForMember(u => u.Version, p => p.MapFrom(m => m.Version))
                .ForMember(u => u.UserCreatedId, p => p.MapFrom(m => m.UserCreatedId))
                .ForMember(u => u.UserName, p => p.MapFrom(m => m.UserName))
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.CompanyId))
                .ForMember(u => u.CompanyRuc, p => p.MapFrom(m => m.CompanyRuc))
                .ForMember(u => u.CompanyName, p => p.MapFrom(m => m.CompanyName))
                .ForMember(u => u.CompanyDistrictName, p => p.MapFrom(m => m.CompanyDistrictName))
                .ForMember(u => u.CompanyAddress, p => p.MapFrom(m => m.CompanyAddress))
                .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.CompanyHeadquarterId))
                .ForMember(u => u.FullName, p => p.MapFrom(m => m.FullName))
                .ForMember(u => u.Email, p => p.MapFrom(m => m.Email))
                .ForMember(u => u.CommercialTerms, p => p.MapFrom(m => m.CommercialTerms))
                .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.StatusQuotationId))
                .ForMember(u => u.TotalQuotation, p => p.MapFrom(m => m.TotalQuotation))                
                .ReverseMap();

            this.CreateMap<QuotationProfileModel, QuotationProfileDto>()                
                .ForMember(u => u.QuotationProfileId, p => p.MapFrom(m => m.QuotationProfileId))                
                .ForMember(u => u.ProfileName, p => p.MapFrom(m => m.ProfileName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.ServiceTypeId))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.TypeFormatId))
                .ForMember(u => u.ServiceTypeName, p => p.MapFrom(m => m.ServiceTypeName))                
                .ReverseMap();

            this.CreateMap<ProfileComponentModel, ProfileComponentDto>()
                .ForMember(u => u.ProfileComponentId, p => p.MapFrom(m => m.ProfileComponentId))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.CategoryId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.CategoryName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.ComponentId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.ComponentName))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.SalePrice))

                .ForMember(u => u.AgeConditionalId, p => p.MapFrom(m => m.AgeConditionalId))
                .ForMember(u => u.Age, p => p.MapFrom(m => m.Age))
                .ForMember(u => u.GenderConditionalId, p => p.MapFrom(m => m.GenderConditionalId))
                .ReverseMap();


            this.CreateMap<AdditionalComponentsQuoteModel, AdditionalComponentsQuoteDto>()
                .ForMember(u => u.AdditionalComponentsQuoteId, p => p.MapFrom(m => m.AdditionalComponentsQuoteId))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.CategoryId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.CategoryName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.ComponentId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.ComponentName))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.SalePrice))
                .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
                .ReverseMap();


            this.CreateMap<Quotation, QuotationDto>()
               .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
               .ForMember(u => u.Code, p => p.MapFrom(m => m.v_Code))
               .ForMember(u => u.Version, p => p.MapFrom(m => m.i_Version))
               .ForMember(u => u.UserCreatedId, p => p.MapFrom(m => m.i_UserCreatedId))               
               .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))          
               .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
               .ForMember(u => u.FullName, p => p.MapFrom(m => m.v_FullName))
               .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
               .ForMember(u => u.CommercialTerms, p => p.MapFrom(m => m.v_CommercialTerms))
               .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.i_StatusQuotationId))
                .ForMember(u => u.TotalQuotation, p => p.MapFrom(m => m.r_TotalQuotation))
               .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))               
               .ReverseMap();

            this.CreateMap<QuotationProfile, QuotationProfileDto>()                
                .ForMember(u => u.QuotationProfileId, p => p.MapFrom(m => m.i_QuotationProfileId))
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
                .ForMember(u => u.ProfileName, p => p.MapFrom(m => m.v_ProfileName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.i_ServiceTypeId))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.i_TypeFormatId))
                .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
                .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
                .ReverseMap();

            this.CreateMap<ProfileComponent, ProfileComponentDto>()
                .ForMember(u => u.ProfileComponentId, p => p.MapFrom(m => m.i_ProfileComponentId))
                .ForMember(u => u.QuotationProfileId, p => p.MapFrom(m => m.i_QuotationProfileId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
                .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
                .ReverseMap();

            this.CreateMap<Quotation, QuotationRegisterDto>()
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
              .ForMember(u => u.Code, p => p.MapFrom(m => m.v_Code))
              .ForMember(u => u.Version, p => p.MapFrom(m => m.i_Version))
              .ForMember(u => u.UserCreatedId, p => p.MapFrom(m => m.i_UserCreatedId))
              .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
              .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
              .ForMember(u => u.FullName, p => p.MapFrom(m => m.v_FullName))
              .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
              .ForMember(u => u.CommercialTerms, p => p.MapFrom(m => m.v_CommercialTerms))
              .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
              .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.i_StatusQuotationId))
               .ForMember(u => u.TotalQuotation, p => p.MapFrom(m => m.r_TotalQuotation))
              .ReverseMap();

            this.CreateMap<QuotationProfile, QuotationProfileRegisterDto>()
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
                .ForMember(u => u.ProfileName, p => p.MapFrom(m => m.v_ProfileName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.i_ServiceTypeId))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.i_TypeFormatId))
               .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
                .ReverseMap();

            this.CreateMap<ProfileComponent, ProfileComponentRegisterDto>()
                .ForMember(u => u.QuotationProfileId, p => p.MapFrom(m => m.i_QuotationProfileId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                
                .ForMember(u => u.AgeConditionalId, p => p.MapFrom(m => m.i_AgeConditionalId))
                .ForMember(u => u.Age, p => p.MapFrom(m => m.i_Age))
                .ForMember(u => u.GenderConditionalId, p => p.MapFrom(m => m.i_GenderConditionalId))
                .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
                .ReverseMap();

            this.CreateMap<AdditionalComponentsQuote, AdditionalComponentsQuoteRegisterDto>()
              .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
              .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
              .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
              .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
              .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
              .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
              .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
              .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
              .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
              .ReverseMap();

            this.CreateMap<Quotation, QuotationUpdateDto>()
              .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
              .ForMember(u => u.Code, p => p.MapFrom(m => m.v_Code))
              .ForMember(u => u.Version, p => p.MapFrom(m => m.i_Version))
              .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
              .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
              .ForMember(u => u.FullName, p => p.MapFrom(m => m.v_FullName))
              .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))              
              .ForMember(u => u.CommercialTerms, p => p.MapFrom(m => m.v_CommercialTerms))
              .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.i_StatusQuotationId))
               .ForMember(u => u.TotalQuotation, p => p.MapFrom(m => m.r_TotalQuotation))
              .ForMember(u => u.UpdateUserId, p => p.MapFrom(m => m.i_UpdateUserId))
              .ReverseMap();

            this.CreateMap<QuotationProfile, QuotationProfileUpdateDto>()
                .ForMember(u => u.QuotationProfileId, p => p.MapFrom(m => m.i_QuotationProfileId))
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
                .ForMember(u => u.ProfileName, p => p.MapFrom(m => m.v_ProfileName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.i_ServiceTypeId))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.i_TypeFormatId))
               .ForMember(u => u.UpdateUserId, p => p.MapFrom(m => m.i_UpdateUserId))
               .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
                .ReverseMap();

            this.CreateMap<ProfileComponent, ProfileComponentUpdateDto>()
                .ForMember(u => u.ProfileComponentId, p => p.MapFrom(m => m.i_ProfileComponentId))
                .ForMember(u => u.QuotationProfileId, p => p.MapFrom(m => m.i_QuotationProfileId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))                
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                .ForMember(u => u.AgeConditionalId, p => p.MapFrom(m => m.i_AgeConditionalId))
                .ForMember(u => u.Age, p => p.MapFrom(m => m.i_Age))
                .ForMember(u => u.GenderConditionalId, p => p.MapFrom(m => m.i_GenderConditionalId))
                .ForMember(u => u.UpdateUserId, p => p.MapFrom(m => m.i_UpdateUserId))
                .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
                .ReverseMap();

            this.CreateMap<AdditionalComponentsQuote, AdditionalComponentsQuoteUpdateDto>()
               .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
               .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
               .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
               .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
               .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
               .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
               .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
               .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
               .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
               .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.RecordType, p => p.MapFrom(m => m.RecordType))
           .ReverseMap();

            this.CreateMap<Secuential, SecuentialDto>()
                .ForMember(u => u.OwnerCompanyId, p => p.MapFrom(m => m.i_OwnerCompanyId))
                .ForMember(u => u.Process, p => p.MapFrom(m => m.v_Process))
                .ForMember(u => u.SystemUserId, p => p.MapFrom(m => m.i_SystemUserId))
                .ReverseMap();

            this.CreateMap< ProtocolProfile, ProtocolProfileRegisterDto>()
                .ForMember(u => u.Name, p => p.MapFrom(m => m.v_Name))
                .ReverseMap();

            this.CreateMap<ProfileDetail, ProfileDetailRegisterDto>()
              .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
              .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
              .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
              .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
              .ForMember(u => u.ListPrice, p => p.MapFrom(m => m.r_ListPrice))
              .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
              .ReverseMap();

            this.CreateMap<ProtocolProfile, DropdownListDto>()
                .ForMember(u => u.Id, p => p.MapFrom(m => m.i_ProtocolProfileId))
                .ForMember(u => u.Value, p => p.MapFrom(m => m.v_Name))
                .ReverseMap();

            this.CreateMap<QuotationFilterModel, QuotationFilterDto>()
            .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.QuotationId))
            .ForMember(u => u.NroQuotation, p => p.MapFrom(m => m.NroQuotation))
            .ForMember(u => u.ShippingDate, p => p.MapFrom(m => m.ShippingDate))
            .ForMember(u => u.AcceptanceDate, p => p.MapFrom(m => m.AcceptanceDate))
            .ForMember(u => u.CompanyName, p => p.MapFrom(m => m.CompanyName))
            .ForMember(u => u.Total, p => p.MapFrom(m => m.Total))
            .ForMember(u => u.USDate, p => p.MapFrom(m => m.USDate))
            .ForMember(u => u.TrackingDescription, p => p.MapFrom(m => m.TrackingDescription))
            .ForMember(u => u.StatusQuotationName, p => p.MapFrom(m => m.StatusQuotationName))
            .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.StatusQuotationId))
            .ForMember(u => u.Indicator, p => p.MapFrom(m => m.Indicator))
            .ReverseMap();

            this.CreateMap<QuoteTrackingFilterModel, QuoteTrackingFilterDto>()
                .ForMember(u => u.QuoteTrackingId, p => p.MapFrom(m => m.QuoteTrackingId))
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.QuotationId))
                .ForMember(u => u.Version, p => p.MapFrom(m => m.Version))
                .ForMember(u => u.Date, p => p.MapFrom(m => m.Date))
                .ForMember(u => u.Commentary, p => p.MapFrom(m => m.Commentary))
                .ForMember(u => u.StatusName, p => p.MapFrom(m => m.StatusName))
            .ReverseMap();


            this.CreateMap<QuoteTracking, ListQuoteTrackingDto>()
                .ForMember(u => u.QuoteTrackingId, p => p.MapFrom(m => m.i_QuoteTrackingId))
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
                .ForMember(u => u.Date, p => p.MapFrom(m => m.d_Date))
                .ForMember(u => u.Commentary, p => p.MapFrom(m => m.v_Commentary))
                .ForMember(u => u.StatusName, p => p.MapFrom(m => m.v_StatusName))
                .ReverseMap();

            this.CreateMap<QuoteTracking, QuoteTrackingRegisterDto>()               
               .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))               
               .ForMember(u => u.Commentary, p => p.MapFrom(m => m.v_Commentary))
               .ForMember(u => u.StatusName, p => p.MapFrom(m => m.v_StatusName))
               .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
               .ReverseMap();

            this.CreateMap<QuoteTracking, QuoteTrackingUpdateDto>()
               .ForMember(u => u.QuoteTrackingId, p => p.MapFrom(m => m.i_QuoteTrackingId))
               .ForMember(u => u.Date, p => p.MapFrom(m => m.d_Date))
               .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
               .ForMember(u => u.Commentary, p => p.MapFrom(m => m.v_Commentary))
               .ForMember(u => u.UpdateUserId, p => p.MapFrom(m => m.i_UpdateUserId))
               .ReverseMap();


            this.CreateMap<Quotation, QuotationNewVersionDto>()
             .ForMember(u => u.Code, p => p.MapFrom(m => m.v_Code))
             .ForMember(u => u.Version, p => p.MapFrom(m => m.i_Version))
             .ForMember(u => u.UserCreatedId, p => p.MapFrom(m => m.i_UserCreatedId))
             .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
             .ForMember(u => u.CompanyHeadquarterId, p => p.MapFrom(m => m.i_CompanyHeadquarterId))
             .ForMember(u => u.FullName, p => p.MapFrom(m => m.v_FullName))
             .ForMember(u => u.Email, p => p.MapFrom(m => m.v_Email))
             .ForMember(u => u.CommercialTerms, p => p.MapFrom(m => m.v_CommercialTerms))
             .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
             .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.i_StatusQuotationId))
              .ForMember(u => u.TotalQuotation, p => p.MapFrom(m => m.r_TotalQuotation))
             .ReverseMap();

            this.CreateMap<QuotationProfile, QuotationProfileNewVersionDto>()
                .ForMember(u => u.ProfileName, p => p.MapFrom(m => m.v_ProfileName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.i_ServiceTypeId))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.i_TypeFormatId))
               .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
                .ReverseMap();

            this.CreateMap<ProfileComponent, ProfileComponentNewVersionDto>()
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                .ForMember(u => u.AgeConditionalId, p => p.MapFrom(m => m.i_AgeConditionalId))
                .ForMember(u => u.Age, p => p.MapFrom(m => m.i_Age))
                .ForMember(u => u.GenderConditionalId, p => p.MapFrom(m => m.i_GenderConditionalId))
                .ForMember(u => u.RecordStatus, p => p.MapFrom(m => m.RecordStatus))
                .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
                .ReverseMap();

            this.CreateMap<AdditionalComponentsQuote, AdditionalComponentsQuoteNewVersionDto>()
              .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
              .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
              .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
              .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
              .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
              .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
              .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
              .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
            .ReverseMap();


            this.CreateMap<QuotationVersionModel, QuotationVersionDto>()
            .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.QuotationId))
            .ForMember(u => u.NroQuotation, p => p.MapFrom(m => m.NroQuotation))
            .ForMember(u => u.Version, p => p.MapFrom(m => m.Version))
            .ForMember(u => u.ShippingDate, p => p.MapFrom(m => m.ShippingDate))            
            .ForMember(u => u.CompanyName, p => p.MapFrom(m => m.CompanyName))
            .ForMember(u => u.Total, p => p.MapFrom(m => m.Total))
            .ForMember(u => u.USDate, p => p.MapFrom(m => m.USDate))
            .ForMember(u => u.TrackingDescription, p => p.MapFrom(m => m.TrackingDescription))
            .ForMember(u => u.StatusQuotationName, p => p.MapFrom(m => m.StatusQuotationName))
            .ForMember(u => u.StatusQuotationId, p => p.MapFrom(m => m.StatusQuotationId))            
            .ReverseMap();

            this.CreateMap<Quotation, QuotationUpdateProcess>()
            .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
            .ForMember(u => u.Code, p => p.MapFrom(m => m.v_Code))
            .ReverseMap();


            this.CreateMap<PriceList, PriceListDto>()
            .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
            .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
            .ForMember(u => u.Price, p => p.MapFrom(m => m.r_Price))
            .ForMember(u => u.InsertUserId, p => p.MapFrom(m => m.i_InsertUserId))
            .ForMember(u => u.UpdateUserId, p => p.MapFrom(m => m.i_UpdateUserId))
            .ReverseMap();

            this.CreateMap<SystemUser, SystemUserDto>()
                .ForMember(u => u.SystemUserId, p => p.MapFrom(m => m.i_SystemUserId))
                .ForMember(u => u.PersonId, p => p.MapFrom(m => m.i_PersonId))
                .ForMember(u => u.UserName, p => p.MapFrom(m => m.v_UserName))
                .ReverseMap();

            this.CreateMap<Role,ListRoleDto>()
                .ForMember(u => u.RoleId, p => p.MapFrom(m => m.i_RoleId))
                .ForMember(u => u.RoleName, p => p.MapFrom(m => m.v_Description))
                .ReverseMap();


            this.CreateMap<OwnerCompany, ListOwnerCompanyDto>()
                .ForMember(u => u.OwnerCompanyId, p => p.MapFrom(m => m.i_OwnerCompanyId))
                .ForMember(u => u.OwnerCompanyName, p => p.MapFrom(m => m.v_BusinessName))
                .ReverseMap();

            this.CreateMap<ProtocolListModel, ProtocolListDto>()
                .ForMember(u => u.ProtocolId, p => p.MapFrom(m => m.i_ProtocolId))
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.CompanyName, p => p.MapFrom(m => m.v_CompanyName))
                .ForMember(u => u.ProtocolName, p => p.MapFrom(m => m.v_ProtocolName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.i_ServiceTypeId))
                .ForMember(u => u.ServiceTypeName, p => p.MapFrom(m => m.v_ServiceTypeName))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.i_TypeFormatId))
                .ForMember(u => u.TypeFormatName, p => p.MapFrom(m => m.v_TypeFormatName))
                .ForMember(u => u.QuotationProfileIdRef, p => p.MapFrom(m => m.i_QuotationProfileIdRef))
                .ReverseMap();

            this.CreateMap<ProtocolDetail, ProtocolDetailListDto>()
                .ForMember(u => u.ProtocolDetailId, p => p.MapFrom(m => m.i_ProtocolDetailId))
                .ForMember(u => u.ProtocolId, p => p.MapFrom(m => m.i_ProtocolId))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                .ForMember(u => u.AgeConditionalId, p => p.MapFrom(m => m.i_AgeConditionalId))
                .ForMember(u => u.Age, p => p.MapFrom(m => m.i_Age))
                .ForMember(u => u.GenderConditionalId, p => p.MapFrom(m => m.i_GenderConditionalId))
                .ForMember(u => u.QuotationProfileIdRef, p => p.MapFrom(m => m.i_QuotationProfileIdRef))
                .ReverseMap();

            this.CreateMap<Protocol, ProtocolRegisterDto>()
                .ForMember(u => u.CompanyId, p => p.MapFrom(m => m.i_CompanyId))
                .ForMember(u => u.ProtocolName, p => p.MapFrom(m => m.v_ProtocolName))
                .ForMember(u => u.ServiceTypeId, p => p.MapFrom(m => m.i_ServiceTypeId))
                .ForMember(u => u.TypeFormatId, p => p.MapFrom(m => m.i_TypeFormatId))
                .ForMember(u => u.QuotationProfileIdRef, p => p.MapFrom(m => m.i_QuotationProfileIdRef))
                .ReverseMap();

            this.CreateMap<ProtocolDetail, ProtocolDetailRegisterDto>()
                .ForMember(u => u.ProtocolId, p => p.MapFrom(m => m.i_ProtocolId))
                .ForMember(u => u.CategoryId, p => p.MapFrom(m => m.i_CategoryId))
                .ForMember(u => u.CategoryName, p => p.MapFrom(m => m.v_CategoryName))
                .ForMember(u => u.ComponentId, p => p.MapFrom(m => m.v_ComponentId))
                .ForMember(u => u.ComponentName, p => p.MapFrom(m => m.v_ComponentName))
                .ForMember(u => u.MinPrice, p => p.MapFrom(m => m.r_MinPrice))
                .ForMember(u => u.PriceList, p => p.MapFrom(m => m.r_PriceList))
                .ForMember(u => u.SalePrice, p => p.MapFrom(m => m.r_SalePrice))
                .ForMember(u => u.AgeConditionalId, p => p.MapFrom(m => m.i_AgeConditionalId))
                .ForMember(u => u.Age, p => p.MapFrom(m => m.i_Age))
                .ForMember(u => u.GenderConditionalId, p => p.MapFrom(m => m.i_GenderConditionalId))
                .ForMember(u => u.QuotationProfileIdRef, p => p.MapFrom(m => m.i_QuotationProfileIdRef))
                .ReverseMap();

            this.CreateMap<Quotation, QuotationMigrateDto>()
                .ForMember(u => u.QuotationId, p => p.MapFrom(m => m.i_QuotationId))
                .ReverseMap();
        }
    }
}
