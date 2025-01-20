﻿using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Dietitian
{
    public class DietitianRepository(AppDbContext _context) : IDietitianInterface<AvailableDietDTO>
    {
        public Task<GeneralResponse> AssignMenuToPatient(int patient_id, int dietitian_id, MenuToPatientDTO menuPatientDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AvailableDietDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AvailableDietDTO>> GetAvailableDietitans()
        {
            var availableDietList = await _context.Dieticians
               .Where(_ => _.Maximum_Patient_Number > _.Patient_Number).ToListAsync();

            var userIds = availableDietList.Select(d => d.User_Id).Distinct().ToList();

            var userDictionary = await _context.Users
                .Where(u => userIds.Contains(u.User_Id))
                .ToDictionaryAsync(u => u.User_Id, u => u.User_Name);

            var availableDietListDTO = availableDietList.Select(d => new AvailableDietDTO
            {
                Dietitan_Id = d.Dietician_Id,
                Dietitan_Name = userDictionary.ContainsKey((int)d.User_Id) ? userDictionary[(int)d.User_Id] : "Unknown"
            }).ToList();

            return availableDietListDTO;
        }

        public Task<AvailableDietDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Insert(AvailableDietDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> Update(AvailableDietDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
