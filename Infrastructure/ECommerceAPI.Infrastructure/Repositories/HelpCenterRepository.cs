﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Infrastructure.Repository;
using ECommerceAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class HelpCenterRepository:Repository<HelpCenter>, IHelpCenterRepository
    {
        public HelpCenterRepository(ECommerceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
