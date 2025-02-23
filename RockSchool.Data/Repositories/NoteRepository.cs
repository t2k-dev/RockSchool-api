using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories
{
    public class NoteRepository : BaseRepository
    {
        public NoteRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
        {
        }

        public async Task<NoteEntity[]?> GetNotes(int branchId)
        {
            return await RockSchoolContext.Notes.Where(n => n.Branch.BranchId == branchId).ToArrayAsync();
        }
    }
}
